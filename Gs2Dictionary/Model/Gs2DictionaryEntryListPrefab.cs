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
using Gs2.Unity.Gs2Dictionary.Model;
using Gs2.Unity.Gs2Dictionary.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Dictionary.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Dictionary
{
    [RequireComponent(typeof(Gs2DictionaryEntryListFetcher))]
    [AddComponentMenu("GS2 UIKit/Datastore/Gs2DictionaryEntryListPrefab")]
    public partial class Gs2DictionaryEntryListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2DictionaryEntryPrefab> _cache = new Dictionary<string, Gs2DictionaryEntryPrefab>();

        public void Update()
        {
            if (_entryListFetcher.Fetched)
            {
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!_entryListFetcher.Models.Select(v => v.Name).Contains(instantiateName))
                    {
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteEntry.Invoke(instantiateName);
                    }
                }

                foreach (var entryModel in _entryListFetcher.Models)
                {
                    if (!_cache.ContainsKey(entryModel.Name))
                    {
                        var item = Instantiate(entryPrefab, populateNode);
                        item.name = entryModel.Name;
                        item.Awake();
                        item.Set(Namespace, entryModel.Name);
                        item.gameObject.SetActive(true);
                        _cache[entryModel.Name] = item;
                        onCreateEntry.Invoke(entryModel, null);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2DictionaryEntryListPrefab
    {
        private Gs2DictionaryEntryListFetcher _entryListFetcher;

        public void Awake()
        {
            _entryListFetcher = GetComponent<Gs2DictionaryEntryListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2DictionaryEntryListPrefab
    {
        public Namespace Namespace
        {
            get => _entryListFetcher.Namespace;
            set => _entryListFetcher.Namespace = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2DictionaryEntryListPrefab
    {
        public Gs2DictionaryEntryPrefab entryPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DictionaryEntryListPrefab
    {
        [Serializable]
        private class CreateEntryEvent : UnityEvent<EzEntryModel, EzEntry>
        {
            
        }
        
        [SerializeField]
        private CreateEntryEvent onCreateEntry = new CreateEntryEvent();
        
        public event UnityAction<EzEntryModel, EzEntry> OnCreateEntry
        {
            add => onCreateEntry.AddListener(value);
            remove => onCreateEntry.RemoveListener(value);
        }

        [Serializable]
        private class DeleteEntryEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private DeleteEntryEvent onDeleteEntry = new DeleteEntryEvent();
        
        public event UnityAction<string> OnDeleteEntry
        {
            add => onDeleteEntry.AddListener(value);
            remove => onDeleteEntry.RemoveListener(value);
        }
    }
}