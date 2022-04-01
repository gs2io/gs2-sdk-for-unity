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
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Mission/Gs2MissionTaskLabel")]
    public partial class Gs2MissionTaskLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_counterFetcher.Fetched && _missionTaskFetcher.Fetched && _missionGroupFetcher.Fetched)
            {
                onUpdate.Invoke(format.Replace(
                    "{current}", _counterFetcher.Counter?.Values?.FirstOrDefault(v => v.ResetType == _missionGroupFetcher.MissionGroup.ResetType)?.Value.ToString() ?? "0"
                ).Replace(
                    "{targetValue}", _missionTaskFetcher.Task.TargetValue.ToString()
                ));
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2MissionTaskLabel
    {
        private Gs2MissionCounterFetcher _counterFetcher;
        private Gs2MissionMissionTaskFetcher _missionTaskFetcher;
        private Gs2MissionMissionGroupFetcher _missionGroupFetcher;

        public void Awake()
        {
            _counterFetcher = GetComponentInParent<Gs2MissionCounterFetcher>();
            _missionTaskFetcher = GetComponentInParent<Gs2MissionMissionTaskFetcher>();
            _missionGroupFetcher = GetComponentInParent<Gs2MissionMissionGroupFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2MissionTaskLabel
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2MissionTaskLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionTaskLabel
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