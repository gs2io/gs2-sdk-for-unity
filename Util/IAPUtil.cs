#if GS2_ENABLE_PURCHASING

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Cysharp.Threading.Tasks;
using Gs2.Core;
using Gs2.Core.Exception;
using Gs2.Core.Model;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

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
        )
        {
            if (_status != Status.None)
            {
                callback.Invoke(
                    new AsyncResult<PurchaseParameters>(
                        null, 
                        new ConflictException(
                            new RequestError[]
                            {
                                new RequestError("state", "money.state.state.error.running")
                            }
                        )
                    )
                );
                yield break;
            }
            _exception = null;
            _receipt = null;
            _status = Status.Initializing;
            
#if UNITY_INCLUDE_TESTS
            var builder = ConfigurationBuilder.Instance(new TestingPurchasingModule());
#else
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
#endif
            var ids = new IDs {{contentsId, contentsId}};
            builder.AddProduct(contentsId, ProductType.Consumable, ids);
            UnityPurchasing.Initialize(new Gs2StoreListener(this), builder);
            while (_status == Status.Initializing)
            {
                yield return new WaitForSeconds(1);
            }
            if (_status == Status.InitializeFailed)
            {
                callback.Invoke(new AsyncResult<PurchaseParameters>(null, _exception));
                yield break;
            }
             
            _status = Status.Purchasing;
            _controller.InitiatePurchase(_controller.products.WithID(contentsId));
            while (_status == Status.Purchasing)
            {
                yield return new WaitForSeconds(1);
            }
            
            callback.Invoke(new AsyncResult<PurchaseParameters>(
                new PurchaseParameters {
                    receipt=_receipt,
                    controller=_controller,
                    product=_controller.products.WithID(contentsId),
                }, _exception));
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
            
#if UNITY_INCLUDE_TESTS
            var builder = ConfigurationBuilder.Instance(new TestingPurchasingModule());
#else
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
#endif
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
    
#if UNITY_INCLUDE_TESTS
    
    class TestingPurchasingModule : AbstractPurchasingModule
    {
        public override void Configure()
        {
            var assembly = typeof( StandardPurchasingModule ).Assembly;
            var type = assembly.GetType("UnityEngine.Purchasing.FakeStore");
            var store = type.GetConstructor(new Type[] { })?.Invoke(new object[] { });
            RegisterStore("fake", (IStore) store);
        }
    }
#endif
}

#endif
