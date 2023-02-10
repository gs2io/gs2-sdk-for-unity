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

namespace Gs2.Unity.Gs2Experience.ScriptableObject
{
    [CreateAssetMenu(fileName = "User", menuName = "Game Server Services/Gs2Experience/User")]
    public class User : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string userId;

        public string NamespaceName => this.Namespace.NamespaceName;
        public string UserId => this.userId;

#if UNITY_INCLUDE_TESTS
        public static User Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<User>(assetPath)
            );
        }
#endif

        public static User New(
            Namespace Namespace,
            string userId
        )
        {
            var instance = CreateInstance<User>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.userId = userId;
            return instance;
        }

        public User Clone()
        {
            var instance = CreateInstance<User>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.userId = userId;
            return instance;
        }
    }
}