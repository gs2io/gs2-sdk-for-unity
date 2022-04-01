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
using Gs2.Unity.Gs2Auth.Model;
using Gs2.Unity.Gs2Key.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;
using ErrorEvent = Gs2.Unity.UiKit.Core.ErrorEvent;

namespace Gs2.Unity.UiKit.Gs2Auth
{
	[AddComponentMenu("GS2 UIKit/Auth/Gs2AuthLoginAction")]
    public partial class Gs2AuthLoginAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var future = _clientHolder.Gs2.Auth.AccessToken(
            ).Login(
                userId,
                key.Grn,
                body,
                signature
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
                        
                        var future2 = future.Result.Model();
                        yield return future2;
                        
                        onLoginComplete.Invoke(future2.Result);
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }

                onError.Invoke(future.Error, null);
                yield break;
            }
            
            var future3 = future.Result.Model();
            yield return future3;
            
            onLoginComplete.Invoke(future3.Result);
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
    
    public partial class Gs2AuthLoginAction
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
    
    public partial class Gs2AuthLoginAction
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2AuthLoginAction
    {
        public Key key;
        public string userId;
        public string body;
        public string signature;

        public void Set(
            string userId,
            string body,
            string signature
        )
        {
            this.userId = userId;
            this.body = body;
            this.signature = signature;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AuthLoginAction
    {
        [Serializable]
        private class LoginCompleteEvent : UnityEvent<EzAccessToken>
        {
            
        }
        
        [SerializeField]
        private LoginCompleteEvent onLoginComplete = new LoginCompleteEvent();
        
        public event UnityAction<EzAccessToken> OnLoginComplete
        {
            add => onLoginComplete.AddListener(value);
            remove => onLoginComplete.RemoveListener(value);
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