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

namespace Gs2.Unity.Gs2Formation.ScriptableObject
{
    [CreateAssetMenu(fileName = "OwnPropertyForm", menuName = "Game Server Services/Gs2Formation/OwnPropertyForm")]
    public class OwnPropertyForm : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string formModelName;
        public string propertyId;

        public string NamespaceName => this.Namespace.NamespaceName;
        public string FormModelName => this.formModelName;
        public string PropertyId => this.propertyId;

#if UNITY_INCLUDE_TESTS
        public static OwnPropertyForm Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnPropertyForm>(assetPath)
            );
        }
#endif

        public static OwnPropertyForm New(
            Namespace Namespace,
            string formModelName,
            string propertyId
        )
        {
            var instance = CreateInstance<OwnPropertyForm>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.formModelName = formModelName;
            instance.propertyId = propertyId;
            return instance;
        }

        public OwnPropertyForm Clone()
        {
            var instance = CreateInstance<OwnPropertyForm>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.formModelName = formModelName;
            instance.propertyId = propertyId;
            return instance;
        }
    }
}