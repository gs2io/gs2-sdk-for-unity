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
using Gs2.Gs2Log.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Log.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzInGameLog
	{
		[SerializeField]
		public long Timestamp;
		[SerializeField]
		public string UserId;
		[SerializeField]
		public List<Gs2.Unity.Gs2Log.Model.EzInGameLogTag> Tags;
		[SerializeField]
		public string Payload;

        public Gs2.Gs2Log.Model.InGameLog ToModel()
        {
            return new Gs2.Gs2Log.Model.InGameLog {
                Timestamp = Timestamp,
                UserId = UserId,
                Tags = Tags?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                Payload = Payload,
            };
        }

        public static EzInGameLog FromModel(Gs2.Gs2Log.Model.InGameLog model)
        {
            return new EzInGameLog {
                Timestamp = model.Timestamp ?? 0,
                UserId = model.UserId == null ? null : model.UserId,
                Tags = model.Tags == null ? new List<Gs2.Unity.Gs2Log.Model.EzInGameLogTag>() : model.Tags.Select(v => {
                    return Gs2.Unity.Gs2Log.Model.EzInGameLogTag.FromModel(v);
                }).ToList(),
                Payload = model.Payload == null ? null : model.Payload,
            };
        }
    }
}