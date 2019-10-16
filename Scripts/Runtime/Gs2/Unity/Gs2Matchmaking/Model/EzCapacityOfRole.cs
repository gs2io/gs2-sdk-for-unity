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
	public class EzCapacityOfRole
	{
		/** ロール名 */
		public string RoleName { get; set; }
		/** ロール名の別名リスト */
		public List<string> RoleAliases { get; set; }
		/** 募集人数 */
		public int Capacity { get; set; }
		/** 参加者のプレイヤー情報リスト */
		public List<EzPlayer> Participants { get; set; }

		public EzCapacityOfRole()
		{

		}

		public EzCapacityOfRole(Gs2.Gs2Matchmaking.Model.CapacityOfRole @capacityOfRole)
		{
			RoleName = @capacityOfRole.roleName;
			RoleAliases = @capacityOfRole.roleAliases != null ? @capacityOfRole.roleAliases.Select(value =>
                {
                    return value;
                }
			).ToList() : new List<string>(new string[] {});
			Capacity = @capacityOfRole.capacity.HasValue ? @capacityOfRole.capacity.Value : 0;
			Participants = @capacityOfRole.participants != null ? @capacityOfRole.participants.Select(value =>
                {
                    return new EzPlayer(value);
                }
			).ToList() : new List<EzPlayer>(new EzPlayer[] {});
		}

        public CapacityOfRole ToModel()
        {
            return new CapacityOfRole {
                roleName = RoleName,
                roleAliases = RoleAliases != null ? RoleAliases.Select(Value0 =>
                        {
                            return Value0;
                        }
                ).ToList() : new List<string>(new string[] {}),
                capacity = Capacity,
                participants = Participants != null ? Participants.Select(Value0 =>
                        {
                            return new Player
                            {
                                userId = Value0.UserId,
                                attributes = Value0.Attributes != null ? Value0.Attributes.Select(Value1 =>
                                        {
                                            return new Attribute_
                                            {
                                                name = Value1.Name,
                                                value = Value1.Value,
                                            };
                                        }
                                ).ToList() : new List<Attribute_>(new Attribute_[] {}),
                                roleName = Value0.RoleName,
                            };
                        }
                ).ToList() : new List<Player>(new Player[] {}),
            };
        }
	}
}
