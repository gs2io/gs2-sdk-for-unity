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
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Formation.Model
{
	[Preserve]
	[System.Serializable]
	public class EzMoldModel
	{
		/** フォームの保存領域名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** メタデータ */
		[UnityEngine.SerializeField]
		public string Metadata;
		/** None */
		[UnityEngine.SerializeField]
		public EzFormModel FormModel;
		/** フォームを保存できる初期キャパシティ */
		[UnityEngine.SerializeField]
		public int InitialMaxCapacity;
		/** フォームを保存できるキャパシティ */
		[UnityEngine.SerializeField]
		public int MaxCapacity;

		public EzMoldModel()
		{

		}

		public EzMoldModel(Gs2.Gs2Formation.Model.MoldModel @moldModel)
		{
			Name = @moldModel.name;
			Metadata = @moldModel.metadata;
			FormModel = @moldModel.formModel != null ? new EzFormModel(@moldModel.formModel) : null;
			InitialMaxCapacity = @moldModel.initialMaxCapacity.HasValue ? @moldModel.initialMaxCapacity.Value : 0;
			MaxCapacity = @moldModel.maxCapacity.HasValue ? @moldModel.maxCapacity.Value : 0;
		}

        public virtual MoldModel ToModel()
        {
            return new MoldModel {
                name = Name,
                metadata = Metadata,
                formModel = new FormModel {
                    name = FormModel.Name,
                    metadata = FormModel.Metadata,
                    slots = FormModel.Slots != null ? FormModel.Slots.Select(Value1 =>
                            {
                                return new SlotModel
                                {
                                    name = Value1.Name,
                                    propertyRegex = Value1.PropertyRegex,
                                    metadata = Value1.Metadata,
                                };
                            }
                    ).ToList() : new List<SlotModel>(new SlotModel[] {}),
                },
                initialMaxCapacity = InitialMaxCapacity,
                maxCapacity = MaxCapacity,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.Name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.Name);
            }
            if(this.Metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.Metadata);
            }
            if(this.FormModel != null)
            {
                writer.WritePropertyName("formModel");
                this.FormModel.WriteJson(writer);
            }
            writer.WritePropertyName("initialMaxCapacity");
            writer.Write(this.InitialMaxCapacity);
            writer.WritePropertyName("maxCapacity");
            writer.Write(this.MaxCapacity);
            writer.WriteObjectEnd();
        }
	}
}
