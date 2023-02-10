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

namespace Gs2.Unity.Gs2Inbox.ScriptableObject
{
    [CreateAssetMenu(fileName = "Received", menuName = "Game Server Services/Gs2Inbox/Received")]
    public class Received : UnityEngine.ScriptableObject
    {
        public User User;

        public string NamespaceName => this.User.NamespaceName;
        public string UserId => this.User.UserId;

#if UNITY_INCLUDE_TESTS
        public static Received Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Received>(assetPath)
            );
        }
#endif

        public static Received New(
            User User
        )
        {
            var instance = CreateInstance<Received>();
            instance.name = "Runtime";
            instance.User = User;
            return instance;
        }

        public Received Clone()
        {
            var instance = CreateInstance<Received>();
            instance.name = "Runtime";
            instance.User = User;
            return instance;
        }
    }
}