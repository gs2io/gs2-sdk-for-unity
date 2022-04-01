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
using System.Collections.Generic;
using System.Linq;
using Gs2.Unity.Gs2Mission.Model;
using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    [RequireComponent(typeof(Gs2MissionMissionGroupFetcher))]
    [RequireComponent(typeof(Gs2MissionMissionTaskListFetcher))]
    [AddComponentMenu("GS2 UIKit/Mission/Gs2MissionMissionTaskList")]
    public partial class Gs2MissionMissionTaskList : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2MissionMissionTask> _cache = new Dictionary<string, Gs2MissionMissionTask>();

        private MissionGroup _missionGroup;
        private ErrorEvent _onError;

        public void Update()
        {
            if (_missionTaskListFetcher.Tasks != null)
            {
                var activeNames = _missionTaskListFetcher.Tasks.Select(v => v.Name);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        var instance = _cache[instantiateName];
                        Destroy(instance.gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteMission.Invoke(instance);
                    }
                }

                foreach (var missionTask in _missionTaskListFetcher.Tasks)
                {
                    if (!_cache.ContainsKey(missionTask.Name))
                    {
                        var item = Instantiate(missionPrefab, populateNode);
                        item.name = missionTask.Name;
                        item.gameObject.SetActive(true);
                        item.Set(_missionTaskListFetcher.missionGroup, missionTask);
                        _cache[missionTask.Name] = item;
                        onCreateMission.Invoke(item, missionTask);
                    }
                }
            }
        }

        public void Set(
            Namespace @namespace,
            EzMissionGroupModel missionGroup,
            ErrorEvent onError
        )
        {
            _missionGroup = ScriptableObject.CreateInstance<MissionGroup>();
            _missionGroup.Namespace = @namespace;
            _missionGroup.missionGroupName = missionGroup.Name;
            _onError = onError;

            var missionTaskListFetcher = GetComponentInParent<Gs2MissionMissionTaskListFetcher>() ?? GetComponent<Gs2MissionMissionTaskListFetcher>();
            missionTaskListFetcher.missionGroup = _missionGroup;
            var missionGroupFetcher = GetComponentInParent<Gs2MissionMissionGroupFetcher>() ?? GetComponent<Gs2MissionMissionGroupFetcher>();
            missionGroupFetcher.missionGroup = _missionGroup;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2MissionMissionTaskList
    {
        private Gs2MissionMissionTaskListFetcher _missionTaskListFetcher;

        public void Awake()
        {
            _missionTaskListFetcher = GetComponentInParent<Gs2MissionMissionTaskListFetcher>() ?? GetComponent<Gs2MissionMissionTaskListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2MissionMissionTaskList
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2MissionMissionTaskList
    {
        public Gs2MissionMissionTask missionPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionMissionTaskList
    {
        [Serializable]
        private class CreateMissionEvent : UnityEvent<Gs2MissionMissionTask, EzMissionTaskModel>
        {
            
        }
        
        [SerializeField]
        private CreateMissionEvent onCreateMission = new CreateMissionEvent();
        
        public event UnityAction<Gs2MissionMissionTask, EzMissionTaskModel> OnCreateMission
        {
            add => onCreateMission.AddListener(value);
            remove => onCreateMission.RemoveListener(value);
        }

        [Serializable]
        private class DeleteMissionEvent : UnityEvent<Gs2MissionMissionTask>
        {
            
        }
        
        [SerializeField]
        private DeleteMissionEvent onDeleteMission = new DeleteMissionEvent();
        
        public event UnityAction<Gs2MissionMissionTask> OnDeleteMission
        {
            add => onDeleteMission.AddListener(value);
            remove => onDeleteMission.RemoveListener(value);
        }
    }
}