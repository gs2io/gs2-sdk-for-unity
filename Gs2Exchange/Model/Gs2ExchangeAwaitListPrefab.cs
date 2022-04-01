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
    [RequireComponent(typeof(Gs2ExchangeAwaitListFetcher))]
    [AddComponentMenu("GS2 UIKit/Exchange/Gs2ExchangeAwaitListPrefab")]
    public partial class Gs2ExchangeAwaitListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2ExchangeAwaitPrefab> _cache = new Dictionary<string, Gs2ExchangeAwaitPrefab>();

        public void Update()
        {
            if (_awaitListFetcher.Awaites != null)
            {
                var activeNames = _awaitListFetcher.Awaites.Select(v => v.Name);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteExchange.Invoke(instantiateName);
                    }
                }

                foreach (var status in _awaitListFetcher.Awaites)
                {
                    if (!_cache.ContainsKey(status.Name))
                    {
                        var item = Instantiate(awaitPrefab, populateNode);
                        item.name = status.Name;
                        item.Set(Rate, status.Name);
                        item.gameObject.SetActive(true);
                        _cache[status.Name] = item;
                        onCreateExchange.Invoke(_awaitListFetcher.Model, status);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2ExchangeAwaitListPrefab
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2ExchangeAwaitListFetcher _awaitListFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _awaitListFetcher = GetComponent<Gs2ExchangeAwaitListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ExchangeAwaitListPrefab
    {
        public Rate Rate
        {
            get => _awaitListFetcher.rate;
            set => _awaitListFetcher.rate = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ExchangeAwaitListPrefab
    {
        public Gs2ExchangeAwaitPrefab awaitPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeAwaitListPrefab
    {
        [Serializable]
        private class CreateExchangeEvent : UnityEvent<EzRateModel, EzAwait>
        {
            
        }
        
        [SerializeField]
        private CreateExchangeEvent onCreateExchange = new CreateExchangeEvent();
        
        public event UnityAction<EzRateModel, EzAwait> OnCreateExchange
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