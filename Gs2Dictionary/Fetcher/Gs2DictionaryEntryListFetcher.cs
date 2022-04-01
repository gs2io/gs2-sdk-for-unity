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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Gs2.Core.Exception;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Gs2Dictionary.Model;
using Gs2.Unity.Gs2Dictionary.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Gs2.Unity.UiKit.Gs2Dictionary.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Dictionary/Gs2DictionaryEntryListFetcher")]
    public partial class Gs2DictionaryEntryListFetcher : MonoBehaviour
    {
        private IEnumerator Fetch()
        {
            Exception e;
            while (true)
            {
                if (_gameSessionHolder != null && _gameSessionHolder.Initialized && 
                    _clientHolder != null && _clientHolder.Initialized &&
                    Namespace != null)
                {
                    {
                        var it = _clientHolder.Gs2.Dictionary.Namespace(
                            Namespace.namespaceName
                        ).EntryModels(
                        );
                        var entryModels = new List<EzEntryModel>();
                        while (it.HasNext())
                        {
                            yield return it.Next();
                            if (it.Error != null)
                            {
                                if (it.Error is BadRequestException || it.Error is NotFoundException)
                                {
                                    onError.Invoke(e = it.Error, null);
                                    goto END;
                                }

                                onError.Invoke(new CanIgnoreException(it.Error), null);
                                break;
                            }

                            if (it.Current != null)
                            {
                                entryModels.Add(it.Current);
                            }
                        }

                        Models = entryModels;
                    }
                    {
                        var it = _clientHolder.Gs2.Dictionary.Namespace(
                            Namespace.namespaceName
                        ).Me(
                            _gameSessionHolder.GameSession
                        ).Entries(
                        );
                        var forms = new List<EzEntry>();
                        while (it.HasNext())
                        {
                            yield return it.Next();
                            if (it.Error != null)
                            {
                                if (it.Error is BadRequestException || it.Error is NotFoundException)
                                {
                                    onError.Invoke(e = it.Error, null);
                                    goto END;
                                }

                                onError.Invoke(new CanIgnoreException(it.Error), null);
                                break;
                            }

                            if (it.Current != null)
                            {
                                forms.Add(it.Current);
                            }
                        }

                        Entries = forms;
                        Fetched = true;
                    }
                }

                yield return new WaitForSeconds(1);
            }
            END:
            
            var transform1 = transform;
            var builder = new StringBuilder(transform1.name);
            var current = transform1.parent;

            while (current != null)
            {
                builder.Insert(0, current.name + "/");
                current = current.parent;
            }
            
            Debug.LogError(e);
            Debug.LogError($"{GetType()} の自動更新が停止されました。 {builder}");
            Debug.LogError($"Automatic update of {GetType()} has been stopped. {builder}");
        }

        public void OnEnable()
        {
            StartCoroutine(nameof(Fetch));
        }

        public void OnDisable()
        {
            StopCoroutine(nameof(Fetch));
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2DictionaryEntryListFetcher
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
    
    public partial class Gs2DictionaryEntryListFetcher
    {
        public List<EzEntryModel> Models { get; private set; }
        public List<EzEntry> Entries { get; private set; }
        public bool Fetched { get; private set; }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2DictionaryEntryListFetcher
    {
        public Namespace Namespace;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DictionaryEntryListFetcher
    {
        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();
        
        public event UnityAction<Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}