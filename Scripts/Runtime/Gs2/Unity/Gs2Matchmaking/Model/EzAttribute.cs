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
using Gs2.Gs2Matchmaking.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Matchmaking.Model
{
	[Preserve]
	public class EzAttribute
	{
		/** 属性名 */
		public string Name { get; set; }
		/** 属性値 */
		public int Value { get; set; }

		public EzAttribute()
		{

		}

		public EzAttribute(Gs2.Gs2Matchmaking.Model.Attribute @attribute)
		{
			Name = @attribute.name;
			Value = @attribute.value.HasValue ? @attribute.value.Value : 0;
		}

        public Attribute ToModel()
        {
            return new Attribute {
                name = Name,
                value = Value,
            };
        }
	}
}
