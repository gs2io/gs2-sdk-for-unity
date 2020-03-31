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
using Gs2.Gs2Limit.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Limit.Model
{
	[Preserve]
	[System.Serializable]
	public class EzLimitModel
	{
		/** 回数制限の種類 */
		[UnityEngine.SerializeField]
		public string LimitModelId;
		/** 回数制限の種類名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** 回数制限の種類のメタデータ */
		[UnityEngine.SerializeField]
		public string Metadata;
		/** リセットタイミング */
		[UnityEngine.SerializeField]
		public string ResetType;
		/** リセットをする日にち */
		[UnityEngine.SerializeField]
		public int ResetDayOfMonth;
		/** リセットする曜日 */
		[UnityEngine.SerializeField]
		public string ResetDayOfWeek;
		/** リセット時刻 */
		[UnityEngine.SerializeField]
		public int ResetHour;

		public EzLimitModel()
		{

		}

		public EzLimitModel(Gs2.Gs2Limit.Model.LimitModel @limitModel)
		{
			LimitModelId = @limitModel.limitModelId;
			Name = @limitModel.name;
			Metadata = @limitModel.metadata;
			ResetType = @limitModel.resetType;
			ResetDayOfMonth = @limitModel.resetDayOfMonth.HasValue ? @limitModel.resetDayOfMonth.Value : 0;
			ResetDayOfWeek = @limitModel.resetDayOfWeek;
			ResetHour = @limitModel.resetHour.HasValue ? @limitModel.resetHour.Value : 0;
		}

        public virtual LimitModel ToModel()
        {
            return new LimitModel {
                limitModelId = LimitModelId,
                name = Name,
                metadata = Metadata,
                resetType = ResetType,
                resetDayOfMonth = ResetDayOfMonth,
                resetDayOfWeek = ResetDayOfWeek,
                resetHour = ResetHour,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.LimitModelId != null)
            {
                writer.WritePropertyName("limitModelId");
                writer.Write(this.LimitModelId);
            }
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
            if(this.ResetType != null)
            {
                writer.WritePropertyName("resetType");
                writer.Write(this.ResetType);
            }
            writer.WritePropertyName("resetDayOfMonth");
            writer.Write(this.ResetDayOfMonth);
            if(this.ResetDayOfWeek != null)
            {
                writer.WritePropertyName("resetDayOfWeek");
                writer.Write(this.ResetDayOfWeek);
            }
            writer.WritePropertyName("resetHour");
            writer.Write(this.ResetHour);
            writer.WriteObjectEnd();
        }
	}
}
