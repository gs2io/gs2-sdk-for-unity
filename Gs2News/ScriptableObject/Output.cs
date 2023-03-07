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

namespace Gs2.Unity.Gs2News.ScriptableObject
{
    [CreateAssetMenu(fileName = "Output", menuName = "Game Server Services/Gs2News/Output")]
    public class Output : UnityEngine.ScriptableObject
    {
        public Progress Progress;
        public string outputName;

        public string NamespaceName => this.Progress?.NamespaceName;
        public string UploadToken => this.Progress?.UploadToken;
        public string OutputName => this.outputName;

#if UNITY_INCLUDE_TESTS
        public static Output Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Output>(assetPath)
            );
        }
#endif

        public static Output New(
            Progress Progress,
            string outputName
        )
        {
            var instance = CreateInstance<Output>();
            instance.name = "Runtime";
            instance.Progress = Progress;
            instance.outputName = outputName;
            return instance;
        }

        public Output Clone()
        {
            var instance = CreateInstance<Output>();
            instance.name = "Runtime";
            instance.Progress = Progress;
            instance.outputName = outputName;
            return instance;
        }
    }
}