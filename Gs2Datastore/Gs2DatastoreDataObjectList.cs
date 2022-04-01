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
using Gs2.Unity.Gs2Datastore.Model;
using Gs2.Unity.Gs2Datastore.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Datastore.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Datastore
{
    [RequireComponent(typeof(Gs2DatastoreDataObjectListFetcher))]
    [AddComponentMenu("GS2 UIKit/Datastore/Gs2DatastoreDataObjectList")]
    public partial class Gs2DatastoreDataObjectList : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2DatastoreDataObject> _cache = new Dictionary<string, Gs2DatastoreDataObject>();

        public void Update()
        {
            if (_dataObjectListFetcher.DataObjects != null)
            {
                void OnSelect(EzDataObject dataObject)
                {
                    onSelect.Invoke(dataObject);
                }

                var activeNames = _dataObjectListFetcher.DataObjects.Select(v => v.Name);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        _cache[instantiateName].OnSelect -= OnSelect;
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteDatastore.Invoke(instantiateName);
                    }
                }

                foreach (var dataObject in _dataObjectListFetcher.DataObjects)
                {
                    if (!_cache.ContainsKey(dataObject.Name))
                    {
                        var item = Instantiate(questPrefab, populateNode);
                        item.name = dataObject.Name;
                        item.Set(user, dataObject.Name);
                        item.OnSelect += OnSelect;
                        _cache[dataObject.Name] = item;
                        onCreateDatastore.Invoke(dataObject);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2DatastoreDataObjectList
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2DatastoreDataObjectListFetcher _dataObjectListFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _dataObjectListFetcher = GetComponent<Gs2DatastoreDataObjectListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2DatastoreDataObjectList
    {
        public User user
        {
            get => _dataObjectListFetcher.user;
            set => _dataObjectListFetcher.user = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2DatastoreDataObjectList
    {
        public Gs2DatastoreDataObject questPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DatastoreDataObjectList
    {
        [Serializable]
        private class CreateDatastoreEvent : UnityEvent<EzDataObject>
        {
            
        }
        
        [SerializeField]
        private CreateDatastoreEvent onCreateDatastore = new CreateDatastoreEvent();
        
        public event UnityAction<EzDataObject> OnCreateDatastore
        {
            add => onCreateDatastore.AddListener(value);
            remove => onCreateDatastore.RemoveListener(value);
        }

        [Serializable]
        private class DeleteDatastoreEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private DeleteDatastoreEvent onDeleteDatastore = new DeleteDatastoreEvent();
        
        public event UnityAction<string> OnDeleteDatastore
        {
            add => onDeleteDatastore.AddListener(value);
            remove => onDeleteDatastore.RemoveListener(value);
        }
        
        [Serializable]
        private class SelectEvent : UnityEvent<EzDataObject>
        {
            
        }
        
        [SerializeField]
        private SelectEvent onSelect = new SelectEvent();
        
        public event UnityAction<EzDataObject> OnSelect
        {
            add => onSelect.AddListener(value);
            remove => onSelect.RemoveListener(value);
        }
    }
}