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

namespace Gs2.Gs2Account.Model
{
	[Preserve]
	public class Account
	{

        /** ゲームプレイヤーアカウント */
        public string accountId { set; get; }

        /**
         * ゲームプレイヤーアカウントを設定
         *
         * @param accountId ゲームプレイヤーアカウント
         * @return this
         */
        public Account WithAccountId(string accountId) {
            this.accountId = accountId;
            return this;
        }

        /** アカウントID */
        public string userId { set; get; }

        /**
         * アカウントIDを設定
         *
         * @param userId アカウントID
         * @return this
         */
        public Account WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** パスワード */
        public string password { set; get; }

        /**
         * パスワードを設定
         *
         * @param password パスワード
         * @return this
         */
        public Account WithPassword(string password) {
            this.password = password;
            return this;
        }

        /** 現在時刻に対する補正値（現在時刻を起点とした秒数） */
        public int? timeOffset { set; get; }

        /**
         * 現在時刻に対する補正値（現在時刻を起点とした秒数）を設定
         *
         * @param timeOffset 現在時刻に対する補正値（現在時刻を起点とした秒数）
         * @return this
         */
        public Account WithTimeOffset(int? timeOffset) {
            this.timeOffset = timeOffset;
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
        public Account WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.accountId != null)
            {
                writer.WritePropertyName("accountId");
                writer.Write(this.accountId);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.password != null)
            {
                writer.WritePropertyName("password");
                writer.Write(this.password);
            }
            if(this.timeOffset.HasValue)
            {
                writer.WritePropertyName("timeOffset");
                writer.Write(this.timeOffset.Value);
            }
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Account FromDict(JsonData data)
        {
            return new Account()
                .WithAccountId(data.Keys.Contains("accountId") && data["accountId"] != null ? data["accountId"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithPassword(data.Keys.Contains("password") && data["password"] != null ? data["password"].ToString() : null)
                .WithTimeOffset(data.Keys.Contains("timeOffset") && data["timeOffset"] != null ? (int?)int.Parse(data["timeOffset"].ToString()) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null);
        }
	}
}