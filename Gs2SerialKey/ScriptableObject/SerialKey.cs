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

namespace Gs2.Unity.Gs2SerialKey.ScriptableObject
{
    [CreateAssetMenu(fileName = "SerialKey", menuName = "Game Server Services/Gs2SerialKey/SerialKey")]
    public class SerialKey : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string serialKeyCode;

        public string NamespaceName => this.Namespace?.NamespaceName;
        public string SerialKeyCode => this.serialKeyCode;

#if UNITY_INCLUDE_TESTS
        public static SerialKey Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<SerialKey>(assetPath)
            );
        }
#endif

        public static SerialKey New(
            Namespace Namespace,
            string serialKeyCode
        )
        {
            var instance = CreateInstance<SerialKey>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.serialKeyCode = serialKeyCode;
            return instance;
        }

        public SerialKey Clone()
        {
            var instance = CreateInstance<SerialKey>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.serialKeyCode = serialKeyCode;
            return instance;
        }
    }
}