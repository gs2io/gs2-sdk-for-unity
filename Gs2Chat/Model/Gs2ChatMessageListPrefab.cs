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
using System.Collections.Generic;
using System.Linq;
using Gs2.Unity.Gs2Chat.Model;
using Gs2.Unity.Gs2Chat.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Chat.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Chat
{
    [RequireComponent(typeof(Gs2ChatMessageListFetcher))]
	[AddComponentMenu("GS2 UIKit/Chat/Gs2ChatMessageListPrefab")]
    public partial class Gs2ChatMessageListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2ChatMessagePrefab> _cache = new Dictionary<string, Gs2ChatMessagePrefab>();

        public void Update()
        {
            if (_messageListFetcher.Messages != null)
            {
                var activeNames = _messageListFetcher.Messages.Select(v => v.Name);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteChat.Invoke(instantiateName);
                    }
                }

                foreach (var status in _messageListFetcher.Messages)
                {
                    if (!_cache.ContainsKey(status.Name))
                    {
                        var item = Instantiate(messagePrefab, populateNode);
                        item.name = status.Name;
                        item.Set(Room, status.Name);
                        item.gameObject.SetActive(true);
                        _cache[status.Name] = item;
                        onCreateChat.Invoke(status);
                    }
                }
            }
        }
        
        private Room _room;
        
        public void Set(
            Namespace @namespace,
            EzRoom room
        )
        {
            _room = ScriptableObject.CreateInstance<Room>();
            _room.Namespace = @namespace;
            _room.roomName = room.Name;
            
            var messageListFetcher = GetComponent<Gs2ChatMessageListFetcher>();
            messageListFetcher.room = _room;
            var roomFetcher = GetComponent<Gs2ChatRoomFetcher>();
            if (roomFetcher != null)
            {
                roomFetcher.room = _room;
            }
        }

        public void OnDestroy()
        {
            if (_room != null)
            {
                Destroy(_room);
                _room = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2ChatMessageListPrefab
    {
        private Gs2ChatMessageListFetcher _messageListFetcher;
        private Gs2ChatRoomFetcher _roomFetcher;

        public void Awake()
        {
            _messageListFetcher = GetComponent<Gs2ChatMessageListFetcher>();
            _roomFetcher = GetComponent<Gs2ChatRoomFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ChatMessageListPrefab
    {
        public Room Room
        {
            get => _messageListFetcher.room;
            set => _messageListFetcher.room = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ChatMessageListPrefab
    {
        public Gs2ChatMessagePrefab messagePrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ChatMessageListPrefab
    {
        [Serializable]
        private class CreateChatEvent : UnityEvent<EzMessage>
        {
            
        }
        
        [SerializeField]
        private CreateChatEvent onCreateChat = new CreateChatEvent();
        
        public event UnityAction<EzMessage> OnCreateChat
        {
            add => onCreateChat.AddListener(value);
            remove => onCreateChat.RemoveListener(value);
        }

        [Serializable]
        private class DeleteChatEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private DeleteChatEvent onDeleteChat = new DeleteChatEvent();
        
        public event UnityAction<string> OnDeleteChat
        {
            add => onDeleteChat.AddListener(value);
            remove => onDeleteChat.RemoveListener(value);
        }
    }
}