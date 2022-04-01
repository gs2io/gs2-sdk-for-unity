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
using System.Collections.Generic;
using System.Linq;
using Gs2.Unity.Gs2Quest.Model;
using Gs2.Unity.Gs2Quest.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using Gs2.Unity.UiKit.Gs2Quest.Model;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Quest
{
    [RequireComponent(typeof(Gs2QuestQuestListFetcher))]
    [AddComponentMenu("GS2 UIKit/Quest/Gs2QuestQuestListPrefab")]
    public partial class Gs2QuestQuestListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2QuestQuestPrefab> _cache = new Dictionary<string, Gs2QuestQuestPrefab>();

        private QuestGroup _questGroup;
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

        public void Update()
        {
            if (_questListFetcher.Quests != null)
            {
                var activeNames = _questListFetcher.Quests.Select(v => v.Name);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteQuest.Invoke(instantiateName);
                    }
                }

                foreach (var quest in _questListFetcher.Quests)
                {
                    if (!_cache.ContainsKey(quest.Name))
                    {
                        var item = Instantiate(questPrefab, populateNode);
                        item.name = quest.Name;
                        item.Set(_questListFetcher.questGroup, quest, _questListFetcher.onError);
                        item.gameObject.SetActive(true);
                        _cache[quest.Name] = item;
                        onCreateQuest.Invoke(quest);
                    }
                }
            }
        }

        public void Set(
            Namespace @namespace,
            EzQuestGroupModel questGroup,
            ErrorEvent onError
        )
        {
            _questGroup = ScriptableObject.CreateInstance<QuestGroup>();
            _questGroup.Namespace = @namespace;
            _questGroup.questGroupName = questGroup.Name;
            _onError = onError;

            var questListFetcher = GetComponentInParent<Gs2QuestQuestListFetcher>() ?? GetComponent<Gs2QuestQuestListFetcher>();
            questListFetcher.questGroup = _questGroup;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2QuestQuestListPrefab
    {
        private Gs2QuestQuestListFetcher _questListFetcher;

        public void Awake()
        {
            _questListFetcher = GetComponentInParent<Gs2QuestQuestListFetcher>() ?? GetComponent<Gs2QuestQuestListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2QuestQuestListPrefab
    {
        public QuestGroup QuestGroup
        {
            get => _questListFetcher != null && _questListFetcher.Fetched ? _questListFetcher.questGroup : null;
            set => _questListFetcher.questGroup = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2QuestQuestListPrefab
    {
        public Gs2QuestQuestPrefab questPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2QuestQuestListPrefab
    {
        [Serializable]
        private class CreateQuestEvent : UnityEvent<EzQuestModel>
        {
            
        }
        
        [SerializeField]
        private CreateQuestEvent onCreateQuest = new CreateQuestEvent();
        
        public event UnityAction<EzQuestModel> OnCreateQuest
        {
            add => onCreateQuest.AddListener(value);
            remove => onCreateQuest.RemoveListener(value);
        }

        [Serializable]
        private class DeleteQuestEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private DeleteQuestEvent onDeleteQuest = new DeleteQuestEvent();
        
        public event UnityAction<string> OnDeleteQuest
        {
            add => onDeleteQuest.AddListener(value);
            remove => onDeleteQuest.RemoveListener(value);
        }
    }
}