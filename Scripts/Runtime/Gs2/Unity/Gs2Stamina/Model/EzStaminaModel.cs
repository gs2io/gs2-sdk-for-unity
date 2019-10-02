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
	public class EzStaminaModel
	{
		/** スタミナの種類名 */
		public string Name { get; set; }
		/** スタミナの種類のメタデータ */
		public string Metadata { get; set; }
		/** スタミナを回復する速度(分) */
		public int RecoverIntervalMinutes { get; set; }
		/** 時間経過後に回復する量 */
		public int RecoverValue { get; set; }
		/** スタミナの最大値の初期値 */
		public int InitialCapacity { get; set; }
		/** 最大値を超えて回復するか */
		public bool IsOverflow { get; set; }
		/** 溢れた状況での最大値 */
		public int MaxCapacity { get; set; }
		/** GS2-Experience と連携する際に使用するスタミナ最大値テーブル */
		public EzMaxStaminaTable MaxStaminaTable { get; set; }
		/** GS2-Experience と連携する際に使用する回復間隔テーブル */
		public EzRecoverIntervalTable RecoverIntervalTable { get; set; }
		/** GS2-Experience と連携する際に使用する回復量テーブル */
		public EzRecoverValueTable RecoverValueTable { get; set; }

		public EzStaminaModel()
		{

		}

		public EzStaminaModel(Gs2.Gs2Stamina.Model.StaminaModel @staminaModel)
		{
			Name = @staminaModel.name;
			Metadata = @staminaModel.metadata;
			RecoverIntervalMinutes = @staminaModel.recoverIntervalMinutes.HasValue ? @staminaModel.recoverIntervalMinutes.Value : 0;
			RecoverValue = @staminaModel.recoverValue.HasValue ? @staminaModel.recoverValue.Value : 0;
			InitialCapacity = @staminaModel.initialCapacity.HasValue ? @staminaModel.initialCapacity.Value : 0;
			IsOverflow = @staminaModel.isOverflow.HasValue ? @staminaModel.isOverflow.Value : false;
			MaxCapacity = @staminaModel.maxCapacity.HasValue ? @staminaModel.maxCapacity.Value : 0;
			MaxStaminaTable = @staminaModel.maxStaminaTable != null ? new EzMaxStaminaTable(@staminaModel.maxStaminaTable) : null;
			RecoverIntervalTable = @staminaModel.recoverIntervalTable != null ? new EzRecoverIntervalTable(@staminaModel.recoverIntervalTable) : null;
			RecoverValueTable = @staminaModel.recoverValueTable != null ? new EzRecoverValueTable(@staminaModel.recoverValueTable) : null;
		}

        public StaminaModel ToModel()
        {
            return new StaminaModel {
                name = Name,
                metadata = Metadata,
                recoverIntervalMinutes = RecoverIntervalMinutes,
                recoverValue = RecoverValue,
                initialCapacity = InitialCapacity,
                isOverflow = IsOverflow,
                maxCapacity = MaxCapacity,
                maxStaminaTable = new MaxStaminaTable {
                    name = MaxStaminaTable.Name,
                    metadata = MaxStaminaTable.Metadata,
                    experienceModelId = MaxStaminaTable.ExperienceModelId,
                    values = MaxStaminaTable.Values != null ? MaxStaminaTable.Values.Select(Value1 =>
                            {
                                return (int?)Value1;
                            }
                    ).ToList() : new List<int?>(new int?[] {}),
                },
                recoverIntervalTable = new RecoverIntervalTable {
                    name = RecoverIntervalTable.Name,
                    metadata = RecoverIntervalTable.Metadata,
                    experienceModelId = RecoverIntervalTable.ExperienceModelId,
                    values = RecoverIntervalTable.Values != null ? RecoverIntervalTable.Values.Select(Value1 =>
                            {
                                return (int?)Value1;
                            }
                    ).ToList() : new List<int?>(new int?[] {}),
                },
                recoverValueTable = new RecoverValueTable {
                    name = RecoverValueTable.Name,
                    metadata = RecoverValueTable.Metadata,
                    experienceModelId = RecoverValueTable.ExperienceModelId,
                    values = RecoverValueTable.Values != null ? RecoverValueTable.Values.Select(Value1 =>
                            {
                                return (int?)Value1;
                            }
                    ).ToList() : new List<int?>(new int?[] {}),
                },
            };
        }
	}
}
