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
using Gs2.Unity.Gs2Quest.Model;
using Gs2.Unity.Gs2Quest.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Quest
{
    [AddComponentMenu("GS2 UIKit/Quest/Gs2QuestLoadProgress")]
    public partial class Gs2QuestLoadProgress : MonoBehaviour
    {
        
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => _questProgressFetcher.Fetched);

            if (_questProgressFetcher.Progress == null)
            {
                onNotFoundProgress.Invoke();
            }
            else
            {
                onLoadProgress.Invoke(_questProgressFetcher.Progress);
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
    
    public partial class Gs2QuestLoadProgress
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
    
    public partial class Gs2QuestLoadProgress
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
    
    public partial class Gs2QuestLoadProgress
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2QuestLoadProgress
    {
        [Serializable]
        private class LoadProgressEvent : UnityEvent<EzProgress>
        {
            
        }
        
        [SerializeField]
        private LoadProgressEvent onLoadProgress = new LoadProgressEvent();
        
        public event UnityAction<EzProgress> OnLoadProgress
        {
            add => onLoadProgress.AddListener(value);
            remove => onLoadProgress.RemoveListener(value);
        }

        [Serializable]
        private class NotFoundProgressEvent : UnityEvent
        {
            
        }
        
        [SerializeField]
        private NotFoundProgressEvent onNotFoundProgress = new NotFoundProgressEvent();
        
        public event UnityAction OnNotFoundProgress
        {
            add => onNotFoundProgress.AddListener(value);
            remove => onNotFoundProgress.RemoveListener(value);
        }
    }
}