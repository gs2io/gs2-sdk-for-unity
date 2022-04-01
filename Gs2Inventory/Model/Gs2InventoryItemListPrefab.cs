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
    [RequireComponent(typeof(Gs2InventoryItemListFetcher))]
    [AddComponentMenu("GS2 UIKit/Inventory/Gs2InventoryItemListPrefab")]
    public partial class Gs2InventoryItemListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2InventoryItemPrefab> _cache = new Dictionary<string, Gs2InventoryItemPrefab>();

        public void Update()
        {
            if (_itemListFetcher.ItemModels != null)
            {
                void OnSelect(Gs2InventoryItemPrefab item)
                {
                    onSelect.Invoke(item);
                }

                var activeNames = _itemListFetcher.ItemModels.Select(v => v.Name);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        _cache[instantiateName].OnSelect -= OnSelect;
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteInventory.Invoke(instantiateName);
                    }
                }

                foreach (var itemModel in _itemListFetcher.ItemModels)
                {
                    if (!_cache.ContainsKey(itemModel.Name))
                    {
                        var item = Instantiate(questPrefab, populateNode);
                        item.name = itemModel.Name;
                        item.Set(Inventory, itemModel.Name);
                        item.OnSelect += OnSelect;
                        _cache[itemModel.Name] = item;
                        onCreateInventory.Invoke(itemModel);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2InventoryItemListPrefab
    {
        private Gs2InventoryItemListFetcher _itemListFetcher;

        public void Awake()
        {
            _itemListFetcher = GetComponent<Gs2InventoryItemListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2InventoryItemListPrefab
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
    
    public partial class Gs2InventoryItemListPrefab
    {
        public Gs2InventoryItemPrefab questPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryItemListPrefab
    {
        [Serializable]
        private class CreateInventoryEvent : UnityEvent<EzItemModel>
        {
            
        }
        
        [SerializeField]
        private CreateInventoryEvent onCreateInventory = new CreateInventoryEvent();
        
        public event UnityAction<EzItemModel> OnCreateInventory
        {
            add => onCreateInventory.AddListener(value);
            remove => onCreateInventory.RemoveListener(value);
        }

        [Serializable]
        private class DeleteInventoryEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private DeleteInventoryEvent onDeleteInventory = new DeleteInventoryEvent();
        
        public event UnityAction<string> OnDeleteInventory
        {
            add => onDeleteInventory.AddListener(value);
            remove => onDeleteInventory.RemoveListener(value);
        }
        
        [Serializable]
        private class SelectEvent : UnityEvent<Gs2InventoryItemPrefab>
        {
            
        }
        
        [SerializeField]
        private SelectEvent onSelect = new SelectEvent();
        
        public event UnityAction<Gs2InventoryItemPrefab> OnSelect
        {
            add => onSelect.AddListener(value);
            remove => onSelect.RemoveListener(value);
        }
    }
}