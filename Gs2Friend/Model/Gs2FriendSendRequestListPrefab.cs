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
    [RequireComponent(typeof(Gs2FriendSendRequestListFetcher))]
    [AddComponentMenu("GS2 UIKit/Friend/Gs2FriendSendRequestListPrefab")]
    public partial class Gs2FriendSendRequestListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2FriendSendRequestPrefab> _cache = new Dictionary<string, Gs2FriendSendRequestPrefab>();

        public void Update()
        {
            if (_sendRequestListFetcher.SendRequests != null)
            {
                var activeNames = _sendRequestListFetcher.SendRequests.Select(v => v.TargetUserId);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteSendRequest.Invoke(instantiateName);
                    }
                }

                foreach (var sendRequest in _sendRequestListFetcher.SendRequests)
                {
                    if (!_cache.ContainsKey(sendRequest.TargetUserId))
                    {
                        var item = Instantiate(sendRequestPrefab, populateNode);
                        item.name = sendRequest.TargetUserId;
                        item.Set(Namespace, sendRequest.TargetUserId);
                        item.gameObject.SetActive(true);
                        _cache[sendRequest.TargetUserId] = item;
                        onCreateSendRequest.Invoke(sendRequest);
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
    
    public partial class Gs2FriendSendRequestListPrefab
    {
        private Gs2FriendSendRequestListFetcher _sendRequestListFetcher;

        public void Awake()
        {
            _sendRequestListFetcher = GetComponent<Gs2FriendSendRequestListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FriendSendRequestListPrefab
    {
        public Namespace Namespace
        {
            get => _sendRequestListFetcher.Namespace;
            set => _sendRequestListFetcher.Namespace = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2FriendSendRequestListPrefab
    {
        public Gs2FriendSendRequestPrefab sendRequestPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendSendRequestListPrefab
    {
        [Serializable]
        private class CreateSendRequestEvent : UnityEvent<EzFriendRequest>
        {
            
        }
        
        [SerializeField]
        private CreateSendRequestEvent onCreateSendRequest = new CreateSendRequestEvent();
        
        public event UnityAction<EzFriendRequest> OnCreateSendRequest
        {
            add => onCreateSendRequest.AddListener(value);
            remove => onCreateSendRequest.RemoveListener(value);
        }

        [Serializable]
        private class DeleteSendRequestEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private DeleteSendRequestEvent onDeleteSendRequest = new DeleteSendRequestEvent();
        
        public event UnityAction<string> OnDeleteSendRequest
        {
            add => onDeleteSendRequest.AddListener(value);
            remove => onDeleteSendRequest.RemoveListener(value);
        }
    }
}