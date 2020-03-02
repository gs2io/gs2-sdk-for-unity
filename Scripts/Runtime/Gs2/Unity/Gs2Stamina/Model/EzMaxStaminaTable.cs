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
	public class EzMaxStaminaTable
	{
		/** 最大スタミナ値テーブル名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** 最大スタミナ値テーブルのメタデータ */
		[UnityEngine.SerializeField]
		public string Metadata;
		/** 経験値の種類マスター のGRN */
		[UnityEngine.SerializeField]
		public string ExperienceModelId;
		/** ランク毎のスタミナの最大値テーブル */
		[UnityEngine.SerializeField]
		public List<int> Values;

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

        public virtual MaxStaminaTable ToModel()
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
            if(this.ExperienceModelId != null)
            {
                writer.WritePropertyName("experienceModelId");
                writer.Write(this.ExperienceModelId);
            }
            if(this.Values != null)
            {
                writer.WritePropertyName("values");
                writer.WriteArrayStart();
                foreach(var item in this.Values)
                {
                    writer.Write(item);
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
        }
	}
}
