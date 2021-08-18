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

using Gs2.Gs2Matchmaking.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Matchmaking.Model
{
	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzPlayer
	{
		[SerializeField]
		public string UserId;
		[SerializeField]
		public List<Gs2.Unity.Gs2Matchmaking.Model.EzAttribute> Attributes;
		[SerializeField]
		public string RoleName;
		[SerializeField]
		public List<string> DenyUserIds;

        public Gs2.Gs2Matchmaking.Model.Player ToModel()
        {
            return new Gs2.Gs2Matchmaking.Model.Player {
                UserId = UserId,
                Attributes = Attributes?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                RoleName = RoleName,
                DenyUserIds = DenyUserIds?.Select(v => {
                    return v;
                }).ToArray(),
            };
        }

        public static EzPlayer FromModel(Gs2.Gs2Matchmaking.Model.Player model)
        {
            return new EzPlayer {
                UserId = model.UserId == null ? null : model.UserId,
                Attributes = model.Attributes == null ? new List<Gs2.Unity.Gs2Matchmaking.Model.EzAttribute>() : model.Attributes.Select(v => {
                    return Gs2.Unity.Gs2Matchmaking.Model.EzAttribute.FromModel(v);
                }).ToList(),
                RoleName = model.RoleName == null ? null : model.RoleName,
                DenyUserIds = model.DenyUserIds == null ? new List<string>() : model.DenyUserIds.Select(v => {
                    return v;
                }).ToList(),
            };
        }
    }
}