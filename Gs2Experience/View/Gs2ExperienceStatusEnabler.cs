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

using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Experience
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Experience/Gs2ExperienceStatusEnabler")]
    public partial class Gs2ExperienceStatusEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (!_statusFetcher.Fetched)
            {
                target.SetActive(loading);
            }
            else if (_statusFetcher.Status.RankValue < _statusFetcher.Status.RankCapValue)
            {
                target.SetActive(active);
            }
            else if (_statusFetcher.Status.RankValue >= _statusFetcher.Status.RankCapValue)
            {
                target.SetActive(capped);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2ExperienceStatusEnabler
    {
        private Gs2ExperienceStatusFetcher _statusFetcher;

        public void Awake()
        {
            _statusFetcher = GetComponentInParent<Gs2ExperienceStatusFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ExperienceStatusEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ExperienceStatusEnabler
    {
        public bool loading;
        public bool active;
        public bool capped;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExperienceStatusEnabler
    {
        
    }
}