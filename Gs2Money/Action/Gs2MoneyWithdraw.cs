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
using Gs2.Unity.Gs2Money.Model;
using Gs2.Unity.Gs2Money.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Money.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Money
{
    [AddComponentMenu("GS2 UIKit/Money/Gs2MoneyWithdraw")]
    public partial class Gs2MoneyWithdraw : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => _walletFetcher.Fetched);
            
            var future = _clientHolder.Gs2.Money.Namespace(
                Wallet.Namespace.namespaceName
            ).Me(
                _gameSessionHolder.GameSession
            ).Wallet(
                Wallet.slot
            ).Withdraw(
                withdrawAmount,
                paidOnly
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
                        onWithdrawComplete.Invoke(_walletFetcher.Wallet);
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }

                onError.Invoke(future.Error, null);
                yield break;
            }
            onWithdrawComplete.Invoke(_walletFetcher.Wallet);
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
    
    public partial class Gs2MoneyWithdraw
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2MoneyWalletFetcher _walletFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _walletFetcher = GetComponentInParent<Gs2MoneyWalletFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2MoneyWithdraw
    {
        public Wallet Wallet
        {
            get => _walletFetcher.wallet;
            set => _walletFetcher.wallet = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2MoneyWithdraw
    {
        public int withdrawAmount;
        public bool paidOnly;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MoneyWithdraw
    {
        [Serializable]
        private class WithdrawCompleteEvent : UnityEvent<EzWallet>
        {
            
        }
        
        [SerializeField]
        private WithdrawCompleteEvent onWithdrawComplete = new WithdrawCompleteEvent();
        
        public event UnityAction<EzWallet> OnWithdrawComplete
        {
            add => onWithdrawComplete.AddListener(value);
            remove => onWithdrawComplete.RemoveListener(value);
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