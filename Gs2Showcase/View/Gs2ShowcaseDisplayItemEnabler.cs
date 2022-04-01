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

using Gs2.Unity.UiKit.Gs2Showcase.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.GsShowcase
{
    [AddComponentMenu("GS2 UIKit/Showcase/Gs2ShowcaseDisplayItemEnabler")]
    public partial class Gs2ShowcaseDisplayItemEnabler : MonoBehaviour
    {
        public void Start()
        {
            Update();
        }

        public void Update()
        {
            if (!_displayItemFetcher.Fetched)
            {
                target.SetActive(loading);
            }
            else
            {
                target.SetActive(loaded);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2ShowcaseDisplayItemEnabler
    {
        private Gs2ShowcaseDisplayItemFetcher _displayItemFetcher;

        public void Awake()
        {
            _displayItemFetcher = GetComponentInParent<Gs2ShowcaseDisplayItemFetcher>() ?? GetComponent<Gs2ShowcaseDisplayItemFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ShowcaseDisplayItemEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ShowcaseDisplayItemEnabler
    {
        public bool loading;
        public bool loaded;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ShowcaseDisplayItemEnabler
    {
        
    }
}