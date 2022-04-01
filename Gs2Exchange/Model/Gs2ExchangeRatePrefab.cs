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
using Gs2.Unity.Gs2Exchange.Model;
using Gs2.Unity.Gs2Exchange.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Exchange
{
    [RequireComponent(typeof(Gs2ExchangeRateFetcher))]
    [AddComponentMenu("GS2 UIKit/Exchange/Gs2ExchangeRatePrefab")]
    public partial class Gs2ExchangeRatePrefab : MonoBehaviour
    {
        private Rate _rate;
        
        public void Set(
            Namespace namespace_,
            string rateName
        )
        {
            _rate = ScriptableObject.CreateInstance<Rate>();
            _rate.Namespace = namespace_;
            _rate.rateName = rateName;
            _rateFetcher.rate = _rate;
            
            var rateFetcher = GetComponent<Gs2ExchangeRateFetcher>();
            rateFetcher.rate = _rate;
        }

        public void OnDestroy()
        {
            if (_rate != null)
            {
                Destroy(_rate);
                _rate = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2ExchangeRatePrefab
    {
        private Gs2ExchangeRateFetcher _rateFetcher;

        public void Awake()
        {
            _rateFetcher = GetComponent<Gs2ExchangeRateFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ExchangeRatePrefab
    {
        public Rate Rate
        {
            get => _rateFetcher.rate;
            set => _rateFetcher.rate = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ExchangeRatePrefab
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeRatePrefab
    {
        
    }
}