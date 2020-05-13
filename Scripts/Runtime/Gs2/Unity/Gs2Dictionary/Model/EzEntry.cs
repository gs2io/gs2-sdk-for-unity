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
using Gs2.Gs2Dictionary.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Dictionary.Model
{
	[Preserve]
	[System.Serializable]
	public class EzEntry
	{
		/** エントリー のGRN */
		[UnityEngine.SerializeField]
		public string EntryId;
		/** ユーザーID */
		[UnityEngine.SerializeField]
		public string UserId;
		/** エントリーの種類名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** None */
		[UnityEngine.SerializeField]
		public long AcquiredAt;

		public EzEntry()
		{

		}

		public EzEntry(Gs2.Gs2Dictionary.Model.Entry @entry)
		{
			EntryId = @entry.entryId;
			UserId = @entry.userId;
			Name = @entry.name;
			AcquiredAt = @entry.acquiredAt.HasValue ? @entry.acquiredAt.Value : 0;
		}

        public virtual Entry ToModel()
        {
            return new Entry {
                entryId = EntryId,
                userId = UserId,
                name = Name,
                acquiredAt = AcquiredAt,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.EntryId != null)
            {
                writer.WritePropertyName("entryId");
                writer.Write(this.EntryId);
            }
            if(this.UserId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.UserId);
            }
            if(this.Name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.Name);
            }
            writer.WritePropertyName("acquiredAt");
            writer.Write(this.AcquiredAt);
            writer.WriteObjectEnd();
        }
	}
}
