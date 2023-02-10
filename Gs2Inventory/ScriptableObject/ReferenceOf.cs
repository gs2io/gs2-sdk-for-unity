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
 *
 * deny overwrite
 */
#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Inventory.ScriptableObject
{
    [CreateAssetMenu(fileName = "ReferenceOf", menuName = "Game Server Services/Gs2Inventory/ReferenceOf")]
    public class ReferenceOf : UnityEngine.ScriptableObject
    {
        public ItemSet ItemSet;

#if UNITY_INCLUDE_TESTS
        public static ReferenceOf Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<ReferenceOf>(assetPath)
            );
        }
#endif

        public static ReferenceOf New(
            ItemSet ItemSet
        )
        {
            var instance = CreateInstance<ReferenceOf>();
            instance.name = "Runtime";
            instance.ItemSet = ItemSet;
            return instance;
        }

        public ReferenceOf Clone()
        {
            var instance = CreateInstance<ReferenceOf>();
            instance.name = "Runtime";
            instance.ItemSet = ItemSet;
            return instance;
        }
    }
}