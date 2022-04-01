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
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inventory
{
    [RequireComponent(typeof(Gs2InventoryInventoryFetcher))]
    [AddComponentMenu("GS2 UIKit/Inventory/Gs2InventoryInventoryPrefab")]
    public partial class Gs2InventoryInventoryPrefab : MonoBehaviour
    {
        private Inventory _inventory;
        
        public void Set(
            Namespace @namespace,
            string inventoryName
        )
        {
            _inventory = ScriptableObject.CreateInstance<Inventory>();
            _inventory.Namespace = @namespace;
            _inventory.inventoryName = inventoryName;
            
            var inventoryFetcher = GetComponent<Gs2InventoryInventoryFetcher>();
            inventoryFetcher.inventory = _inventory;
        }

        public void OnDestroy()
        {
            if (_inventory != null)
            {
                Destroy(_inventory);
                _inventory = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2InventoryInventoryPrefab
    {
        private Gs2InventoryInventoryFetcher _inventoryFetcher;

        public void Awake()
        {
            _inventoryFetcher = GetComponent<Gs2InventoryInventoryFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2InventoryInventoryPrefab
    {
        public Inventory Inventory
        {
            get => _inventoryFetcher != null && _inventoryFetcher.Fetched ? _inventoryFetcher.inventory : null;
            set => _inventoryFetcher.inventory = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2InventoryInventoryPrefab
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryInventoryPrefab
    {
        
    }
}