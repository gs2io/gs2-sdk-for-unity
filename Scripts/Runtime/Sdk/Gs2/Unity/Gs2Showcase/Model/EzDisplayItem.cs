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
	public class EzDisplayItem
	{
		/** 陳列商品ID */
		[UnityEngine.SerializeField]
		public string DisplayItemId;
		/** 種類 */
		[UnityEngine.SerializeField]
		public string Type;
		/** 陳列する商品 */
		[UnityEngine.SerializeField]
		public EzSalesItem SalesItem;
		/** 陳列する商品グループ */
		[UnityEngine.SerializeField]
		public EzSalesItemGroup SalesItemGroup;

		public EzDisplayItem()
		{

		}

		public EzDisplayItem(Gs2.Gs2Showcase.Model.DisplayItem @displayItem)
		{
			DisplayItemId = @displayItem.displayItemId;
			Type = @displayItem.type;
			SalesItem = @displayItem.salesItem != null ? new EzSalesItem(@displayItem.salesItem) : null;
			SalesItemGroup = @displayItem.salesItemGroup != null ? new EzSalesItemGroup(@displayItem.salesItemGroup) : null;
		}

        public virtual DisplayItem ToModel()
        {
            return new DisplayItem {
                displayItemId = DisplayItemId,
                type = Type,
                salesItem = new SalesItem {
                    name = SalesItem.Name,
                    metadata = SalesItem.Metadata,
                    consumeActions = SalesItem.ConsumeActions != null ? SalesItem.ConsumeActions.Select(Value1 =>
                            {
                                return new ConsumeAction
                                {
                                    action = Value1.Action,
                                    request = Value1.Request,
                                };
                            }
                    ).ToList() : new List<ConsumeAction>(new ConsumeAction[] {}),
                    acquireActions = SalesItem.AcquireActions != null ? SalesItem.AcquireActions.Select(Value1 =>
                            {
                                return new AcquireAction
                                {
                                    action = Value1.Action,
                                    request = Value1.Request,
                                };
                            }
                    ).ToList() : new List<AcquireAction>(new AcquireAction[] {}),
                },
                salesItemGroup = new SalesItemGroup {
                    name = SalesItemGroup.Name,
                    metadata = SalesItemGroup.Metadata,
                    salesItems = SalesItemGroup.SalesItems != null ? SalesItemGroup.SalesItems.Select(Value1 =>
                            {
                                return new SalesItem
                                {
                                    name = Value1.Name,
                                    metadata = Value1.Metadata,
                                    consumeActions = Value1.ConsumeActions != null ? Value1.ConsumeActions.Select(Value2 =>
                                            {
                                                return new ConsumeAction
                                                {
                                                    action = Value2.Action,
                                                    request = Value2.Request,
                                                };
                                            }
                                    ).ToList() : new List<ConsumeAction>(new ConsumeAction[] {}),
                                    acquireActions = Value1.AcquireActions != null ? Value1.AcquireActions.Select(Value2 =>
                                            {
                                                return new AcquireAction
                                                {
                                                    action = Value2.Action,
                                                    request = Value2.Request,
                                                };
                                            }
                                    ).ToList() : new List<AcquireAction>(new AcquireAction[] {}),
                                };
                            }
                    ).ToList() : new List<SalesItem>(new SalesItem[] {}),
                },
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.DisplayItemId != null)
            {
                writer.WritePropertyName("displayItemId");
                writer.Write(this.DisplayItemId);
            }
            if(this.Type != null)
            {
                writer.WritePropertyName("type");
                writer.Write(this.Type);
            }
            if(this.SalesItem != null)
            {
                writer.WritePropertyName("salesItem");
                this.SalesItem.WriteJson(writer);
            }
            if(this.SalesItemGroup != null)
            {
                writer.WritePropertyName("salesItemGroup");
                this.SalesItemGroup.WriteJson(writer);
            }
            writer.WriteObjectEnd();
        }
	}
}
