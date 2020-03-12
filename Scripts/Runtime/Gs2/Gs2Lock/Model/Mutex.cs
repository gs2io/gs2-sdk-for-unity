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

namespace Gs2.Gs2Lock.Model
{
	[Preserve]
	public class Mutex
	{

        /** ミューテックス */
        public string mutexId { set; get; }

        /**
         * ミューテックスを設定
         *
         * @param mutexId ミューテックス
         * @return this
         */
        public Mutex WithMutexId(string mutexId) {
            this.mutexId = mutexId;
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
        public Mutex WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** プロパティID */
        public string propertyId { set; get; }

        /**
         * プロパティIDを設定
         *
         * @param propertyId プロパティID
         * @return this
         */
        public Mutex WithPropertyId(string propertyId) {
            this.propertyId = propertyId;
            return this;
        }

        /** ロックを取得したトランザクションID */
        public string transactionId { set; get; }

        /**
         * ロックを取得したトランザクションIDを設定
         *
         * @param transactionId ロックを取得したトランザクションID
         * @return this
         */
        public Mutex WithTransactionId(string transactionId) {
            this.transactionId = transactionId;
            return this;
        }

        /** 参照回数 */
        public int? referenceCount { set; get; }

        /**
         * 参照回数を設定
         *
         * @param referenceCount 参照回数
         * @return this
         */
        public Mutex WithReferenceCount(int? referenceCount) {
            this.referenceCount = referenceCount;
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
        public Mutex WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        /** ロックの有効期限 */
        public long? ttlAt { set; get; }

        /**
         * ロックの有効期限を設定
         *
         * @param ttlAt ロックの有効期限
         * @return this
         */
        public Mutex WithTtlAt(long? ttlAt) {
            this.ttlAt = ttlAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.mutexId != null)
            {
                writer.WritePropertyName("mutexId");
                writer.Write(this.mutexId);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.propertyId != null)
            {
                writer.WritePropertyName("propertyId");
                writer.Write(this.propertyId);
            }
            if(this.transactionId != null)
            {
                writer.WritePropertyName("transactionId");
                writer.Write(this.transactionId);
            }
            if(this.referenceCount.HasValue)
            {
                writer.WritePropertyName("referenceCount");
                writer.Write(this.referenceCount.Value);
            }
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            if(this.ttlAt.HasValue)
            {
                writer.WritePropertyName("ttlAt");
                writer.Write(this.ttlAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Mutex FromDict(JsonData data)
        {
            return new Mutex()
                .WithMutexId(data.Keys.Contains("mutexId") && data["mutexId"] != null ? data["mutexId"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithPropertyId(data.Keys.Contains("propertyId") && data["propertyId"] != null ? data["propertyId"].ToString() : null)
                .WithTransactionId(data.Keys.Contains("transactionId") && data["transactionId"] != null ? data["transactionId"].ToString() : null)
                .WithReferenceCount(data.Keys.Contains("referenceCount") && data["referenceCount"] != null ? (int?)int.Parse(data["referenceCount"].ToString()) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithTtlAt(data.Keys.Contains("ttlAt") && data["ttlAt"] != null ? (long?)long.Parse(data["ttlAt"].ToString()) : null);
        }
	}
}