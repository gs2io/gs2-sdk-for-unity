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
using Gs2.Gs2Exchange.Model;
using System.Collections.Generic;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Exchange.Model
{
	[Preserve]
	[System.Serializable]
	public class EzAwait
	{
		/** ユーザーID */
		[UnityEngine.SerializeField]
		public string UserId;
		/** 交換レート名 */
		[UnityEngine.SerializeField]
		public string RateName;
		/** 交換待機の名前 */
		[UnityEngine.SerializeField]
		public string Name;
		/** 作成日時 */
		[UnityEngine.SerializeField]
		public long ExchangedAt;

		public EzAwait()
		{

		}

		public EzAwait(Gs2.Gs2Exchange.Model.Await @await)
		{
			UserId = @await.userId;
			RateName = @await.rateName;
			Name = @await.name;
			ExchangedAt = @await.exchangedAt.HasValue ? @await.exchangedAt.Value : 0;
		}

        public virtual Await ToModel()
        {
            return new Await {
                userId = UserId,
                rateName = RateName,
                name = Name,
                exchangedAt = ExchangedAt,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.UserId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.UserId);
            }
            if(this.RateName != null)
            {
                writer.WritePropertyName("rateName");
                writer.Write(this.RateName);
            }
            if(this.Name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.Name);
            }
            writer.WritePropertyName("exchangedAt");
            writer.Write(this.ExchangedAt);
            writer.WriteObjectEnd();
        }
	}
}
