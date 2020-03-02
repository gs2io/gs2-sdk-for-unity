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
using Gs2.Gs2Money.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Money.Model
{
	[Preserve]
	[System.Serializable]
	public class EzWallet
	{
		/** スロット番号 */
		[UnityEngine.SerializeField]
		public int Slot;
		/** 有償課金通貨所持量 */
		[UnityEngine.SerializeField]
		public int Paid;
		/** 無償課金通貨所持量 */
		[UnityEngine.SerializeField]
		public int Free;
		/** 最終更新日時 */
		[UnityEngine.SerializeField]
		public long UpdatedAt;

		public EzWallet()
		{

		}

		public EzWallet(Gs2.Gs2Money.Model.Wallet @wallet)
		{
			Slot = @wallet.slot.HasValue ? @wallet.slot.Value : 0;
			Paid = @wallet.paid.HasValue ? @wallet.paid.Value : 0;
			Free = @wallet.free.HasValue ? @wallet.free.Value : 0;
			UpdatedAt = @wallet.updatedAt.HasValue ? @wallet.updatedAt.Value : 0;
		}

        public virtual Wallet ToModel()
        {
            return new Wallet {
                slot = Slot,
                paid = Paid,
                free = Free,
                updatedAt = UpdatedAt,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WritePropertyName("slot");
            writer.Write(this.Slot);
            writer.WritePropertyName("paid");
            writer.Write(this.Paid);
            writer.WritePropertyName("free");
            writer.Write(this.Free);
            writer.WritePropertyName("updatedAt");
            writer.Write(this.UpdatedAt);
            writer.WriteObjectEnd();
        }
	}
}
