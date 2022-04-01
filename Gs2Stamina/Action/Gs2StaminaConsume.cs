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
using Gs2.Unity.Gs2Stamina.Model;
using Gs2.Unity.Gs2Stamina.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Stamina.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Stamina
{
    [AddComponentMenu("GS2 UIKit/Stamina/Gs2StaminaConsume")]
    public partial class Gs2StaminaConsume : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => _staminaFetcher.Stamina != null);
            
            var future = _clientHolder.Gs2.Stamina.Namespace(
                Stamina.Namespace.namespaceName
            ).Me(
                _gameSessionHolder.GameSession
            ).Stamina(
                Stamina.staminaName
            ).Consume(
                consumeValue
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
                        onConsumeComplete.Invoke(_staminaFetcher.Stamina);
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }

                onError.Invoke(future.Error, null);
                yield break;
            }
            onConsumeComplete.Invoke(_staminaFetcher.Stamina);
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
    
    public partial class Gs2StaminaConsume
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2StaminaStaminaFetcher _staminaFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _staminaFetcher = GetComponentInParent<Gs2StaminaStaminaFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2StaminaConsume
    {
        public Stamina Stamina
        {
            get => _staminaFetcher.stamina;
            set => _staminaFetcher.stamina = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2StaminaConsume
    {
        public int consumeValue;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaConsume
    {
        [Serializable]
        private class ConsumeCompleteEvent : UnityEvent<EzStamina>
        {
            
        }
        
        [SerializeField]
        private ConsumeCompleteEvent onConsumeComplete = new ConsumeCompleteEvent();
        
        public event UnityAction<EzStamina> OnConsumeComplete
        {
            add => onConsumeComplete.AddListener(value);
            remove => onConsumeComplete.RemoveListener(value);
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