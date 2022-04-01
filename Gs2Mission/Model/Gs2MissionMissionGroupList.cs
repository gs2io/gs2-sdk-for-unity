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

using System.Collections.Generic;
using System.Linq;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    [RequireComponent(typeof(Gs2MissionMissionGroupListFetcher))]
    [AddComponentMenu("GS2 UIKit/Mission/Gs2MissionMissionGroupList")]
    public partial class Gs2MissionMissionGroupList : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2MissionMissionGroup> _cache = new Dictionary<string, Gs2MissionMissionGroup>();

        public void Update()
        {
            if (_missionGroupListFetcher.MissionGroups != null)
            {
                var activeNames = _missionGroupListFetcher.MissionGroups.Select(v => v.Name);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        var instance = _cache[instantiateName];
                        Destroy(instance.gameObject);
                        _cache.Remove(instantiateName);
                    }
                }

                foreach (var missionGroup in _missionGroupListFetcher.MissionGroups)
                {
                    if (!_cache.ContainsKey(missionGroup.Name))
                    {
                        var item = Instantiate(missionGroupPrefab, populateNode);
                        item.name = missionGroup.Name;
                        item.Set(_missionGroupListFetcher.Namespace, missionGroup, _missionGroupListFetcher.onError);
                        item.gameObject.SetActive(true);
                        _cache[missionGroup.Name] = item;
                    }
                }
            }
        }

        public void OnDestroy()
        {
            foreach (var component in _cache.Values)
            {
                Destroy(component);
            }
            _cache.Clear();
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2MissionMissionGroupList
    {
        private Gs2MissionMissionGroupListFetcher _missionGroupListFetcher;

        public void Awake()
        {
            _missionGroupListFetcher = GetComponentInParent<Gs2MissionMissionGroupListFetcher>() ?? GetComponent<Gs2MissionMissionGroupListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2MissionMissionGroupList
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2MissionMissionGroupList
    {
        public Gs2MissionMissionGroup missionGroupPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionMissionGroupList
    {
        
    }
}