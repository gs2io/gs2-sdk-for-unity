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
    [CreateAssetMenu(fileName = "OwnReceived", menuName = "Game Server Services/Gs2Inbox/OwnReceived")]
    public class OwnReceived : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;

        public string NamespaceName => this.Namespace.NamespaceName;

#if UNITY_INCLUDE_TESTS
        public static OwnReceived Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnReceived>(assetPath)
            );
        }
#endif

        public static OwnReceived New(
            Namespace Namespace
        )
        {
            var instance = CreateInstance<OwnReceived>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            return instance;
        }

        public OwnReceived Clone()
        {
            var instance = CreateInstance<OwnReceived>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            return instance;
        }
    }
}