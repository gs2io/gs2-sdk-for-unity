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
using Gs2.Unity.UiKit.Gs2Chat.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Chat
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Chat/Gs2ChatMessageLabel")]
    public partial class Gs2ChatMessageLabel : MonoBehaviour
    {
        private DateTime? _createdAt = null;
        
        public void Update()
        {
            if (_messageFetcher.Fetched && _createdAt == null)
            {
                _createdAt = UnixTime.FromUnixTime(_messageFetcher.Message.CreatedAt).ToLocalTime();
            }

            if (_createdAt != null)
            {
                onUpdate.Invoke(format.Replace(
                    "{metadata}", _messageFetcher.Message.Metadata
                ).Replace(
                    "{yyyy}", _createdAt.Value.ToString("yyyy")
                ).Replace(
                    "{yy}", _createdAt.Value.ToString("yy")
                ).Replace(
                    "{MM}", _createdAt.Value.ToString("MM")
                ).Replace(
                    "{MMM}", _createdAt.Value.ToString("MMM")
                ).Replace(
                    "{dd}", _createdAt.Value.ToString("dd")
                ).Replace(
                    "{hh}", _createdAt.Value.ToString("hh")
                ).Replace(
                    "{HH}", _createdAt.Value.ToString("HH")
                ).Replace(
                    "{tt}", _createdAt.Value.ToString("tt")
                ).Replace(
                    "{mm}", _createdAt.Value.ToString("mm")
                ).Replace(
                    "{ss}", _createdAt.Value.ToString("ss")
                ));
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2ChatMessageLabel
    {
        private Gs2ChatMessageFetcher _messageFetcher;

        public void Awake()
        {
            _messageFetcher = GetComponentInParent<Gs2ChatMessageFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ChatMessageLabel
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ChatMessageLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ChatMessageLabel
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