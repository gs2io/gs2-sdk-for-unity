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
// ReSharper disable CheckNamespace

using System;
using Gs2.Unity.UiKit.Gs2Dictionary.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Dictionary
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Dictionary/Gs2DictionaryEntryEnabler")]
    public partial class Gs2DictionaryEntryEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (!_itemSetFetcher.Fetched)
            {
                target.SetActive(loading);
            }
            else 
            {
                if (_itemSetFetcher.Entry == null)
                {
                    target.SetActive(loaded);
                }
                else
                {
                    target.SetActive(have);
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2DictionaryEntryEnabler
    {
        private Gs2DictionaryEntryFetcher _itemSetFetcher;

        public void Awake()
        {
            _itemSetFetcher = GetComponentInParent<Gs2DictionaryEntryFetcher>() ?? GetComponent<Gs2DictionaryEntryFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2DictionaryEntryEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2DictionaryEntryEnabler
    {
        public bool loading;
        public bool loaded;
        public bool have;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DictionaryEntryEnabler
    {
        
    }
}