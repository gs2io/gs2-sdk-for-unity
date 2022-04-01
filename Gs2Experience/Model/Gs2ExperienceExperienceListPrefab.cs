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
    [RequireComponent(typeof(Gs2ExperienceExperienceListFetcher))]
    [AddComponentMenu("GS2 UIKit/Experience/Gs2ExperienceExperienceListPrefab")]
    public partial class Gs2ExperienceExperienceListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2ExperienceExperiencePrefab> _cache = new Dictionary<string, Gs2ExperienceExperiencePrefab>();

        public void Update()
        {
            if (_questGroupListFetcher.ExperienceModels != null)
            {
                var activeNames = _questGroupListFetcher.ExperienceModels.Select(v => v.Name);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteExperience.Invoke(instantiateName);
                    }
                }

                foreach (var experience in _questGroupListFetcher.ExperienceModels)
                {
                    if (!_cache.ContainsKey(experience.Name))
                    {
                        var item = Instantiate(experiencePrefab, populateNode);
                        item.name = experience.Name;
                        item.Set(Namespace, experience.Name);
                        item.gameObject.SetActive(true);
                        _cache[experience.Name] = item;
                        onCreateExperience.Invoke(experience);
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
    
    public partial class Gs2ExperienceExperienceListPrefab
    {
        private Gs2ExperienceExperienceListFetcher _questGroupListFetcher;

        public void Awake()
        {
            _questGroupListFetcher = GetComponent<Gs2ExperienceExperienceListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ExperienceExperienceListPrefab
    {
        public Namespace Namespace
        {
            get => _questGroupListFetcher.Namespace;
            set => _questGroupListFetcher.Namespace = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ExperienceExperienceListPrefab
    {
        public Gs2ExperienceExperiencePrefab experiencePrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExperienceExperienceListPrefab
    {
        [Serializable]
        private class CreateExperienceEvent : UnityEvent<EzExperienceModel>
        {
            
        }
        
        [SerializeField]
        private CreateExperienceEvent onCreateExperience = new CreateExperienceEvent();
        
        public event UnityAction<EzExperienceModel> OnCreateExperience
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