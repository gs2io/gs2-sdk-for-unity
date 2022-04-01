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
using System.Linq;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Mission/Gs2MissionTaskProgress")]
    public partial class Gs2MissionTaskProgress : MonoBehaviour
    {
        public void Update()
        {
            if (_counterFetcher.Fetched && _missionTaskFetcher.Fetched && _missionGroupFetcher.Fetched)
            {
                var value = _counterFetcher.Counter?.Values?
                    .FirstOrDefault(v => v.ResetType == _missionGroupFetcher.MissionGroup.ResetType)?.Value ?? 0;
                var targetValue = _missionTaskFetcher.Task.TargetValue;
                onUpdate.Invoke(Math.Min(1.0f, (float) value / targetValue));
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2MissionTaskProgress
    {
        private Gs2MissionCounterFetcher _counterFetcher;
        private Gs2MissionMissionTaskFetcher _missionTaskFetcher;
        private Gs2MissionMissionGroupFetcher _missionGroupFetcher;

        public void Awake()
        {
            _counterFetcher = GetComponentInParent<Gs2MissionCounterFetcher>();
            _missionTaskFetcher = GetComponentInParent<Gs2MissionMissionTaskFetcher>();
            _missionGroupFetcher = GetComponentInParent<Gs2MissionMissionGroupFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2MissionTaskProgress
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2MissionTaskProgress
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionTaskProgress
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<float>
        {
            
        }
        
        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();
        
        public event UnityAction<float> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}