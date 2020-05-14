#if UNITY_PURCHASING

using System.Collections;
using System.Collections.Generic;
using Gs2.Core;
using Gs2.Core.Exception;
using Gs2.Core.Model;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;

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
    
    public class IAPUtil
    {
        private IStoreController _controller;

        private Status _status = Status.None;
        private Gs2Exception _exception;
        private string _receipt;
 
        public IEnumerator Buy(
            UnityAction<AsyncResult<string>> callback,
            string contentsId
        )
        {
            if (_status != Status.None)
            {
                callback.Invoke(
                    new AsyncResult<string>(
                        null, 
                        new ConflictException(
                            new List<RequestError>
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
                callback.Invoke(new AsyncResult<string>(null, _exception));
                yield break;
            }
             
            _status = Status.Purchasing;
            _controller.InitiatePurchase(_controller.products.WithID(contentsId));
            while (_status == Status.Purchasing)
            {
                yield return new WaitForSeconds(1);
            }
            
            callback.Invoke(new AsyncResult<string>(_receipt, _exception));
        }
 
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
}

#endif
