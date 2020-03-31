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
using Gs2.Gs2Lock.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Lock.Model
{
	[Preserve]
	[System.Serializable]
	public class EzMutex
	{
		/** ミューテックス */
		[UnityEngine.SerializeField]
		public string MutexId;
		/** プロパティID */
		[UnityEngine.SerializeField]
		public string PropertyId;
		/** ロックを取得したトランザクションID */
		[UnityEngine.SerializeField]
		public string TransactionId;
		/** 参照回数 */
		[UnityEngine.SerializeField]
		public int ReferenceCount;
		/** ロックの有効期限 */
		[UnityEngine.SerializeField]
		public long TtlAt;

		public EzMutex()
		{

		}

		public EzMutex(Gs2.Gs2Lock.Model.Mutex @mutex)
		{
			MutexId = @mutex.mutexId;
			PropertyId = @mutex.propertyId;
			TransactionId = @mutex.transactionId;
			ReferenceCount = @mutex.referenceCount.HasValue ? @mutex.referenceCount.Value : 0;
			TtlAt = @mutex.ttlAt.HasValue ? @mutex.ttlAt.Value : 0;
		}

        public virtual Mutex ToModel()
        {
            return new Mutex {
                mutexId = MutexId,
                propertyId = PropertyId,
                transactionId = TransactionId,
                referenceCount = ReferenceCount,
                ttlAt = TtlAt,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.MutexId != null)
            {
                writer.WritePropertyName("mutexId");
                writer.Write(this.MutexId);
            }
            if(this.PropertyId != null)
            {
                writer.WritePropertyName("propertyId");
                writer.Write(this.PropertyId);
            }
            if(this.TransactionId != null)
            {
                writer.WritePropertyName("transactionId");
                writer.Write(this.TransactionId);
            }
            writer.WritePropertyName("referenceCount");
            writer.Write(this.ReferenceCount);
            writer.WritePropertyName("ttlAt");
            writer.Write(this.TtlAt);
            writer.WriteObjectEnd();
        }
	}
}
