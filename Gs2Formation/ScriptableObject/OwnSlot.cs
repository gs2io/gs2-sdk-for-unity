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
    [CreateAssetMenu(fileName = "OwnSlot", menuName = "Game Server Services/Gs2Formation/OwnSlot")]
    public class OwnSlot : UnityEngine.ScriptableObject
    {
        public OwnForm Form;
        public string slotName;

        public string NamespaceName => this.Form.NamespaceName;
        public string MoldName => this.Form.MoldName;
        public int Index => this.Form.Index;
        public string SlotName => this.slotName;

#if UNITY_INCLUDE_TESTS
        public static OwnSlot Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnSlot>(assetPath)
            );
        }
#endif

        public static OwnSlot New(
            OwnForm Form,
            string slotName
        )
        {
            var instance = CreateInstance<OwnSlot>();
            instance.name = "Runtime";
            instance.Form = Form;
            instance.slotName = slotName;
            return instance;
        }

        public OwnSlot Clone()
        {
            var instance = CreateInstance<OwnSlot>();
            instance.name = "Runtime";
            instance.Form = Form;
            instance.slotName = slotName;
            return instance;
        }
    }
}