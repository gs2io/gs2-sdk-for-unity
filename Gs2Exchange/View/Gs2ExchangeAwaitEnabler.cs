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
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Exchange
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Exchange/Gs2ExchangeAwaitEnabler")]
    public partial class Gs2ExchangeAwaitEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (!_awaitFetcher.Fetched)
            {
                target.SetActive(loading);
            }
            else 
            {
                var span = UnixTime.FromUnixTime(_awaitFetcher.Await.ExchangedAt).Add(TimeSpan.FromMinutes(_awaitFetcher.Model.LockTime)) - DateTime.UtcNow;
                if (span.TotalSeconds > 0)
                {
                    target.SetActive(waiting);
                }
                else
                {
                    target.SetActive(complete);
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2ExchangeAwaitEnabler
    {
        private Gs2ExchangeAwaitFetcher _awaitFetcher;

        public void Awake()
        {
            _awaitFetcher = GetComponentInParent<Gs2ExchangeAwaitFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ExchangeAwaitEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ExchangeAwaitEnabler
    {
        public bool loading;
        public bool waiting;
        public bool complete;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeAwaitEnabler
    {
        
    }
}