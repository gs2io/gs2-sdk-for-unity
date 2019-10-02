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
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Showcase.Model
{
	[Preserve]
	public class EzDisplayItem
	{
		/** 陳列商品ID */
		public string DisplayItemId { get; set; }
		/** 種類 */
		public string Type { get; set; }
		/** 陳列する商品 */
		public EzSalesItem SalesItem { get; set; }
		/** 陳列する商品グループ */
		public EzSalesItemGroup SalesItemGroup { get; set; }

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

        public DisplayItem ToModel()
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
	}
}
