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

namespace Gs2.Unity.Gs2Quest.ScriptableObject
{
    [CreateAssetMenu(fileName = "CompletedQuestList", menuName = "Game Server Services/Gs2Quest/CompletedQuestList")]
    public class CompletedQuestList : UnityEngine.ScriptableObject
    {
        public User User;
        public string questGroupName;

        public string NamespaceName => this.User.NamespaceName;
        public string UserId => this.User.UserId;
        public string QuestGroupName => this.questGroupName;

#if UNITY_INCLUDE_TESTS
        public static CompletedQuestList Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<CompletedQuestList>(assetPath)
            );
        }
#endif

        public static CompletedQuestList New(
            User User,
            string questGroupName
        )
        {
            var instance = CreateInstance<CompletedQuestList>();
            instance.name = "Runtime";
            instance.User = User;
            instance.questGroupName = questGroupName;
            return instance;
        }

        public CompletedQuestList Clone()
        {
            var instance = CreateInstance<CompletedQuestList>();
            instance.name = "Runtime";
            instance.User = User;
            instance.questGroupName = questGroupName;
            return instance;
        }
    }
}