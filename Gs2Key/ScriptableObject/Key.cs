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

namespace Gs2.Unity.Gs2Key.ScriptableObject
{
    [CreateAssetMenu(fileName = "Key", menuName = "Game Server Services/Gs2Key/Key")]
    public class Key : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string keyName;

        public string NamespaceName => this.Namespace?.NamespaceName;
        public string KeyName => this.keyName;

        public string Grn => $"grn:gs2:{{region}}:{{ownerId}}:key:{Namespace.namespaceName}:key:{keyName}";

#if UNITY_INCLUDE_TESTS
        public static Key Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Key>(assetPath)
            );
        }
#endif

        public static Key New(
            Namespace Namespace,
            string keyName
        )
        {
            var instance = CreateInstance<Key>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.keyName = keyName;
            return instance;
        }

        public Key Clone()
        {
            var instance = CreateInstance<Key>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.keyName = keyName;
            return instance;
        }
    }
}