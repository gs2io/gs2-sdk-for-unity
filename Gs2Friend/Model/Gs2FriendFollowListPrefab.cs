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
    [RequireComponent(typeof(Gs2FriendFollowListFetcher))]
    [AddComponentMenu("GS2 UIKit/Friend/Gs2FriendFollowListPrefab")]
    public partial class Gs2FriendFollowListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2FriendFollowPrefab> _cache = new Dictionary<string, Gs2FriendFollowPrefab>();

        public void Update()
        {
            if (_friendListFetcher.Follows != null)
            {
                var activeNames = _friendListFetcher.Follows.Select(v => v.UserId);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteFriend.Invoke(instantiateName);
                    }
                }

                foreach (var friendUser in _friendListFetcher.Follows)
                {
                    if (!_cache.ContainsKey(friendUser.UserId))
                    {
                        var item = Instantiate(followPrefab, populateNode);
                        item.name = friendUser.UserId;
                        item.Set(Namespace, friendUser.UserId);
                        item.gameObject.SetActive(true);
                        _cache[friendUser.UserId] = item;
                        onCreateFriend.Invoke(friendUser);
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
    
    public partial class Gs2FriendFollowListPrefab
    {
        private Gs2FriendFollowListFetcher _friendListFetcher;

        public void Awake()
        {
            _friendListFetcher = GetComponent<Gs2FriendFollowListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FriendFollowListPrefab
    {
        public Namespace Namespace
        {
            get => _friendListFetcher.Namespace;
            set => _friendListFetcher.Namespace = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2FriendFollowListPrefab
    {
        public Gs2FriendFollowPrefab followPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendFollowListPrefab
    {
        [Serializable]
        private class CreateFriendEvent : UnityEvent<EzFollowUser>
        {
            
        }
        
        [SerializeField]
        private CreateFriendEvent onCreateFriend = new CreateFriendEvent();
        
        public event UnityAction<EzFollowUser> OnCreateFriend
        {
            add => onCreateFriend.AddListener(value);
            remove => onCreateFriend.RemoveListener(value);
        }

        [Serializable]
        private class DeleteFriendEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private DeleteFriendEvent onDeleteFriend = new DeleteFriendEvent();
        
        public event UnityAction<string> OnDeleteFriend
        {
            add => onDeleteFriend.AddListener(value);
            remove => onDeleteFriend.RemoveListener(value);
        }
    }
}