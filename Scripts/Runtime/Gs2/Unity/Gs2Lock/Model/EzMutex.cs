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
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Lock.Model
{
	[Preserve]
	public class EzMutex
	{
		/** ミューテックス */
		public string MutexId { get; set; }
		/** プロパティID */
		public string PropertyId { get; set; }
		/** ロックを取得したトランザクションID */
		public string TransactionId { get; set; }
		/** 参照回数 */
		public int ReferenceCount { get; set; }
		/** ロックの有効期限 */
		public long TtlAt { get; set; }

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

        public Mutex ToModel()
        {
            return new Mutex {
                mutexId = MutexId,
                propertyId = PropertyId,
                transactionId = TransactionId,
                referenceCount = ReferenceCount,
                ttlAt = TtlAt,
            };
        }
	}
}
