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
namespace Gs2.Unity.Gs2Enchant.ScriptableObject
{
    [CreateAssetMenu(fileName = "OwnRarityParameterStatus", menuName = "Game Server Services/Gs2Enchant/OwnRarityParameterStatus")]
    public class OwnRarityParameterStatus : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string parameterName;
        public string propertyId;

        public string NamespaceName => this.Namespace.NamespaceName;
        public string ParameterName => this.parameterName;
        public string PropertyId => this.propertyId;

#if UNITY_INCLUDE_TESTS
        public static OwnRarityParameterStatus Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnRarityParameterStatus>(assetPath)
            );
        }
#endif
        public static OwnRarityParameterStatus New(
            Namespace @namespace,
            string parameterName,
            string propertyId
        )
        {
            var instance = CreateInstance<OwnRarityParameterStatus>();
            instance.name = "Runtime";
            instance.Namespace = @namespace;
            instance.parameterName = parameterName;
            instance.propertyId = propertyId;
            return instance;
        }
        public OwnRarityParameterStatus Clone()
        {
            var instance = CreateInstance<OwnRarityParameterStatus>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.parameterName = parameterName;
            instance.propertyId = propertyId;
            return instance;
        }
    }
}