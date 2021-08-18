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
	public class EzGathering
	{
		[SerializeField]
		public string GatheringId;
		[SerializeField]
		public string Name;
		[SerializeField]
		public List<Gs2.Unity.Gs2Matchmaking.Model.EzAttributeRange> AttributeRanges;
		[SerializeField]
		public List<Gs2.Unity.Gs2Matchmaking.Model.EzCapacityOfRole> CapacityOfRoles;
		[SerializeField]
		public List<string> AllowUserIds;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public long ExpiresAt;
		[SerializeField]
		public long CreatedAt;
		[SerializeField]
		public long UpdatedAt;

        public Gs2.Gs2Matchmaking.Model.Gathering ToModel()
        {
            return new Gs2.Gs2Matchmaking.Model.Gathering {
                GatheringId = GatheringId,
                Name = Name,
                AttributeRanges = AttributeRanges?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                CapacityOfRoles = CapacityOfRoles?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                AllowUserIds = AllowUserIds?.Select(v => {
                    return v;
                }).ToArray(),
                Metadata = Metadata,
                ExpiresAt = ExpiresAt,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
            };
        }

        public static EzGathering FromModel(Gs2.Gs2Matchmaking.Model.Gathering model)
        {
            return new EzGathering {
                GatheringId = model.GatheringId == null ? null : model.GatheringId,
                Name = model.Name == null ? null : model.Name,
                AttributeRanges = model.AttributeRanges == null ? new List<Gs2.Unity.Gs2Matchmaking.Model.EzAttributeRange>() : model.AttributeRanges.Select(v => {
                    return Gs2.Unity.Gs2Matchmaking.Model.EzAttributeRange.FromModel(v);
                }).ToList(),
                CapacityOfRoles = model.CapacityOfRoles == null ? new List<Gs2.Unity.Gs2Matchmaking.Model.EzCapacityOfRole>() : model.CapacityOfRoles.Select(v => {
                    return Gs2.Unity.Gs2Matchmaking.Model.EzCapacityOfRole.FromModel(v);
                }).ToList(),
                AllowUserIds = model.AllowUserIds == null ? new List<string>() : model.AllowUserIds.Select(v => {
                    return v;
                }).ToList(),
                Metadata = model.Metadata == null ? null : model.Metadata,
                ExpiresAt = model.ExpiresAt ?? 0,
                CreatedAt = model.CreatedAt ?? 0,
                UpdatedAt = model.UpdatedAt ?? 0,
            };
        }
    }
}