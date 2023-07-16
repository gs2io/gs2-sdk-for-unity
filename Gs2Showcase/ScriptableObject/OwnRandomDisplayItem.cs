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

namespace Gs2.Unity.Gs2Showcase.ScriptableObject
{
    [CreateAssetMenu(fileName = "OwnRandomDisplayItem", menuName = "Game Server Services/Gs2Showcase/OwnRandomDisplayItem")]
    public class OwnRandomDisplayItem : UnityEngine.ScriptableObject
    {
        public RandomShowcase RandomShowcase;
        public string displayItemName;

        public string NamespaceName => this.RandomShowcase?.NamespaceName;
        public string ShowcaseName => this.RandomShowcase?.ShowcaseName;
        public string DisplayItemName => this.displayItemName;

#if UNITY_INCLUDE_TESTS
        public static OwnRandomDisplayItem Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnRandomDisplayItem>(assetPath)
            );
        }
#endif

        public static OwnRandomDisplayItem New(
            RandomShowcase RandomShowcase,
            string displayItemName
        )
        {
            var instance = CreateInstance<OwnRandomDisplayItem>();
            instance.name = "Runtime";
            instance.RandomShowcase = RandomShowcase;
            instance.displayItemName = displayItemName;
            return instance;
        }

        public OwnRandomDisplayItem Clone()
        {
            var instance = CreateInstance<OwnRandomDisplayItem>();
            instance.name = "Runtime";
            instance.RandomShowcase = RandomShowcase;
            instance.displayItemName = displayItemName;
            return instance;
        }
    }
}