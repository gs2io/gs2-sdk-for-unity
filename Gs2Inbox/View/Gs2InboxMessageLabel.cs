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
using Gs2.Unity.UiKit.Gs2Inbox.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inbox
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Inbox/Gs2InboxMessageLabel")]
    public partial class Gs2InboxMessageLabel : MonoBehaviour
    {
        private DateTime? _receivedAt = null;
        private DateTime? _expiredAt = null;
        
        public void Update()
        {
            if (_messageFetcher.Fetched && _receivedAt == null)
            {
                _receivedAt = UnixTime.FromUnixTime(_messageFetcher.Message.ReceivedAt).ToLocalTime();
                _expiredAt = UnixTime.FromUnixTime(_messageFetcher.Message.ExpiresAt).ToLocalTime();
            }

            if (_receivedAt != null)
            {
                onUpdate.Invoke(format.Replace(
                    "{metadata}", _messageFetcher.Message.Metadata
                ).Replace(
                    "{r:yyyy}", _receivedAt.Value.ToString("yyyy")
                ).Replace(
                    "{r:yy}", _receivedAt.Value.ToString("yy")
                ).Replace(
                    "{r:MM}", _receivedAt.Value.ToString("MM")
                ).Replace(
                    "{r:MMM}", _receivedAt.Value.ToString("MMM")
                ).Replace(
                    "{r:dd}", _receivedAt.Value.ToString("dd")
                ).Replace(
                    "{r:hh}", _receivedAt.Value.ToString("hh")
                ).Replace(
                    "{r:HH}", _receivedAt.Value.ToString("HH")
                ).Replace(
                    "{r:tt}", _receivedAt.Value.ToString("tt")
                ).Replace(
                    "{r:mm}", _receivedAt.Value.ToString("mm")
                ).Replace(
                    "{r:ss}", _receivedAt.Value.ToString("ss")
                ).Replace(
                    "{e:yyyy}", _expiredAt?.ToString("yyyy") ?? ""
                ).Replace(
                    "{e:yy}", _expiredAt?.ToString("yy") ?? ""
                ).Replace(
                    "{e:MM}", _expiredAt?.ToString("MM") ?? ""
                ).Replace(
                    "{e:MMM}", _expiredAt?.ToString("MMM") ?? ""
                ).Replace(
                    "{e:dd}", _expiredAt?.ToString("dd") ?? ""
                ).Replace(
                    "{e:hh}", _expiredAt?.ToString("hh") ?? ""
                ).Replace(
                    "{e:HH}", _expiredAt?.ToString("HH") ?? ""
                ).Replace(
                    "{e:tt}", _expiredAt?.ToString("tt") ?? ""
                ).Replace(
                    "{e:mm}", _expiredAt?.ToString("mm") ?? ""
                ).Replace(
                    "{e:ss}", _expiredAt?.ToString("ss") ?? ""
                ));
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2InboxMessageLabel
    {
        private Gs2InboxMessageFetcher _messageFetcher;

        public void Awake()
        {
            _messageFetcher = GetComponentInParent<Gs2InboxMessageFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2InboxMessageLabel
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2InboxMessageLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InboxMessageLabel
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