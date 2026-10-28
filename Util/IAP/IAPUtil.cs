#if GS2_ENABLE_PURCHASING

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gs2.Core;
using Gs2.Core.Exception;
using Gs2.Core.Model;
using Gs2.Core.Domain;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Gs2.Unity.Util
{
    public enum Status
    {
        None,
        Initializing,
        Initialized,
        InitializeFailed,
        Purchasing,
        Purchased,
        PurchaseFailed,
    }

    public class PurchaseParameters
    {
        public string receipt;
        /// <summary>
        /// The store's id for this purchase. A GS2 request that consumes the
        /// receipt should carry it as the duplication avoider, so that retrying
        /// the same purchase (a second click, a restored pending order) is
        /// answered with the first result instead of a duplicate transaction.
        /// </summary>
        public string transactionId;
#if !GS2_IAP_5_0_0_OR_NEWER
        public IStoreController controller;
        public Product product;
#else
        public StoreController controller;
        public PendingOrder order;
        public PendingOrder product => order;
#endif
    }

#if GS2_IAP_5_0_0_OR_NEWER
    public static class StoreControllerExt {
        public static void ConfirmPendingPurchase(this StoreController self, PendingOrder order) {
            self.ConfirmPurchase(order);
        }
    }
#endif

    public class IAPUtil
    {
#if !GS2_IAP_5_0_0_OR_NEWER
        private IStoreController _controller;
#endif

        private Status _status = Status.None;
        private Gs2Exception _exception;
        private string _receipt;
        private string _transactionId;

        public IEnumerator Buy(
            UnityAction<AsyncResult<PurchaseParameters>> callback,
            string contentsId,
            ProductType productType = ProductType.Consumable
        ) {
            var future = BuyFuture(contentsId, productType);
            yield return future;
            callback.Invoke(new AsyncResult<PurchaseParameters>(
                future.Result,
                future.Error
            ));
        }

        public Gs2Future<PurchaseParameters> BuyFuture(
            string contentsId,
            ProductType productType = ProductType.Consumable
        )
        {
            IEnumerator Impl(Gs2Future<PurchaseParameters> result) {

                if (_status != Status.None)
                {
                    result.OnError(
                        new ConflictException(
                            new[]
                            {
                                new RequestError("state", "money.state.state.error.running")
                            }
                        )
                    );
                    yield break;
                }
                _exception = null;
                _receipt = null;
                _transactionId = null;
                _status = Status.Initializing;

#if !GS2_IAP_5_0_0_OR_NEWER
                var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
                builder.AddProduct(contentsId, productType);
                UnityPurchasing.Initialize(new Gs2StoreListener(this), builder);
                while (_status == Status.Initializing)
                {
                    yield return new WaitForSeconds(1);
                }
                if (_status == Status.InitializeFailed)
                {
                    result.OnError(_exception);
                    yield break;
                }

                _status = Status.Purchasing;
                _controller.InitiatePurchase(_controller.products.WithID(contentsId));
                while (_status == Status.Purchasing)
                {
                    yield return new WaitForSeconds(0.1f);
                }

                result.OnComplete(
                    new PurchaseParameters {
                        receipt = _receipt,
                        transactionId = _transactionId,
                        controller = _controller,
                        product = _controller.products.WithID(contentsId),
                    }
                );
#else
                using (var session = new Gs2StoreSession(this, contentsId)) {

                    var connectTask = session.Controller.Connect();
                    while (!connectTask.IsCompleted) {
                        yield return null;
                    }
                    if (connectTask.IsFaulted || connectTask.IsCanceled) {
                        _status = Status.InitializeFailed;
                        result.OnError(
                            new BadGatewayException(
                                connectTask.Exception?.GetBaseException().Message ?? "connect failed"
                            )
                        );
                        yield break;
                    }
                    _status = Status.Initialized;

                    session.Controller.FetchProducts(new List<ProductDefinition>
                    {
                        new(contentsId, productType),
                    });
                    while (!session.ProductsFetched) {
                        yield return new WaitForSeconds(0.1f);
                    }
                    if (session.Product == null) {
                        _status = Status.PurchaseFailed;
                        result.OnError(
                            _exception ?? new BadGatewayException("product not found: " + contentsId)
                        );
                        yield break;
                    }

                    session.Controller.FetchPurchases();
                    while (!session.PurchasesFetched) {
                        yield return new WaitForSeconds(0.1f);
                    }

                    if (_exception != null) {
                        result.OnError(_exception);
                        yield break;
                    }

                    if (session.Order == null) {
                        _status = Status.Purchasing;
                        session.Controller.PurchaseProduct(session.Product);
                        while (_status == Status.Purchasing) {
                            yield return new WaitForSeconds(0.1f);
                        }
                    }

                    if (_exception != null || session.Order == null) {
                        _status = Status.PurchaseFailed;
                        result.OnError(
                            _exception ?? new BadGatewayException("purchase failed: " + contentsId)
                        );
                        yield break;
                    }

                    result.OnComplete(
                        new PurchaseParameters {
                            receipt = _receipt,
                            transactionId = _transactionId,
                            controller = session.Controller,
                            order = session.Order,
                        }
                    );
                }
#endif
            }

            return new Gs2InlineFuture<PurchaseParameters>(Impl);
        }

#if GS2_ENABLE_UNITASK

        public async UniTask<PurchaseParameters> BuyAsync(
            string contentsId,
            ProductType productType = ProductType.Consumable
        )
        {
            if (_status != Status.None)
            {
#if GS2_IAP_5_0_0_OR_NEWER
                throw new ConflictException(new[] {
                    new RequestError("state", "money.state.state.error.running")
                });
#else
                return null;
#endif
            }
            _exception = null;
            _receipt = null;
            _transactionId = null;
            _status = Status.Initializing;

#if !GS2_IAP_5_0_0_OR_NEWER
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct(contentsId, productType);
            UnityPurchasing.Initialize(new Gs2StoreListener(this), builder);
            while (_status == Status.Initializing)
            {
                await UniTask.Delay(TimeSpan.FromMilliseconds(100));
            }
            if (_status == Status.InitializeFailed)
            {
                throw this._exception ?? new Exception();
            }

            _status = Status.Purchasing;

            foreach (var product in _controller.products.all ?? Array.Empty<Product>()) {
                if (product.hasReceipt) {
                    return new PurchaseParameters
                    {
                        receipt = product.receipt,
                        transactionId = product.transactionID,
                        controller = _controller,
                        product = product,
                    };
                }
            }

            _controller.InitiatePurchase(_controller.products.WithID(contentsId));
            while (_status == Status.Purchasing)
            {
                await UniTask.Delay(TimeSpan.FromMilliseconds(100));
            }

            return new PurchaseParameters
            {
                receipt = _receipt,
                transactionId = _transactionId,
                controller = _controller,
                product = _controller.products.WithID(contentsId),
            };
#else
            using (var session = new Gs2StoreSession(this, contentsId)) {

                try {
                    await session.Controller.Connect();
                }
                catch (Exception e) {
                    _status = Status.InitializeFailed;
                    throw new BadGatewayException(e.Message);
                }
                _status = Status.Initialized;

                session.Controller.FetchProducts(new List<ProductDefinition>
                {
                    new(contentsId, productType),
                });
                while (!session.ProductsFetched) {
                    await UniTask.Delay(TimeSpan.FromMilliseconds(100));
                }
                if (session.Product == null) {
                    _status = Status.PurchaseFailed;
                    throw _exception ?? new BadGatewayException("product not found: " + contentsId);
                }

                session.Controller.FetchPurchases();
                while (!session.PurchasesFetched) {
                    await UniTask.Delay(TimeSpan.FromMilliseconds(100));
                }

                if (_exception != null) {
                    throw _exception;
                }

                if (session.Order == null) {
                    _status = Status.Purchasing;
                    session.Controller.PurchaseProduct(session.Product);
                    while (_status == Status.Purchasing) {
                        await UniTask.Delay(TimeSpan.FromMilliseconds(100));
                    }
                }

                if (_exception != null || session.Order == null) {
                    _status = Status.PurchaseFailed;
                    throw _exception ?? new BadGatewayException("purchase failed: " + contentsId);
                }

                return new PurchaseParameters
                {
                    receipt = _receipt,
                    transactionId = _transactionId,
                    controller = session.Controller,
                    order = session.Order,
                };
            }
#endif
        }
#endif

#if !GS2_IAP_5_0_0_OR_NEWER
        private class Gs2StoreListener : IStoreListener
        {
            private readonly IAPUtil _client;

            public Gs2StoreListener(IAPUtil client)
            {
                _client = client;
            }

            public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
            {
                _client._controller = controller;
                _client._status = Status.Initialized;
            }

#if GS2_IAP_4_6_0_OR_NEWER
            public void OnInitializeFailed(InitializationFailureReason error, string message)
            {
                _client._exception = new BadGatewayException(error.ToString() + " " + message);
                _client._status = Status.InitializeFailed;
            }

            [Obsolete]
#endif
            public void OnInitializeFailed(InitializationFailureReason error)
            {
                _client._exception = new BadGatewayException(error.ToString());
                _client._status = Status.InitializeFailed;
            }

            public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
            {
                _client._receipt = e.purchasedProduct.receipt;
                _client._transactionId = e.purchasedProduct.transactionID;
                _client._status = Status.Purchased;
                return PurchaseProcessingResult.Pending;
            }

            public void OnPurchaseFailed(Product product, PurchaseFailureReason error)
            {
                _client._exception = new BadGatewayException(error.ToString());
                _client._status = Status.PurchaseFailed;
            }
        }
#else
        // Info.Receipt is already a unified receipt. Only replace Apple's payload;
        // wrapping the entire receipt again would send JSON where GS2 expects a JWS.
        private static string BuildReceipt(IOrderInfo info)
        {
            if (info == null || string.IsNullOrWhiteSpace(info.TransactionID)) {
                throw new BadGatewayException("purchase transaction ID is missing");
            }
            var jws = info.Apple?.jwsRepresentation;
            if (!string.IsNullOrWhiteSpace(jws)) {
                return new JsonData {
                    ["Store"] = "AppleAppStore",
                    ["TransactionID"] = info.TransactionID,
                    ["Payload"] = jws,
                }.ToJson();
            }

            var receipt = info.Receipt;
            if (string.IsNullOrWhiteSpace(receipt)) {
                throw new BadGatewayException("purchase receipt is missing");
            }
            // StoreKit 1 may still supply a valid legacy payload without a JWS.
            // Reject empty Apple payloads, including receipts without Apple metadata.
            try {
                var data = JsonMapper.ToObject(receipt);
                if (info.Apple != null ||
                    (data.IsObject && data.Keys.Contains("Store") &&
                     data["Store"]?.ToString() == "AppleAppStore")) {
                    if (!data.IsObject || !data.Keys.Contains("Payload") ||
                        data["Payload"] == null || !data["Payload"].IsString ||
                        string.IsNullOrWhiteSpace(data["Payload"].ToString())) {
                        throw new BadGatewayException("Apple purchase has neither JWS nor a legacy receipt payload");
                    }
                }
            }
            catch (Gs2Exception) {
                throw;
            }
            catch (Exception) {
                throw new BadGatewayException("purchase receipt is not valid JSON");
            }
            return receipt;
        }

        private class Gs2StoreSession : IDisposable
        {
            private readonly IAPUtil _client;
            private readonly string _contentsId;

            public readonly StoreController Controller;

            public bool ProductsFetched { get; private set; }

            public Product Product { get; private set; }

            public bool PurchasesFetched { get; private set; }

            public PendingOrder Order { get; private set; }

            public Gs2StoreSession(IAPUtil client, string contentsId)
            {
                _client = client;
                _contentsId = contentsId;

                Controller = UnityIAPServices.StoreController();

                Controller.OnProductsFetched += OnProductsFetched;
                Controller.OnProductsFetchFailed += OnProductsFetchFailed;
                Controller.OnPurchasesFetched += OnPurchasesFetched;
                Controller.OnPurchasesFetchFailed += OnPurchasesFetchFailed;
                Controller.OnPurchasePending += OnPurchasePending;
                Controller.OnPurchaseDeferred += OnPurchaseDeferred;
                Controller.OnPurchaseFailed += OnPurchaseFailed;
            }

            public void Dispose()
            {
                Controller.OnProductsFetched -= OnProductsFetched;
                Controller.OnProductsFetchFailed -= OnProductsFetchFailed;
                Controller.OnPurchasesFetched -= OnPurchasesFetched;
                Controller.OnPurchasesFetchFailed -= OnPurchasesFetchFailed;
                Controller.OnPurchasePending -= OnPurchasePending;
                Controller.OnPurchaseDeferred -= OnPurchaseDeferred;
                Controller.OnPurchaseFailed -= OnPurchaseFailed;
                // A failed or unconfirmed order remains in the store and can be
                // fetched again on the next Buy call, including on this instance.
                _client._status = Status.None;
            }

            private void OnProductsFetched(List<Product> products)
            {
                Product = products.FirstOrDefault(product => product.definition?.id == _contentsId);
                ProductsFetched = true;
            }

            private void OnProductsFetchFailed(ProductFetchFailed failure)
            {
                _client._exception = new BadGatewayException(failure.FailureReason);
                ProductsFetched = true;
            }

            private void OnPurchasesFetched(Orders orders)
            {
                foreach (var pending in orders.PendingOrders) {
                    OnPurchasePending(pending);
                }
                foreach (var deferred in orders.DeferredOrders) {
                    OnPurchaseDeferred(deferred);
                }
                PurchasesFetched = true;
            }

            private void OnPurchasesFetchFailed(PurchasesFetchFailureDescription failure)
            {
                _client._exception = new BadGatewayException(
                    "failed to fetch purchases: " + failure.FailureReason + " " + failure.Message
                );
                _client._status = Status.PurchaseFailed;
                PurchasesFetched = true;
            }

            private void OnPurchasePending(PendingOrder order)
            {
                if (Order != null || !IsTarget(order)) {
                    return;
                }
                Order = order;
                try {
                    _client._receipt = BuildReceipt(order.Info);
                    _client._transactionId = order.Info.TransactionID;
                    _client._status = Status.Purchased;
                }
                catch (Gs2Exception e) {
                    _client._exception = e;
                    _client._status = Status.PurchaseFailed;
                }
            }

            private void OnPurchaseDeferred(DeferredOrder order)
            {
                if (Order != null || !IsTarget(order)) {
                    return;
                }
                _client._exception = new BadGatewayException(
                    "purchase deferred: " + _contentsId
                );
                _client._status = Status.PurchaseFailed;
            }

            private void OnPurchaseFailed(FailedOrder order)
            {
                if (Order != null) {
                    return;
                }
                _client._exception = new BadGatewayException(
                    order.FailureReason + " " + order.Details
                );
                _client._status = Status.PurchaseFailed;
            }

            private bool IsTarget(Order order)
            {
                var items = order?.CartOrdered?.Items();
                if (items == null) {
                    return false;
                }
                return items.Any(item => item.Product?.definition?.id == _contentsId);
            }
        }
#endif
    }
}

#endif
