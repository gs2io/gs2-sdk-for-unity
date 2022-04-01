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
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inventory
{
    /// <summary>
    /// Main
    /// </summary>

    [RequireComponent(typeof(Gs2InventoryItemSetFetcher))]
    [AddComponentMenu("GS2 UIKit/Inventory/Gs2InventoryItemSetPrefab")]
    public partial class Gs2InventoryItemSetPrefab : MonoBehaviour
    {
        private Item _item;
        private ItemSet _itemSet;
        
        public void Set(
            Inventory inventory,
            string itemName,
            string itemSetName
        )
        {
            _item = ScriptableObject.CreateInstance<Item>();
            _item.inventory = inventory;
            _item.itemName = itemName;
            
            _itemSet = ScriptableObject.CreateInstance<ItemSet>();
            _itemSet.item = _item;
            _itemSet.itemSetName = itemSetName;
            ItemSet = _itemSet;
        }

        public void OnDestroy()
        {
            if (_item != null)
            {
                Destroy(_item);
                _item = null;
            }
            if (_itemSet != null)
            {
                Destroy(_itemSet);
                _itemSet = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2InventoryItemSetPrefab
    {
        private Gs2InventoryItemSetFetcher _inventoryItemSetFetcher;

        public void Awake()
        {
            _inventoryItemSetFetcher = GetComponent<Gs2InventoryItemSetFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2InventoryItemSetPrefab
    {
        public ItemSet ItemSet
        {
            get => _inventoryItemSetFetcher.itemSet;
            set => _inventoryItemSetFetcher.itemSet = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2InventoryItemSetPrefab
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryItemSetPrefab
    {
        
    }
}