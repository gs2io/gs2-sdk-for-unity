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
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Formation
{
    [RequireComponent(typeof(Gs2FormationMoldFetcher))]
    [RequireComponent(typeof(Gs2FormationFormListFetcher))]
    [AddComponentMenu("GS2 UIKit/Formation/Gs2FormationFormListPrefab")]
    public partial class Gs2FormationFormListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2FormationFormPrefab> _cache = new Dictionary<string, Gs2FormationFormPrefab>();

        private Mold _mold;
        private ErrorEvent _onError;

        public void Set(
            Namespace @namespace, 
            EzMoldModel moldModel, 
            EzMold mold, 
            ErrorEvent onError
        )
        {
            _mold = ScriptableObject.CreateInstance<Mold>();
            _mold.Namespace = @namespace;
            _mold.moldName = moldModel.Name;
            _onError = onError;

            var formListFetcher = GetComponent<Gs2FormationFormListFetcher>();
            formListFetcher.mold = _mold;
            var moldFetcher = GetComponent<Gs2FormationMoldFetcher>();
            moldFetcher.mold = _mold;
        }

        public void Update()
        {
            if (_formListFetcher.Fetched && _moldFetcher.Fetched)
            {
                for (var i = 0; i < (_moldFetcher.Mold?.Capacity ?? _moldFetcher.Model.InitialMaxCapacity); i++)
                {
                    if (!_cache.ContainsKey(i.ToString()))
                    {
                        var item = Instantiate(formPrefab, populateNode);
                        item.name = i.ToString();
                        item.Set(Mold, i);
                        item.gameObject.SetActive(true);
                        _cache[i.ToString()] = item;
                        onCreateFormation.Invoke(_formListFetcher.Model, _formListFetcher.Forms.FirstOrDefault(v => v.Index == i));
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2FormationFormListPrefab
    {
        private Gs2FormationFormListFetcher _formListFetcher;
        private Gs2FormationMoldFetcher _moldFetcher;

        public void Awake()
        {
            _formListFetcher = GetComponent<Gs2FormationFormListFetcher>();
            _formListFetcher.OnError += OnError;
            
            _moldFetcher = GetComponent<Gs2FormationMoldFetcher>();
            _moldFetcher.OnError += OnError;
        }
        
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

        public void OnDestroy()
        {
            _formListFetcher.OnError -= OnError;

            if (_mold != null)
            {
                Destroy(_mold);
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FormationFormListPrefab
    {
        public Mold Mold
        {
            get => _formListFetcher.mold;
            set => _formListFetcher.mold = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2FormationFormListPrefab
    {
        public Gs2FormationFormPrefab formPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationFormListPrefab
    {
        [Serializable]
        private class CreateFormationEvent : UnityEvent<EzFormModel, EzForm>
        {
            
        }
        
        [SerializeField]
        private CreateFormationEvent onCreateFormation = new CreateFormationEvent();
        
        public event UnityAction<EzFormModel, EzForm> OnCreateFormation
        {
            add => onCreateFormation.AddListener(value);
            remove => onCreateFormation.RemoveListener(value);
        }
    }
}