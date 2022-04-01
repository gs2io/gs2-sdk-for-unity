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
using Gs2.Unity.Gs2Inbox.Model;
using Gs2.Unity.Gs2Inbox.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inbox.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inbox
{
    [RequireComponent(typeof(Gs2InboxMessageFetcher))]
    [AddComponentMenu("GS2 UIKit/Inbox/Gs2InboxMessagePrefab")]
    public partial class Gs2InboxMessagePrefab : MonoBehaviour
    {
        private Message _message;
        
        public void Set(
            Namespace namespace_,
            EzMessage message
        )
        {
            _message = ScriptableObject.CreateInstance<Message>();
            _message.Namespace = namespace_;
            _message.messageName = message.Name;
            Message = _message;
        }

        public void OnDestroy()
        {
            if (_message != null)
            {
                Destroy(_message);
                _message = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2InboxMessagePrefab
    {
        private Gs2InboxMessageFetcher _messageFetcher;

        public void Awake()
        {
            _messageFetcher = GetComponent<Gs2InboxMessageFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2InboxMessagePrefab
    {
        public Message Message
        {
            get => _messageFetcher.message;
            set => _messageFetcher.message = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2InboxMessagePrefab
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InboxMessagePrefab
    {
        
    }
}