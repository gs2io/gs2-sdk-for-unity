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
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Project.Model
{
	[Preserve]
	public class Receipt : IComparable
	{

        /** 領収書 */
        public string receiptId { set; get; }

        /**
         * 領収書を設定
         *
         * @param receiptId 領収書
         * @return this
         */
        public Receipt WithReceiptId(string receiptId) {
            this.receiptId = receiptId;
            return this;
        }

        /** GS2アカウントの名前 */
        public string accountName { set; get; }

        /**
         * GS2アカウントの名前を設定
         *
         * @param accountName GS2アカウントの名前
         * @return this
         */
        public Receipt WithAccountName(string accountName) {
            this.accountName = accountName;
            return this;
        }

        /** 請求書名 */
        public string name { set; get; }

        /**
         * 請求書名を設定
         *
         * @param name 請求書名
         * @return this
         */
        public Receipt WithName(string name) {
            this.name = name;
            return this;
        }

        /** 請求月 */
        public long? date { set; get; }

        /**
         * 請求月を設定
         *
         * @param date 請求月
         * @return this
         */
        public Receipt WithDate(long? date) {
            this.date = date;
            return this;
        }

        /** 請求金額 */
        public string amount { set; get; }

        /**
         * 請求金額を設定
         *
         * @param amount 請求金額
         * @return this
         */
        public Receipt WithAmount(string amount) {
            this.amount = amount;
            return this;
        }

        /** PDF URL */
        public string pdfUrl { set; get; }

        /**
         * PDF URLを設定
         *
         * @param pdfUrl PDF URL
         * @return this
         */
        public Receipt WithPdfUrl(string pdfUrl) {
            this.pdfUrl = pdfUrl;
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

        /** 最終更新日時 */
        public long? updatedAt { set; get; }

        /**
         * 最終更新日時を設定
         *
         * @param updatedAt 最終更新日時
         * @return this
         */
        public Receipt WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
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
            if(this.accountName != null)
            {
                writer.WritePropertyName("accountName");
                writer.Write(this.accountName);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.date.HasValue)
            {
                writer.WritePropertyName("date");
                writer.Write(this.date.Value);
            }
            if(this.amount != null)
            {
                writer.WritePropertyName("amount");
                writer.Write(this.amount);
            }
            if(this.pdfUrl != null)
            {
                writer.WritePropertyName("pdfUrl");
                writer.Write(this.pdfUrl);
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

    public static string GetReceiptNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:::gs2:account:(?<accountName>.*):receipt:(?<receiptName>.*)");
        if (!match.Groups["receiptName"].Success)
        {
            return null;
        }
        return match.Groups["receiptName"].Value;
    }

    public static string GetAccountNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:::gs2:account:(?<accountName>.*):receipt:(?<receiptName>.*)");
        if (!match.Groups["accountName"].Success)
        {
            return null;
        }
        return match.Groups["accountName"].Value;
    }

    	[Preserve]
        public static Receipt FromDict(JsonData data)
        {
            return new Receipt()
                .WithReceiptId(data.Keys.Contains("receiptId") && data["receiptId"] != null ? data["receiptId"].ToString() : null)
                .WithAccountName(data.Keys.Contains("accountName") && data["accountName"] != null ? data["accountName"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithDate(data.Keys.Contains("date") && data["date"] != null ? (long?)long.Parse(data["date"].ToString()) : null)
                .WithAmount(data.Keys.Contains("amount") && data["amount"] != null ? data["amount"].ToString() : null)
                .WithPdfUrl(data.Keys.Contains("pdfUrl") && data["pdfUrl"] != null ? data["pdfUrl"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as Receipt;
            var diff = 0;
            if (receiptId == null && receiptId == other.receiptId)
            {
                // null and null
            }
            else
            {
                diff += receiptId.CompareTo(other.receiptId);
            }
            if (accountName == null && accountName == other.accountName)
            {
                // null and null
            }
            else
            {
                diff += accountName.CompareTo(other.accountName);
            }
            if (name == null && name == other.name)
            {
                // null and null
            }
            else
            {
                diff += name.CompareTo(other.name);
            }
            if (date == null && date == other.date)
            {
                // null and null
            }
            else
            {
                diff += (int)(date - other.date);
            }
            if (amount == null && amount == other.amount)
            {
                // null and null
            }
            else
            {
                diff += amount.CompareTo(other.amount);
            }
            if (pdfUrl == null && pdfUrl == other.pdfUrl)
            {
                // null and null
            }
            else
            {
                diff += pdfUrl.CompareTo(other.pdfUrl);
            }
            if (createdAt == null && createdAt == other.createdAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(createdAt - other.createdAt);
            }
            if (updatedAt == null && updatedAt == other.updatedAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(updatedAt - other.updatedAt);
            }
            return diff;
        }
	}
}