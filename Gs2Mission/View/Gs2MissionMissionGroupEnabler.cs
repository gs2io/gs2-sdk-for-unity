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

using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    [AddComponentMenu("GS2 UIKit/Mission/Gs2MissionMissionGroupEnabler")]
    public partial class Gs2MissionMissionGroupEnabler : MonoBehaviour
    {
        public void Start()
        {
            Update();
        }

        public void Update()
        {
            if (!_missionGroupFetcher.Fetched)
            {
                target.SetActive(loading);
            }
            else
            {
                target.SetActive(loaded);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2MissionMissionGroupEnabler
    {
        private Gs2MissionMissionGroupFetcher _missionGroupFetcher;

        public void Awake()
        {
            _missionGroupFetcher = GetComponentInParent<Gs2MissionMissionGroupFetcher>() ?? GetComponent<Gs2MissionMissionGroupFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2MissionMissionGroupEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2MissionMissionGroupEnabler
    {
        public bool loading;
        public bool loaded;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionMissionGroupEnabler
    {
        
    }
}