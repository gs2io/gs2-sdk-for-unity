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
using Gs2.Unity.Gs2Experience.Model;
using Gs2.Unity.Gs2Experience.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Experience
{
    [RequireComponent(typeof(Gs2ExperienceExperienceFetcher))]
    [AddComponentMenu("GS2 UIKit/Experience/Gs2ExperienceExperiencePrefab")]
    public partial class Gs2ExperienceExperiencePrefab : MonoBehaviour
    {
        private Experience _experience;
        
        public void Set(
            Namespace namespace_,
            string experienceName
        )
        {
            _experience = ScriptableObject.CreateInstance<Experience>();
            _experience.Namespace = namespace_;
            _experience.experienceName = experienceName;
            _experienceFetcher.experience = _experience;
            Experience = _experience;
        }

        public void OnDestroy()
        {
            if (_experience != null)
            {
                Destroy(_experience);
                _experience = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2ExperienceExperiencePrefab
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2ExperienceExperienceFetcher _experienceFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _experienceFetcher = GetComponent<Gs2ExperienceExperienceFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ExperienceExperiencePrefab
    {
        public Experience Experience
        {
            get => _experienceFetcher.experience;
            set => _experienceFetcher.experience = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ExperienceExperiencePrefab
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExperienceExperiencePrefab
    {
        
    }
}