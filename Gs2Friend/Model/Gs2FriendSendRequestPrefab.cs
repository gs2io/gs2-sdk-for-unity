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
using Gs2.Unity.Gs2Friend.Model;
using Gs2.Unity.Gs2Friend.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Friend
{
    [RequireComponent(typeof(Gs2FriendSendRequestFetcher))]
    [AddComponentMenu("GS2 UIKit/Friend/Gs2FriendSendRequestPrefab")]
    public partial class Gs2FriendSendRequestPrefab : MonoBehaviour
    {
        private SendFriendRequest _sendFriendRequest;
        private User _user;
        
        public void Set(
            Namespace namespace_,
            string userId
        )
        {
            _sendFriendRequest = ScriptableObject.CreateInstance<SendFriendRequest>();
            _sendFriendRequest.Namespace = namespace_;
            _sendFriendRequest.userId = userId;

            _user = ScriptableObject.CreateInstance<User>();
            _user.Namespace = namespace_;
            _user.userId = userId;

            var friendSendRequestFetcher = GetComponent<Gs2FriendSendRequestFetcher>();
            friendSendRequestFetcher.sendFriendRequest = _sendFriendRequest;

            var publicProfileFetcher = GetComponent<Gs2FriendPublicProfileFetcher>();
            if (publicProfileFetcher != null)
            {
                publicProfileFetcher.user = _user;
            }
        }

        public void OnDestroy()
        {
            if (_sendFriendRequest != null)
            {
                Destroy(_sendFriendRequest);
                _sendFriendRequest = null;
            }
            if (_user != null)
            {
                Destroy(_user);
                _user = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2FriendSendRequestPrefab
    {
        private Gs2FriendSendRequestFetcher _friendSendRequestFetcher;

        public void Awake()
        {
            _friendSendRequestFetcher = GetComponent<Gs2FriendSendRequestFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FriendSendRequestPrefab
    {
        public SendFriendRequest SendFriendRequest
        {
            get => _friendSendRequestFetcher.sendFriendRequest;
            set => _friendSendRequestFetcher.sendFriendRequest = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2FriendSendRequestPrefab
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendSendRequestPrefab
    {
        
    }
}