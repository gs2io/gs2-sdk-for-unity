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
    [AddComponentMenu("GS2 UIKit/Quest/Gs2QuestStartQuest")]
    public partial class Gs2QuestStartQuest : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => _questFetcher.Quest != null);
                
            var future = _clientHolder.Gs2.Quest.Namespace(
                _questFetcher.quest.questGroup.Namespace.namespaceName
            ).Me(
                _gameSessionHolder.GameSession
            ).Start(
                _questFetcher.quest.questGroup.questGroupName,
                _questFetcher.quest.questName,
                forceStart
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
                        onStartComplete.Invoke(_questFetcher.Quest);
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }

                onError.Invoke(future.Error, null);
                yield break;
            }
            onStartComplete.Invoke(_questFetcher.Quest);
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
    
    public partial class Gs2QuestStartQuest
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2QuestQuestFetcher _questFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _questFetcher = GetComponentInParent<Gs2QuestQuestFetcher>() ?? GetComponent<Gs2QuestQuestFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2QuestStartQuest
    {
        public Quest Quest
        {
            get => _questFetcher.quest;
            set => _questFetcher.quest = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2QuestStartQuest
    {
        public bool forceStart;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2QuestStartQuest
    {
        [Serializable]
        private class StartCompleteEvent : UnityEvent<EzQuestModel>
        {
            
        }
        
        [SerializeField]
        private StartCompleteEvent onStartComplete = new StartCompleteEvent();
        
        public event UnityAction<EzQuestModel> OnStartComplete
        {
            add => onStartComplete.AddListener(value);
            remove => onStartComplete.RemoveListener(value);
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