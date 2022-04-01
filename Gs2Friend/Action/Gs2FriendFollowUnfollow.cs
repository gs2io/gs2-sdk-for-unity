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
using System.Collections;
using Gs2.Core.Exception;
using Gs2.Unity.Gs2Friend.Model;
using Gs2.Unity.Gs2Friend.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Friend
{
    [AddComponentMenu("GS2 UIKit/Friend/Gs2FriendFollowUnfollow")]
    public partial class Gs2FriendFollowUnfollow : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => _followFetcher.Fetched);
                
            var future = _clientHolder.Gs2.Friend.Namespace(
                _followFetcher.follow.Namespace.namespaceName
            ).Me(
                _gameSessionHolder.GameSession
            ).FollowUser(
                _followFetcher.follow.userId
            ).Unfollow();
            yield return future;
            if (future.Error != null)
            {
                if (future.Error is TransactionException e)
                {
                    IEnumerator Retry()
                    {
                        var retryFuture = e.Retry();
                        yield return retryFuture;
                        if (retryFuture.Error != null)
                        {
                            onError.Invoke(future.Error, Retry);
                            yield break;
                        }
                        onUnfollowComplete.Invoke(this);
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }

                onError.Invoke(future.Error, null);
                yield break;
            }
            onUnfollowComplete.Invoke(this);
        }
        
        public void OnEnable()
        {
            StartCoroutine(nameof(Process));
        }
        
        public void OnDisable()
        {
            StopCoroutine(nameof(Process));
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2FriendFollowUnfollow
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2FriendFollowFetcher _followFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _followFetcher = GetComponentInParent<Gs2FriendFollowFetcher>() ?? GetComponent<Gs2FriendFollowFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FriendFollowUnfollow
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
    
    public partial class Gs2FriendFollowUnfollow
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendFollowUnfollow
    {
        [Serializable]
        private class UnfollowCompleteEvent : UnityEvent<Gs2FriendFollowUnfollow>
        {
            
        }
        
        [SerializeField]
        private UnfollowCompleteEvent onUnfollowComplete = new UnfollowCompleteEvent();
        
        public event UnityAction<Gs2FriendFollowUnfollow> OnUnfollowComplete
        {
            add => onUnfollowComplete.AddListener(value);
            remove => onUnfollowComplete.RemoveListener(value);
        }

        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();
        
        public event UnityAction<Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}