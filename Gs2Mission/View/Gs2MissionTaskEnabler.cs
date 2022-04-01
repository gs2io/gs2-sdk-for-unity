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

namespace Gs2.Unity.UiKit.GsMission
{
    [AddComponentMenu("GS2 UIKit/Mission/Gs2MissionTaskEnabler")]
    public partial class Gs2MissionTaskEnabler : MonoBehaviour
    {
        public void Start()
        {
            Update();
        }

        public void Update()
        {
            if (!_missionTaskFetcher.Fetched || !_missionCompleteFetcher.Fetched || !_missionCounterFetcher.Fetched)
            {
                target.SetActive(loading);
            }
            else if (_missionCompleteFetcher.Complete?.ReceivedMissionTaskNames?.Contains(_missionTaskFetcher.Task.Name) ?? false)
            {
                target.SetActive(received);
            }
            else if (_missionCompleteFetcher.Complete?.CompletedMissionTaskNames?.Contains(_missionTaskFetcher.Task.Name) ?? false)
            {
                target.SetActive(completed);
            }
            else
            {
                var open = _missionTaskFetcher.Task.PremiseMissionTaskName == null || (_missionCompleteFetcher.Complete?.CompletedMissionTaskNames?.Contains(_missionTaskFetcher.Task.PremiseMissionTaskName) ?? false);
                if (open)
                {
                    target.SetActive(opened);
                }
                else
                {
                    target.SetActive(closed);
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2MissionTaskEnabler
    {
        private Gs2MissionMissionTaskFetcher _missionTaskFetcher;
        private Gs2MissionCompleteFetcher _missionCompleteFetcher;
        private Gs2MissionCounterFetcher _missionCounterFetcher;

        public void Awake()
        {
            _missionTaskFetcher = GetComponentInParent<Gs2MissionMissionTaskFetcher>() ?? GetComponent<Gs2MissionMissionTaskFetcher>();
            _missionCompleteFetcher = GetComponentInParent<Gs2MissionCompleteFetcher>() ?? GetComponent<Gs2MissionCompleteFetcher>();
            _missionCounterFetcher = GetComponentInParent<Gs2MissionCounterFetcher>() ?? GetComponent<Gs2MissionCounterFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2MissionTaskEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2MissionTaskEnabler
    {
        public bool loading;
        public bool opened;
        public bool completed;
        public bool received;
        public bool closed;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionTaskEnabler
    {
        
    }
}