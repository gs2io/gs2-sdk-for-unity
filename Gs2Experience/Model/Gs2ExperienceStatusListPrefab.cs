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
using Gs2.Unity.Gs2Experience.Model;
using Gs2.Unity.Gs2Experience.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Experience
{
    [RequireComponent(typeof(Gs2ExperienceStatusListFetcher))]
    [AddComponentMenu("GS2 UIKit/Experience/Gs2ExperienceStatusList")]
    public partial class Gs2ExperienceStatusList : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2ExperienceStatusPrefab> _cache = new Dictionary<string, Gs2ExperienceStatusPrefab>();

        public void Update()
        {
            if (_statusListFetcher.Statuses != null)
            {
                var activeNames = _statusListFetcher.Statuses.Select(v => v.PropertyId);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteExperience.Invoke(instantiateName);
                    }
                }

                foreach (var status in _statusListFetcher.Statuses)
                {
                    if (!_cache.ContainsKey(status.PropertyId))
                    {
                        var item = Instantiate(statusPrefab, populateNode);
                        item.name = status.PropertyId;
                        item.Set(Experience, status.PropertyId);
                        item.gameObject.SetActive(true);
                        _cache[status.PropertyId] = item;
                        onCreateExperience.Invoke(_statusListFetcher.Model, status);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2ExperienceStatusList
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2ExperienceStatusListFetcher _statusListFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _statusListFetcher = GetComponent<Gs2ExperienceStatusListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ExperienceStatusList
    {
        public Experience Experience
        {
            get => _statusListFetcher.experience;
            set => _statusListFetcher.experience = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ExperienceStatusList
    {
        public Gs2ExperienceStatusPrefab statusPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExperienceStatusList
    {
        [Serializable]
        private class CreateExperienceEvent : UnityEvent<EzExperienceModel, EzStatus>
        {
            
        }
        
        [SerializeField]
        private CreateExperienceEvent onCreateExperience = new CreateExperienceEvent();
        
        public event UnityAction<EzExperienceModel, EzStatus> OnCreateExperience
        {
            add => onCreateExperience.AddListener(value);
            remove => onCreateExperience.RemoveListener(value);
        }

        [Serializable]
        private class DeleteExperienceEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private DeleteExperienceEvent onDeleteExperience = new DeleteExperienceEvent();
        
        public event UnityAction<string> OnDeleteExperience
        {
            add => onDeleteExperience.AddListener(value);
            remove => onDeleteExperience.RemoveListener(value);
        }
    }
}