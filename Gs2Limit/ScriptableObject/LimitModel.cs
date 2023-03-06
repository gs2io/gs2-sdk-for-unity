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

namespace Gs2.Unity.Gs2Limit.ScriptableObject
{
    [CreateAssetMenu(fileName = "LimitModel", menuName = "Game Server Services/Gs2Limit/LimitModel")]
    public class LimitModel : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string limitName;

        public string NamespaceName => this.Namespace?.NamespaceName;
        public string LimitName => this.limitName;

#if UNITY_INCLUDE_TESTS
        public static LimitModel Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<LimitModel>(assetPath)
            );
        }
#endif

        public static LimitModel New(
            Namespace Namespace,
            string limitName
        )
        {
            var instance = CreateInstance<LimitModel>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.limitName = limitName;
            return instance;
        }

        public LimitModel Clone()
        {
            var instance = CreateInstance<LimitModel>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.limitName = limitName;
            return instance;
        }
    }
}