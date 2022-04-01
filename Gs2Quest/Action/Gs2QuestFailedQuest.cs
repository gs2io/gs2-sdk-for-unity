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
using Gs2.Unity.Gs2Quest.Model;
using Gs2.Unity.Gs2Quest.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Quest
{
    [AddComponentMenu("GS2 UIKit/Quest/Gs2QuestFailedQuest")]
    public partial class Gs2QuestFailedQuest : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => _questProgressFetcher.Progress != null);
                
            var future = _clientHolder.Gs2.Quest.Namespace(
                _questProgressFetcher.Namespace.namespaceName
            ).Me(
                _gameSessionHolder.GameSession
            ).Progress(
            ).End(
                false
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
                        onReportComplete.Invoke(_questProgressFetcher.Progress);
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }

                onError.Invoke(future.Error, null);
                yield break;
            }
            onReportComplete.Invoke(_questProgressFetcher.Progress);
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
    
    public partial class Gs2QuestFailedQuest
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2QuestProgressFetcher _questProgressFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _questProgressFetcher = GetComponentInParent<Gs2QuestProgressFetcher>() ?? GetComponent<Gs2QuestProgressFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2QuestFailedQuest
    {
        public Namespace Namespace
        {
            get => _questProgressFetcher.Namespace;
            set => _questProgressFetcher.Namespace = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2QuestFailedQuest
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2QuestFailedQuest
    {
        [Serializable]
        private class ReportCompleteEvent : UnityEvent<EzProgress>
        {
            
        }
        
        [SerializeField]
        private ReportCompleteEvent onReportComplete = new ReportCompleteEvent();
        
        public event UnityAction<EzProgress> OnReportComplete
        {
            add => onReportComplete.AddListener(value);
            remove => onReportComplete.RemoveListener(value);
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