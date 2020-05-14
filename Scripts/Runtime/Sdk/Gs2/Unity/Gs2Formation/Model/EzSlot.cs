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
using Gs2.Gs2Formation.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Formation.Model
{
	[Preserve]
	[System.Serializable]
	public class EzSlot
	{
		/** スロットモデル名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** プロパティID */
		[UnityEngine.SerializeField]
		public string PropertyId;

		public EzSlot()
		{

		}

		public EzSlot(Gs2.Gs2Formation.Model.Slot @slot)
		{
			Name = @slot.name;
			PropertyId = @slot.propertyId;
		}

        public virtual Slot ToModel()
        {
            return new Slot {
                name = Name,
                propertyId = PropertyId,
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
            if(this.PropertyId != null)
            {
                writer.WritePropertyName("propertyId");
                writer.Write(this.PropertyId);
            }
            writer.WriteObjectEnd();
        }
	}
}
