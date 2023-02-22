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
    [CreateAssetMenu(fileName = "Score", menuName = "Game Server Services/Gs2Ranking/Score")]
    public class Score : UnityEngine.ScriptableObject
    {
        public User User;
        public CategoryModel CategoryModel;
        public string uniqueId;

        public string NamespaceName => this.User.NamespaceName;
        public string UserId => this.User.UserId;
        public string CategoryName => this.CategoryModel.CategoryName;
        public string UniqueId => this.uniqueId;

#if UNITY_INCLUDE_TESTS
        public static Score Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Score>(assetPath)
            );
        }
#endif

        public static Score New(
            User User,
            CategoryModel CategoryModel,
            string uniqueId
        )
        {
            var instance = CreateInstance<Score>();
            instance.name = "Runtime";
            instance.User = User;
            instance.CategoryModel = CategoryModel;
            instance.uniqueId = uniqueId;
            return instance;
        }

        public Score Clone()
        {
            var instance = CreateInstance<Score>();
            instance.name = "Runtime";
            instance.User = User;
            instance.CategoryModel = CategoryModel;
            instance.uniqueId = uniqueId;
            return instance;
        }
    }
}