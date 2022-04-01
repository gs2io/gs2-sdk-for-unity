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
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inventory
{
    [AddComponentMenu("GS2 UIKit/Inventory/Gs2InventoryConsumeItemSet")]
    public partial class Gs2InventoryConsumeItemSet : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => _itemSetFetcher.Fetched);
                
            var future = _clientHolder.Gs2.Inventory.Namespace(
                _itemSetFetcher.itemSet.item.inventory.Namespace.namespaceName
            ).Me(
                _gameSessionHolder.GameSession
            ).Inventory(
                _itemSetFetcher.itemSet.item.inventory.inventoryName
            ).ItemSet(
                _itemSetFetcher.itemSet.item.itemName,
                _itemSetFetcher.itemSet.itemSetName
            ).Consume(
                consumeCount
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
                        onConsumeComplete.Invoke(this);
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }

                onError.Invoke(future.Error, null);
                yield break;
            }
            onConsumeComplete.Invoke(this);
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
    
    public partial class Gs2InventoryConsumeItemSet
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2InventoryItemSetFetcher _itemSetFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _itemSetFetcher = GetComponentInParent<Gs2InventoryItemSetFetcher>() ?? GetComponent<Gs2InventoryItemSetFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2InventoryConsumeItemSet
    {
        public ItemSet ItemSet
        {
            get => _itemSetFetcher.itemSet;
            set => _itemSetFetcher.itemSet = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2InventoryConsumeItemSet
    {
        public int consumeCount;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryConsumeItemSet
    {
        [Serializable]
        private class ConsumeCompleteEvent : UnityEvent<Gs2InventoryConsumeItemSet>
        {
            
        }
        
        [SerializeField]
        private ConsumeCompleteEvent onConsumeComplete = new ConsumeCompleteEvent();
        
        public event UnityAction<Gs2InventoryConsumeItemSet> OnConsumeComplete
        {
            add => onConsumeComplete.AddListener(value);
            remove => onConsumeComplete.RemoveListener(value);
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