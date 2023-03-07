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
    [CreateAssetMenu(fileName = "Spatial", menuName = "Game Server Services/Gs2MegaField/Spatial")]
    public class Spatial : UnityEngine.ScriptableObject
    {
        public User User;
        public string areaModelName;
        public string layerModelName;

        public string NamespaceName => this.User?.NamespaceName;
        public string UserId => this.User?.UserId;
        public string AreaModelName => this.areaModelName;
        public string LayerModelName => this.layerModelName;

#if UNITY_INCLUDE_TESTS
        public static Spatial Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Spatial>(assetPath)
            );
        }
#endif

        public static Spatial New(
            User User,
            string areaModelName,
            string layerModelName
        )
        {
            var instance = CreateInstance<Spatial>();
            instance.name = "Runtime";
            instance.User = User;
            instance.areaModelName = areaModelName;
            instance.layerModelName = layerModelName;
            return instance;
        }

        public Spatial Clone()
        {
            var instance = CreateInstance<Spatial>();
            instance.name = "Runtime";
            instance.User = User;
            instance.areaModelName = areaModelName;
            instance.layerModelName = layerModelName;
            return instance;
        }
    }
}