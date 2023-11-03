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
// ReSharper disable InconsistentNaming
// ReSharper disable Unity.NoNullPropagation

#pragma warning disable CS0109 // Member does not hide an inherited member; new keyword is not required
#pragma warning disable CS0108, CS0114

#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Inventory.ScriptableObject
{
    [CreateAssetMenu(fileName = "OwnSimpleItem", menuName = "Game Server Services/Gs2Inventory/OwnSimpleItem")]
    public class OwnSimpleItem : UnityEngine.ScriptableObject
    {
        public SimpleInventoryModel SimpleInventoryModel;
        public string itemName;

        public string NamespaceName => this.SimpleInventoryModel.NamespaceName;
        public string InventoryName => this.SimpleInventoryModel.InventoryName;
        public string ItemName => this.itemName;

#if UNITY_INCLUDE_TESTS
        public static OwnSimpleItem Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnSimpleItem>(assetPath)
            );
        }
#endif
        public static OwnSimpleItem New(
            SimpleInventoryModel @simpleInventoryModel,
            string itemName
        )
        {
            var instance = CreateInstance<OwnSimpleItem>();
            instance.name = "Runtime";
            instance.SimpleInventoryModel = @simpleInventoryModel;
            instance.itemName = itemName;
            return instance;
        }
        public OwnSimpleItem Clone()
        {
            var instance = CreateInstance<OwnSimpleItem>();
            instance.name = "Runtime";
            instance.SimpleInventoryModel = SimpleInventoryModel;
            instance.itemName = itemName;
            return instance;
        }
    }
}