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
using Gs2.Gs2Experience.Model;
using System.Collections.Generic;
using System.Linq;


namespace Gs2.Unity.Gs2Experience.Model
{
	public class EzProperty
	{
		/** 経験値の種類名 */
		public string ExperienceName { get; set; }
		/** プロパティID */
		public string PropertyId { get; set; }

		public EzProperty()
		{

		}

		public EzProperty(Gs2.Gs2Experience.Model.Property @property)
		{
			ExperienceName = @property.experienceName;
			PropertyId = @property.propertyId;
		}

        public Property ToModel()
        {
            return new Property {
                experienceName = ExperienceName,
                propertyId = PropertyId,
            };
        }
	}
}
