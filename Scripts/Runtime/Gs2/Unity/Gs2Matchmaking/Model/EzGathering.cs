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
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Matchmaking.Model
{
	[Preserve]
	public class EzGathering
	{
		/** ギャザリング */
		public string GatheringId { get; set; }
		/** ギャザリング名 */
		public string Name { get; set; }
		/** 募集条件 */
		public List<EzAttributeRange> AttributeRanges { get; set; }
		/** 参加者 */
		public List<EzCapacityOfRole> CapacityOfRoles { get; set; }
		/** 参加を許可するユーザIDリスト */
		public List<string> AllowUserIds { get; set; }
		/** メタデータ */
		public string Metadata { get; set; }
		/** 作成日時 */
		public long CreatedAt { get; set; }
		/** 最終更新日時 */
		public long UpdatedAt { get; set; }

		public EzGathering()
		{

		}

		public EzGathering(Gs2.Gs2Matchmaking.Model.Gathering @gathering)
		{
			GatheringId = @gathering.gatheringId;
			Name = @gathering.name;
			AttributeRanges = @gathering.attributeRanges != null ? @gathering.attributeRanges.Select(value =>
                {
                    return new EzAttributeRange(value);
                }
			).ToList() : new List<EzAttributeRange>(new EzAttributeRange[] {});
			CapacityOfRoles = @gathering.capacityOfRoles != null ? @gathering.capacityOfRoles.Select(value =>
                {
                    return new EzCapacityOfRole(value);
                }
			).ToList() : new List<EzCapacityOfRole>(new EzCapacityOfRole[] {});
			AllowUserIds = @gathering.allowUserIds != null ? @gathering.allowUserIds.Select(value =>
                {
                    return value;
                }
			).ToList() : new List<string>(new string[] {});
			Metadata = @gathering.metadata;
			CreatedAt = @gathering.createdAt.HasValue ? @gathering.createdAt.Value : 0;
			UpdatedAt = @gathering.updatedAt.HasValue ? @gathering.updatedAt.Value : 0;
		}

        public Gathering ToModel()
        {
            return new Gathering {
                gatheringId = GatheringId,
                name = Name,
                attributeRanges = AttributeRanges != null ? AttributeRanges.Select(Value0 =>
                        {
                            return new AttributeRange
                            {
                                name = Value0.Name,
                                min = Value0.Min,
                                max = Value0.Max,
                            };
                        }
                ).ToList() : new List<AttributeRange>(new AttributeRange[] {}),
                capacityOfRoles = CapacityOfRoles != null ? CapacityOfRoles.Select(Value0 =>
                        {
                            return new CapacityOfRole
                            {
                                roleName = Value0.RoleName,
                                roleAliases = Value0.RoleAliases != null ? Value0.RoleAliases.Select(Value1 =>
                                        {
                                            return Value1;
                                        }
                                ).ToList() : new List<string>(new string[] {}),
                                capacity = Value0.Capacity,
                                participants = Value0.Participants != null ? Value0.Participants.Select(Value1 =>
                                        {
                                            return new Player
                                            {
                                                userId = Value1.UserId,
                                                attributes = Value1.Attributes != null ? Value1.Attributes.Select(Value2 =>
                                                        {
                                                            return new Attribute_
                                                            {
                                                                name = Value2.Name,
                                                                value = Value2.Value,
                                                            };
                                                        }
                                                ).ToList() : new List<Attribute_>(new Attribute_[] {}),
                                                roleName = Value1.RoleName,
                                            };
                                        }
                                ).ToList() : new List<Player>(new Player[] {}),
                            };
                        }
                ).ToList() : new List<CapacityOfRole>(new CapacityOfRole[] {}),
                allowUserIds = AllowUserIds != null ? AllowUserIds.Select(Value0 =>
                        {
                            return Value0;
                        }
                ).ToList() : new List<string>(new string[] {}),
                metadata = Metadata,
                createdAt = CreatedAt,
                updatedAt = UpdatedAt,
            };
        }
	}
}
