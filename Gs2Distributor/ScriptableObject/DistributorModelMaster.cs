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

namespace Gs2.Unity.Gs2Distributor.ScriptableObject
{
    [CreateAssetMenu(fileName = "DistributorModelMaster", menuName = "Game Server Services/Gs2Distributor/DistributorModelMaster")]
    public class DistributorModelMaster : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string distributorName;

        public string NamespaceName => this.Namespace.NamespaceName;
        public string DistributorName => this.distributorName;

#if UNITY_INCLUDE_TESTS
        public static DistributorModelMaster Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<DistributorModelMaster>(assetPath)
            );
        }
#endif

        public static DistributorModelMaster New(
            Namespace Namespace,
            string distributorName
        )
        {
            var instance = CreateInstance<DistributorModelMaster>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.distributorName = distributorName;
            return instance;
        }

        public DistributorModelMaster Clone()
        {
            var instance = CreateInstance<DistributorModelMaster>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.distributorName = distributorName;
            return instance;
        }
    }
}