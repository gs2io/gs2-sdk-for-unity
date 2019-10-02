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
	public class EzShowcase
	{
		/** 商品名 */
		public string Name { get; set; }
		/** 商品のメタデータ */
		public string Metadata { get; set; }
		/** インベントリに格納可能なアイテムモデル一覧 */
		public List<EzDisplayItem> DisplayItems { get; set; }

		public EzShowcase()
		{

		}

		public EzShowcase(Gs2.Gs2Showcase.Model.Showcase @showcase)
		{
			Name = @showcase.name;
			Metadata = @showcase.metadata;
			DisplayItems = @showcase.displayItems != null ? @showcase.displayItems.Select(value =>
                {
                    return new EzDisplayItem(value);
                }
			).ToList() : new List<EzDisplayItem>(new EzDisplayItem[] {});
		}

        public Showcase ToModel()
        {
            return new Showcase {
                name = Name,
                metadata = Metadata,
                displayItems = DisplayItems != null ? DisplayItems.Select(Value0 =>
                        {
                            return new DisplayItem
                            {
                                displayItemId = Value0.DisplayItemId,
                                type = Value0.Type,
                                salesItem = new SalesItem {
                                    name = Value0.SalesItem.Name,
                                    metadata = Value0.SalesItem.Metadata,
                                    consumeActions = Value0.SalesItem.ConsumeActions != null ? Value0.SalesItem.ConsumeActions.Select(Value2 =>
                                            {
                                                return new ConsumeAction
                                                {
                                                    action = Value2.Action,
                                                    request = Value2.Request,
                                                };
                                            }
                                    ).ToList() : new List<ConsumeAction>(new ConsumeAction[] {}),
                                    acquireActions = Value0.SalesItem.AcquireActions != null ? Value0.SalesItem.AcquireActions.Select(Value2 =>
                                            {
                                                return new AcquireAction
                                                {
                                                    action = Value2.Action,
                                                    request = Value2.Request,
                                                };
                                            }
                                    ).ToList() : new List<AcquireAction>(new AcquireAction[] {}),
                                },
                                salesItemGroup = new SalesItemGroup {
                                    name = Value0.SalesItemGroup.Name,
                                    metadata = Value0.SalesItemGroup.Metadata,
                                    salesItems = Value0.SalesItemGroup.SalesItems != null ? Value0.SalesItemGroup.SalesItems.Select(Value2 =>
                                            {
                                                return new SalesItem
                                                {
                                                    name = Value2.Name,
                                                    metadata = Value2.Metadata,
                                                    consumeActions = Value2.ConsumeActions != null ? Value2.ConsumeActions.Select(Value3 =>
                                                            {
                                                                return new ConsumeAction
                                                                {
                                                                    action = Value3.Action,
                                                                    request = Value3.Request,
                                                                };
                                                            }
                                                    ).ToList() : new List<ConsumeAction>(new ConsumeAction[] {}),
                                                    acquireActions = Value2.AcquireActions != null ? Value2.AcquireActions.Select(Value3 =>
                                                            {
                                                                return new AcquireAction
                                                                {
                                                                    action = Value3.Action,
                                                                    request = Value3.Request,
                                                                };
                                                            }
                                                    ).ToList() : new List<AcquireAction>(new AcquireAction[] {}),
                                                };
                                            }
                                    ).ToList() : new List<SalesItem>(new SalesItem[] {}),
                                },
                            };
                        }
                ).ToList() : new List<DisplayItem>(new DisplayItem[] {}),
            };
        }
	}
}
