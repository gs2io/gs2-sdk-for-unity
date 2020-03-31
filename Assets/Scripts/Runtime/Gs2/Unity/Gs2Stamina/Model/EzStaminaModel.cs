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
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Stamina.Model
{
	[Preserve]
	[System.Serializable]
	public class EzStaminaModel
	{
		/** スタミナの種類名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** スタミナの種類のメタデータ */
		[UnityEngine.SerializeField]
		public string Metadata;
		/** スタミナを回復する速度(分) */
		[UnityEngine.SerializeField]
		public int RecoverIntervalMinutes;
		/** 時間経過後に回復する量 */
		[UnityEngine.SerializeField]
		public int RecoverValue;
		/** スタミナの最大値の初期値 */
		[UnityEngine.SerializeField]
		public int InitialCapacity;
		/** 最大値を超えて回復するか */
		[UnityEngine.SerializeField]
		public bool IsOverflow;
		/** 溢れた状況での最大値 */
		[UnityEngine.SerializeField]
		public int MaxCapacity;
		/** GS2-Experience と連携する際に使用するスタミナ最大値テーブル */
		[UnityEngine.SerializeField]
		public EzMaxStaminaTable MaxStaminaTable;
		/** GS2-Experience と連携する際に使用する回復間隔テーブル */
		[UnityEngine.SerializeField]
		public EzRecoverIntervalTable RecoverIntervalTable;
		/** GS2-Experience と連携する際に使用する回復量テーブル */
		[UnityEngine.SerializeField]
		public EzRecoverValueTable RecoverValueTable;

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

        public virtual StaminaModel ToModel()
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

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.Name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.Name);
            }
            if(this.Metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.Metadata);
            }
            writer.WritePropertyName("recoverIntervalMinutes");
            writer.Write(this.RecoverIntervalMinutes);
            writer.WritePropertyName("recoverValue");
            writer.Write(this.RecoverValue);
            writer.WritePropertyName("initialCapacity");
            writer.Write(this.InitialCapacity);
            writer.WritePropertyName("isOverflow");
            writer.Write(this.IsOverflow);
            writer.WritePropertyName("maxCapacity");
            writer.Write(this.MaxCapacity);
            if(this.MaxStaminaTable != null)
            {
                writer.WritePropertyName("maxStaminaTable");
                this.MaxStaminaTable.WriteJson(writer);
            }
            if(this.RecoverIntervalTable != null)
            {
                writer.WritePropertyName("recoverIntervalTable");
                this.RecoverIntervalTable.WriteJson(writer);
            }
            if(this.RecoverValueTable != null)
            {
                writer.WritePropertyName("recoverValueTable");
                this.RecoverValueTable.WriteJson(writer);
            }
            writer.WriteObjectEnd();
        }
	}
}
