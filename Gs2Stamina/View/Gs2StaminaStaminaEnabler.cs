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

using Gs2.Unity.UiKit.Gs2Stamina.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Stamina
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Stamina/Gs2StaminaStaminaEnabler")]
    public partial class Gs2StaminaStaminaEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (!_staminaFetcher.Fetched)
            {
                target.SetActive(loading);
            }
            else if (_staminaFetcher.Stamina.Value < _staminaFetcher.Stamina.MaxValue)
            {
                target.SetActive(recovering);
            }
            else if (_staminaFetcher.Stamina.Value == _staminaFetcher.Stamina.MaxValue)
            {
                target.SetActive(max);
            }
            else if (_staminaFetcher.Stamina.Value > _staminaFetcher.Stamina.MaxValue)
            {
                target.SetActive(overflow);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2StaminaStaminaEnabler
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
    
    public partial class Gs2StaminaStaminaEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2StaminaStaminaEnabler
    {
        public bool loading;
        public bool recovering;
        public bool max;
        public bool overflow;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaStaminaEnabler
    {
        
    }
}