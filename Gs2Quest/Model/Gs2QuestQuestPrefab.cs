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
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable CheckNamespace

using System;
using System.Collections;
using Gs2.Unity.Gs2Quest.Model;
using Gs2.Unity.Gs2Quest.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Quest.Model
{
    [RequireComponent(typeof(Gs2QuestQuestFetcher))]
    [RequireComponent(typeof(Gs2QuestCompletedQuestListFetcher))]
    [AddComponentMenu("GS2 UIKit/Quest/Gs2QuestQuestPrefab")]
    public class Gs2QuestQuestPrefab : MonoBehaviour
    {
        private Quest _quest;
        private ErrorEvent _onError;

        private void OnError(
            Exception exception, 
            Func<IEnumerator> retryFunc
        )
        {
            if (_onError != null)
            {
                _onError.Invoke(exception, retryFunc);
            }
        }

        public void Awake()
        {
            var displayItemFetcher = GetComponent<Gs2QuestQuestFetcher>();
            displayItemFetcher.OnError += OnError;
            var completedQuestListFetcher = GetComponent<Gs2QuestCompletedQuestListFetcher>();
            completedQuestListFetcher.OnError += OnError;
        }

        public void OnDestroy()
        {
            var displayItemFetcher = GetComponent<Gs2QuestQuestFetcher>();
            displayItemFetcher.OnError -= OnError;
            var completedQuestListFetcher = GetComponent<Gs2QuestCompletedQuestListFetcher>();
            completedQuestListFetcher.OnError -= OnError;

            if (_quest != null)
            {
                Destroy(_quest);
            }
        }

        public void Set(
            QuestGroup questGroup,
            EzQuestModel quest,
            ErrorEvent onError
        )
        {
            _quest = ScriptableObject.CreateInstance<Quest>();
            _quest.questGroup = questGroup;
            _quest.questName = quest.Name;
            _onError = onError;

            var displayItemFetcher = GetComponent<Gs2QuestQuestFetcher>();
            displayItemFetcher.quest = _quest;
            var completedQuestListFetcher = GetComponent<Gs2QuestCompletedQuestListFetcher>();
            completedQuestListFetcher.questGroup = questGroup;
        }
    }
}