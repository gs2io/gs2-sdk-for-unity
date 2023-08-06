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

namespace Gs2.Unity.Gs2Ranking.ScriptableObject
{
    public class OwnRanking : UnityEngine.ScriptableObject
    {
        public CategoryModel CategoryModel;
        public long index;

        public string NamespaceName => this.CategoryModel.NamespaceName;
        public string CategoryName => this.CategoryModel.CategoryName;
        public long Index => this.index;

#if UNITY_INCLUDE_TESTS
        public static OwnRanking Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnRanking>(assetPath)
            );
        }
#endif

        public static OwnRanking New(
            CategoryModel CategoryModel,
            long index
        )
        {
            var instance = CreateInstance<OwnRanking>();
            instance.name = "Runtime";
            instance.CategoryModel = CategoryModel;
            instance.index = index;
            return instance;
        }

        public OwnRanking Clone()
        {
            var instance = CreateInstance<OwnRanking>();
            instance.name = "Runtime";
            instance.CategoryModel = CategoryModel;
            instance.index = index;
            return instance;
        }
    }
}