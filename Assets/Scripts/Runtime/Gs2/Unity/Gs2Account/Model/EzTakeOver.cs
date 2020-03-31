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
using Gs2.Gs2Account.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Account.Model
{
	[Preserve]
	[System.Serializable]
	public class EzTakeOver
	{
		/** ユーザーID */
		[UnityEngine.SerializeField]
		public string UserId;
		/** スロット番号 */
		[UnityEngine.SerializeField]
		public int Type;
		/** 引き継ぎ用ユーザーID */
		[UnityEngine.SerializeField]
		public string UserIdentifier;
		/** 作成日時 */
		[UnityEngine.SerializeField]
		public long CreatedAt;

		public EzTakeOver()
		{

		}

		public EzTakeOver(Gs2.Gs2Account.Model.TakeOver @takeOver)
		{
			UserId = @takeOver.userId;
			Type = @takeOver.type.HasValue ? @takeOver.type.Value : 0;
			UserIdentifier = @takeOver.userIdentifier;
			CreatedAt = @takeOver.createdAt.HasValue ? @takeOver.createdAt.Value : 0;
		}

        public virtual TakeOver ToModel()
        {
            return new TakeOver {
                userId = UserId,
                type = Type,
                userIdentifier = UserIdentifier,
                createdAt = CreatedAt,
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
            writer.WritePropertyName("type");
            writer.Write(this.Type);
            if(this.UserIdentifier != null)
            {
                writer.WritePropertyName("userIdentifier");
                writer.Write(this.UserIdentifier);
            }
            writer.WritePropertyName("createdAt");
            writer.Write(this.CreatedAt);
            writer.WriteObjectEnd();
        }
	}
}
