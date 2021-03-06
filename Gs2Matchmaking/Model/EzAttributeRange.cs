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
using Gs2.Gs2Matchmaking.Model;
using System.Collections.Generic;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Matchmaking.Model
{
	[Preserve]
	[System.Serializable]
	public class EzAttributeRange
	{
		/** 属性名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** ギャザリング参加可能な属性値の最小値 */
		[UnityEngine.SerializeField]
		public int Min;
		/** ギャザリング参加可能な属性値の最大値 */
		[UnityEngine.SerializeField]
		public int Max;

		public EzAttributeRange()
		{

		}

		public EzAttributeRange(Gs2.Gs2Matchmaking.Model.AttributeRange @attributeRange)
		{
			Name = @attributeRange.name;
			Min = @attributeRange.min.HasValue ? @attributeRange.min.Value : 0;
			Max = @attributeRange.max.HasValue ? @attributeRange.max.Value : 0;
		}

        public virtual AttributeRange ToModel()
        {
            return new AttributeRange {
                name = Name,
                min = Min,
                max = Max,
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
            writer.WritePropertyName("min");
            writer.Write(this.Min);
            writer.WritePropertyName("max");
            writer.Write(this.Max);
            writer.WriteObjectEnd();
        }
	}
}
