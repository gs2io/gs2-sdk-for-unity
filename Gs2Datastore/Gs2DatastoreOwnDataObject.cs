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
using Gs2.Unity.Gs2Datastore.Model;
using Gs2.Unity.Gs2Datastore.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Datastore.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Datastore
{
    /// <summary>
    /// Main
    /// </summary>

    [RequireComponent(typeof(Gs2DatastoreOwnDataObjectFetcher))]
    [AddComponentMenu("GS2 UIKit/Datastore/Gs2DatastoreOwnDataObject")]
    public partial class Gs2DatastoreOwnDataObject : MonoBehaviour
    {
        private OwnDataObject _dataObject;
        
        public void Set(
            Namespace namespace_,
            string dataObjectName
        )
        {
            _dataObject = ScriptableObject.CreateInstance<OwnDataObject>();
            _dataObject.Namespace = namespace_;
            _dataObject.dataObjectName = dataObjectName;
            _dataObjectFetcher.dataObject = _dataObject;
            OwnDataObject = _dataObject;
        }

        public void OnDestroy()
        {
            if (_dataObject != null)
            {
                Destroy(_dataObject);
                _dataObject = null;
            }
        }

        public void StartDatastore()
        {
            onSelect.Invoke(_dataObjectFetcher.DataObject);
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2DatastoreOwnDataObject
    {
        private Gs2DatastoreOwnDataObjectFetcher _dataObjectFetcher;

        public void Awake()
        {
            _dataObjectFetcher = GetComponent<Gs2DatastoreOwnDataObjectFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2DatastoreOwnDataObject
    {
        public OwnDataObject OwnDataObject
        {
            get => _dataObjectFetcher.dataObject;
            set => _dataObjectFetcher.dataObject = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2DatastoreOwnDataObject
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DatastoreOwnDataObject
    {
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