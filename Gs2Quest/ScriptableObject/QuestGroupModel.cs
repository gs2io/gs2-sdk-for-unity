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
    [CreateAssetMenu(fileName = "QuestGroupModel", menuName = "Game Server Services/Gs2Quest/QuestGroupModel")]
    public class QuestGroupModel : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string questGroupName;

        public string NamespaceName => this.Namespace?.NamespaceName;
        public string QuestGroupName => this.questGroupName;

#if UNITY_INCLUDE_TESTS
        public static QuestGroupModel Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<QuestGroupModel>(assetPath)
            );
        }
#endif

        public static QuestGroupModel New(
            Namespace Namespace,
            string questGroupName
        )
        {
            var instance = CreateInstance<QuestGroupModel>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.questGroupName = questGroupName;
            return instance;
        }

        public QuestGroupModel Clone()
        {
            var instance = CreateInstance<QuestGroupModel>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.questGroupName = questGroupName;
            return instance;
        }
    }
}