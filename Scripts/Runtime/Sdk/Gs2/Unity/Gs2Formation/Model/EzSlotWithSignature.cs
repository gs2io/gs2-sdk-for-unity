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
	public class EzSlotWithSignature
	{
		/** スロットモデル名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** プロパティの種類 */
		[UnityEngine.SerializeField]
		public string PropertyType;
		/** ペイロード */
		[UnityEngine.SerializeField]
		public string Body;
		/** プロパティIDのリソースを所有していることを証明する署名 */
		[UnityEngine.SerializeField]
		public string Signature;

		public EzSlotWithSignature()
		{

		}

		public EzSlotWithSignature(Gs2.Gs2Formation.Model.SlotWithSignature @slotWithSignature)
		{
			Name = @slotWithSignature.name;
			PropertyType = @slotWithSignature.propertyType;
			Body = @slotWithSignature.body;
			Signature = @slotWithSignature.signature;
		}

        public virtual SlotWithSignature ToModel()
        {
            return new SlotWithSignature {
                name = Name,
                propertyType = PropertyType,
                body = Body,
                signature = Signature,
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
            if(this.PropertyType != null)
            {
                writer.WritePropertyName("propertyType");
                writer.Write(this.PropertyType);
            }
            if(this.Body != null)
            {
                writer.WritePropertyName("body");
                writer.Write(this.Body);
            }
            if(this.Signature != null)
            {
                writer.WritePropertyName("signature");
                writer.Write(this.Signature);
            }
            writer.WriteObjectEnd();
        }
	}
}
