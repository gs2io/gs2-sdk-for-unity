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
	public class TakeOver
	{

        /** 引き継ぎ設定 */
        public string takeOverId { set; get; }

        /**
         * 引き継ぎ設定を設定
         *
         * @param takeOverId 引き継ぎ設定
         * @return this
         */
        public TakeOver WithTakeOverId(string takeOverId) {
            this.takeOverId = takeOverId;
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
        public TakeOver WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** スロット番号 */
        public int? type { set; get; }

        /**
         * スロット番号を設定
         *
         * @param type スロット番号
         * @return this
         */
        public TakeOver WithType(int? type) {
            this.type = type;
            return this;
        }

        /** 引き継ぎ用ユーザーID */
        public string userIdentifier { set; get; }

        /**
         * 引き継ぎ用ユーザーIDを設定
         *
         * @param userIdentifier 引き継ぎ用ユーザーID
         * @return this
         */
        public TakeOver WithUserIdentifier(string userIdentifier) {
            this.userIdentifier = userIdentifier;
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
        public TakeOver WithPassword(string password) {
            this.password = password;
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
        public TakeOver WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.takeOverId != null)
            {
                writer.WritePropertyName("takeOverId");
                writer.Write(this.takeOverId);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.type.HasValue)
            {
                writer.WritePropertyName("type");
                writer.Write(this.type.Value);
            }
            if(this.userIdentifier != null)
            {
                writer.WritePropertyName("userIdentifier");
                writer.Write(this.userIdentifier);
            }
            if(this.password != null)
            {
                writer.WritePropertyName("password");
                writer.Write(this.password);
            }
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static TakeOver FromDict(JsonData data)
        {
            return new TakeOver()
                .WithTakeOverId(data.Keys.Contains("takeOverId") && data["takeOverId"] != null ? data["takeOverId"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithType(data.Keys.Contains("type") && data["type"] != null ? (int?)int.Parse(data["type"].ToString()) : null)
                .WithUserIdentifier(data.Keys.Contains("userIdentifier") && data["userIdentifier"] != null ? data["userIdentifier"].ToString() : null)
                .WithPassword(data.Keys.Contains("password") && data["password"] != null ? data["password"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null);
        }
	}
}