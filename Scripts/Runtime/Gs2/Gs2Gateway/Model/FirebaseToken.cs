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

namespace Gs2.Gs2Gateway.Model
{
	[Preserve]
	public class FirebaseToken
	{

        /** Firebaseデバイストークン のGRN */
        public string firebaseTokenId { set; get; }

        /**
         * Firebaseデバイストークン のGRNを設定
         *
         * @param firebaseTokenId Firebaseデバイストークン のGRN
         * @return this
         */
        public FirebaseToken WithFirebaseTokenId(string firebaseTokenId) {
            this.firebaseTokenId = firebaseTokenId;
            return this;
        }

        /** オーナーID */
        public string ownerId { set; get; }

        /**
         * オーナーIDを設定
         *
         * @param ownerId オーナーID
         * @return this
         */
        public FirebaseToken WithOwnerId(string ownerId) {
            this.ownerId = ownerId;
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
        public FirebaseToken WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** Firebase Cloud Messaging のデバイストークン */
        public string token { set; get; }

        /**
         * Firebase Cloud Messaging のデバイストークンを設定
         *
         * @param token Firebase Cloud Messaging のデバイストークン
         * @return this
         */
        public FirebaseToken WithToken(string token) {
            this.token = token;
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
        public FirebaseToken WithCreatedAt(long? createdAt) {
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
        public FirebaseToken WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.firebaseTokenId != null)
            {
                writer.WritePropertyName("firebaseTokenId");
                writer.Write(this.firebaseTokenId);
            }
            if(this.ownerId != null)
            {
                writer.WritePropertyName("ownerId");
                writer.Write(this.ownerId);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.token != null)
            {
                writer.WritePropertyName("token");
                writer.Write(this.token);
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
        public static FirebaseToken FromDict(JsonData data)
        {
            return new FirebaseToken()
                .WithFirebaseTokenId(data.Keys.Contains("firebaseTokenId") && data["firebaseTokenId"] != null ? data["firebaseTokenId"].ToString() : null)
                .WithOwnerId(data.Keys.Contains("ownerId") && data["ownerId"] != null ? data["ownerId"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithToken(data.Keys.Contains("token") && data["token"] != null ? data["token"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}