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
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Formation
{
    [RequireComponent(typeof(Gs2FormationMoldFetcher))]
    [AddComponentMenu("GS2 UIKit/Formation/Gs2FormationMoldPrefab")]
    public partial class Gs2FormationMoldPrefab : MonoBehaviour
    {
        private Mold _mold;
        
        public void Set(
            Namespace namespace_,
            string moldName
        )
        {
            _mold = ScriptableObject.CreateInstance<Mold>();
            _mold.Namespace = namespace_;
            _mold.moldName = moldName;
            
            var moldFetcher = GetComponent<Gs2FormationMoldFetcher>();
            moldFetcher.mold = _mold;
        }

        public void OnDestroy()
        {
            if (_mold != null)
            {
                Destroy(_mold);
                _mold = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2FormationMoldPrefab
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2FormationMoldFetcher _moldFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _moldFetcher = GetComponent<Gs2FormationMoldFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FormationMoldPrefab
    {
        public Mold Mold
        {
            get => _moldFetcher.mold;
            set => _moldFetcher.mold = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2FormationMoldPrefab
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationMoldPrefab
    {
        
    }
}