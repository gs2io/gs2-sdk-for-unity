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
using Gs2.Unity.Gs2Chat.Model;
using Gs2.Unity.Gs2Chat.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Chat.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Chat
{
	[AddComponentMenu("GS2 UIKit/Chat/Gs2ChatRoomSubscribeAction")]
    public partial class Gs2ChatRoomSubscribeAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var future = _clientHolder.Gs2.Chat.Namespace(
                _roomFetcher.room.Namespace.namespaceName
            ).Me(
                _gameSessionHolder.GameSession
            ).Subscribe(
                _roomFetcher.room.roomName
            ).Subscribe();
            yield return future;
            if (future.Error != null)
            {
                if (future.Error is BadRequestException)
                {
                    
                }
                else if (future.Error is TransactionException e)
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
                        onSubscribeComplete.Invoke(_roomFetcher.room);
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }
                else
                {
                    onError.Invoke(future.Error, null);
                    yield break;
                }
            }
            onSubscribeComplete.Invoke(_roomFetcher.room);
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
    
    public partial class Gs2ChatRoomSubscribeAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2ChatRoomFetcher _roomFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _roomFetcher = GetComponentInParent<Gs2ChatRoomFetcher>() ?? GetComponent<Gs2ChatRoomFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ChatRoomSubscribeAction
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ChatRoomSubscribeAction
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ChatRoomSubscribeAction
    {
        [Serializable]
        private class SubscribeCompleteEvent : UnityEvent<Room>
        {
            
        }
        
        [SerializeField]
        private SubscribeCompleteEvent onSubscribeComplete = new SubscribeCompleteEvent();
        
        public event UnityAction<Room> OnSubscribeComplete
        {
            add => onSubscribeComplete.AddListener(value);
            remove => onSubscribeComplete.RemoveListener(value);
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