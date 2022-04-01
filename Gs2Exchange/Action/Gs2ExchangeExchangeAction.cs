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
    [AddComponentMenu("GS2 UIKit/Exchange/Gs2ExchangeExchangeAction")]
    public partial class Gs2ExchangeExchangeAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => _rateFetcher.Fetched);
            
            var future = _clientHolder.Gs2.Exchange.Namespace(
                Rate.Namespace.namespaceName
            ).Me(
                _gameSessionHolder.GameSession
            ).Exchange(
            ).Exchange(
                Rate.rateName,
                exchangeCount
            );
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
                        onExchangeComplete.Invoke(_rateFetcher.RateModel);
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }

                onError.Invoke(future.Error, null);
                yield break;
            }
            onExchangeComplete.Invoke(_rateFetcher.RateModel);
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
    
    public partial class Gs2ExchangeExchangeAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2ExchangeRateFetcher _rateFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _rateFetcher = GetComponentInParent<Gs2ExchangeRateFetcher>() ?? GetComponent<Gs2ExchangeRateFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ExchangeExchangeAction
    {
        public Rate Rate
        {
            get => _rateFetcher.rate;
            set => _rateFetcher.rate = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ExchangeExchangeAction
    {
        public int exchangeCount;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeExchangeAction
    {
        [Serializable]
        private class ExchangeCompleteEvent : UnityEvent<EzRateModel>
        {
            
        }
        
        [SerializeField]
        private ExchangeCompleteEvent onExchangeComplete = new ExchangeCompleteEvent();
        
        public event UnityAction<EzRateModel> OnExchangeComplete
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