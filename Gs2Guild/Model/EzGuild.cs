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
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Guild.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzGuild
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string GuildModelName;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Name;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string DisplayName;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int Attribute1;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int Attribute2;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int Attribute3;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int Attribute4;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int Attribute5;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Metadata;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string JoinPolicy;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<Gs2.Unity.Gs2Guild.Model.EzRoleModel> CustomRoles;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<Gs2.Unity.Gs2Guild.Model.EzMember> Members;

        public Gs2.Gs2Guild.Model.Guild ToModel()
        {
            return new Gs2.Gs2Guild.Model.Guild {
                GuildModelName = GuildModelName,
                Name = Name,
                DisplayName = DisplayName,
                Attribute1 = Attribute1,
                Attribute2 = Attribute2,
                Attribute3 = Attribute3,
                Attribute4 = Attribute4,
                Attribute5 = Attribute5,
                Metadata = Metadata,
                JoinPolicy = JoinPolicy,
                CustomRoles = CustomRoles?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                Members = Members?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
            };
        }

        public static EzGuild FromModel(Gs2.Gs2Guild.Model.Guild model)
        {
            return new EzGuild {
                GuildModelName = model.GuildModelName == null ? null : model.GuildModelName,
                Name = model.Name == null ? null : model.Name,
                DisplayName = model.DisplayName == null ? null : model.DisplayName,
                Attribute1 = model.Attribute1 ?? 0,
                Attribute2 = model.Attribute2 ?? 0,
                Attribute3 = model.Attribute3 ?? 0,
                Attribute4 = model.Attribute4 ?? 0,
                Attribute5 = model.Attribute5 ?? 0,
                Metadata = model.Metadata == null ? null : model.Metadata,
                JoinPolicy = model.JoinPolicy == null ? null : model.JoinPolicy,
                CustomRoles = model.CustomRoles == null ? new List<Gs2.Unity.Gs2Guild.Model.EzRoleModel>() : model.CustomRoles.Select(v => {
                    return Gs2.Unity.Gs2Guild.Model.EzRoleModel.FromModel(v);
                }).ToList(),
                Members = model.Members == null ? new List<Gs2.Unity.Gs2Guild.Model.EzMember>() : model.Members.Select(v => {
                    return Gs2.Unity.Gs2Guild.Model.EzMember.FromModel(v);
                }).ToList(),
            };
        }
    }
}