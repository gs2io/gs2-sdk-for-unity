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
    [CreateAssetMenu(fileName = "Job", menuName = "Game Server Services/Gs2JobQueue/Job")]
    public class Job : UnityEngine.ScriptableObject
    {
        public User User;
        public string jobName;

        public string NamespaceName => this.User.NamespaceName;
        public string UserId => this.User.UserId;
        public string JobName => this.jobName;

#if UNITY_INCLUDE_TESTS
        public static Job Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Job>(assetPath)
            );
        }
#endif

        public static Job New(
            User User,
            string jobName
        )
        {
            var instance = CreateInstance<Job>();
            instance.name = "Runtime";
            instance.User = User;
            instance.jobName = jobName;
            return instance;
        }

        public Job Clone()
        {
            var instance = CreateInstance<Job>();
            instance.name = "Runtime";
            instance.User = User;
            instance.jobName = jobName;
            return instance;
        }
    }
}