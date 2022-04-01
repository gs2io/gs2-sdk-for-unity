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
using System.Linq;
using Gs2.Unity.Gs2Showcase.Model;
using Gs2.Unity.Gs2Showcase.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Showcase.Fetcher;
using Gs2.Unity.UiKit.Gs2Showcase.Model;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Showcase
{
    [AddComponentMenu("GS2 UIKit/Showcase/Gs2ShowcaseDisplayItemListPrefab")]
    public partial class Gs2ShowcaseDisplayItemListPrefab : MonoBehaviour
    {
        private readonly Dictionary<string, Gs2ShowcaseDisplayItemPrefab> _cache = new Dictionary<string, Gs2ShowcaseDisplayItemPrefab>();

        public void Update()
        {
            if (_displayItemListFetcher.Fetched)
            {
                var activeNames = _displayItemListFetcher.DisplayItems.Select(v => v.DisplayItemId);
                foreach (var instantiateName in _cache.Keys.ToList())
                {
                    if (!activeNames.Contains(instantiateName))
                    {
                        Destroy(_cache[instantiateName].gameObject);
                        _cache.Remove(instantiateName);

                        onDeleteQuest.Invoke(instantiateName);
                    }
                }

                foreach (var displayItem in _displayItemListFetcher.DisplayItems)
                {
                    if (!_cache.ContainsKey(displayItem.DisplayItemId))
                    {
                        var item = Instantiate(displayItemPrefab, populateNode);
                        item.name = displayItem.DisplayItemId;
                        item.Set(_displayItemListFetcher.showcase, displayItem, _displayItemListFetcher.onError);
                        item.gameObject.SetActive(true);
                        _cache[displayItem.DisplayItemId] = item;
                        onCreateDisplayItem.Invoke(displayItem);
                    }
                }
            }
        }

        public void OnDestroy()
        {
            foreach (var component in _cache.Values)
            {
                Destroy(component);
            }
            _cache.Clear();
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2ShowcaseDisplayItemListPrefab
    {
        private Gs2ShowcaseDisplayItemListFetcher _displayItemListFetcher;

        public void Awake()
        {
            _displayItemListFetcher = GetComponentInParent<Gs2ShowcaseDisplayItemListFetcher>() ?? GetComponent<Gs2ShowcaseDisplayItemListFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ShowcaseDisplayItemListPrefab
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ShowcaseDisplayItemListPrefab
    {
        public Gs2ShowcaseDisplayItemPrefab displayItemPrefab;
        public Transform populateNode;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ShowcaseDisplayItemListPrefab
    {
        [Serializable]
        private class CreateDisplayItemEvent : UnityEvent<EzDisplayItem>
        {
            
        }
        
        [SerializeField]
        private CreateDisplayItemEvent onCreateDisplayItem = new CreateDisplayItemEvent();
        
        public event UnityAction<EzDisplayItem> OnCreateDisplayItem
        {
            add => onCreateDisplayItem.AddListener(value);
            remove => onCreateDisplayItem.RemoveListener(value);
        }

        [Serializable]
        private class DeleteQuestEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private DeleteQuestEvent onDeleteQuest = new DeleteQuestEvent();
        
        public event UnityAction<string> OnDeleteQuest
        {
            add => onDeleteQuest.AddListener(value);
            remove => onDeleteQuest.RemoveListener(value);
        }
    }
}