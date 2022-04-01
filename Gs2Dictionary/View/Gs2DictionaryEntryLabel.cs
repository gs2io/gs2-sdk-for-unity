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
using Gs2.Unity.UiKit.Gs2Dictionary.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Dictionary
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Dictionary/Gs2DictionaryEntryLabel")]
    public partial class Gs2DictionaryEntryLabel : MonoBehaviour
    {
        private DateTime? _acquiredAt = null;
        
        public void Update()
        {
            if (_entryFetcher.Fetched && _acquiredAt == null)
            {
                if (_entryFetcher.Entry != null)
                {
                    _acquiredAt = UnixTime.FromUnixTime(_entryFetcher.Entry.AcquiredAt).ToLocalTime();
                }
                else
                {
                    _acquiredAt = DateTime.Now;
                }
            }

            if (_entryFetcher.Fetched)
            {
                onUpdate.Invoke(format.Replace(
                    "{yyyy}", _acquiredAt.Value.ToString("yyyy") ?? ""
                ).Replace(
                    "{yy}", _acquiredAt.Value.ToString("yy") ?? ""
                ).Replace(
                    "{MM}", _acquiredAt.Value.ToString("MM") ?? ""
                ).Replace(
                    "{MMM}", _acquiredAt.Value.ToString("MMM") ?? ""
                ).Replace(
                    "{dd}", _acquiredAt.Value.ToString("dd") ?? ""
                ).Replace(
                    "{hh}", _acquiredAt.Value.ToString("hh") ?? ""
                ).Replace(
                    "{HH}", _acquiredAt.Value.ToString("HH") ?? ""
                ).Replace(
                    "{tt}", _acquiredAt.Value.ToString("tt") ?? ""
                ).Replace(
                    "{mm}", _acquiredAt.Value.ToString("mm") ?? ""
                ).Replace(
                    "{ss}", _acquiredAt.Value.ToString("ss") ?? ""
                ).Replace(
                    "{metadata}", _entryFetcher.Model.Metadata.ToString()
                ));
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2DictionaryEntryLabel
    {
        private Gs2DictionaryEntryFetcher _entryFetcher;

        public void Awake()
        {
            _entryFetcher = GetComponentInParent<Gs2DictionaryEntryFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2DictionaryEntryLabel
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2DictionaryEntryLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DictionaryEntryLabel
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