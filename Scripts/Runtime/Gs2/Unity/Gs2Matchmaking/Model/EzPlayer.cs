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
	public class EzPlayer
	{
		/** ユーザーID */
		public string UserId { get; set; }
		/** 属性値のリスト */
		public List<EzAttribute> Attributes { get; set; }
		/** ロール名 */
		public string RoleName { get; set; }

		public EzPlayer()
		{

		}

		public EzPlayer(Gs2.Gs2Matchmaking.Model.Player @player)
		{
			UserId = @player.userId;
			Attributes = @player.attributes != null ? @player.attributes.Select(value =>
                {
                    return new EzAttribute(value);
                }
			).ToList() : new List<EzAttribute>(new EzAttribute[] {});
			RoleName = @player.roleName;
		}

        public Player ToModel()
        {
            return new Player {
                userId = UserId,
                attributes = Attributes != null ? Attributes.Select(Value0 =>
                        {
                            return new Attribute_
                            {
                                name = Value0.Name,
                                value = Value0.Value,
                            };
                        }
                ).ToList() : new List<Attribute_>(new Attribute_[] {}),
                roleName = RoleName,
            };
        }
	}
}
