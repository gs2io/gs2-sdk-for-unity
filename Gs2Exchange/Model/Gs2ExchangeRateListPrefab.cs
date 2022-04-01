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
using Gs2.Unity.Gs2Exchange.Model;
using Gs2.Unity.Gs2Exchange.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Exchange
{
    [RequireComponent(typeof(Gs2ExchangeRateListFetcher))]
    [AddComponentMenu("GS2 UIKit/Exchange/Gs2ExchangeRateListPrefab")]
    public partial class Gs2ExchangeRateListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2ExchangeRatePrefab> _cache = new Dictionary<string, Gs2ExchangeRatePrefab>();

        public void Update()
        {
            if (_questGroupListFetcher.RateModels != null)
            {
                var activeNames = _questGroupListFetcher.RateModels.Select(v => v.Name);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteExchange.Invoke(instantiateName);
                    }
                }

                foreach (var experience in _questGroupListFetcher.RateModels)
                {
                    if (!_cache.ContainsKey(experience.Name))
                    {
                        var item = Instantiate(questGroupPrefab, populateNode);
                        item.name = experience.Name;
                        item.Set(Namespace, experience.Name);
                        item.gameObject.SetActive(true);
                        _cache[experience.Name] = item;
                        onCreateExchange.Invoke(experience);
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
    
    public partial class Gs2ExchangeRateListPrefab
    {
        private Gs2ExchangeRateListFetcher _questGroupListFetcher;

        public void Awake()
        {
            _questGroupListFetcher = GetComponent<Gs2ExchangeRateListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ExchangeRateListPrefab
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
    
    public partial class Gs2ExchangeRateListPrefab
    {
        public Gs2ExchangeRatePrefab questGroupPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeRateListPrefab
    {
        [Serializable]
        private class CreateExchangeEvent : UnityEvent<EzRateModel>
        {
            
        }
        
        [SerializeField]
        private CreateExchangeEvent onCreateExchange = new CreateExchangeEvent();
        
        public event UnityAction<EzRateModel> OnCreateExchange
        {
            add => onCreateExchange.AddListener(value);
            remove => onCreateExchange.RemoveListener(value);
        }

        [Serializable]
        private class DeleteExchangeEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private DeleteExchangeEvent onDeleteExchange = new DeleteExchangeEvent();
        
        public event UnityAction<string> OnDeleteExchange
        {
            add => onDeleteExchange.AddListener(value);
            remove => onDeleteExchange.RemoveListener(value);
        }
    }
}