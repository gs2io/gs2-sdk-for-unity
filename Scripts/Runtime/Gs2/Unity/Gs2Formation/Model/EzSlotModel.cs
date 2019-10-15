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
	public class EzSlotModel
	{
		/** スロットモデル名 */
		public string Name { get; set; }
		/** プロパティとして設定可能な値の正規表現 */
		public string PropertyRegex { get; set; }
		/** メタデータ */
		public string Metadata { get; set; }

		public EzSlotModel()
		{

		}

		public EzSlotModel(Gs2.Gs2Formation.Model.SlotModel @slotModel)
		{
			Name = @slotModel.name;
			PropertyRegex = @slotModel.propertyRegex;
			Metadata = @slotModel.metadata;
		}

        public SlotModel ToModel()
        {
            return new SlotModel {
                name = Name,
                propertyRegex = PropertyRegex,
                metadata = Metadata,
            };
        }
	}
}
