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
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Matchmaking.Model
{
	[Preserve]
	[System.Serializable]
	public class EzAttribute
	{
		/** 属性名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** 属性値 */
		[UnityEngine.SerializeField]
		public int Value;

		public EzAttribute()
		{

		}

		public EzAttribute(Gs2.Gs2Matchmaking.Model.Attribute_ @attribute)
		{
			Name = @attribute.name;
			Value = @attribute.value.HasValue ? @attribute.value.Value : 0;
		}

        public virtual Attribute_ ToModel()
        {
            return new Attribute_ {
                name = Name,
                value = Value,
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
            writer.WritePropertyName("value");
            writer.Write(this.Value);
            writer.WriteObjectEnd();
        }
	}
}
