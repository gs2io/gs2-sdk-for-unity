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
    [RequireComponent(typeof(Gs2FormationFormFetcher))]
    [RequireComponent(typeof(Gs2FormationSlotListFetcher))]
    [AddComponentMenu("GS2 UIKit/Formation/Gs2FormationSlotListPrefab")]
    public partial class Gs2FormationSlotListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2FormationSlotPrefab> _cache = new Dictionary<string, Gs2FormationSlotPrefab>();

        private ErrorEvent _onError;

        public void Set(
            Form formKey, 
            EzFormModel formModel, 
            EzForm form, 
            ErrorEvent onError
        )
        {
            var slotListFetcher = GetComponent<Gs2FormationSlotListFetcher>();
            slotListFetcher.form = formKey;
            var formFetcher = GetComponent<Gs2FormationFormFetcher>();
            formFetcher.form = formKey;
        }

        public void Update()
        {
            if (_slotListFetcher.Fetched && _formFetcher.Fetched)
            {
                foreach (var slot in _formFetcher.Model.Slots)
                {
                    if (!_cache.ContainsKey(slot.Name))
                    {
                        var item = Instantiate(slotPrefab, populateNode);
                        item.name = slot.Name;
                        item.Set(Form, slot.Name);
                        item.gameObject.SetActive(true);
                        _cache[slot.Name] = item;
                        onCreateFormation.Invoke(slot, _formFetcher.Form.Slots.FirstOrDefault(v => v.Name == slot.Name));
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2FormationSlotListPrefab
    {
        private Gs2FormationSlotListFetcher _slotListFetcher;
        private Gs2FormationFormFetcher _formFetcher;

        public void Awake()
        {
            _slotListFetcher = GetComponent<Gs2FormationSlotListFetcher>();
            _slotListFetcher.OnError += OnError;
            
            _formFetcher = GetComponent<Gs2FormationFormFetcher>();
            _formFetcher.OnError += OnError;
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
            _slotListFetcher.OnError -= OnError;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FormationSlotListPrefab
    {
        public Form Form
        {
            get => _slotListFetcher.form;
            set => _slotListFetcher.form = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2FormationSlotListPrefab
    {
        public Gs2FormationSlotPrefab slotPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationSlotListPrefab
    {
        [Serializable]
        private class CreateFormationEvent : UnityEvent<EzSlotModel, EzSlot>
        {
            
        }
        
        [SerializeField]
        private CreateFormationEvent onCreateFormation = new CreateFormationEvent();
        
        public event UnityAction<EzSlotModel, EzSlot> OnCreateFormation
        {
            add => onCreateFormation.AddListener(value);
            remove => onCreateFormation.RemoveListener(value);
        }
    }
}