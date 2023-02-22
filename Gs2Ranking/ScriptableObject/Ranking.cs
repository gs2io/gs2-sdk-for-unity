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
    [CreateAssetMenu(fileName = "Ranking", menuName = "Game Server Services/Gs2Ranking/Ranking")]
    public class Ranking : UnityEngine.ScriptableObject
    {
        public User User;
        public CategoryModel CategoryModel;
        public long index;

        public string NamespaceName => this.User.NamespaceName;
        public string UserId => this.User.UserId;
        public string CategoryName => this.CategoryModel.CategoryName;
        public long Index => this.index;

#if UNITY_INCLUDE_TESTS
        public static Ranking Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Ranking>(assetPath)
            );
        }
#endif

        public static Ranking New(
            User User,
            CategoryModel CategoryModel,
            int index
        )
        {
            var instance = CreateInstance<Ranking>();
            instance.name = "Runtime";
            instance.User = User;
            instance.CategoryModel = CategoryModel;
            instance.index = index;
            return instance;
        }

        public Ranking Clone()
        {
            var instance = CreateInstance<Ranking>();
            instance.name = "Runtime";
            instance.User = User;
            instance.CategoryModel = CategoryModel;
            instance.index = index;
            return instance;
        }
    }
}