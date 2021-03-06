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
using Gs2.Gs2Limit.Model;
using System.Collections.Generic;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Limit.Model
{
	[Preserve]
	[System.Serializable]
	public class EzCounter
	{
		/** カウンター */
		[UnityEngine.SerializeField]
		public string CounterId;
		/** 回数制限の種類の名前 */
		[UnityEngine.SerializeField]
		public string LimitName;
		/** カウンターの名前 */
		[UnityEngine.SerializeField]
		public string Name;
		/** カウント値 */
		[UnityEngine.SerializeField]
		public int Count;
		/** 作成日時 */
		[UnityEngine.SerializeField]
		public long CreatedAt;
		/** 最終更新日時 */
		[UnityEngine.SerializeField]
		public long UpdatedAt;

		public EzCounter()
		{

		}

		public EzCounter(Gs2.Gs2Limit.Model.Counter @counter)
		{
			CounterId = @counter.counterId;
			LimitName = @counter.limitName;
			Name = @counter.name;
			Count = @counter.count.HasValue ? @counter.count.Value : 0;
			CreatedAt = @counter.createdAt.HasValue ? @counter.createdAt.Value : 0;
			UpdatedAt = @counter.updatedAt.HasValue ? @counter.updatedAt.Value : 0;
		}

        public virtual Counter ToModel()
        {
            return new Counter {
                counterId = CounterId,
                limitName = LimitName,
                name = Name,
                count = Count,
                createdAt = CreatedAt,
                updatedAt = UpdatedAt,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.CounterId != null)
            {
                writer.WritePropertyName("counterId");
                writer.Write(this.CounterId);
            }
            if(this.LimitName != null)
            {
                writer.WritePropertyName("limitName");
                writer.Write(this.LimitName);
            }
            if(this.Name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.Name);
            }
            writer.WritePropertyName("count");
            writer.Write(this.Count);
            writer.WritePropertyName("createdAt");
            writer.Write(this.CreatedAt);
            writer.WritePropertyName("updatedAt");
            writer.Write(this.UpdatedAt);
            writer.WriteObjectEnd();
        }
	}
}
