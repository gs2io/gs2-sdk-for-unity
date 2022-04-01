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
using Gs2.Unity.Gs2Dictionary.Model;
using Gs2.Unity.Gs2Dictionary.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Dictionary.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Dictionary
{
    /// <summary>
    /// Main
    /// </summary>

    [RequireComponent(typeof(Gs2DictionaryEntryFetcher))]
    [AddComponentMenu("GS2 UIKit/Datastore/Gs2DictionaryEntryPrefab")]
    public partial class Gs2DictionaryEntryPrefab : MonoBehaviour
    {
        private Entry _entry;
        
        public void Set(
            Namespace namespace_,
            string entryName
        )
        {
            _entry = ScriptableObject.CreateInstance<Entry>();
            _entry.Namespace = namespace_;
            _entry.entryName = entryName;
            
            var entryFetcher = GetComponent<Gs2DictionaryEntryFetcher>();
            entryFetcher.entry = _entry;
        }

        public void OnDestroy()
        {
            if (_entry != null)
            {
                Destroy(_entry);
                _entry = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2DictionaryEntryPrefab
    {
        private Gs2DictionaryEntryFetcher _entryFetcher;

        public void Awake()
        {
            _entryFetcher = GetComponent<Gs2DictionaryEntryFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2DictionaryEntryPrefab
    {
        public Entry Entry
        {
            get => _entryFetcher.entry;
            set => _entryFetcher.entry = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2DictionaryEntryPrefab
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DictionaryEntryPrefab
    {
        
    }
}