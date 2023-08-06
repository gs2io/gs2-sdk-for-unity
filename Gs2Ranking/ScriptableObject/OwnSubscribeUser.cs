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
    [CreateAssetMenu(fileName = "OwnSubscribeUser", menuName = "Game Server Services/Gs2Ranking/OwnSubscribeUser")]
    public class OwnSubscribeUser : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string categoryName;
        public string targetUserId;

        public string NamespaceName => this.Namespace.NamespaceName;
        public string CategoryName => this.categoryName;
        public string TargetUserId => this.targetUserId;

#if UNITY_INCLUDE_TESTS
        public static OwnSubscribeUser Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnSubscribeUser>(assetPath)
            );
        }
#endif

        public static OwnSubscribeUser New(
            Namespace Namespace,
            string categoryName,
            string targetUserId
        )
        {
            var instance = CreateInstance<OwnSubscribeUser>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.categoryName = categoryName;
            instance.targetUserId = targetUserId;
            return instance;
        }

        public OwnSubscribeUser Clone()
        {
            var instance = CreateInstance<OwnSubscribeUser>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.categoryName = categoryName;
            instance.targetUserId = targetUserId;
            return instance;
        }
    }
}