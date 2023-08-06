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

namespace Gs2.Unity.Gs2MegaField.ScriptableObject
{
    [CreateAssetMenu(fileName = "Layer", menuName = "Game Server Services/Gs2MegaField/Layer")]
    public class Layer : LayerModel
    {
        public Namespace Namespace;
        public string areaModelName;

        public string AreaModelName => this.areaModelName;

#if UNITY_INCLUDE_TESTS
        public static Layer Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Layer>(assetPath)
            );
        }
#endif

        public static Layer New(
            Namespace Namespace,
            string areaModelName,
            string layerModelName
        )
        {
            var instance = CreateInstance<Layer>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.areaModelName = areaModelName;
            instance.layerModelName = layerModelName;
            return instance;
        }

        public Layer Clone()
        {
            var instance = CreateInstance<Layer>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.areaModelName = areaModelName;
            instance.layerModelName = layerModelName;
            return instance;
        }
    }
}