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
using System.Collections;
using Gs2.Unity.Gs2Mission.Model;
using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Mission/Gs2MissionMissionTask")]
    public partial class Gs2MissionMissionTask : MonoBehaviour
    {
        private MissionTask _missionTask;
        private MissionCounter _missionCounter;

        public void Set(
            MissionGroup missionGroup,
            EzMissionTaskModel missionTask
        )
        {
            _missionTask = ScriptableObject.CreateInstance<MissionTask>();
            _missionTask.missionGroup = missionGroup;
            _missionTask.missionTaskName = missionTask.Name;
            
            
            var missionTaskFetcher = GetComponentInParent<Gs2MissionMissionTaskFetcher>() ?? GetComponent<Gs2MissionMissionTaskFetcher>();
            var completeFetcher = GetComponentInParent<Gs2MissionCompleteFetcher>() ?? GetComponent<Gs2MissionCompleteFetcher>();
            missionTaskFetcher.missionTask = _missionTask;
            completeFetcher.missionGroup = missionGroup;

            IEnumerator FetchCounter()
            {
                yield return new WaitUntil(() => _missionTaskFetcher.Fetched);
                _missionCounter = ScriptableObject.CreateInstance<MissionCounter>();
                _missionCounter.Namespace = missionGroup.Namespace;
                _missionCounter.missionCounterName = _missionTaskFetcher.Task.CounterName;
                _counterFetcher.missionCounter = _missionCounter;
            }

            StartCoroutine(FetchCounter());
        }
        
        public void OnDestroy()
        {
            if (_missionTask != null)
            {
                Destroy(_missionTask);
                _missionTask = null;
            }
            if (_missionCounter != null)
            {
                Destroy(_missionCounter);
                _missionCounter = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2MissionMissionTask
    {
        private Gs2MissionMissionTaskFetcher _missionTaskFetcher;
        private Gs2MissionCompleteFetcher _completeFetcher;
        private Gs2MissionCounterFetcher _counterFetcher;

        public void Awake()
        {
            _missionTaskFetcher = GetComponentInParent<Gs2MissionMissionTaskFetcher>() ?? GetComponent<Gs2MissionMissionTaskFetcher>();
            _completeFetcher = GetComponentInParent<Gs2MissionCompleteFetcher>() ?? GetComponent<Gs2MissionCompleteFetcher>();
            _counterFetcher = GetComponentInParent<Gs2MissionCounterFetcher>() ?? GetComponent<Gs2MissionCounterFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2MissionMissionTask
    {
        public MissionTask MissionTask
        {
            get => _missionTaskFetcher.missionTask;
            set => _missionTaskFetcher.missionTask = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2MissionMissionTask
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionMissionTask
    {
        
    }
}