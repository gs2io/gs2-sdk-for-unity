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
using Gs2.Unity.Gs2Account.Model;
using Gs2.Unity.Gs2Account.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Account.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Account
{
    [AddComponentMenu("GS2 UIKit/Account/Gs2AccountTakeOverAddAction")]
    public partial class Gs2AccountTakeOverAddAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => _clientHolder.Initialized);

            var future = _clientHolder.Gs2.Account.Namespace(
                _takeOverFetcher.takeOver.Namespace.namespaceName
            ).Me(
                _gameSessionHolder.GameSession
            ).TakeOver(
                _takeOverFetcher.takeOver.type
            ).AddTakeOverSetting(
                userIdentifier,
                password
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
                        onAddComplete.Invoke(Namespace, future.Result.UserId, future.Result.Type ?? -1);
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }

                onError.Invoke(future.Error, null);
                yield break;
            }
            onAddComplete.Invoke(Namespace, future.Result.UserId, future.Result.Type ?? -1);
        }
        
        public void OnEnable()
        {
            Debug.Log("Enable");
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
    
    public partial class Gs2AccountTakeOverAddAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2AccountTakeOverFetcher _takeOverFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _takeOverFetcher = GetComponentInParent<Gs2AccountTakeOverFetcher>() ?? GetComponent<Gs2AccountTakeOverFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2AccountTakeOverAddAction
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2AccountTakeOverAddAction
    {
        public Namespace Namespace;
        
        public string userIdentifier;
        public string password;

        public void UserIdentifier(string value)
        {
            userIdentifier = value;
        }
        public void Password(string value)
        {
            password = value;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AccountTakeOverAddAction
    {
        [Serializable]
        private class AddCompleteEvent : UnityEvent<Namespace, string, int>
        {
            
        }
        
        [SerializeField]
        private AddCompleteEvent onAddComplete = new AddCompleteEvent();
        
        public event UnityAction<Namespace, string, int> OnAddComplete
        {
            add => onAddComplete.AddListener(value);
            remove => onAddComplete.RemoveListener(value);
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