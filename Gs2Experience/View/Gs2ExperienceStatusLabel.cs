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
using Gs2.Core.Util;
using Gs2.Unity.Gs2Experience.Model;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Experience
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Experience/Gs2ExperienceStatusLabel")]
    public partial class Gs2ExperienceStatusLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_statusFetcher.Fetched)
            {
                var prevRankUpExperience = _statusFetcher.Model.NextRankUpExperienceValue(_statusFetcher.Status.RankValue-1, _statusFetcher.Status.RankCapValue) ?? 0;
                var nextRankUpExperience = _statusFetcher.Model.NextRankUpExperienceValue(_statusFetcher.Status.RankValue, _statusFetcher.Status.RankCapValue);
                var currentStep = _statusFetcher.Status.ExperienceValue - prevRankUpExperience;
                var nextRankStep = nextRankUpExperience == null ? 0 : nextRankUpExperience.Value - prevRankUpExperience;
                onUpdate.Invoke(format.Replace(
                    "{currentExperience}", _statusFetcher.Status.ExperienceValue.ToString()
                ).Replace(
                    "{nextRankUpExperience}", _statusFetcher.Model.NextRankUpExperienceValue(_statusFetcher.Status.RankValue, _statusFetcher.Status.RankCapValue)?.ToString() ?? "0"
                ).Replace(
                    "{currentStep}", currentStep.ToString()
                ).Replace(
                    "{nextRankStep}", nextRankStep.ToString()
                ).Replace(
                    "{currentRank}", _statusFetcher.Status.RankValue.ToString()
                ).Replace(
                    "{capRank}", _statusFetcher.Status.RankCapValue.ToString()
                ).Replace(
                    "{capRankMax}", _statusFetcher.Model.MaxRankCap.ToString()
                ));
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2ExperienceStatusLabel
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
    
    public partial class Gs2ExperienceStatusLabel
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ExperienceStatusLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExperienceStatusLabel
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