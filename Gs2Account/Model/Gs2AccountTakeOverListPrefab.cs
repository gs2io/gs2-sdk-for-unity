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
using Gs2.Unity.Gs2Account.Model;
using Gs2.Unity.Gs2Account.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Account.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Account
{
    [RequireComponent(typeof(Gs2AccountTakeOverListFetcher))]
    [AddComponentMenu("GS2 UIKit/Account/Gs2AccountTakeOverListPrefab")]
    public partial class Gs2AccountTakeOverListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2AccountTakeOverPrefab> _cache = new Dictionary<string, Gs2AccountTakeOverPrefab>();

        public void Update()
        {
            if (_questGroupListFetcher.TakeOvers != null)
            {
                var activeNames = _questGroupListFetcher.TakeOvers.Select(v => v.Type.ToString());
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteAccount.Invoke(instantiateName);
                    }
                }

                foreach (var takeOver in _questGroupListFetcher.TakeOvers)
                {
                    if (!_cache.ContainsKey(takeOver.Type.ToString()))
                    {
                        var item = Instantiate(takeOverPrefab, populateNode);
                        item.name = takeOver.Type.ToString();
                        item.Set(Namespace, takeOver.Type);
                        item.gameObject.SetActive(true);
                        _cache[takeOver.Type.ToString()] = item;
                        onCreateAccount.Invoke(takeOver);
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
    
    public partial class Gs2AccountTakeOverListPrefab
    {
        private Gs2AccountTakeOverListFetcher _questGroupListFetcher;

        public void Awake()
        {
            _questGroupListFetcher = GetComponent<Gs2AccountTakeOverListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2AccountTakeOverListPrefab
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
    
    public partial class Gs2AccountTakeOverListPrefab
    {
        public Gs2AccountTakeOverPrefab takeOverPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AccountTakeOverListPrefab
    {
        [Serializable]
        private class CreateAccountEvent : UnityEvent<EzTakeOver>
        {
            
        }
        
        [SerializeField]
        private CreateAccountEvent onCreateAccount = new CreateAccountEvent();
        
        public event UnityAction<EzTakeOver> OnCreateAccount
        {
            add => onCreateAccount.AddListener(value);
            remove => onCreateAccount.RemoveListener(value);
        }

        [Serializable]
        private class DeleteAccountEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private DeleteAccountEvent onDeleteAccount = new DeleteAccountEvent();
        
        public event UnityAction<string> OnDeleteAccount
        {
            add => onDeleteAccount.AddListener(value);
            remove => onDeleteAccount.RemoveListener(value);
        }
    }
}