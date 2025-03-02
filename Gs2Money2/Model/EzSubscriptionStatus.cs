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
using Gs2.Gs2Money2.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Money2.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzSubscriptionStatus
	{
		[SerializeField]
		public string ContentName;
		[SerializeField]
		public string UserId;
		[SerializeField]
		public string Status;
		[SerializeField]
		public long ExpiresAt;
		[SerializeField]
		public List<Gs2.Unity.Gs2Money2.Model.EzSubscribeTransaction> Detail;

        public Gs2.Gs2Money2.Model.SubscriptionStatus ToModel()
        {
            return new Gs2.Gs2Money2.Model.SubscriptionStatus {
                ContentName = ContentName,
                UserId = UserId,
                Status = Status,
                ExpiresAt = ExpiresAt,
                Detail = Detail?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
            };
        }

        public static EzSubscriptionStatus FromModel(Gs2.Gs2Money2.Model.SubscriptionStatus model)
        {
            return new EzSubscriptionStatus {
                ContentName = model.ContentName == null ? null : model.ContentName,
                UserId = model.UserId == null ? null : model.UserId,
                Status = model.Status == null ? null : model.Status,
                ExpiresAt = model.ExpiresAt ?? 0,
                Detail = model.Detail == null ? new List<Gs2.Unity.Gs2Money2.Model.EzSubscribeTransaction>() : model.Detail.Select(v => {
                    return Gs2.Unity.Gs2Money2.Model.EzSubscribeTransaction.FromModel(v);
                }).ToList(),
            };
        }
    }
}