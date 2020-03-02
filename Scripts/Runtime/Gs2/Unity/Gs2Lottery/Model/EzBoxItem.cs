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
	public class EzBoxItem
	{
		/** 入手アクションのリスト */
		[UnityEngine.SerializeField]
		public List<EzAcquireAction> AcquireActions;
		/** 残り数量 */
		[UnityEngine.SerializeField]
		public int Remaining;
		/** 初期数量 */
		[UnityEngine.SerializeField]
		public int Initial;

		public EzBoxItem()
		{

		}

		public EzBoxItem(Gs2.Gs2Lottery.Model.BoxItem @boxItem)
		{
			AcquireActions = @boxItem.acquireActions != null ? @boxItem.acquireActions.Select(value =>
                {
                    return new EzAcquireAction(value);
                }
			).ToList() : new List<EzAcquireAction>(new EzAcquireAction[] {});
			Remaining = @boxItem.remaining.HasValue ? @boxItem.remaining.Value : 0;
			Initial = @boxItem.initial.HasValue ? @boxItem.initial.Value : 0;
		}

        public virtual BoxItem ToModel()
        {
            return new BoxItem {
                acquireActions = AcquireActions != null ? AcquireActions.Select(Value0 =>
                        {
                            return new AcquireAction
                            {
                                action = Value0.Action,
                                request = Value0.Request,
                            };
                        }
                ).ToList() : new List<AcquireAction>(new AcquireAction[] {}),
                remaining = Remaining,
                initial = Initial,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.AcquireActions != null)
            {
                writer.WritePropertyName("acquireActions");
                writer.WriteArrayStart();
                foreach(var item in this.AcquireActions)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            writer.WritePropertyName("remaining");
            writer.Write(this.Remaining);
            writer.WritePropertyName("initial");
            writer.Write(this.Initial);
            writer.WriteObjectEnd();
        }
	}
}
