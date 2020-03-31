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
using Gs2.Gs2Experience.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Experience.Model
{
	[Preserve]
	[System.Serializable]
	public class EzStatus
	{
		/** 経験値の種類の名前 */
		[UnityEngine.SerializeField]
		public string ExperienceName;
		/** プロパティID */
		[UnityEngine.SerializeField]
		public string PropertyId;
		/** 累計獲得経験値 */
		[UnityEngine.SerializeField]
		public long ExperienceValue;
		/** 現在のランク */
		[UnityEngine.SerializeField]
		public long RankValue;
		/** 現在のランクキャップ */
		[UnityEngine.SerializeField]
		public long RankCapValue;

		public EzStatus()
		{

		}

		public EzStatus(Gs2.Gs2Experience.Model.Status @status)
		{
			ExperienceName = @status.experienceName;
			PropertyId = @status.propertyId;
			ExperienceValue = @status.experienceValue.HasValue ? @status.experienceValue.Value : 0;
			RankValue = @status.rankValue.HasValue ? @status.rankValue.Value : 0;
			RankCapValue = @status.rankCapValue.HasValue ? @status.rankCapValue.Value : 0;
		}

        public virtual Status ToModel()
        {
            return new Status {
                experienceName = ExperienceName,
                propertyId = PropertyId,
                experienceValue = ExperienceValue,
                rankValue = RankValue,
                rankCapValue = RankCapValue,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.ExperienceName != null)
            {
                writer.WritePropertyName("experienceName");
                writer.Write(this.ExperienceName);
            }
            if(this.PropertyId != null)
            {
                writer.WritePropertyName("propertyId");
                writer.Write(this.PropertyId);
            }
            writer.WritePropertyName("experienceValue");
            writer.Write(this.ExperienceValue);
            writer.WritePropertyName("rankValue");
            writer.Write(this.RankValue);
            writer.WritePropertyName("rankCapValue");
            writer.Write(this.RankCapValue);
            writer.WriteObjectEnd();
        }
	}
}
