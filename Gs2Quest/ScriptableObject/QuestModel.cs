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
    [CreateAssetMenu(fileName = "QuestModel", menuName = "Game Server Services/Gs2Quest/QuestModel")]
    public class QuestModel : UnityEngine.ScriptableObject
    {
        public QuestGroupModel QuestGroupModel;
        public string questName;

        public string NamespaceName => this.QuestGroupModel?.NamespaceName;
        public string QuestGroupName => this.QuestGroupModel?.QuestGroupName;
        public string QuestName => this.questName;

#if UNITY_INCLUDE_TESTS
        public static QuestModel Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<QuestModel>(assetPath)
            );
        }
#endif

        public static QuestModel New(
            QuestGroupModel QuestGroupModel,
            string questName
        )
        {
            var instance = CreateInstance<QuestModel>();
            instance.name = "Runtime";
            instance.QuestGroupModel = QuestGroupModel;
            instance.questName = questName;
            return instance;
        }

        public QuestModel Clone()
        {
            var instance = CreateInstance<QuestModel>();
            instance.name = "Runtime";
            instance.QuestGroupModel = QuestGroupModel;
            instance.questName = questName;
            return instance;
        }
    }
}