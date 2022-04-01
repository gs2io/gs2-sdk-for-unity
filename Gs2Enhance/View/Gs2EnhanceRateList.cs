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
using Gs2.Unity.Gs2Enhance.Model;
using Gs2.Unity.Gs2Enhance.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Enhance.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Enhance
{
    [RequireComponent(typeof(Gs2EnhanceRateListFetcher))]
    [AddComponentMenu("GS2 UIKit/Enhance/Gs2EnhanceRateList")]
    public partial class Gs2EnhanceRateList : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2EnhanceRate> _cache = new Dictionary<string, Gs2EnhanceRate>();

        public void Update()
        {
            if (_questGroupListFetcher.RateModels != null)
            {
                void OnSelect(EzRateModel model)
                {
                    onSelect.Invoke(model);
                }
                
                var activeNames = _questGroupListFetcher.RateModels.Select(v => v.Name);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        _cache[instantiateName].OnSelect -= OnSelect;
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteEnhance.Invoke(instantiateName);
                    }
                }

                foreach (var experience in _questGroupListFetcher.RateModels)
                {
                    if (!_cache.ContainsKey(experience.Name))
                    {
                        var item = Instantiate(questGroupPrefab, populateNode);
                        item.name = experience.Name;
                        item.Set(Namespace, experience.Name);
                        item.OnSelect += OnSelect;
                        _cache[experience.Name] = item;
                        onCreateEnhance.Invoke(experience);
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
    
    public partial class Gs2EnhanceRateList
    {
        private Gs2EnhanceRateListFetcher _questGroupListFetcher;

        public void Awake()
        {
            _questGroupListFetcher = GetComponent<Gs2EnhanceRateListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2EnhanceRateList
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
    
    public partial class Gs2EnhanceRateList
    {
        public Gs2EnhanceRate questGroupPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnhanceRateList
    {
        [Serializable]
        private class CreateEnhanceEvent : UnityEvent<EzRateModel>
        {
            
        }
        
        [SerializeField]
        private CreateEnhanceEvent onCreateEnhance = new CreateEnhanceEvent();
        
        public event UnityAction<EzRateModel> OnCreateEnhance
        {
            add => onCreateEnhance.AddListener(value);
            remove => onCreateEnhance.RemoveListener(value);
        }

        [Serializable]
        private class DeleteEnhanceEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private DeleteEnhanceEvent onDeleteEnhance = new DeleteEnhanceEvent();
        
        public event UnityAction<string> OnDeleteEnhance
        {
            add => onDeleteEnhance.AddListener(value);
            remove => onDeleteEnhance.RemoveListener(value);
        }
        
        [Serializable]
        private class SelectEvent : UnityEvent<EzRateModel>
        {
            
        }
        
        [SerializeField]
        private SelectEvent onSelect = new SelectEvent();
        
        public event UnityAction<EzRateModel> OnSelect
        {
            add => onSelect.AddListener(value);
            remove => onSelect.RemoveListener(value);
        }
    }
}