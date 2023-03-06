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

namespace Gs2.Unity.Gs2Quest.ScriptableObject
{
    [CreateAssetMenu(fileName = "Progress", menuName = "Game Server Services/Gs2Quest/Progress")]
    public class Progress : UnityEngine.ScriptableObject
    {
        public User User;

        public string NamespaceName => this.User?.NamespaceName;
        public string UserId => this.User?.UserId;

#if UNITY_INCLUDE_TESTS
        public static Progress Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Progress>(assetPath)
            );
        }
#endif

        public static Progress New(
            User User
        )
        {
            var instance = CreateInstance<Progress>();
            instance.name = "Runtime";
            instance.User = User;
            return instance;
        }

        public Progress Clone()
        {
            var instance = CreateInstance<Progress>();
            instance.name = "Runtime";
            instance.User = User;
            return instance;
        }
    }
}