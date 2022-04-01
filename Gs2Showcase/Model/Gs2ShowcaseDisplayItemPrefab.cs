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
using Gs2.Unity.Gs2Showcase.Model;
using Gs2.Unity.Gs2Showcase.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Showcase.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Showcase.Model
{
    [RequireComponent(typeof(Gs2ShowcaseDisplayItemFetcher))]
    [AddComponentMenu("GS2 UIKit/Showcase/Gs2ShowcaseDisplayItemPrefab")]
    public class Gs2ShowcaseDisplayItemPrefab : MonoBehaviour
    {
        private DisplayItem _displayItem;
        private ErrorEvent _onError;

        private void OnError(
            Exception exception, 
            Func<IEnumerator> retryFunc
        )
        {
            if (_onError != null)
            {
                _onError.Invoke(exception, retryFunc);
            }
        }

        public void Awake()
        {
            var displayItemFetcher = GetComponent<Gs2ShowcaseDisplayItemFetcher>();
            displayItemFetcher.OnError += OnError;
        }

        public void OnDestroy()
        {
            var displayItemFetcher = GetComponent<Gs2ShowcaseDisplayItemFetcher>();
            displayItemFetcher.OnError -= OnError;

            if (_displayItem != null)
            {
                Destroy(_displayItem);
            }
        }

        public void Set(
            Showcase showcase,
            EzDisplayItem displayItem,
            ErrorEvent onError
        )
        {
            _displayItem = ScriptableObject.CreateInstance<DisplayItem>();
            _displayItem.showcase = showcase;
            _displayItem.displayItemId = displayItem.DisplayItemId;
            _onError = onError;

            var displayItemFetcher = GetComponent<Gs2ShowcaseDisplayItemFetcher>();
            displayItemFetcher.displayItem = _displayItem;
        }
    }
}