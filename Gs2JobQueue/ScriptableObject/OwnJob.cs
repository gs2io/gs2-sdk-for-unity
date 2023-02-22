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

namespace Gs2.Unity.Gs2JobQueue.ScriptableObject
{
    [CreateAssetMenu(fileName = "OwnJob", menuName = "Game Server Services/Gs2JobQueue/OwnJob")]
    public class OwnJob : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string jobName;

        public string NamespaceName => this.Namespace.NamespaceName;
        public string JobName => this.jobName;

#if UNITY_INCLUDE_TESTS
        public static OwnJob Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnJob>(assetPath)
            );
        }
#endif

        public static OwnJob New(
            Namespace Namespace,
            string jobName
        )
        {
            var instance = CreateInstance<OwnJob>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.jobName = jobName;
            return instance;
        }

        public OwnJob Clone()
        {
            var instance = CreateInstance<OwnJob>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.jobName = jobName;
            return instance;
        }
    }
}