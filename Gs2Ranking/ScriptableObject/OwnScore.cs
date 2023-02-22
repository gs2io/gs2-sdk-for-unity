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
    [CreateAssetMenu(fileName = "OwnScore", menuName = "Game Server Services/Gs2Ranking/OwnScore")]
    public class OwnScore : UnityEngine.ScriptableObject
    {
        public CategoryModel CategoryModel;
        public string scorerUserId;
        public string uniqueId;

        public string NamespaceName => this.CategoryModel.NamespaceName;
        public string CategoryName => this.CategoryModel.CategoryName;
        public string ScorerUserId => this.scorerUserId;
        public string UniqueId => this.uniqueId;

#if UNITY_INCLUDE_TESTS
        public static OwnScore Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnScore>(assetPath)
            );
        }
#endif

        public static OwnScore New(
            CategoryModel CategoryModel,
            string scorerUserId,
            string uniqueId
        )
        {
            var instance = CreateInstance<OwnScore>();
            instance.name = "Runtime";
            instance.CategoryModel = CategoryModel;
            instance.scorerUserId = scorerUserId;
            instance.uniqueId = uniqueId;
            return instance;
        }

        public OwnScore Clone()
        {
            var instance = CreateInstance<OwnScore>();
            instance.name = "Runtime";
            instance.CategoryModel = CategoryModel;
            instance.scorerUserId = scorerUserId;
            instance.uniqueId = uniqueId;
            return instance;
        }
    }
}