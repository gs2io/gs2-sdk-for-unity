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
    [CreateAssetMenu(fileName = "FormModel", menuName = "Game Server Services/Gs2Formation/FormModel")]
    public class FormModel : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string moldModelName;
        public string formModelName;

        public string NamespaceName => this.Namespace?.NamespaceName;
        public string MoldModelName => this.moldModelName;
        public string FormModelName => this.formModelName;

#if UNITY_INCLUDE_TESTS
        public static FormModel Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<FormModel>(assetPath)
            );
        }
#endif

        public static FormModel NewByFormModelName(
            Namespace Namespace,
            string formModelName
        )
        {
            var instance = CreateInstance<FormModel>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.formModelName = formModelName;
            return instance;
        }

        public static FormModel NewByMoldModelName(
            Namespace Namespace,
            string moldModelName
        )
        {
            var instance = CreateInstance<FormModel>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.moldModelName = moldModelName;
            return instance;
        }

        public FormModel Clone()
        {
            var instance = CreateInstance<FormModel>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.moldModelName = moldModelName;
            instance.formModelName = formModelName;
            return instance;
        }
    }
}