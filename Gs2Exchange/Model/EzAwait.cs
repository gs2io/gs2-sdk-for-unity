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
using Gs2.Gs2Exchange.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Exchange.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzAwait
	{
		[SerializeField]
		public string UserId;
		[SerializeField]
		public string RateName;
		[SerializeField]
		public string Name;
		[SerializeField]
		public int SkipSeconds;
		[SerializeField]
		public List<Gs2.Unity.Gs2Exchange.Model.EzConfig> Config;
		[SerializeField]
		public long ExchangedAt;
		[SerializeField]
		public long AcquirableAt;

        public Gs2.Gs2Exchange.Model.Await ToModel()
        {
            return new Gs2.Gs2Exchange.Model.Await {
                UserId = UserId,
                RateName = RateName,
                Name = Name,
                SkipSeconds = SkipSeconds,
                Config = Config?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                ExchangedAt = ExchangedAt,
                AcquirableAt = AcquirableAt,
            };
        }

        public static EzAwait FromModel(Gs2.Gs2Exchange.Model.Await model)
        {
            return new EzAwait {
                UserId = model.UserId == null ? null : model.UserId,
                RateName = model.RateName == null ? null : model.RateName,
                Name = model.Name == null ? null : model.Name,
                SkipSeconds = model.SkipSeconds ?? 0,
                Config = model.Config == null ? new List<Gs2.Unity.Gs2Exchange.Model.EzConfig>() : model.Config.Select(v => {
                    return Gs2.Unity.Gs2Exchange.Model.EzConfig.FromModel(v);
                }).ToList(),
                ExchangedAt = model.ExchangedAt ?? 0,
                AcquirableAt = model.AcquirableAt ?? 0,
            };
        }
    }
}