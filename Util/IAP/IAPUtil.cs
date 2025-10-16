#if GS2_ENABLE_PURCHASING

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
#if !GS2_IAP_5_0_0_OR_NEWER
        public IStoreController controller;
        public Product product;
#else
        public StoreController controller;
        public PendingOrder order;
        public PendingOrder product => order;
#endif
    }
    
    public static class StoreControllerExt {
        public static void ConfirmPendingPurchase(this StoreController self, PendingOrder order) {
            self.ConfirmPurchase(order);
        }
    }
    
    public class IAPUtil
    {
#if !GS2_IAP_5_0_0_OR_NEWER
        private IStoreController _controller;
#endif

        private Status _status = Status.None;
        private Gs2Exception _exception;
        private string _receipt;
 
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
                        controller = _controller,
                        product = _controller.products.WithID(contentsId),
                    }
                );
#else
                _receipt = null;
                _exception = null;
            
                var controller = UnityIAPServices.StoreController();
                controller.Connect().Start();
            
                _status = Status.Purchasing;

                PendingOrder order = null;
                controller.FetchProducts(new List<ProductDefinition>
                {
                    new(contentsId, productType),
                });
                controller.OnPurchasePending += pending =>
                {
                    _receipt = pending.Info.Receipt;
                    order = pending;
                    _status = Status.Purchased;
                };
                controller.OnPurchaseFailed += failed =>
                {
                    _exception = new BadGatewayException(failed.ToString());
                    _status = Status.PurchaseFailed;
                };
            
                while (_status == Status.Purchasing)
                {
                    yield return new WaitForSeconds(0.1f);
                }

                result.OnComplete(
                    new PurchaseParameters {
                        receipt = _receipt,
                        controller = controller,
                        order = order,
                    }
                );
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
                return null;
            }
            _exception = null;
            _receipt = null;
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
                controller = _controller,
                product = _controller.products.WithID(contentsId),
            };
#else
            _receipt = null;
            _exception = null;
            
            var controller = UnityIAPServices.StoreController();
            await controller.Connect();
            
            _status = Status.Purchasing;

            PendingOrder order = null;
            controller.FetchProducts(new List<ProductDefinition>
            {
                new(contentsId, productType),
            });
            controller.OnPurchasePending += pending =>
            {
                _receipt = pending.Info.Receipt;
                order = pending;
                _status = Status.Purchased;
            };
            controller.OnPurchaseFailed += failed =>
            {
                _exception = new BadGatewayException(failed.ToString());
                _status = Status.PurchaseFailed;
            };
            
            while (_status == Status.Purchasing)
            {
                await UniTask.Delay(TimeSpan.FromMilliseconds(100));
            }
            
            return new PurchaseParameters
            {
                receipt = _receipt,
                controller = controller,
                order = order,
            };
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
                _client._status = Status.Purchased;
                return PurchaseProcessingResult.Pending;
            }
 
            public void OnPurchaseFailed(Product product, PurchaseFailureReason error)
            {
                _client._exception = new BadGatewayException(error.ToString());
                _client._status = Status.PurchaseFailed;
            }
        }
#endif
    }
}

#endif
