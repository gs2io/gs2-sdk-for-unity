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
	public class WalletDetail
	{

        /** ウォレットの詳細 */
        public string walletDetailId { set; get; }

        /**
         * ウォレットの詳細を設定
         *
         * @param walletDetailId ウォレットの詳細
         * @return this
         */
        public WalletDetail WithWalletDetailId(string walletDetailId) {
            this.walletDetailId = walletDetailId;
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
        public WalletDetail WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** None */
        public int? slot { set; get; }

        /**
         * Noneを設定
         *
         * @param slot None
         * @return this
         */
        public WalletDetail WithSlot(int? slot) {
            this.slot = slot;
            return this;
        }

        /** 単価 */
        public float? price { set; get; }

        /**
         * 単価を設定
         *
         * @param price 単価
         * @return this
         */
        public WalletDetail WithPrice(float? price) {
            this.price = price;
            return this;
        }

        /** 所持量 */
        public int? count { set; get; }

        /**
         * 所持量を設定
         *
         * @param count 所持量
         * @return this
         */
        public WalletDetail WithCount(int? count) {
            this.count = count;
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
        public WalletDetail WithCreatedAt(long? createdAt) {
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
        public WalletDetail WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.walletDetailId != null)
            {
                writer.WritePropertyName("walletDetailId");
                writer.Write(this.walletDetailId);
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
            if(this.price.HasValue)
            {
                writer.WritePropertyName("price");
                writer.Write(this.price.Value);
            }
            if(this.count.HasValue)
            {
                writer.WritePropertyName("count");
                writer.Write(this.count.Value);
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
        public static WalletDetail FromDict(JsonData data)
        {
            return new WalletDetail()
                .WithWalletDetailId(data.Keys.Contains("walletDetailId") && data["walletDetailId"] != null ? data["walletDetailId"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithSlot(data.Keys.Contains("slot") && data["slot"] != null ? (int?)int.Parse(data["slot"].ToString()) : null)
                .WithPrice(data.Keys.Contains("price") && data["price"] != null ? (float?)float.Parse(data["price"].ToString()) : null)
                .WithCount(data.Keys.Contains("count") && data["count"] != null ? (int?)int.Parse(data["count"].ToString()) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}