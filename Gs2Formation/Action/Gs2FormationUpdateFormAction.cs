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
using System.Collections.Generic;
using Gs2.Core.Exception;
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.Gs2Key.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Formation
{
    [AddComponentMenu("GS2 UIKit/Formation/Gs2FormationUpdateFormAction")]
    public partial class Gs2FormationUpdateFormAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => _formFetcher.Fetched);
            yield return new WaitUntil(() => slots.Count > 0);
            
            var future = _clientHolder.Gs2.Formation.Namespace(
                Form.mold.Namespace.namespaceName
            ).Me(
                _gameSessionHolder.GameSession
            ).Mold(
                Form.mold.moldName
            ).Form(
                Form.index
            ).SetForm(
                slots.ToArray(),
                key.Grn
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
                        onUpdateComplete.Invoke(_formFetcher.Model, _formFetcher.Form);
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }

                onError.Invoke(future.Error, null);
                yield break;
            }
            onUpdateComplete.Invoke(_formFetcher.Model, _formFetcher.Form);
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
    
    public partial class Gs2FormationUpdateFormAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2FormationFormFetcher _formFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _formFetcher = GetComponentInParent<Gs2FormationFormFetcher>() ?? GetComponent<Gs2FormationFormFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FormationUpdateFormAction
    {
        public Form Form
        {
            get => _formFetcher.form;
            set => _formFetcher.form = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2FormationUpdateFormAction
    {
        public List<EzSlotWithSignature> slots;
        public Key key;

        public void Slots(List<EzSlotWithSignature> value)
        {
            slots = value;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationUpdateFormAction
    {
        [Serializable]
        private class UpdateCompleteEvent : UnityEvent<EzFormModel, EzForm>
        {
            
        }
        
        [SerializeField]
        private UpdateCompleteEvent onUpdateComplete = new UpdateCompleteEvent();
        
        public event UnityAction<EzFormModel, EzForm> OnUpdateComplete
        {
            add => onUpdateComplete.AddListener(value);
            remove => onUpdateComplete.RemoveListener(value);
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