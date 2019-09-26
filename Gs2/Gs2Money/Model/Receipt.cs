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
using Gs2.Core.Model;
using LitJson;

namespace Gs2.Gs2Money.Model
{
	public class Receipt
	{

        /** レシート */
        public string receiptId { set; get; }

        /**
         * レシートを設定
         *
         * @param receiptId レシート
         * @return this
         */
        public Receipt WithReceiptId(string receiptId) {
            this.receiptId = receiptId;
            return this;
        }

        /** トランザクションID */
        public string transactionId { set; get; }

        /**
         * トランザクションIDを設定
         *
         * @param transactionId トランザクションID
         * @return this
         */
        public Receipt WithTransactionId(string transactionId) {
            this.transactionId = transactionId;
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
        public Receipt WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** 種類 */
        public string type { set; get; }

        /**
         * 種類を設定
         *
         * @param type 種類
         * @return this
         */
        public Receipt WithType(string type) {
            this.type = type;
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
        public Receipt WithSlot(int? slot) {
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
        public Receipt WithPrice(float? price) {
            this.price = price;
            return this;
        }

        /** 有償課金通貨 */
        public int? paid { set; get; }

        /**
         * 有償課金通貨を設定
         *
         * @param paid 有償課金通貨
         * @return this
         */
        public Receipt WithPaid(int? paid) {
            this.paid = paid;
            return this;
        }

        /** 無償課金通貨 */
        public int? free { set; get; }

        /**
         * 無償課金通貨を設定
         *
         * @param free 無償課金通貨
         * @return this
         */
        public Receipt WithFree(int? free) {
            this.free = free;
            return this;
        }

        /** 総数 */
        public int? total { set; get; }

        /**
         * 総数を設定
         *
         * @param total 総数
         * @return this
         */
        public Receipt WithTotal(int? total) {
            this.total = total;
            return this;
        }

        /** ストアプラットフォームで販売されているコンテンツID */
        public string contentsId { set; get; }

        /**
         * ストアプラットフォームで販売されているコンテンツIDを設定
         *
         * @param contentsId ストアプラットフォームで販売されているコンテンツID
         * @return this
         */
        public Receipt WithContentsId(string contentsId) {
            this.contentsId = contentsId;
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
        public Receipt WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.receiptId != null)
            {
                writer.WritePropertyName("receiptId");
                writer.Write(this.receiptId);
            }
            if(this.transactionId != null)
            {
                writer.WritePropertyName("transactionId");
                writer.Write(this.transactionId);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.type != null)
            {
                writer.WritePropertyName("type");
                writer.Write(this.type);
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
            if(this.total.HasValue)
            {
                writer.WritePropertyName("total");
                writer.Write(this.total.Value);
            }
            if(this.contentsId != null)
            {
                writer.WritePropertyName("contentsId");
                writer.Write(this.contentsId);
            }
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            writer.WriteObjectEnd();
        }
	}
}