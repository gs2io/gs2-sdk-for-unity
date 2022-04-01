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
using Gs2.Unity.Gs2Account.Model;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Account
{
    [AddComponentMenu("GS2 UIKit/Account/Gs2AccountRepositorySaveAction")]
    public partial class Gs2AccountRepositorySaveAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return null;
            try
            {
                _accountRepository.Save(account);
                onSaveComplete.Invoke(account);

            }
            catch (Exception e)
            {
                onError.Invoke(e, null);
            }
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
    
    public partial class Gs2AccountRepositorySaveAction
    {
        protected AbstractAccountRepository _accountRepository;

        public void Awake()
        {
            _accountRepository = GetComponentInParent<AbstractAccountRepository>() ?? GetComponent<AbstractAccountRepository>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2AccountRepositorySaveAction
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2AccountRepositorySaveAction
    {
        public EzAccount account;

        public void Account(EzAccount value)
        {
            account = value;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AccountRepositorySaveAction
    {
        [Serializable]
        private class SaveCompleteEvent : UnityEvent<EzAccount>
        {
            
        }
        
        [SerializeField]
        private SaveCompleteEvent onSaveComplete = new SaveCompleteEvent();
        
        public event UnityAction<EzAccount> OnSaveComplete
        {
            add => onSaveComplete.AddListener(value);
            remove => onSaveComplete.RemoveListener(value);
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