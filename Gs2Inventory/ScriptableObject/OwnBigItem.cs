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
    [CreateAssetMenu(fileName = "OwnBigItem", menuName = "Game Server Services/Gs2Inventory/OwnBigItem")]
    public class OwnBigItem : UnityEngine.ScriptableObject
    {
        public BigInventoryModel BigInventoryModel;
        public string itemName;

        public string NamespaceName => this.BigInventoryModel.NamespaceName;
        public string InventoryName => this.BigInventoryModel.InventoryName;
        public string ItemName => this.itemName;

#if UNITY_INCLUDE_TESTS
        public static OwnBigItem Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnBigItem>(assetPath)
            );
        }
#endif
        public static OwnBigItem New(
            BigInventoryModel @bigInventoryModel,
            string itemName
        )
        {
            var instance = CreateInstance<OwnBigItem>();
            instance.name = "Runtime";
            instance.BigInventoryModel = @bigInventoryModel;
            instance.itemName = itemName;
            return instance;
        }
        public OwnBigItem Clone()
        {
            var instance = CreateInstance<OwnBigItem>();
            instance.name = "Runtime";
            instance.BigInventoryModel = BigInventoryModel;
            instance.itemName = itemName;
            return instance;
        }
    }
}