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
using Gs2.Gs2Guild.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Guild.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzGuildModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public int DefaultMaximumMemberCount;
		[SerializeField]
		public int MaximumMemberCount;
		[SerializeField]
		public List<Gs2.Unity.Gs2Guild.Model.EzRoleModel> Roles;
		[SerializeField]
		public int RejoinCoolTimeMinutes;

        public Gs2.Gs2Guild.Model.GuildModel ToModel()
        {
            return new Gs2.Gs2Guild.Model.GuildModel {
                Name = Name,
                Metadata = Metadata,
                DefaultMaximumMemberCount = DefaultMaximumMemberCount,
                MaximumMemberCount = MaximumMemberCount,
                Roles = Roles?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                RejoinCoolTimeMinutes = RejoinCoolTimeMinutes,
            };
        }

        public static EzGuildModel FromModel(Gs2.Gs2Guild.Model.GuildModel model)
        {
            return new EzGuildModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                DefaultMaximumMemberCount = model.DefaultMaximumMemberCount ?? 0,
                MaximumMemberCount = model.MaximumMemberCount ?? 0,
                Roles = model.Roles == null ? new List<Gs2.Unity.Gs2Guild.Model.EzRoleModel>() : model.Roles.Select(v => {
                    return Gs2.Unity.Gs2Guild.Model.EzRoleModel.FromModel(v);
                }).ToList(),
                RejoinCoolTimeMinutes = model.RejoinCoolTimeMinutes ?? 0,
            };
        }
    }
}