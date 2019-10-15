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
	public class EzForm
	{
		/** フォームの保存領域の名前 */
		public string Name { get; set; }
		/** 保存領域のインデックス */
		public int Index { get; set; }
		/** スロットリスト */
		public List<EzSlot> Slots { get; set; }

		public EzForm()
		{

		}

		public EzForm(Gs2.Gs2Formation.Model.Form @form)
		{
			Name = @form.name;
			Index = @form.index.HasValue ? @form.index.Value : 0;
			Slots = @form.slots != null ? @form.slots.Select(value =>
                {
                    return new EzSlot(value);
                }
			).ToList() : new List<EzSlot>(new EzSlot[] {});
		}

        public Form ToModel()
        {
            return new Form {
                name = Name,
                index = Index,
                slots = Slots != null ? Slots.Select(Value0 =>
                        {
                            return new Slot
                            {
                                name = Value0.Name,
                                propertyId = Value0.PropertyId,
                            };
                        }
                ).ToList() : new List<Slot>(new Slot[] {}),
            };
        }
	}
}
