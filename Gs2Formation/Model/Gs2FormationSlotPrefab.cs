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
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Formation
{
    /// <summary>
    /// Main
    /// </summary>

    [RequireComponent(typeof(Gs2FormationSlotFetcher))]
    [AddComponentMenu("GS2 UIKit/Formation/Gs2FormationSlotPrefab")]
    public partial class Gs2FormationSlotPrefab : MonoBehaviour
    {
        private Slot _slot;
        
        public void Set(
            Form form,
            string slotName
        )
        {
            _slot = ScriptableObject.CreateInstance<Slot>();
            _slot.form = form;
            _slot.slotName = slotName;
            
            var slotFetcher = GetComponent<Gs2FormationSlotFetcher>();
            slotFetcher.slot = _slot;
        }

        public void OnDestroy()
        {
            if (_slot != null)
            {
                Destroy(_slot);
                _slot = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2FormationSlotPrefab
    {
        private Gs2FormationSlotFetcher _slotFetcher;

        public void Awake()
        {
            _slotFetcher = GetComponent<Gs2FormationSlotFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FormationSlotPrefab
    {
        public Slot Slot
        {
            get => _slotFetcher.slot;
            set => _slotFetcher.slot = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2FormationSlotPrefab
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationSlotPrefab
    {
        
    }
}