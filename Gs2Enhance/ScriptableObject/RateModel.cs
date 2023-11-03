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
// ReSharper disable InconsistentNaming
// ReSharper disable Unity.NoNullPropagation

#pragma warning disable CS0109 // Member does not hide an inherited member; new keyword is not required
#pragma warning disable CS0108, CS0114

#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Enhance.ScriptableObject
{
    [CreateAssetMenu(fileName = "RateModel", menuName = "Game Server Services/Gs2Enhance/RateModel")]
    public class RateModel : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string rateName;

        public string NamespaceName => this.Namespace?.NamespaceName;
        public string RateName => this.rateName;

#if UNITY_INCLUDE_TESTS
        public static RateModel Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<RateModel>(assetPath)
            );
        }
#endif

        public static RateModel New(
            Namespace Namespace,
            string rateName
        )
        {
            var instance = CreateInstance<RateModel>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.rateName = rateName;
            return instance;
        }

        public RateModel Clone()
        {
            var instance = CreateInstance<RateModel>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.rateName = rateName;
            return instance;
        }
    }
}