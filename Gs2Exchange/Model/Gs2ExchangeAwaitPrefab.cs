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
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Exchange
{
    /// <summary>
    /// Main
    /// </summary>

    [RequireComponent(typeof(Gs2ExchangeAwaitFetcher))]
    [AddComponentMenu("GS2 UIKit/Exchange/Gs2ExchangeAwaitPrefab")]
    public partial class Gs2ExchangeAwaitPrefab : MonoBehaviour
    {
        private Await _await;
        
        public void Set(
            Rate rate,
            string awaitName
        )
        {
            _await = ScriptableObject.CreateInstance<Await>();
            _await.rate = rate;
            _await.awaitName = awaitName;
            
            var awaitFetcher = GetComponent<Gs2ExchangeAwaitFetcher>();
            awaitFetcher.await_ = _await;
        }

        public void OnDestroy()
        {
            if (_await != null)
            {
                Destroy(_await);
                _await = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2ExchangeAwaitPrefab
    {
        private Gs2ExchangeAwaitFetcher _awaitFetcher;

        public void Awake()
        {
            _awaitFetcher = GetComponent<Gs2ExchangeAwaitFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ExchangeAwaitPrefab
    {
        public Await Await
        {
            get => _awaitFetcher.await_;
            set => _awaitFetcher.await_ = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ExchangeAwaitPrefab
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeAwaitPrefab
    {
        
    }
}