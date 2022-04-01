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
    [AddComponentMenu("GS2 UIKit/Friend/Gs2FriendReceiveRequestAccept")]
    public partial class Gs2FriendReceiveRequestAccept : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => _receiveRequestFetcher.Fetched);
                
            var future = _clientHolder.Gs2.Friend.Namespace(
                _receiveRequestFetcher.receiveFriendRequest.Namespace.namespaceName
            ).Me(
                _gameSessionHolder.GameSession
            ).ReceiveFriendRequest(
                _receiveRequestFetcher.receiveFriendRequest.userId
            ).Accept();
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
                        onAcceptComplete.Invoke(this);
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }

                onError.Invoke(future.Error, null);
                yield break;
            }
            onAcceptComplete.Invoke(this);
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
    
    public partial class Gs2FriendReceiveRequestAccept
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2FriendReceiveRequestFetcher _receiveRequestFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _receiveRequestFetcher = GetComponentInParent<Gs2FriendReceiveRequestFetcher>() ?? GetComponent<Gs2FriendReceiveRequestFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FriendReceiveRequestAccept
    {
        public ReceiveFriendRequest ReceiveFriendRequest
        {
            get => _receiveRequestFetcher.receiveFriendRequest;
            set => _receiveRequestFetcher.receiveFriendRequest = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2FriendReceiveRequestAccept
    {
        public bool withProfile;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendReceiveRequestAccept
    {
        [Serializable]
        private class AcceptCompleteEvent : UnityEvent<Gs2FriendReceiveRequestAccept>
        {
            
        }
        
        [SerializeField]
        private AcceptCompleteEvent onAcceptComplete = new AcceptCompleteEvent();
        
        public event UnityAction<Gs2FriendReceiveRequestAccept> OnAcceptComplete
        {
            add => onAcceptComplete.AddListener(value);
            remove => onAcceptComplete.RemoveListener(value);
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