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
using Gs2.Unity.Gs2Experience.Model;
using Gs2.Unity.Gs2Experience.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Experience
{
    /// <summary>
    /// Main
    /// </summary>

    [RequireComponent(typeof(Gs2ExperienceStatusFetcher))]
    [AddComponentMenu("GS2 UIKit/Experience/Gs2ExperienceStatusPrefab")]
    public partial class Gs2ExperienceStatusPrefab : MonoBehaviour
    {
        private Status _status;
        
        public void Set(
            Experience experience,
            string propertyId
        )
        {
            _status = ScriptableObject.CreateInstance<Status>();
            _status.experience = experience;
            _status.propertyId = propertyId;
            _statusFetcher.status = _status;
            Status = _status;
        }

        public void OnDestroy()
        {
            if (_status != null)
            {
                Destroy(_status);
                _status = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2ExperienceStatusPrefab
    {
        private Gs2ExperienceStatusFetcher _statusFetcher;

        public void Awake()
        {
            _statusFetcher = GetComponent<Gs2ExperienceStatusFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ExperienceStatusPrefab
    {
        public Status Status
        {
            get => _statusFetcher.status;
            set => _statusFetcher.status = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ExperienceStatusPrefab
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExperienceStatusPrefab
    {
        
    }
}