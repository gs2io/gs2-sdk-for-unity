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
using Gs2.Unity.UiKit.Gs2Stamina.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Stamina
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Stamina/Gs2StaminaStaminaProgress")]
    public partial class Gs2StaminaStaminaProgress : MonoBehaviour
    {
        public void Update()
        {
            if (_staminaFetcher.Fetched)
            {
                onUpdate.Invoke(Math.Min(1.0f, (float) _staminaFetcher.Stamina.Value / _staminaFetcher.Stamina.MaxValue));
                onUpdateInverse.Invoke(1.0f - Math.Min(1.0f, (float) _staminaFetcher.Stamina.Value / _staminaFetcher.Stamina.MaxValue));
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2StaminaStaminaProgress
    {
        private Gs2StaminaStaminaFetcher _staminaFetcher;

        public void Awake()
        {
            _staminaFetcher = GetComponentInParent<Gs2StaminaStaminaFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2StaminaStaminaProgress
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2StaminaStaminaProgress
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaStaminaProgress
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<float>
        {
            
        }
        
        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();
        
        public event UnityAction<float> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
        
        [Serializable]
        private class UpdateInverseEvent : UnityEvent<float>
        {
            
        }
        
        [SerializeField]
        private UpdateInverseEvent onUpdateInverse = new UpdateInverseEvent();
        
        public event UnityAction<float> OnUpdateInverse
        {
            add => onUpdateInverse.AddListener(value);
            remove => onUpdateInverse.RemoveListener(value);
        }
    }
}