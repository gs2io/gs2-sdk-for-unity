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
	public class EzFormModel
	{
		/** フォームの種類名 */
		public string Name { get; set; }
		/** フォームの種類のメタデータ */
		public string Metadata { get; set; }
		/** スリットリスト */
		public List<EzSlotModel> Slots { get; set; }

		public EzFormModel()
		{

		}

		public EzFormModel(Gs2.Gs2Formation.Model.FormModel @formModel)
		{
			Name = @formModel.name;
			Metadata = @formModel.metadata;
			Slots = @formModel.slots != null ? @formModel.slots.Select(value =>
                {
                    return new EzSlotModel(value);
                }
			).ToList() : new List<EzSlotModel>(new EzSlotModel[] {});
		}

        public FormModel ToModel()
        {
            return new FormModel {
                name = Name,
                metadata = Metadata,
                slots = Slots != null ? Slots.Select(Value0 =>
                        {
                            return new SlotModel
                            {
                                name = Value0.Name,
                                propertyRegex = Value0.PropertyRegex,
                                metadata = Value0.Metadata,
                            };
                        }
                ).ToList() : new List<SlotModel>(new SlotModel[] {}),
            };
        }
	}
}
