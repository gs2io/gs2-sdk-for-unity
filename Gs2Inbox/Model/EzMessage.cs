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
using Gs2.Gs2Inbox.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Inbox.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzMessage
	{
		[SerializeField]
		public string MessageId;
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public bool IsRead;
		[SerializeField]
		public List<Gs2.Unity.Core.Model.EzAcquireAction> ReadAcquireActions;
		[SerializeField]
		public long ReceivedAt;
		[SerializeField]
		public long ReadAt;
		[SerializeField]
		public long ExpiresAt;

        public Gs2.Gs2Inbox.Model.Message ToModel()
        {
            return new Gs2.Gs2Inbox.Model.Message {
                MessageId = MessageId,
                Name = Name,
                Metadata = Metadata,
                IsRead = IsRead,
                ReadAcquireActions = ReadAcquireActions?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                ReceivedAt = ReceivedAt,
                ReadAt = ReadAt,
                ExpiresAt = ExpiresAt,
            };
        }

        public static EzMessage FromModel(Gs2.Gs2Inbox.Model.Message model)
        {
            return new EzMessage {
                MessageId = model.MessageId == null ? null : model.MessageId,
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                IsRead = model.IsRead ?? false,
                ReadAcquireActions = model.ReadAcquireActions == null ? new List<Gs2.Unity.Core.Model.EzAcquireAction>() : model.ReadAcquireActions.Select(v => {
                    return Gs2.Unity.Core.Model.EzAcquireAction.FromModel(v);
                }).ToList(),
                ReceivedAt = model.ReceivedAt ?? 0,
                ReadAt = model.ReadAt ?? 0,
                ExpiresAt = model.ExpiresAt ?? 0,
            };
        }
    }
}