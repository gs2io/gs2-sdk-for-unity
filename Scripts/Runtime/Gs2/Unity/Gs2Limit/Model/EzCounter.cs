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
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Limit.Model
{
	[Preserve]
	public class EzCounter
	{
		/** カウンター */
		public string CounterId { get; set; }
		/** カウンターの名前 */
		public string Name { get; set; }
		/** カウント値 */
		public int Count { get; set; }
		/** 作成日時 */
		public long CreatedAt { get; set; }
		/** 最終更新日時 */
		public long UpdatedAt { get; set; }

		public EzCounter()
		{

		}

		public EzCounter(Gs2.Gs2Limit.Model.Counter @counter)
		{
			CounterId = @counter.counterId;
			Name = @counter.name;
			Count = @counter.count.HasValue ? @counter.count.Value : 0;
			CreatedAt = @counter.createdAt.HasValue ? @counter.createdAt.Value : 0;
			UpdatedAt = @counter.updatedAt.HasValue ? @counter.updatedAt.Value : 0;
		}

        public Counter ToModel()
        {
            return new Counter {
                counterId = CounterId,
                name = Name,
                count = Count,
                createdAt = CreatedAt,
                updatedAt = UpdatedAt,
            };
        }
	}
}
