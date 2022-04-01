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

    [RequireComponent(typeof(Gs2FormationFormFetcher))]
    [AddComponentMenu("GS2 UIKit/Formation/Gs2FormationFormPrefab")]
    public partial class Gs2FormationFormPrefab : MonoBehaviour
    {
        private Form _form;
        
        public void Set(
            Mold mold,
            int index
        )
        {
            _form = ScriptableObject.CreateInstance<Form>();
            _form.mold = mold;
            _form.index = index;
            
            var formFetcher = GetComponent<Gs2FormationFormFetcher>();
            formFetcher.form = _form;
        }

        public void OnDestroy()
        {
            if (_form != null)
            {
                Destroy(_form);
                _form = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2FormationFormPrefab
    {
        private Gs2FormationFormFetcher _formFetcher;

        public void Awake()
        {
            _formFetcher = GetComponent<Gs2FormationFormFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FormationFormPrefab
    {
        public Form Form
        {
            get => _formFetcher.form;
            set => _formFetcher.form = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2FormationFormPrefab
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationFormPrefab
    {
        
    }
}