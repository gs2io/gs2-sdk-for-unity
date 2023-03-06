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
    [CreateAssetMenu(fileName = "JobResult", menuName = "Game Server Services/Gs2JobQueue/JobResult")]
    public class JobResult : UnityEngine.ScriptableObject
    {
        public Job Job;
        public int tryNumber;

        public string NamespaceName => this.Job?.NamespaceName;
        public string JobName => this.Job?.JobName;
        public int TryNumber => this.tryNumber;

#if UNITY_INCLUDE_TESTS
        public static JobResult Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<JobResult>(assetPath)
            );
        }
#endif

        public static JobResult New(
            Job Job,
            int tryNumber
        )
        {
            var instance = CreateInstance<JobResult>();
            instance.name = "Runtime";
            instance.Job = Job;
            instance.tryNumber = tryNumber;
            return instance;
        }

        public JobResult Clone()
        {
            var instance = CreateInstance<JobResult>();
            instance.name = "Runtime";
            instance.Job = Job;
            instance.tryNumber = tryNumber;
            return instance;
        }
    }
}