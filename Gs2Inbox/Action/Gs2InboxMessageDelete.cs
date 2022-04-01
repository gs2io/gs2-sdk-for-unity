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
using Gs2.Unity.Gs2Inbox.Model;
using Gs2.Unity.Gs2Inbox.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inbox.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inbox
{
    [AddComponentMenu("GS2 UIKit/Inbox/Gs2InboxMessageDelete")]
    public partial class Gs2InboxMessageDelete : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => _messageFetcher.Fetched);
                
            var future = _clientHolder.Gs2.Inbox.Namespace(
                _messageFetcher.message.Namespace.namespaceName
            ).Me(
                _gameSessionHolder.GameSession
            ).Message(
                _messageFetcher.message.messageName
            ).Delete();
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
                        onDeleteComplete.Invoke(this);
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }

                onError.Invoke(future.Error, null);
                yield break;
            }
            onDeleteComplete.Invoke(this);
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
    
    public partial class Gs2InboxMessageDelete
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2InboxMessageFetcher _messageFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _messageFetcher = GetComponentInParent<Gs2InboxMessageFetcher>() ?? GetComponent<Gs2InboxMessageFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2InboxMessageDelete
    {
        public Message Message
        {
            get => _messageFetcher.message;
            set => _messageFetcher.message = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2InboxMessageDelete
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InboxMessageDelete
    {
        [Serializable]
        private class DeleteCompleteEvent : UnityEvent<Gs2InboxMessageDelete>
        {
            
        }
        
        [SerializeField]
        private DeleteCompleteEvent onDeleteComplete = new DeleteCompleteEvent();
        
        public event UnityAction<Gs2InboxMessageDelete> OnDeleteComplete
        {
            add => onDeleteComplete.AddListener(value);
            remove => onDeleteComplete.RemoveListener(value);
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