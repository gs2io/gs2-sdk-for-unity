#if GS2_ENABLE_PURCHASING

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Gs2.Core;
using Gs2.Core.Exception;
using Gs2.Core.Model;
using Gs2.Core.Domain;
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
        public IStoreController controller;
        public Product product;
    }

    public class IAPUtil
    {
        private IStoreController _controller;

        private Status _status = Status.None;
        private Gs2Exception _exception;
        private string _receipt;
 
        public IEnumerator Buy(
            UnityAction<AsyncResult<PurchaseParameters>> callback,
            string contentsId
        ) {
            var future = BuyFuture(contentsId);
            yield return future;
            callback.Invoke(new AsyncResult<PurchaseParameters>(
                future.Result,
                future.Error
            ));
        }
        
        public Gs2Future<PurchaseParameters> BuyFuture(
            string contentsId
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
                _status = Status.Initializing;
            
                var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
                var ids = new IDs {{contentsId, contentsId}};
                builder.AddProduct(contentsId, ProductType.Consumable, ids);
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
                    yield return new WaitForSeconds(1);
                }
            
                result.OnComplete(
                    new PurchaseParameters {
                        receipt = _receipt,
                        controller = _controller,
                        product = _controller.products.WithID(contentsId),
                    }
                );
            }

            return new Gs2InlineFuture<PurchaseParameters>(Impl);
        }
        
#if GS2_ENABLE_UNITASK
        
        public async UniTask<PurchaseParameters> BuyAsync(
            string contentsId
        )
        {
            if (_status != Status.None)
            {
                return null;
            }
            _exception = null;
            _receipt = null;
            _status = Status.Initializing;
            
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            var ids = new IDs {{contentsId, contentsId}};
            builder.AddProduct(contentsId, ProductType.Consumable, ids);
            UnityPurchasing.Initialize(new Gs2StoreListener(this), builder);
            while (_status == Status.Initializing)
            {
                await UniTask.Delay(TimeSpan.FromMilliseconds(100));
            }
            if (_status == Status.InitializeFailed)
            {
                throw new Exception();
            }
             
            _status = Status.Purchasing;
            _controller.InitiatePurchase(_controller.products.WithID(contentsId));
            while (_status == Status.Purchasing)
            {
                await UniTask.Delay(TimeSpan.FromMilliseconds(100));
            }

            return new PurchaseParameters
            {
                receipt = _receipt,
                controller = _controller,
                product = _controller.products.WithID(contentsId),
            };
        }
#endif

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
            public void OnInitializeFailed(InitializationFailureReason error, string? message)
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
                _client._status = Status.Purchased;
                return PurchaseProcessingResult.Pending;
            }
 
            public void OnPurchaseFailed(Product product, PurchaseFailureReason error)
            {
                _client._exception = new BadGatewayException(error.ToString());
                _client._status = Status.PurchaseFailed;
            }
        }
    }
}

#endif
