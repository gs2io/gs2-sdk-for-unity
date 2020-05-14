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
	public class EzMold
	{
		/** フォームの保存領域の名前 */
		[UnityEngine.SerializeField]
		public string Name;
		/** ユーザーID */
		[UnityEngine.SerializeField]
		public string UserId;
		/** 現在のキャパシティ */
		[UnityEngine.SerializeField]
		public int Capacity;

		public EzMold()
		{

		}

		public EzMold(Gs2.Gs2Formation.Model.Mold @mold)
		{
			Name = @mold.name;
			UserId = @mold.userId;
			Capacity = @mold.capacity.HasValue ? @mold.capacity.Value : 0;
		}

        public virtual Mold ToModel()
        {
            return new Mold {
                name = Name,
                userId = UserId,
                capacity = Capacity,
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
            if(this.UserId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.UserId);
            }
            writer.WritePropertyName("capacity");
            writer.Write(this.Capacity);
            writer.WriteObjectEnd();
        }
	}
}
