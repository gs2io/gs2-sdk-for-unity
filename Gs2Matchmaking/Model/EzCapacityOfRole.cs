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
using Gs2.Gs2Matchmaking.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Matchmaking.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzCapacityOfRole
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string RoleName;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<string> RoleAliases;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int Capacity;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<Gs2.Unity.Gs2Matchmaking.Model.EzPlayer> Participants;

        public Gs2.Gs2Matchmaking.Model.CapacityOfRole ToModel()
        {
            return new Gs2.Gs2Matchmaking.Model.CapacityOfRole {
                RoleName = RoleName,
                RoleAliases = RoleAliases?.Select(v => {
                    return v;
                }).ToArray(),
                Capacity = Capacity,
                Participants = Participants?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
            };
        }

        public static EzCapacityOfRole FromModel(Gs2.Gs2Matchmaking.Model.CapacityOfRole model)
        {
            return new EzCapacityOfRole {
                RoleName = model.RoleName == null ? null : model.RoleName,
                RoleAliases = model.RoleAliases == null ? new List<string>() : model.RoleAliases.Select(v => {
                    return v;
                }).ToList(),
                Capacity = model.Capacity ?? 0,
                Participants = model.Participants == null ? new List<Gs2.Unity.Gs2Matchmaking.Model.EzPlayer>() : model.Participants.Select(v => {
                    return Gs2.Unity.Gs2Matchmaking.Model.EzPlayer.FromModel(v);
                }).ToList(),
            };
        }
    }
}