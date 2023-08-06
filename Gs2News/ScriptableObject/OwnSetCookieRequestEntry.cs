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

namespace Gs2.Unity.Gs2News.ScriptableObject
{
    public class OwnSetCookieRequestEntry : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string key;
        public string value;

        public string NamespaceName => this.Namespace.NamespaceName;
        public string Key => this.key;
        public string Value => this.value;

#if UNITY_INCLUDE_TESTS
        public static OwnSetCookieRequestEntry Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnSetCookieRequestEntry>(assetPath)
            );
        }
#endif

        public static OwnSetCookieRequestEntry New(
            Namespace Namespace,
            string key,
            string value
        )
        {
            var instance = CreateInstance<OwnSetCookieRequestEntry>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.key = key;
            instance.value = value;
            return instance;
        }

        public OwnSetCookieRequestEntry Clone()
        {
            var instance = CreateInstance<OwnSetCookieRequestEntry>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.key = key;
            instance.value = value;
            return instance;
        }
    }
}