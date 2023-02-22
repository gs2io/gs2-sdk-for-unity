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

namespace Gs2.Unity.Gs2MegaField.ScriptableObject
{
    [CreateAssetMenu(fileName = "OwnSpatial", menuName = "Game Server Services/Gs2MegaField/OwnSpatial")]
    public class OwnSpatial : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string areaModelName;
        public string layerModelName;

        public string NamespaceName => this.Namespace.NamespaceName;
        public string AreaModelName => this.areaModelName;
        public string LayerModelName => this.layerModelName;

#if UNITY_INCLUDE_TESTS
        public static OwnSpatial Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnSpatial>(assetPath)
            );
        }
#endif

        public static OwnSpatial New(
            Namespace Namespace,
            string areaModelName,
            string layerModelName
        )
        {
            var instance = CreateInstance<OwnSpatial>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.areaModelName = areaModelName;
            instance.layerModelName = layerModelName;
            return instance;
        }

        public OwnSpatial Clone()
        {
            var instance = CreateInstance<OwnSpatial>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.areaModelName = areaModelName;
            instance.layerModelName = layerModelName;
            return instance;
        }
    }
}