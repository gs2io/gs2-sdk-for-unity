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

using System;
using Gs2.Gs2Chat.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Chat.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzMessage
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string RoomName;
		[SerializeField]
		public string UserId;
		[SerializeField]
		public int Category;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public long CreatedAt;

        public Gs2.Gs2Chat.Model.Message ToModel()
        {
            return new Gs2.Gs2Chat.Model.Message {
                Name = Name,
                RoomName = RoomName,
                UserId = UserId,
                Category = Category,
                Metadata = Metadata,
                CreatedAt = CreatedAt,
            };
        }

        public static EzMessage FromModel(Gs2.Gs2Chat.Model.Message model)
        {
            return new EzMessage {
                Name = model.Name == null ? null : model.Name,
                RoomName = model.RoomName == null ? null : model.RoomName,
                UserId = model.UserId == null ? null : model.UserId,
                Category = model.Category ?? 0,
                Metadata = model.Metadata == null ? null : model.Metadata,
                CreatedAt = model.CreatedAt ?? 0,
            };
        }
    }
}