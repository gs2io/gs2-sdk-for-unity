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
using System.Collections.Generic;
using System.Linq;
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inventory
{
    [RequireComponent(typeof(Gs2InventoryItemSetListFetcher))]
    [AddComponentMenu("GS2 UIKit/Inventory/Gs2InventoryItemSetListPrefab")]
    public partial class Gs2InventoryItemSetListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2InventoryItemSetPrefab> _cache = new Dictionary<string, Gs2InventoryItemSetPrefab>();

        private Inventory _inventory;
        private ErrorEvent _onError;

        public void Set(
            Namespace @namespace, 
            EzInventoryModel inventoryModel, 
            EzInventory inventory, 
            ErrorEvent onError
        )
        {
            _inventory = ScriptableObject.CreateInstance<Inventory>();
            _inventory.Namespace = @namespace;
            _inventory.inventoryName = inventoryModel.Name;
            _onError = onError;

            var itemSetListFetcher = GetComponent<Gs2InventoryItemSetListFetcher>();
            itemSetListFetcher.inventory = _inventory;
        }

        public void Update()
        {
            if (_itemListFetcher.Fetched)
            {
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    var isSkeleton = _itemListFetcher.ItemModels.Count(v => instantiateName == v.Name) != 0;
                    var existsItemSet = _itemListFetcher.ItemSets
                        .Where(v => v.Count > 0)
                        .Select(v => v.ItemName + ":" + v.Name)
                        .Count(v => v.StartsWith(instantiateName)) > 0;
                    if ((existsItemSet && isSkeleton) || (!existsItemSet && !isSkeleton))
                    {
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteItemSet.Invoke(instantiateName);
                    }
                }

                foreach (var itemModel in _itemListFetcher.ItemModels)
                {
                    var itemSets = _itemListFetcher.ItemSets
                        .Where(v => v.ItemName == itemModel.Name)
                        .Where(v => v.Count > 0)
                        .ToList();
                    if (itemSets.Count == 0)
                    {
                        if (!_cache.ContainsKey(itemModel.Name))
                        {
                            var item = Instantiate(inventoryItemSetPrefabPrefab, populateNode);
                            item.name = itemModel.Name;
                            item.Awake();
                            item.Set(Inventory, itemModel.Name, null);
                            item.gameObject.SetActive(true);
                            item.transform.SetSiblingIndex(itemModel.SortValue);
                            _cache[itemModel.Name] = item;
                            onCreateItemSet.Invoke(itemModel, null);
                        }
                    }
                    else
                    {
                        foreach (var itemSet in itemSets)
                        {
                            if (!_cache.ContainsKey(itemModel.Name + ":" + itemSet.Name))
                            {
                                var item = Instantiate(inventoryItemSetPrefabPrefab, populateNode);
                                item.name = itemModel.Name;
                                item.Awake();
                                item.Set(Inventory, itemModel.Name, itemSet.Name);
                                item.gameObject.SetActive(true);
                                item.transform.SetSiblingIndex(itemModel.SortValue);
                                _cache[itemModel.Name + ":" + itemSet.Name] = item;
                                onCreateItemSet.Invoke(itemModel, itemSet);
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2InventoryItemSetListPrefab
    {
        private Gs2InventoryItemSetListFetcher _itemListFetcher;

        private void OnError(
            Exception exception, 
            Func<IEnumerator> retryFunc
        )
        {
            if (_onError != null)
            {
                _onError.Invoke(exception, retryFunc);
            }
        }

        public void Awake()
        {
            _itemListFetcher = GetComponent<Gs2InventoryItemSetListFetcher>();
            _itemListFetcher.OnError += OnError;
        }
        
        public void OnDestroy()
        {
            _itemListFetcher.OnError -= OnError;

            if (_inventory != null)
            {
                Destroy(_inventory);
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2InventoryItemSetListPrefab
    {
        public Inventory Inventory
        {
            get => _itemListFetcher.inventory;
            set => _itemListFetcher.inventory = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2InventoryItemSetListPrefab
    {
        public Gs2InventoryItemSetPrefab inventoryItemSetPrefabPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryItemSetListPrefab
    {
        [Serializable]
        private class CreateItemSetEvent : UnityEvent<EzItemModel, EzItemSet>
        {
            
        }
        
        [SerializeField]
        private CreateItemSetEvent onCreateItemSet = new CreateItemSetEvent();
        
        public event UnityAction<EzItemModel, EzItemSet> OnCreateItemSet
        {
            add => onCreateItemSet.AddListener(value);
            remove => onCreateItemSet.RemoveListener(value);
        }

        [Serializable]
        private class DeleteItemSetEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private DeleteItemSetEvent onDeleteItemSet = new DeleteItemSetEvent();
        
        public event UnityAction<string> OnDeleteItemSet
        {
            add => onDeleteItemSet.AddListener(value);
            remove => onDeleteItemSet.RemoveListener(value);
        }
    }
}