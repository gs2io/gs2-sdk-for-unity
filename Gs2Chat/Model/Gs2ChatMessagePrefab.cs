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
using Gs2.Unity.Gs2Chat.Model;
using Gs2.Unity.Gs2Chat.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Chat.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Chat
{
    /// <summary>
    /// Main
    /// </summary>

    [RequireComponent(typeof(Gs2ChatMessageFetcher))]
	[AddComponentMenu("GS2 UIKit/Chat/Gs2ChatMessagePrefab")]
    public partial class Gs2ChatMessagePrefab : MonoBehaviour
    {
        private Message _message;
        
        public void Set(
            Room room,
            string messageName
        )
        {
            _message = ScriptableObject.CreateInstance<Message>();
            _message.room = room;
            _message.messageName = messageName;
            
            var messageFetcher = GetComponent<Gs2ChatMessageFetcher>();
            messageFetcher.message = _message;
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
    
    public partial class Gs2ChatMessagePrefab
    {
        private Gs2ChatMessageFetcher _messageFetcher;

        public void Awake()
        {
            _messageFetcher = GetComponent<Gs2ChatMessageFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ChatMessagePrefab
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
    
    public partial class Gs2ChatMessagePrefab
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ChatMessagePrefab
    {
        
    }
}