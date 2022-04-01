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
    [RequireComponent(typeof(Gs2FriendFollowFetcher))]
    [AddComponentMenu("GS2 UIKit/Friend/Gs2FriendFollowPrefab")]
    public partial class Gs2FriendFollowPrefab : MonoBehaviour
    {
        private Follow _follow;
        
        public void Set(
            Namespace namespace_,
            string userId
        )
        {
            _follow = ScriptableObject.CreateInstance<Follow>();
            _follow.Namespace = namespace_;
            _follow.userId = userId;
            
            var followFetcher = GetComponent<Gs2FriendFollowFetcher>();
            followFetcher.follow = _follow;
        }

        public void OnDestroy()
        {
            if (_follow != null)
            {
                Destroy(_follow);
                _follow = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2FriendFollowPrefab
    {
        private Gs2FriendFollowFetcher _followFetcher;

        public void Awake()
        {
            _followFetcher = GetComponent<Gs2FriendFollowFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FriendFollowPrefab
    {
        public Follow Follow
        {
            get => _followFetcher.follow;
            set => _followFetcher.follow = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2FriendFollowPrefab
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendFollowPrefab
    {
        
    }
}