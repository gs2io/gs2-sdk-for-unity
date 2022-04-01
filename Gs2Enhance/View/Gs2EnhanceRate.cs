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
using Gs2.Unity.Gs2Enhance.Model;
using Gs2.Unity.Gs2Enhance.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Enhance.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Enhance
{
    [RequireComponent(typeof(Gs2EnhanceRateFetcher))]
    [AddComponentMenu("GS2 UIKit/Enhance/Gs2EnhanceRate")]
    public partial class Gs2EnhanceRate : MonoBehaviour
    {
        private Rate _rate;
        
        public void Set(
            Namespace namespace_,
            string rateName
        )
        {
            _rate = ScriptableObject.CreateInstance<Rate>();
            _rate.Namespace = namespace_;
            _rate.rateName = rateName;
            _experienceFetcher.rate = _rate;
            Rate = _rate;
        }

        public void OnDestroy()
        {
            if (_rate != null)
            {
                Destroy(_rate);
                _rate = null;
            }
        }

        public void OnClick()
        {
            onSelect.Invoke(_experienceFetcher.RateModel);
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2EnhanceRate
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2EnhanceRateFetcher _experienceFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _experienceFetcher = GetComponent<Gs2EnhanceRateFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2EnhanceRate
    {
        public Rate Rate
        {
            get => _experienceFetcher.rate;
            set => _experienceFetcher.rate = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2EnhanceRate
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnhanceRate
    {
        [Serializable]
        private class SelectEvent : UnityEvent<EzRateModel>
        {
            
        }
        
        [SerializeField]
        private SelectEvent onSelect = new SelectEvent();
        
        public event UnityAction<EzRateModel> OnSelect
        {
            add => onSelect.AddListener(value);
            remove => onSelect.RemoveListener(value);
        }
    }
}