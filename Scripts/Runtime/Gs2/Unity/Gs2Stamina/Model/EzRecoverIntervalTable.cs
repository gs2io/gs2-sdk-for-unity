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
	public class EzRecoverIntervalTable
	{
		/** スタミナ回復間隔テーブル名 */
		public string Name { get; set; }
		/** スタミナ回復間隔テーブルのメタデータ */
		public string Metadata { get; set; }
		/** 経験値の種類マスター のGRN */
		public string ExperienceModelId { get; set; }
		/** ランク毎のスタミナ回復間隔テーブル */
		public List<int> Values { get; set; }

		public EzRecoverIntervalTable()
		{

		}

		public EzRecoverIntervalTable(Gs2.Gs2Stamina.Model.RecoverIntervalTable @recoverIntervalTable)
		{
			Name = @recoverIntervalTable.name;
			Metadata = @recoverIntervalTable.metadata;
			ExperienceModelId = @recoverIntervalTable.experienceModelId;
			Values = @recoverIntervalTable.values != null ? @recoverIntervalTable.values.Select(value =>
                {
                    if (value.HasValue)
                    {
                        return value.Value;
                    }
                    return 0;
                }
			).ToList() : new List<int>(new int[] {});
		}

        public RecoverIntervalTable ToModel()
        {
            return new RecoverIntervalTable {
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
