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
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Exchange
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Exchange/Gs2ExchangeAwaitLabel")]
    public partial class Gs2ExchangeAwaitLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_awaitFetcher.Fetched)
            {
                var span = UnixTime.FromUnixTime(_awaitFetcher.Await.ExchangedAt).Add(TimeSpan.FromMinutes(_awaitFetcher.Model.LockTime)) - DateTime.UtcNow;
                if (span.TotalSeconds < 0)
                {
                    span = TimeSpan.Zero;
                }
                onUpdate.Invoke(format.Replace(
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
    
    public partial class Gs2ExchangeAwaitLabel
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
    
    public partial class Gs2ExchangeAwaitLabel
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ExchangeAwaitLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeAwaitLabel
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