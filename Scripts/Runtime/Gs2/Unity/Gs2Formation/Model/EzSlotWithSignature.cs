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
	public class EzSlotWithSignature
	{
		/** スロットモデル名 */
		public string Name { get; set; }
		/** プロパティの種類 */
		public string PropertyType { get; set; }
		/** ペイロード */
		public string Body { get; set; }
		/** プロパティIDのリソースを所有していることを証明する署名 */
		public string Signature { get; set; }

		public EzSlotWithSignature()
		{

		}

		public EzSlotWithSignature(Gs2.Gs2Formation.Model.SlotWithSignature @slotWithSignature)
		{
			Name = @slotWithSignature.name;
			PropertyType = @slotWithSignature.propertyType;
			Body = @slotWithSignature.body;
			Signature = @slotWithSignature.signature;
		}

        public SlotWithSignature ToModel()
        {
            return new SlotWithSignature {
                name = Name,
                propertyType = PropertyType,
                body = Body,
                signature = Signature,
            };
        }
	}
}
