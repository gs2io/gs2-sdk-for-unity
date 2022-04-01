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

    [RequireComponent(typeof(Gs2InventoryItemFetcher))]
    [AddComponentMenu("GS2 UIKit/Inventory/Gs2InventoryItemPrefab")]
    public partial class Gs2InventoryItemPrefab : MonoBehaviour
    {
        private Item _item;
        
        public void Set(
            Inventory inventory,
            string itemName
        )
        {
            _item = ScriptableObject.CreateInstance<Item>();
            _item.inventory = inventory;
            _item.itemName = itemName;
            Item = _item;
        }

        public void OnDestroy()
        {
            if (_item != null)
            {
                Destroy(_item);
                _item = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2InventoryItemPrefab
    {
        private Gs2InventoryItemFetcher _inventoryItemFetcher;

        public void Awake()
        {
            _inventoryItemFetcher = GetComponent<Gs2InventoryItemFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2InventoryItemPrefab
    {
        public Item Item
        {
            get => _inventoryItemFetcher.item;
            set => _inventoryItemFetcher.item = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2InventoryItemPrefab
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryItemPrefab
    {
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