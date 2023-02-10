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
#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Inventory.ScriptableObject
{
    [CreateAssetMenu(fileName = "ItemSet", menuName = "Game Server Services/Gs2Inventory/ItemSet")]
    public class ItemSet : UnityEngine.ScriptableObject
    {
        public Inventory Inventory;
        public string itemName;
        public string itemSetName;

        public string NamespaceName => this.Inventory.NamespaceName;
        public string InventoryName => this.Inventory.InventoryName;
        public string ItemName => this.itemName;
        public string ItemSetName => this.itemSetName;

#if UNITY_INCLUDE_TESTS
        public static ItemSet Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<ItemSet>(assetPath)
            );
        }
#endif

        public static ItemSet New(
            Inventory Inventory,
            string itemName,
            string itemSetName
        )
        {
            var instance = CreateInstance<ItemSet>();
            instance.name = "Runtime";
            instance.Inventory = Inventory;
            instance.itemName = itemName;
            instance.itemSetName = itemSetName;
            return instance;
        }

        public ItemSet Clone()
        {
            var instance = CreateInstance<ItemSet>();
            instance.name = "Runtime";
            instance.Inventory = Inventory;
            instance.itemName = itemName;
            instance.itemSetName = itemSetName;
            return instance;
        }
    }
}