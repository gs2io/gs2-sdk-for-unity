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

namespace Gs2.Unity.Gs2Lottery.ScriptableObject
{
    [CreateAssetMenu(fileName = "OwnDrawnPrize", menuName = "Game Server Services/Gs2Lottery/OwnDrawnPrize")]
    public class OwnDrawnPrize : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public int index;

        public string NamespaceName => this.Namespace.NamespaceName;
        public int Index => this.index;

#if UNITY_INCLUDE_TESTS
        public static OwnDrawnPrize Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnDrawnPrize>(assetPath)
            );
        }
#endif

        public static OwnDrawnPrize New(
            Namespace Namespace,
            int index
        )
        {
            var instance = CreateInstance<OwnDrawnPrize>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.index = index;
            return instance;
        }

        public OwnDrawnPrize Clone()
        {
            var instance = CreateInstance<OwnDrawnPrize>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.index = index;
            return instance;
        }
    }
}