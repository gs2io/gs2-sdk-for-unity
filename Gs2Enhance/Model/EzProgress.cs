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
using Gs2.Gs2Enhance.Model;
using System.Collections.Generic;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Enhance.Model
{
	[Preserve]
	[System.Serializable]
	public class EzProgress
	{
		/** 強化実行 */
		[UnityEngine.SerializeField]
		public string ProgressId;
		/** レートモデル名 */
		[UnityEngine.SerializeField]
		public string RateName;
		/** 強化対象のプロパティID */
		[UnityEngine.SerializeField]
		public string PropertyId;
		/** 入手できる経験値 */
		[UnityEngine.SerializeField]
		public int ExperienceValue;
		/** 経験値倍率 */
		[UnityEngine.SerializeField]
		public float Rate;

		public EzProgress()
		{

		}

		public EzProgress(Gs2.Gs2Enhance.Model.Progress @progress)
		{
			ProgressId = @progress.progressId;
			RateName = @progress.rateName;
			PropertyId = @progress.propertyId;
			ExperienceValue = @progress.experienceValue.HasValue ? @progress.experienceValue.Value : 0;
			Rate = @progress.rate.HasValue ? @progress.rate.Value : 0;
		}

        public virtual Progress ToModel()
        {
            return new Progress {
                progressId = ProgressId,
                rateName = RateName,
                propertyId = PropertyId,
                experienceValue = ExperienceValue,
                rate = Rate,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.ProgressId != null)
            {
                writer.WritePropertyName("progressId");
                writer.Write(this.ProgressId);
            }
            if(this.RateName != null)
            {
                writer.WritePropertyName("rateName");
                writer.Write(this.RateName);
            }
            if(this.PropertyId != null)
            {
                writer.WritePropertyName("propertyId");
                writer.Write(this.PropertyId);
            }
            writer.WritePropertyName("experienceValue");
            writer.Write(this.ExperienceValue);
            writer.WritePropertyName("rate");
            writer.Write(this.Rate);
            writer.WriteObjectEnd();
        }
	}
}
