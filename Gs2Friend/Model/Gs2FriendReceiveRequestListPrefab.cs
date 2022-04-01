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
using Gs2.Unity.Gs2Friend.Model;
using Gs2.Unity.Gs2Friend.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Friend
{
    [RequireComponent(typeof(Gs2FriendReceiveRequestListFetcher))]
    [AddComponentMenu("GS2 UIKit/Friend/Gs2FriendReceiveRequestListPrefab")]
    public partial class Gs2FriendReceiveRequestListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2FriendReceiveRequestPrefab> _cache = new Dictionary<string, Gs2FriendReceiveRequestPrefab>();

        public void Update()
        {
            if (_messageListFetcher.ReceiveRequests != null)
            {
                var activeNames = _messageListFetcher.ReceiveRequests.Select(v => v.UserId);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteReceiveRequest.Invoke(instantiateName);
                    }
                }

                foreach (var inventory in _messageListFetcher.ReceiveRequests)
                {
                    if (!_cache.ContainsKey(inventory.UserId))
                    {
                        var item = Instantiate(receiveRequestPrefab, populateNode);
                        item.name = inventory.UserId;
                        item.Set(Namespace, inventory.UserId);
                        item.gameObject.SetActive(true);
                        _cache[inventory.UserId] = item;
                        onCreateReceiveRequest.Invoke(inventory);
                    }
                }
            }
        }

        public void OnDestroy()
        {
            foreach (var component in _cache.Values)
            {
                Destroy(component);
            }
            _cache.Clear();
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2FriendReceiveRequestListPrefab
    {
        private Gs2FriendReceiveRequestListFetcher _messageListFetcher;

        public void Awake()
        {
            _messageListFetcher = GetComponent<Gs2FriendReceiveRequestListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FriendReceiveRequestListPrefab
    {
        public Namespace Namespace
        {
            get => _messageListFetcher.Namespace;
            set => _messageListFetcher.Namespace = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2FriendReceiveRequestListPrefab
    {
        public Gs2FriendReceiveRequestPrefab receiveRequestPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendReceiveRequestListPrefab
    {
        [Serializable]
        private class CreateReceiveRequestEvent : UnityEvent<EzFriendRequest>
        {
            
        }
        
        [SerializeField]
        private CreateReceiveRequestEvent onCreateReceiveRequest = new CreateReceiveRequestEvent();
        
        public event UnityAction<EzFriendRequest> OnCreateReceiveRequest
        {
            add => onCreateReceiveRequest.AddListener(value);
            remove => onCreateReceiveRequest.RemoveListener(value);
        }

        [Serializable]
        private class DeleteReceiveRequestEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private DeleteReceiveRequestEvent onDeleteReceiveRequest = new DeleteReceiveRequestEvent();
        
        public event UnityAction<string> OnDeleteReceiveRequest
        {
            add => onDeleteReceiveRequest.AddListener(value);
            remove => onDeleteReceiveRequest.RemoveListener(value);
        }
    }
}