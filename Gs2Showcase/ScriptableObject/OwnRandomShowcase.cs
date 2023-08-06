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

namespace Gs2.Unity.Gs2Showcase.ScriptableObject
{
    [CreateAssetMenu(fileName = "OwnRandomShowcase", menuName = "Game Server Services/Gs2Showcase/OwnRandomShowcase")]
    public class OwnRandomShowcase : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string showcaseName;

        public string NamespaceName => this.Namespace.NamespaceName;
        public string ShowcaseName => this.showcaseName;

#if UNITY_INCLUDE_TESTS
        public static OwnRandomShowcase Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnRandomShowcase>(assetPath)
            );
        }
#endif

        public static OwnRandomShowcase New(
            Namespace Namespace,
            string showcaseName
        )
        {
            var instance = CreateInstance<OwnRandomShowcase>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.showcaseName = showcaseName;
            return instance;
        }

        public OwnRandomShowcase Clone()
        {
            var instance = CreateInstance<OwnRandomShowcase>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.showcaseName = showcaseName;
            return instance;
        }
    }
}