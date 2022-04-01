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
// ReSharper disable CheckNamespace

using Gs2.Unity.UiKit.Gs2Money.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Money
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Money/Gs2MoneyWalletBalanceEnabler")]
    public partial class Gs2MoneyWalletBalanceEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (!_walletFetcher.Fetched)
            {
                target.SetActive(loading);
            }
            else if ((paidOnly ? _walletFetcher.Wallet.Paid : _walletFetcher.Wallet.Paid + _walletFetcher.Wallet.Free) < balance)
            {
                target.SetActive(shortage);
            }
            else
            {
                target.SetActive(satisfaction);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2MoneyWalletBalanceEnabler
    {
        private Gs2MoneyWalletFetcher _walletFetcher;

        public void Awake()
        {
            _walletFetcher = GetComponentInParent<Gs2MoneyWalletFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2MoneyWalletBalanceEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2MoneyWalletBalanceEnabler
    {
        public int balance;
        public bool paidOnly;
        
        public bool loading;
        public bool shortage;
        public bool satisfaction;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MoneyWalletBalanceEnabler
    {
        
    }
}