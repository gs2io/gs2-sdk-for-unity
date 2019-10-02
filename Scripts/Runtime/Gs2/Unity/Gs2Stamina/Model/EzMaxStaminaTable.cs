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
using Gs2.Gs2Stamina.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Stamina.Model
{
	[Preserve]
	public class EzMaxStaminaTable
	{
		/** 最大スタミナ値テーブル名 */
		public string Name { get; set; }
		/** 最大スタミナ値テーブルのメタデータ */
		public string Metadata { get; set; }
		/** 経験値の種類マスター のGRN */
		public string ExperienceModelId { get; set; }
		/** ランク毎のスタミナの最大値テーブル */
		public List<int> Values { get; set; }

		public EzMaxStaminaTable()
		{

		}

		public EzMaxStaminaTable(Gs2.Gs2Stamina.Model.MaxStaminaTable @maxStaminaTable)
		{
			Name = @maxStaminaTable.name;
			Metadata = @maxStaminaTable.metadata;
			ExperienceModelId = @maxStaminaTable.experienceModelId;
			Values = @maxStaminaTable.values != null ? @maxStaminaTable.values.Select(value =>
                {
                    if (value.HasValue)
                    {
                        return value.Value;
                    }
                    return 0;
                }
			).ToList() : new List<int>(new int[] {});
		}

        public MaxStaminaTable ToModel()
        {
            return new MaxStaminaTable {
                name = Name,
                metadata = Metadata,
                experienceModelId = ExperienceModelId,
                values = Values != null ? Values.Select(Value0 =>
                        {
                            return (int?)Value0;
                        }
                ).ToList() : new List<int?>(new int?[] {}),
            };
        }
	}
}
