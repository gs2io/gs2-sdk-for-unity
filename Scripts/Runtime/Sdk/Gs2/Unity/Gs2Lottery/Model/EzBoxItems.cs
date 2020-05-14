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
using Gs2.Gs2Lottery.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Lottery.Model
{
	[Preserve]
	[System.Serializable]
	public class EzBoxItems
	{
		/** ボックス */
		[UnityEngine.SerializeField]
		public string BoxId;
		/** 排出確率テーブル名 */
		[UnityEngine.SerializeField]
		public string PrizeTableName;
		/** ボックスから取り出したアイテムのリスト */
		[UnityEngine.SerializeField]
		public List<EzBoxItem> Items;

		public EzBoxItems()
		{

		}

		public EzBoxItems(Gs2.Gs2Lottery.Model.BoxItems @boxItems)
		{
			BoxId = @boxItems.boxId;
			PrizeTableName = @boxItems.prizeTableName;
			Items = @boxItems.items != null ? @boxItems.items.Select(value =>
                {
                    return new EzBoxItem(value);
                }
			).ToList() : new List<EzBoxItem>(new EzBoxItem[] {});
		}

        public virtual BoxItems ToModel()
        {
            return new BoxItems {
                boxId = BoxId,
                prizeTableName = PrizeTableName,
                items = Items != null ? Items.Select(Value0 =>
                        {
                            return new BoxItem
                            {
                                acquireActions = Value0.AcquireActions != null ? Value0.AcquireActions.Select(Value1 =>
                                        {
                                            return new AcquireAction
                                            {
                                                action = Value1.Action,
                                                request = Value1.Request,
                                            };
                                        }
                                ).ToList() : new List<AcquireAction>(new AcquireAction[] {}),
                                remaining = Value0.Remaining,
                                initial = Value0.Initial,
                            };
                        }
                ).ToList() : new List<BoxItem>(new BoxItem[] {}),
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.BoxId != null)
            {
                writer.WritePropertyName("boxId");
                writer.Write(this.BoxId);
            }
            if(this.PrizeTableName != null)
            {
                writer.WritePropertyName("prizeTableName");
                writer.Write(this.PrizeTableName);
            }
            if(this.Items != null)
            {
                writer.WritePropertyName("items");
                writer.WriteArrayStart();
                foreach(var item in this.Items)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
        }
	}
}
