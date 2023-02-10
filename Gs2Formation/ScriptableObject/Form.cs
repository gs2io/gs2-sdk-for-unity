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
    [CreateAssetMenu(fileName = "Form", menuName = "Game Server Services/Gs2Formation/Form")]
    public class Form : UnityEngine.ScriptableObject
    {
        public MoldModel MoldModel;
        public int index;

        public string NamespaceName => this.MoldModel.NamespaceName;
        public string MoldName => this.MoldModel.MoldName;
        public int Index => this.index;

#if UNITY_INCLUDE_TESTS
        public static Form Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Form>(assetPath)
            );
        }
#endif

        public static Form New(
            MoldModel MoldModel,
            int index
        )
        {
            var instance = CreateInstance<Form>();
            instance.name = "Runtime";
            instance.MoldModel = MoldModel;
            instance.index = index;
            return instance;
        }

        public Form Clone()
        {
            var instance = CreateInstance<Form>();
            instance.name = "Runtime";
            instance.MoldModel = MoldModel;
            instance.index = index;
            return instance;
        }
    }
}