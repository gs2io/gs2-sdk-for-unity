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
	public class EzMaterial
	{
		/** 強化対象の GS2-Inventory アイテムセットGRN */
		[UnityEngine.SerializeField]
		public string MaterialItemSetId;
		/** 消費数量 */
		[UnityEngine.SerializeField]
		public int Count;

		public EzMaterial()
		{

		}

		public EzMaterial(Gs2.Gs2Enhance.Model.Material @material)
		{
			MaterialItemSetId = @material.materialItemSetId;
			Count = @material.count.HasValue ? @material.count.Value : 0;
		}

        public virtual Material ToModel()
        {
            return new Material {
                materialItemSetId = MaterialItemSetId,
                count = Count,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.MaterialItemSetId != null)
            {
                writer.WritePropertyName("materialItemSetId");
                writer.Write(this.MaterialItemSetId);
            }
            writer.WritePropertyName("count");
            writer.Write(this.Count);
            writer.WriteObjectEnd();
        }
	}
}
