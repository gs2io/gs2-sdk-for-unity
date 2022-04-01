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
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Quest
{
    [AddComponentMenu("GS2 UIKit/Quest/Gs2QuestQuestEnabler")]
    public partial class Gs2QuestQuestEnabler : MonoBehaviour
    {
        public void Start()
        {
            Update();
        }

        public void Update()
        {
            if (!_completedQuestListFetcher.Fetched || !_questFetcher.Fetched)
            {
                target.SetActive(loading);
            }
            else if (_completedQuestListFetcher.CompletedQuestList.CompleteQuestNames.Contains(_questFetcher.Quest.Name))
            {
                target.SetActive(completed);
            }
            else
            {
                var open = true;
                foreach (var premiseQuestName in _questFetcher.Quest.PremiseQuestNames)
                {
                    if (!_completedQuestListFetcher.CompletedQuestList.CompleteQuestNames.Contains(premiseQuestName))
                    {
                        open = false;
                    }
                }

                if (open)
                {
                    target.SetActive(opened);
                }
                else
                {
                    target.SetActive(closed);
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2QuestQuestEnabler
    {
        private Gs2QuestQuestFetcher _questFetcher;
        private Gs2QuestCompletedQuestListFetcher _completedQuestListFetcher;

        public void Awake()
        {
            _questFetcher = GetComponentInParent<Gs2QuestQuestFetcher>() ?? GetComponent<Gs2QuestQuestFetcher>();
            _completedQuestListFetcher = GetComponentInParent<Gs2QuestCompletedQuestListFetcher>() ?? GetComponent<Gs2QuestCompletedQuestListFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2QuestQuestEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2QuestQuestEnabler
    {
        public bool loading;
        public bool opened;
        public bool completed;
        public bool closed;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2QuestQuestEnabler
    {
        
    }
}