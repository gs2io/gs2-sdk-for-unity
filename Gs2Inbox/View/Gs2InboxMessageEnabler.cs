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

using Gs2.Unity.UiKit.Gs2Inbox.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Inbox
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Inbox/Gs2InboxMessageEnabler")]
    public partial class Gs2InboxMessageEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (!_messageFetcher.Fetched)
            {
                target.SetActive(loading);
            }
            else if (!_messageFetcher.Message.IsRead)
            {
                target.SetActive(notOpen);
            }
            else 
            {
                target.SetActive(opened);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2InboxMessageEnabler
    {
        private Gs2InboxMessageFetcher _messageFetcher;

        public void Awake()
        {
            _messageFetcher = GetComponentInParent<Gs2InboxMessageFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2InboxMessageEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2InboxMessageEnabler
    {
        public bool loading;
        public bool notOpen;
        public bool opened;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InboxMessageEnabler
    {
        
    }
}