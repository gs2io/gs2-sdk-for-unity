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
using Gs2.Unity.Gs2Inbox.Model;
using Gs2.Unity.Gs2Inbox.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inbox.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inbox
{
    [RequireComponent(typeof(Gs2InboxMessageListFetcher))]
    [AddComponentMenu("GS2 UIKit/Inbox/Gs2InboxMessageListPrefab")]
    public partial class Gs2InboxMessageListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2InboxMessagePrefab> _cache = new Dictionary<string, Gs2InboxMessagePrefab>();

        public void Update()
        {
            if (_messageListFetcher.Fetched)
            {
                var activeNames = _messageListFetcher.Messages.Select(v => v.Name);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        var instance = _cache[instantiateName];
                        Destroy(instance.gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteInbox.Invoke(instance);
                    }
                }

                foreach (var message in _messageListFetcher.Messages)
                {
                    if (!_cache.ContainsKey(message.Name) && message.IsRead)
                    {
                        var item = Instantiate(messagePrefab, populateNode);
                        item.name = message.Name;
                        item.gameObject.SetActive(true);
                        item.Set(_messageListFetcher.Namespace, message);
                        item.transform.SetAsLastSibling();
                        _cache[message.Name] = item;
                        onCreateInbox.Invoke(item, message);
                    }
                }

                foreach (var message in _messageListFetcher.Messages.ToArray().Reverse())
                {
                    if (!_cache.ContainsKey(message.Name) && !message.IsRead)
                    {
                        var item = Instantiate(messagePrefab, populateNode);
                        item.name = message.Name;
                        item.gameObject.SetActive(true);
                        item.Set(_messageListFetcher.Namespace, message);
                        item.transform.SetAsFirstSibling();
                        _cache[message.Name] = item;
                        onCreateInbox.Invoke(item, message);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2InboxMessageListPrefab
    {
        private Gs2InboxMessageListFetcher _messageListFetcher;

        public void Awake()
        {
            _messageListFetcher = GetComponentInParent<Gs2InboxMessageListFetcher>() ?? GetComponent<Gs2InboxMessageListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2InboxMessageListPrefab
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2InboxMessageListPrefab
    {
        public Gs2InboxMessagePrefab messagePrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InboxMessageListPrefab
    {
        [Serializable]
        private class CreateInboxEvent : UnityEvent<Gs2InboxMessagePrefab, EzMessage>
        {
            
        }
        
        [SerializeField]
        private CreateInboxEvent onCreateInbox = new CreateInboxEvent();
        
        public event UnityAction<Gs2InboxMessagePrefab, EzMessage> OnCreateInbox
        {
            add => onCreateInbox.AddListener(value);
            remove => onCreateInbox.RemoveListener(value);
        }

        [Serializable]
        private class DeleteInboxEvent : UnityEvent<Gs2InboxMessagePrefab>
        {
            
        }
        
        [SerializeField]
        private DeleteInboxEvent onDeleteInbox = new DeleteInboxEvent();
        
        public event UnityAction<Gs2InboxMessagePrefab> OnDeleteInbox
        {
            add => onDeleteInbox.AddListener(value);
            remove => onDeleteInbox.RemoveListener(value);
        }
    }
}