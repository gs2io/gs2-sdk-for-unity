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
    [AddComponentMenu("GS2 UIKit/Friend/Gs2FriendProfileUpdate")]
    public partial class Gs2FriendProfileUpdate : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var future = _clientHolder.Gs2.Friend.Namespace(
                Namespace.namespaceName
            ).Me(
                _gameSessionHolder.GameSession
            ).Profile().UpdateProfile(
                publicProfile,
                followerProfile,
                friendProfile
            );
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
                        onProfileComplete.Invoke(this);
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }

                onError.Invoke(future.Error, null);
                yield break;
            }
            onProfileComplete.Invoke(this);
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
    
    public partial class Gs2FriendProfileUpdate
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FriendProfileUpdate
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2FriendProfileUpdate
    {
        public Namespace Namespace;
        public string publicProfile;
        public string followerProfile;
        public string friendProfile;

        public void PublicProfile(string value)
        {
            publicProfile = value;
        }
        public void FollowerProfile(string value)
        {
            followerProfile = value;
        }
        public void FriendProfile(string value)
        {
            friendProfile = value;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendProfileUpdate
    {
        [Serializable]
        private class ProfileCompleteEvent : UnityEvent<Gs2FriendProfileUpdate>
        {
            
        }
        
        [SerializeField]
        private ProfileCompleteEvent onProfileComplete = new ProfileCompleteEvent();
        
        public event UnityAction<Gs2FriendProfileUpdate> OnProfileComplete
        {
            add => onProfileComplete.AddListener(value);
            remove => onProfileComplete.RemoveListener(value);
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