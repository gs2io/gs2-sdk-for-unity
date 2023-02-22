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

namespace Gs2.Unity.Gs2Showcase.ScriptableObject
{
    [CreateAssetMenu(fileName = "DisplayItem", menuName = "Game Server Services/Gs2Showcase/DisplayItem")]
    public class DisplayItem : UnityEngine.ScriptableObject
    {
        public Showcase Showcase;
        public string displayItemId;

        public string NamespaceName => this.Showcase.NamespaceName;
        public string ShowcaseName => this.Showcase.ShowcaseName;
        public string DisplayItemId => this.displayItemId;

#if UNITY_INCLUDE_TESTS
        public static DisplayItem Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<DisplayItem>(assetPath)
            );
        }
#endif

        public static DisplayItem New(
            Showcase Showcase,
            string displayItemId
        )
        {
            var instance = CreateInstance<DisplayItem>();
            instance.name = "Runtime";
            instance.Showcase = Showcase;
            instance.displayItemId = displayItemId;
            return instance;
        }

        public DisplayItem Clone()
        {
            var instance = CreateInstance<DisplayItem>();
            instance.name = "Runtime";
            instance.Showcase = Showcase;
            instance.displayItemId = displayItemId;
            return instance;
        }
    }
}