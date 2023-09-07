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
    [CreateAssetMenu(fileName = "SlotModel", menuName = "Game Server Services/Gs2Formation/SlotModel")]
    public class SlotModel : UnityEngine.ScriptableObject
    {
        public FormModel FormModel;
        public string slotModelName;

        public string NamespaceName => this.FormModel?.NamespaceName;
        public string MoldModelName => this.FormModel?.MoldModelName;
        public string FormModelName => this.FormModel?.FormModelName;
        public string SlotModelName => this.slotModelName;

#if UNITY_INCLUDE_TESTS
        public static SlotModel Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<SlotModel>(assetPath)
            );
        }
#endif

        public static SlotModel New(
            FormModel FormModel,
            string slotModelName
        )
        {
            var instance = CreateInstance<SlotModel>();
            instance.name = "Runtime";
            instance.FormModel = FormModel;
            instance.slotModelName = slotModelName;
            return instance;
        }

        public SlotModel Clone()
        {
            var instance = CreateInstance<SlotModel>();
            instance.name = "Runtime";
            instance.FormModel = FormModel;
            instance.slotModelName = slotModelName;
            return instance;
        }
    }
}