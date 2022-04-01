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
using Gs2.Unity.Gs2Enhance.Model;
using Gs2.Unity.Gs2Enhance.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Enhance.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Enhance
{
    /// <summary>
    /// Main
    /// </summary>

    [RequireComponent(typeof(Gs2EnhanceProgressFetcher))]
    [AddComponentMenu("GS2 UIKit/Enhance/Gs2EnhanceProgress")]
    public partial class Gs2EnhanceProgress : MonoBehaviour
    {
        public void StartEnhance()
        {
            onSelect.Invoke(_progressFetcher.Model, _progressFetcher.Progress);
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2EnhanceProgress
    {
        private Gs2EnhanceProgressFetcher _progressFetcher;

        public void Awake()
        {
            _progressFetcher = GetComponent<Gs2EnhanceProgressFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2EnhanceProgress
    {
        public Namespace Namespace
        {
            get => _progressFetcher.Namespace;
            set => _progressFetcher.Namespace = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2EnhanceProgress
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnhanceProgress
    {
        [Serializable]
        private class SelectEvent : UnityEvent<EzRateModel, EzProgress>
        {
            
        }
        
        [SerializeField]
        private SelectEvent onSelect = new SelectEvent();
        
        public event UnityAction<EzRateModel, EzProgress> OnSelect
        {
            add => onSelect.AddListener(value);
            remove => onSelect.RemoveListener(value);
        }
    }
}