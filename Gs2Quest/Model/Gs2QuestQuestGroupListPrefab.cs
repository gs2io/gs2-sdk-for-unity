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
    [RequireComponent(typeof(Gs2QuestQuestGroupListFetcher))]
    [AddComponentMenu("GS2 UIKit/Quest/Gs2QuestQuestGroupListPrefab")]
    public partial class Gs2QuestQuestGroupListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2QuestQuestGroupPrefab> _cache = new Dictionary<string, Gs2QuestQuestGroupPrefab>();

        public void Update()
        {
            if (_questGroupListFetcher.QuestGroups != null)
            {
                var activeNames = _questGroupListFetcher.QuestGroups.Select(v => v.Name);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteQuest.Invoke(instantiateName);
                    }
                }

                foreach (var questGroup in _questGroupListFetcher.QuestGroups)
                {
                    if (!_cache.ContainsKey(questGroup.Name))
                    {
                        var item = Instantiate(questGroupPrefab, populateNode);
                        item.name = questGroup.Name;
                        item.Set(_questGroupListFetcher.Namespace, questGroup, _questGroupListFetcher.onError);
                        item.gameObject.SetActive(true);
                        _cache[questGroup.Name] = item;
                        onCreateQuestGroup.Invoke(questGroup);
                    }
                }
            }
        }

        public void OnDestroy()
        {
            foreach (var component in _cache.Values)
            {
                Destroy(component);
            }
            _cache.Clear();
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2QuestQuestGroupListPrefab
    {
        private Gs2QuestQuestGroupListFetcher _questGroupListFetcher;

        public void Awake()
        {
            _questGroupListFetcher = GetComponentInParent<Gs2QuestQuestGroupListFetcher>() ?? GetComponent<Gs2QuestQuestGroupListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2QuestQuestGroupListPrefab
    {
        public Namespace Namespace
        {
            get => _questGroupListFetcher != null && _questGroupListFetcher.Fetched ? _questGroupListFetcher.Namespace : null;
            set => _questGroupListFetcher.Namespace = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2QuestQuestGroupListPrefab
    {
        public Gs2QuestQuestGroupPrefab questGroupPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2QuestQuestGroupListPrefab
    {
        [Serializable]
        private class CreateQuestGroupEvent : UnityEvent<EzQuestGroupModel>
        {
            
        }
        
        [SerializeField]
        private CreateQuestGroupEvent onCreateQuestGroup = new CreateQuestGroupEvent();
        
        public event UnityAction<EzQuestGroupModel> OnCreateQuestGroup
        {
            add => onCreateQuestGroup.AddListener(value);
            remove => onCreateQuestGroup.RemoveListener(value);
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