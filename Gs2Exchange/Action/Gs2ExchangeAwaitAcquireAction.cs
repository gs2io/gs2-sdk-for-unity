/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable CheckNamespace

using System;
using System.Collections;
using Gs2.Core.Exception;
using Gs2.Unity.Gs2Exchange.Model;
using Gs2.Unity.Gs2Exchange.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Exchange
{
    [AddComponentMenu("GS2 UIKit/Exchange/Gs2ExchangeAwaitAcquireAction")]
    public partial class Gs2ExchangeAwaitAcquireAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => _awaitFetcher.Fetched);
            
            var future = _clientHolder.Gs2.Exchange.Namespace(
                Await.rate.Namespace.namespaceName
            ).Me(
                _gameSessionHolder.GameSession
            ).Await(
                Await.awaitName,
                Await.rate.rateName
            ).Acquire();
            yield return future;
            if (future.Error != null)
            {
                if (future.Error is TransactionException e)
                {
                    IEnumerator Retry()
                    {
                        var retryFuture = e.Retry();
                        yield return retryFuture;
                        if (retryFuture.Error != null)
                        {
                            onError.Invoke(future.Error, Retry);
                            yield break;
                        }
                        onExchangeComplete.Invoke(_awaitFetcher.Model, _awaitFetcher.Await);
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }

                onError.Invoke(future.Error, null);
                yield break;
            }
            onExchangeComplete.Invoke(_awaitFetcher.Model, _awaitFetcher.Await);
        }
        
        public void OnEnable()
        {
            StartCoroutine(nameof(Process));
        }
        
        public void OnDisable()
        {
            StopCoroutine(nameof(Process));
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2ExchangeAwaitAcquireAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2ExchangeAwaitFetcher _awaitFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _awaitFetcher = GetComponentInParent<Gs2ExchangeAwaitFetcher>() ?? GetComponent<Gs2ExchangeAwaitFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ExchangeAwaitAcquireAction
    {
        public Await Await
        {
            get => _awaitFetcher.await_;
            set => _awaitFetcher.await_ = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ExchangeAwaitAcquireAction
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeAwaitAcquireAction
    {
        [Serializable]
        private class ExchangeCompleteEvent : UnityEvent<EzRateModel, EzAwait>
        {
            
        }
        
        [SerializeField]
        private ExchangeCompleteEvent onExchangeComplete = new ExchangeCompleteEvent();
        
        public event UnityAction<EzRateModel, EzAwait> OnExchangeComplete
        {
            add => onExchangeComplete.AddListener(value);
            remove => onExchangeComplete.RemoveListener(value);
        }

        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();
        
        public event UnityAction<Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}