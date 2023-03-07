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
    [CreateAssetMenu(fileName = "LayerModel", menuName = "Game Server Services/Gs2MegaField/LayerModel")]
    public class LayerModel : UnityEngine.ScriptableObject
    {
        public AreaModel AreaModel;
        public string layerModelName;

        public string NamespaceName => this.AreaModel?.NamespaceName;
        public string AreaModelName => this.AreaModel?.AreaModelName;
        public string LayerModelName => this.layerModelName;

#if UNITY_INCLUDE_TESTS
        public static LayerModel Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<LayerModel>(assetPath)
            );
        }
#endif

        public static LayerModel New(
            AreaModel AreaModel,
            string layerModelName
        )
        {
            var instance = CreateInstance<LayerModel>();
            instance.name = "Runtime";
            instance.AreaModel = AreaModel;
            instance.layerModelName = layerModelName;
            return instance;
        }

        public LayerModel Clone()
        {
            var instance = CreateInstance<LayerModel>();
            instance.name = "Runtime";
            instance.AreaModel = AreaModel;
            instance.layerModelName = layerModelName;
            return instance;
        }
    }
}