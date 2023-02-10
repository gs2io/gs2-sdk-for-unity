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

namespace Gs2.Unity.Gs2Ranking.ScriptableObject
{
    [CreateAssetMenu(fileName = "CategoryModel", menuName = "Game Server Services/Gs2Ranking/CategoryModel")]
    public class CategoryModel : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string categoryName;

        public string NamespaceName => this.Namespace.NamespaceName;
        public string CategoryName => this.categoryName;

#if UNITY_INCLUDE_TESTS
        public static CategoryModel Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<CategoryModel>(assetPath)
            );
        }
#endif

        public static CategoryModel New(
            Namespace Namespace,
            string categoryName
        )
        {
            var instance = CreateInstance<CategoryModel>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.categoryName = categoryName;
            return instance;
        }

        public CategoryModel Clone()
        {
            var instance = CreateInstance<CategoryModel>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.categoryName = categoryName;
            return instance;
        }
    }
}