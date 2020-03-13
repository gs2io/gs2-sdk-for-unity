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

namespace Gs2.Gs2Limit.Model
{
	[Preserve]
	public class ResponseCache
	{

        /** None */
        public string region { set; get; }

        /**
         * Noneを設定
         *
         * @param region None
         * @return this
         */
        public ResponseCache WithRegion(string region) {
            this.region = region;
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
        public ResponseCache WithOwnerId(string ownerId) {
            this.ownerId = ownerId;
            return this;
        }

        /** レスポンスキャッシュ のGRN */
        public string responseCacheId { set; get; }

        /**
         * レスポンスキャッシュ のGRNを設定
         *
         * @param responseCacheId レスポンスキャッシュ のGRN
         * @return this
         */
        public ResponseCache WithResponseCacheId(string responseCacheId) {
            this.responseCacheId = responseCacheId;
            return this;
        }

        /** None */
        public string requestHash { set; get; }

        /**
         * Noneを設定
         *
         * @param requestHash None
         * @return this
         */
        public ResponseCache WithRequestHash(string requestHash) {
            this.requestHash = requestHash;
            return this;
        }

        /** APIの応答内容 */
        public string result { set; get; }

        /**
         * APIの応答内容を設定
         *
         * @param result APIの応答内容
         * @return this
         */
        public ResponseCache WithResult(string result) {
            this.result = result;
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
        public ResponseCache WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        /** None */
        public long? expiredAt { set; get; }

        /**
         * Noneを設定
         *
         * @param expiredAt None
         * @return this
         */
        public ResponseCache WithExpiredAt(long? expiredAt) {
            this.expiredAt = expiredAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.region != null)
            {
                writer.WritePropertyName("region");
                writer.Write(this.region);
            }
            if(this.ownerId != null)
            {
                writer.WritePropertyName("ownerId");
                writer.Write(this.ownerId);
            }
            if(this.responseCacheId != null)
            {
                writer.WritePropertyName("responseCacheId");
                writer.Write(this.responseCacheId);
            }
            if(this.requestHash != null)
            {
                writer.WritePropertyName("requestHash");
                writer.Write(this.requestHash);
            }
            if(this.result != null)
            {
                writer.WritePropertyName("result");
                writer.Write(this.result);
            }
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            if(this.expiredAt.HasValue)
            {
                writer.WritePropertyName("expiredAt");
                writer.Write(this.expiredAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static ResponseCache FromDict(JsonData data)
        {
            return new ResponseCache()
                .WithRegion(data.Keys.Contains("region") && data["region"] != null ? data["region"].ToString() : null)
                .WithOwnerId(data.Keys.Contains("ownerId") && data["ownerId"] != null ? data["ownerId"].ToString() : null)
                .WithResponseCacheId(data.Keys.Contains("responseCacheId") && data["responseCacheId"] != null ? data["responseCacheId"].ToString() : null)
                .WithRequestHash(data.Keys.Contains("requestHash") && data["requestHash"] != null ? data["requestHash"].ToString() : null)
                .WithResult(data.Keys.Contains("result") && data["result"] != null ? data["result"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithExpiredAt(data.Keys.Contains("expiredAt") && data["expiredAt"] != null ? (long?)long.Parse(data["expiredAt"].ToString()) : null);
        }
	}
}