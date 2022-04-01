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
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Gs2Stamina.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Stamina
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Stamina/Gs2StaminaStaminaLabel")]
    public partial class Gs2StaminaStaminaLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_staminaFetcher.Fetched)
            {
                while (_staminaFetcher.Stamina.NextRecoverAt > 0 && _staminaFetcher.Stamina.NextRecoverAt < UnixTime.ToUnixTime(DateTime.UtcNow))
                {
                    _staminaFetcher.Stamina.Value += _staminaFetcher.Stamina.RecoverValue;
                    _staminaFetcher.Stamina.Value = Math.Min(_staminaFetcher.Stamina.Value, _staminaFetcher.Stamina.MaxValue) + _staminaFetcher.Stamina.OverflowValue;
                    _staminaFetcher.Stamina.NextRecoverAt += _staminaFetcher.Stamina.RecoverIntervalMinutes * 60 * 1000;
                }
                var span = (_staminaFetcher.Stamina.NextRecoverAt == 0 ? DateTime.UtcNow : UnixTime.FromUnixTime(_staminaFetcher.Stamina.NextRecoverAt)) - DateTime.UtcNow;
                onUpdate.Invoke(format.Replace(
                    "{current}", _staminaFetcher.Stamina.Value.ToString()
                ).Replace(
                    "{total}", _staminaFetcher.Stamina.Value.ToString()
                ).Replace(
                    "{max}", _staminaFetcher.Stamina.MaxValue.ToString()
                ).Replace(
                    "{overflow}", (_staminaFetcher.Stamina.Value > _staminaFetcher.Stamina.MaxValue ? _staminaFetcher.Stamina.Value - _staminaFetcher.Stamina.MaxValue : _staminaFetcher.Stamina.Value).ToString()
                ).Replace(
                    "{mm}", span.ToString("mm")
                ).Replace(
                    "{ss}", span.ToString("ss")
                ).Replace(
                    "{h}", span.ToString("%h")
                ).Replace(
                    "{m}", span.ToString("%m")
                ).Replace(
                    "{s}", span.ToString("%s")
                ));
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2StaminaStaminaLabel
    {
        private Gs2StaminaStaminaFetcher _staminaFetcher;

        public void Awake()
        {
            _staminaFetcher = GetComponentInParent<Gs2StaminaStaminaFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2StaminaStaminaLabel
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2StaminaStaminaLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaStaminaLabel
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();
        
        public event UnityAction<string> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}