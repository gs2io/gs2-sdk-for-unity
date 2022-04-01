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
    [RequireComponent(typeof(Gs2InventoryInventoryListFetcher))]
    [AddComponentMenu("GS2 UIKit/Inventory/Gs2InventoryInventoryListPrefab")]
    public partial class Gs2InventoryInventoryListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2InventoryInventoryPrefab> _cache = new Dictionary<string, Gs2InventoryInventoryPrefab>();

        public void Update()
        {
            if (_inventoryListFetcher.Fetched)
            {
                var activeNames = _inventoryListFetcher.Models.Select(v => v.Name);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteInventory.Invoke(instantiateName);
                    }
                }

                foreach (var inventory in _inventoryListFetcher.Models)
                {
                    if (!_cache.ContainsKey(inventory.Name))
                    {
                        var item = Instantiate(inventoryPrefab, populateNode);
                        item.name = inventory.Name;
                        item.Set(Namespace, inventory.Name);
                        item.gameObject.SetActive(true);
                        _cache[inventory.Name] = item;
                        onCreateInventory.Invoke(inventory);
                    }
                }
            }
        }

        public void OnDestroy()
        {
            foreach (var component in _cache.Values)
            {
                Destroy(component);
            }
            _cache.Clear();
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2InventoryInventoryListPrefab
    {
        private Gs2InventoryInventoryListFetcher _inventoryListFetcher;

        public void Awake()
        {
            _inventoryListFetcher = GetComponent<Gs2InventoryInventoryListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2InventoryInventoryListPrefab
    {
        public Namespace Namespace
        {
            get => _inventoryListFetcher.Namespace;
            set => _inventoryListFetcher.Namespace = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2InventoryInventoryListPrefab
    {
        public Gs2InventoryInventoryPrefab inventoryPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryInventoryListPrefab
    {
        [Serializable]
        private class CreateInventoryEvent : UnityEvent<EzInventoryModel>
        {
            
        }
        
        [SerializeField]
        private CreateInventoryEvent onCreateInventory = new CreateInventoryEvent();
        
        public event UnityAction<EzInventoryModel> OnCreateInventory
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
    }
}