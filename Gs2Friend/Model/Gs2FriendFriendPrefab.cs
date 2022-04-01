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
    [RequireComponent(typeof(Gs2FriendFriendFetcher))]
    [AddComponentMenu("GS2 UIKit/Friend/Gs2FriendFriendPrefab")]
    public partial class Gs2FriendFriendPrefab : MonoBehaviour
    {
        private Friend _friend;
        
        public void Set(
            Namespace namespace_,
            string userId
        )
        {
            _friend = ScriptableObject.CreateInstance<Friend>();
            _friend.Namespace = namespace_;
            _friend.userId = userId;
            
            var friendFetcher = GetComponent<Gs2FriendFriendFetcher>();
            friendFetcher.friend = _friend;
        }

        public void OnDestroy()
        {
            if (_friend != null)
            {
                Destroy(_friend);
                _friend = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2FriendFriendPrefab
    {
        private Gs2FriendFriendFetcher _friendFetcher;

        public void Awake()
        {
            _friendFetcher = GetComponent<Gs2FriendFriendFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FriendFriendPrefab
    {
        public Friend Friend
        {
            get => _friendFetcher.friend;
            set => _friendFetcher.friend = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2FriendFriendPrefab
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendFriendPrefab
    {
        
    }
}