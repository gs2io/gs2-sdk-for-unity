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
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Formation
{
    [RequireComponent(typeof(Gs2FormationMoldListFetcher))]
    [AddComponentMenu("GS2 UIKit/Formation/Gs2FormationMoldListPrefab")]
    public partial class Gs2FormationMoldListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2FormationMoldPrefab> _cache = new Dictionary<string, Gs2FormationMoldPrefab>();

        public void Update()
        {
            if (_questGroupListFetcher.Fetched)
            {
                var activeNames = _questGroupListFetcher.Models.Select(v => v.Name);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteFormation.Invoke(instantiateName);
                    }
                }

                foreach (var moldModel in _questGroupListFetcher.Models)
                {
                    if (!_cache.ContainsKey(moldModel.Name))
                    {
                        var item = Instantiate(moldPrefab, populateNode);
                        item.name = moldModel.Name;
                        item.Set(Namespace, moldModel.Name);
                        item.gameObject.SetActive(true);
                        _cache[moldModel.Name] = item;
                        onCreateMold.Invoke(moldModel, _questGroupListFetcher.Molds.FirstOrDefault(v => v.Name == moldModel.Name));
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
    
    public partial class Gs2FormationMoldListPrefab
    {
        private Gs2FormationMoldListFetcher _questGroupListFetcher;

        public void Awake()
        {
            _questGroupListFetcher = GetComponent<Gs2FormationMoldListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FormationMoldListPrefab
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
    
    public partial class Gs2FormationMoldListPrefab
    {
        public Gs2FormationMoldPrefab moldPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationMoldListPrefab
    {
        [Serializable]
        private class CreateMoldEvent : UnityEvent<EzMoldModel, EzMold>
        {
            
        }
        
        [SerializeField]
        private CreateMoldEvent onCreateMold = new CreateMoldEvent();
        
        public event UnityAction<EzMoldModel, EzMold> OnCreateMold
        {
            add => onCreateMold.AddListener(value);
            remove => onCreateMold.RemoveListener(value);
        }

        [Serializable]
        private class DeleteFormationEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private DeleteFormationEvent onDeleteFormation = new DeleteFormationEvent();
        
        public event UnityAction<string> OnDeleteFormation
        {
            add => onDeleteFormation.AddListener(value);
            remove => onDeleteFormation.RemoveListener(value);
        }
    }
}