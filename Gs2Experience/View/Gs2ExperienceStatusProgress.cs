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
using Gs2.Unity.Gs2Experience.Model;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Experience
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Experience/Gs2ExperienceStatusProgress")]
    public partial class Gs2ExperienceStatusProgress : MonoBehaviour
    {
        public void Update()
        {
            if (_statusFetcher.Fetched)
            {
                var prevRankUpExperience = _statusFetcher.Model.NextRankUpExperienceValue(_statusFetcher.Status.RankValue-1, _statusFetcher.Status.RankCapValue) ?? 0;
                var nextRankUpExperience = _statusFetcher.Model.NextRankUpExperienceValue(_statusFetcher.Status.RankValue, _statusFetcher.Status.RankCapValue);
                if (nextRankUpExperience == null)
                {
                    onUpdate.Invoke(1);
                    onUpdateInverse.Invoke(0);
                }
                else
                {
                    var currentStep = _statusFetcher.Status.ExperienceValue - prevRankUpExperience;
                    var nextStep = nextRankUpExperience.Value - prevRankUpExperience;
                    onUpdate.Invoke(Math.Min(1.0f, (float) currentStep / nextStep));
                    onUpdateInverse.Invoke(1.0f - Math.Min(1.0f, (float) currentStep / nextStep));
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2ExperienceStatusProgress
    {
        private Gs2ExperienceStatusFetcher _statusFetcher;

        public void Awake()
        {
            _statusFetcher = GetComponentInParent<Gs2ExperienceStatusFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ExperienceStatusProgress
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ExperienceStatusProgress
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExperienceStatusProgress
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
        
        [Serializable]
        private class UpdateInverseEvent : UnityEvent<float>
        {
            
        }
        
        [SerializeField]
        private UpdateInverseEvent onUpdateInverse = new UpdateInverseEvent();
        
        public event UnityAction<float> OnUpdateInverse
        {
            add => onUpdateInverse.AddListener(value);
            remove => onUpdateInverse.RemoveListener(value);
        }
    }
}