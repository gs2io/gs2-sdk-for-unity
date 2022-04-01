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
using Gs2.Unity.Gs2Mission.Model;
using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    [AddComponentMenu("GS2 UIKit/Mission/Gs2MissionReceiveRewards")]
    public partial class Gs2MissionReceiveRewards : MonoBehaviour
    {
        public void Initialize(
            Gs2MissionMissionTask missionTask
        )
        {
            _missionTaskFetcher.missionTask = missionTask.MissionTask;
        }

        private IEnumerator Process()
        {
            yield return new WaitUntil(() => _missionTaskFetcher.Fetched);
                
            var future = _clientHolder.Gs2.Mission.Namespace(
                _missionTaskFetcher.missionTask.missionGroup.Namespace.namespaceName
            ).Me(
                _gameSessionHolder.GameSession
            ).Complete(
                _missionTaskFetcher.missionTask.missionGroup.missionGroupName
            ).ReceiveRewards(
                _missionTaskFetcher.missionTask.missionTaskName
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
                        onReceiveComplete.Invoke(this);
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }

                onError.Invoke(future.Error, null);
                yield break;
            }
            onReceiveComplete.Invoke(this);
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
    
    public partial class Gs2MissionReceiveRewards
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2MissionMissionTaskFetcher _missionTaskFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            _missionTaskFetcher = GetComponentInParent<Gs2MissionMissionTaskFetcher>() ?? GetComponent<Gs2MissionMissionTaskFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2MissionReceiveRewards
    {
        public MissionTask MissionTask
        {
            get => _missionTaskFetcher.missionTask;
            set => _missionTaskFetcher.missionTask = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2MissionReceiveRewards
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionReceiveRewards
    {
        [Serializable]
        private class ReceiveCompleteEvent : UnityEvent<Gs2MissionReceiveRewards>
        {
            
        }
        
        [SerializeField]
        private ReceiveCompleteEvent onReceiveComplete = new ReceiveCompleteEvent();
        
        public event UnityAction<Gs2MissionReceiveRewards> OnReceiveComplete
        {
            add => onReceiveComplete.AddListener(value);
            remove => onReceiveComplete.RemoveListener(value);
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