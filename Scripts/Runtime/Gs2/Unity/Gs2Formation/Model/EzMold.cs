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
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Formation.Model
{
	[Preserve]
	public class EzMold
	{
		/** フォームの保存領域の名前 */
		public string Name { get; set; }
		/** ユーザーID */
		public string UserId { get; set; }
		/** 現在のキャパシティ */
		public int Capacity { get; set; }

		public EzMold()
		{

		}

		public EzMold(Gs2.Gs2Formation.Model.Mold @mold)
		{
			Name = @mold.name;
			UserId = @mold.userId;
			Capacity = @mold.capacity.HasValue ? @mold.capacity.Value : 0;
		}

        public Mold ToModel()
        {
            return new Mold {
                name = Name,
                userId = UserId,
                capacity = Capacity,
            };
        }
	}
}
