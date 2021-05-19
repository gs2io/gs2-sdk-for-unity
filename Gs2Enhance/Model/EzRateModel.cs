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
	public class EzRateModel
	{
		/** 強化レート名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** 強化レートのメタデータ */
		[UnityEngine.SerializeField]
		public string Metadata;
		/** 強化対象に使用できるインベントリモデル のGRN */
		[UnityEngine.SerializeField]
		public string TargetInventoryModelId;
		/** GS2-Experience で入手した経験値を格納する プロパティID に付与するサフィックス */
		[UnityEngine.SerializeField]
		public string AcquireExperienceSuffix;
		/** 強化素材に使用できるインベントリモデル のGRN */
		[UnityEngine.SerializeField]
		public string MaterialInventoryModelId;
		/** 獲得できる経験値の種類マスター のGRN */
		[UnityEngine.SerializeField]
		public string ExperienceModelId;

		public EzRateModel()
		{

		}

		public EzRateModel(Gs2.Gs2Enhance.Model.RateModel @rateModel)
		{
			Name = @rateModel.name;
			Metadata = @rateModel.metadata;
			TargetInventoryModelId = @rateModel.targetInventoryModelId;
			AcquireExperienceSuffix = @rateModel.acquireExperienceSuffix;
			MaterialInventoryModelId = @rateModel.materialInventoryModelId;
			ExperienceModelId = @rateModel.experienceModelId;
		}

        public virtual RateModel ToModel()
        {
            return new RateModel {
                name = Name,
                metadata = Metadata,
                targetInventoryModelId = TargetInventoryModelId,
                acquireExperienceSuffix = AcquireExperienceSuffix,
                materialInventoryModelId = MaterialInventoryModelId,
                experienceModelId = ExperienceModelId,
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
            if(this.TargetInventoryModelId != null)
            {
                writer.WritePropertyName("targetInventoryModelId");
                writer.Write(this.TargetInventoryModelId);
            }
            if(this.AcquireExperienceSuffix != null)
            {
                writer.WritePropertyName("acquireExperienceSuffix");
                writer.Write(this.AcquireExperienceSuffix);
            }
            if(this.MaterialInventoryModelId != null)
            {
                writer.WritePropertyName("materialInventoryModelId");
                writer.Write(this.MaterialInventoryModelId);
            }
            if(this.ExperienceModelId != null)
            {
                writer.WritePropertyName("experienceModelId");
                writer.Write(this.ExperienceModelId);
            }
            writer.WriteObjectEnd();
        }
	}
}
