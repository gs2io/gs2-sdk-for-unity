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
using System;
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Money.Model
{
	[Preserve]
	public class Wallet
	{

        /** ウォレット */
        public string walletId { set; get; }

        /**
         * ウォレットを設定
         *
         * @param walletId ウォレット
         * @return this
         */
        public Wallet WithWalletId(string walletId) {
            this.walletId = walletId;
            return this;
        }

        /** ユーザーID */
        public string userId { set; get; }

        /**
         * ユーザーIDを設定
         *
         * @param userId ユーザーID
         * @return this
         */
        public Wallet WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** スロット番号 */
        public int? slot { set; get; }

        /**
         * スロット番号を設定
         *
         * @param slot スロット番号
         * @return this
         */
        public Wallet WithSlot(int? slot) {
            this.slot = slot;
            return this;
        }

        /** 有償課金通貨所持量 */
        public int? paid { set; get; }

        /**
         * 有償課金通貨所持量を設定
         *
         * @param paid 有償課金通貨所持量
         * @return this
         */
        public Wallet WithPaid(int? paid) {
            this.paid = paid;
            return this;
        }

        /** 無償課金通貨所持量 */
        public int? free { set; get; }

        /**
         * 無償課金通貨所持量を設定
         *
         * @param free 無償課金通貨所持量
         * @return this
         */
        public Wallet WithFree(int? free) {
            this.free = free;
            return this;
        }

        /** 作成日時 */
        public long? createdAt { set; get; }

        /**
         * 作成日時を設定
         *
         * @param createdAt 作成日時
         * @return this
         */
        public Wallet WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        /** 最終更新日時 */
        public long? updatedAt { set; get; }

        /**
         * 最終更新日時を設定
         *
         * @param updatedAt 最終更新日時
         * @return this
         */
        public Wallet WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.walletId != null)
            {
                writer.WritePropertyName("walletId");
                writer.Write(this.walletId);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.slot.HasValue)
            {
                writer.WritePropertyName("slot");
                writer.Write(this.slot.Value);
            }
            if(this.paid.HasValue)
            {
                writer.WritePropertyName("paid");
                writer.Write(this.paid.Value);
            }
            if(this.free.HasValue)
            {
                writer.WritePropertyName("free");
                writer.Write(this.free.Value);
            }
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            if(this.updatedAt.HasValue)
            {
                writer.WritePropertyName("updatedAt");
                writer.Write(this.updatedAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Wallet FromDict(JsonData data)
        {
            return new Wallet()
                .WithWalletId(data.Keys.Contains("walletId") && data["walletId"] != null ? data["walletId"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithSlot(data.Keys.Contains("slot") && data["slot"] != null ? (int?)int.Parse(data["slot"].ToString()) : null)
                .WithPaid(data.Keys.Contains("paid") && data["paid"] != null ? (int?)int.Parse(data["paid"].ToString()) : null)
                .WithFree(data.Keys.Contains("free") && data["free"] != null ? (int?)int.Parse(data["free"].ToString()) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}