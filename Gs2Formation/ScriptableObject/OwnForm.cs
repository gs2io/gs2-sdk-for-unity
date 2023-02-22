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
    [CreateAssetMenu(fileName = "OwnForm", menuName = "Game Server Services/Gs2Formation/OwnForm")]
    public class OwnForm : UnityEngine.ScriptableObject
    {
        public OwnMold Mold;
        public int index;

        public string NamespaceName => this.Mold.NamespaceName;
        public string MoldName => this.Mold.MoldName;
        public int Index => this.index;

#if UNITY_INCLUDE_TESTS
        public static OwnForm Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnForm>(assetPath)
            );
        }
#endif

        public static OwnForm New(
            OwnMold Mold,
            int index
        )
        {
            var instance = CreateInstance<OwnForm>();
            instance.name = "Runtime";
            instance.Mold = Mold;
            instance.index = index;
            return instance;
        }

        public OwnForm Clone()
        {
            var instance = CreateInstance<OwnForm>();
            instance.name = "Runtime";
            instance.Mold = Mold;
            instance.index = index;
            return instance;
        }
    }
}