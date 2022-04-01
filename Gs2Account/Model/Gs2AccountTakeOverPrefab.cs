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
using Gs2.Unity.Gs2Account.Model;
using Gs2.Unity.Gs2Account.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Account.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Account
{
    [RequireComponent(typeof(Gs2AccountTakeOverFetcher))]
    [AddComponentMenu("GS2 UIKit/Account/Gs2AccountTakeOverPrefab")]
    public partial class Gs2AccountTakeOverPrefab : MonoBehaviour
    {
        private TakeOver _takeOver;
        
        public void Set(
            Namespace namespace_,
            int type
        )
        {
            _takeOver = ScriptableObject.CreateInstance<TakeOver>();
            _takeOver.Namespace = namespace_;
            _takeOver.type = type;
            
            var takeOverFetcher = GetComponentInParent<Gs2AccountTakeOverFetcher>() ?? GetComponent<Gs2AccountTakeOverFetcher>();
            takeOverFetcher.takeOver = _takeOver;
        }

        public void OnDestroy()
        {
            if (_takeOver != null)
            {
                Destroy(_takeOver);
                _takeOver = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2AccountTakeOverPrefab
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2AccountTakeOverFetcher _takeOverFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _takeOverFetcher = GetComponentInParent<Gs2AccountTakeOverFetcher>() ?? GetComponent<Gs2AccountTakeOverFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2AccountTakeOverPrefab
    {
        public TakeOver TakeOver
        {
            get => _takeOverFetcher.takeOver;
            set => _takeOverFetcher.takeOver = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2AccountTakeOverPrefab
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AccountTakeOverPrefab
    {
        
    }
}