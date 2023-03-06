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

namespace Gs2.Unity.Gs2SerialKey.ScriptableObject
{
    [CreateAssetMenu(fileName = "IssueJob", menuName = "Game Server Services/Gs2SerialKey/IssueJob")]
    public class IssueJob : UnityEngine.ScriptableObject
    {
        public CampaignModel CampaignModel;
        public string issueJobName;

        public string NamespaceName => this.CampaignModel?.NamespaceName;
        public string CampaignModelName => this.CampaignModel?.CampaignModelName;
        public string IssueJobName => this.issueJobName;

#if UNITY_INCLUDE_TESTS
        public static IssueJob Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<IssueJob>(assetPath)
            );
        }
#endif

        public static IssueJob New(
            CampaignModel CampaignModel,
            string issueJobName
        )
        {
            var instance = CreateInstance<IssueJob>();
            instance.name = "Runtime";
            instance.CampaignModel = CampaignModel;
            instance.issueJobName = issueJobName;
            return instance;
        }

        public IssueJob Clone()
        {
            var instance = CreateInstance<IssueJob>();
            instance.name = "Runtime";
            instance.CampaignModel = CampaignModel;
            instance.issueJobName = issueJobName;
            return instance;
        }
    }
}