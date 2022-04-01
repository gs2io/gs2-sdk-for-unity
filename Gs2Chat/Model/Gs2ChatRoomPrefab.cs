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
using Gs2.Unity.Gs2Chat.Model;
using Gs2.Unity.Gs2Chat.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Chat.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Chat
{
    /// <summary>
    /// Main
    /// </summary>

    [RequireComponent(typeof(Gs2ChatRoomFetcher))]
	[AddComponentMenu("GS2 UIKit/Chat/Gs2ChatRoomPrefab")]
    public partial class Gs2ChatRoomPrefab : MonoBehaviour
    {
        private Room _room;
        
        public void Set(
            Namespace @namespace,
            string roomName
        )
        {
            _room = ScriptableObject.CreateInstance<Room>();
            _room.Namespace = @namespace;
            _room.roomName = roomName;
            
            var roomFetcher = GetComponent<Gs2ChatRoomFetcher>();
            roomFetcher.room = _room;
        }

        public void OnDestroy()
        {
            if (_room != null)
            {
                Destroy(_room);
                _room = null;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2ChatRoomPrefab
    {
        private Gs2ChatRoomFetcher _roomFetcher;

        public void Awake()
        {
            _roomFetcher = GetComponent<Gs2ChatRoomFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ChatRoomPrefab
    {
        public Room Room
        {
            get => _roomFetcher.room;
            set => _roomFetcher.room = value;
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ChatRoomPrefab
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ChatRoomPrefab
    {
        
    }
}