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
using Gs2.Gs2Showcase.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Showcase.Model
{
	[Preserve]
	[System.Serializable]
	public class EzSalesItemGroup
	{
		/** 商品グループ名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** メタデータ */
		[UnityEngine.SerializeField]
		public string Metadata;
		/** 商品リスト */
		[UnityEngine.SerializeField]
		public List<EzSalesItem> SalesItems;

		public EzSalesItemGroup()
		{

		}

		public EzSalesItemGroup(Gs2.Gs2Showcase.Model.SalesItemGroup @salesItemGroup)
		{
			Name = @salesItemGroup.name;
			Metadata = @salesItemGroup.metadata;
			SalesItems = @salesItemGroup.salesItems != null ? @salesItemGroup.salesItems.Select(value =>
                {
                    return new EzSalesItem(value);
                }
			).ToList() : new List<EzSalesItem>(new EzSalesItem[] {});
		}

        public virtual SalesItemGroup ToModel()
        {
            return new SalesItemGroup {
                name = Name,
                metadata = Metadata,
                salesItems = SalesItems != null ? SalesItems.Select(Value0 =>
                        {
                            return new SalesItem
                            {
                                name = Value0.Name,
                                metadata = Value0.Metadata,
                                consumeActions = Value0.ConsumeActions != null ? Value0.ConsumeActions.Select(Value1 =>
                                        {
                                            return new ConsumeAction
                                            {
                                                action = Value1.Action,
                                                request = Value1.Request,
                                            };
                                        }
                                ).ToList() : new List<ConsumeAction>(new ConsumeAction[] {}),
                                acquireActions = Value0.AcquireActions != null ? Value0.AcquireActions.Select(Value1 =>
                                        {
                                            return new AcquireAction
                                            {
                                                action = Value1.Action,
                                                request = Value1.Request,
                                            };
                                        }
                                ).ToList() : new List<AcquireAction>(new AcquireAction[] {}),
                            };
                        }
                ).ToList() : new List<SalesItem>(new SalesItem[] {}),
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
            if(this.SalesItems != null)
            {
                writer.WritePropertyName("salesItems");
                writer.WriteArrayStart();
                foreach(var item in this.SalesItems)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
        }
	}
}
