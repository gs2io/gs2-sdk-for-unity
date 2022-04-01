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
using Gs2.Unity.Gs2Showcase.Model;
using Gs2.Unity.Gs2Showcase.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Showcase.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Showcase
{
    [AddComponentMenu("GS2 UIKit/Showcase/Gs2ShowcaseBuy")]
    public partial class Gs2ShowcaseBuy : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => _displayItemFetcher.DisplayItem != null);
            
            var future = _clientHolder.Gs2.Showcase.Namespace(
                DisplayItem.showcase.Namespace.namespaceName
            ).Me(
                _gameSessionHolder.GameSession
            ).Showcase(
                DisplayItem.showcase.showcaseName
            ).Buy(
                _displayItemFetcher.DisplayItem.DisplayItemId
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
                        onBuyComplete.Invoke(_displayItemFetcher.DisplayItem);
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }

                onError.Invoke(future.Error, null);
                yield break;
            }
            onBuyComplete.Invoke(_displayItemFetcher.DisplayItem);
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
    
    public partial class Gs2ShowcaseBuy
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2ShowcaseDisplayItemFetcher _displayItemFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _displayItemFetcher = GetComponentInParent<Gs2ShowcaseDisplayItemFetcher>() ?? GetComponent<Gs2ShowcaseDisplayItemFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ShowcaseBuy
    {
        public DisplayItem DisplayItem
        {
            get => _displayItemFetcher.displayItem;
            set => _displayItemFetcher.displayItem = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ShowcaseBuy
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ShowcaseBuy
    {
        [Serializable]
        private class BuyCompleteEvent : UnityEvent<EzDisplayItem>
        {
            
        }
        
        [SerializeField]
        private BuyCompleteEvent onBuyComplete = new BuyCompleteEvent();
        
        public event UnityAction<EzDisplayItem> OnBuyComplete
        {
            add => onBuyComplete.AddListener(value);
            remove => onBuyComplete.RemoveListener(value);
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