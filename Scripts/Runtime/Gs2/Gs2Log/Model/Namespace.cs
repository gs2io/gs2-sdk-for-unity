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

namespace Gs2.Gs2Log.Model
{
	[Preserve]
	public class Namespace
	{

        /** ネームスペース */
        public string namespaceId { set; get; }

        /**
         * ネームスペースを設定
         *
         * @param namespaceId ネームスペース
         * @return this
         */
        public Namespace WithNamespaceId(string namespaceId) {
            this.namespaceId = namespaceId;
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
        public Namespace WithOwnerId(string ownerId) {
            this.ownerId = ownerId;
            return this;
        }

        /** カテゴリー名 */
        public string name { set; get; }

        /**
         * カテゴリー名を設定
         *
         * @param name カテゴリー名
         * @return this
         */
        public Namespace WithName(string name) {
            this.name = name;
            return this;
        }

        /** ネームスペースの説明 */
        public string description { set; get; }

        /**
         * ネームスペースの説明を設定
         *
         * @param description ネームスペースの説明
         * @return this
         */
        public Namespace WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** ログの書き出し方法 */
        public string type { set; get; }

        /**
         * ログの書き出し方法を設定
         *
         * @param type ログの書き出し方法
         * @return this
         */
        public Namespace WithType(string type) {
            this.type = type;
            return this;
        }

        /** GCPのクレデンシャル */
        public string gcpCredentialJson { set; get; }

        /**
         * GCPのクレデンシャルを設定
         *
         * @param gcpCredentialJson GCPのクレデンシャル
         * @return this
         */
        public Namespace WithGcpCredentialJson(string gcpCredentialJson) {
            this.gcpCredentialJson = gcpCredentialJson;
            return this;
        }

        /** BigQueryのデータセット名 */
        public string bigQueryDatasetName { set; get; }

        /**
         * BigQueryのデータセット名を設定
         *
         * @param bigQueryDatasetName BigQueryのデータセット名
         * @return this
         */
        public Namespace WithBigQueryDatasetName(string bigQueryDatasetName) {
            this.bigQueryDatasetName = bigQueryDatasetName;
            return this;
        }

        /** ログの保存期間(日) */
        public int? logExpireDays { set; get; }

        /**
         * ログの保存期間(日)を設定
         *
         * @param logExpireDays ログの保存期間(日)
         * @return this
         */
        public Namespace WithLogExpireDays(int? logExpireDays) {
            this.logExpireDays = logExpireDays;
            return this;
        }

        /** AWSのリージョン */
        public string awsRegion { set; get; }

        /**
         * AWSのリージョンを設定
         *
         * @param awsRegion AWSのリージョン
         * @return this
         */
        public Namespace WithAwsRegion(string awsRegion) {
            this.awsRegion = awsRegion;
            return this;
        }

        /** AWSのアクセスキーID */
        public string awsAccessKeyId { set; get; }

        /**
         * AWSのアクセスキーIDを設定
         *
         * @param awsAccessKeyId AWSのアクセスキーID
         * @return this
         */
        public Namespace WithAwsAccessKeyId(string awsAccessKeyId) {
            this.awsAccessKeyId = awsAccessKeyId;
            return this;
        }

        /** AWSのシークレットアクセスキー */
        public string awsSecretAccessKey { set; get; }

        /**
         * AWSのシークレットアクセスキーを設定
         *
         * @param awsSecretAccessKey AWSのシークレットアクセスキー
         * @return this
         */
        public Namespace WithAwsSecretAccessKey(string awsSecretAccessKey) {
            this.awsSecretAccessKey = awsSecretAccessKey;
            return this;
        }

        /** Kinesis Firehose のストリーム名 */
        public string firehoseStreamName { set; get; }

        /**
         * Kinesis Firehose のストリーム名を設定
         *
         * @param firehoseStreamName Kinesis Firehose のストリーム名
         * @return this
         */
        public Namespace WithFirehoseStreamName(string firehoseStreamName) {
            this.firehoseStreamName = firehoseStreamName;
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
        public Namespace WithCreatedAt(long? createdAt) {
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
        public Namespace WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.namespaceId != null)
            {
                writer.WritePropertyName("namespaceId");
                writer.Write(this.namespaceId);
            }
            if(this.ownerId != null)
            {
                writer.WritePropertyName("ownerId");
                writer.Write(this.ownerId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.description != null)
            {
                writer.WritePropertyName("description");
                writer.Write(this.description);
            }
            if(this.type != null)
            {
                writer.WritePropertyName("type");
                writer.Write(this.type);
            }
            if(this.gcpCredentialJson != null)
            {
                writer.WritePropertyName("gcpCredentialJson");
                writer.Write(this.gcpCredentialJson);
            }
            if(this.bigQueryDatasetName != null)
            {
                writer.WritePropertyName("bigQueryDatasetName");
                writer.Write(this.bigQueryDatasetName);
            }
            if(this.logExpireDays.HasValue)
            {
                writer.WritePropertyName("logExpireDays");
                writer.Write(this.logExpireDays.Value);
            }
            if(this.awsRegion != null)
            {
                writer.WritePropertyName("awsRegion");
                writer.Write(this.awsRegion);
            }
            if(this.awsAccessKeyId != null)
            {
                writer.WritePropertyName("awsAccessKeyId");
                writer.Write(this.awsAccessKeyId);
            }
            if(this.awsSecretAccessKey != null)
            {
                writer.WritePropertyName("awsSecretAccessKey");
                writer.Write(this.awsSecretAccessKey);
            }
            if(this.firehoseStreamName != null)
            {
                writer.WritePropertyName("firehoseStreamName");
                writer.Write(this.firehoseStreamName);
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
        public static Namespace FromDict(JsonData data)
        {
            return new Namespace()
                .WithNamespaceId(data.Keys.Contains("namespaceId") && data["namespaceId"] != null ? data["namespaceId"].ToString() : null)
                .WithOwnerId(data.Keys.Contains("ownerId") && data["ownerId"] != null ? data["ownerId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithType(data.Keys.Contains("type") && data["type"] != null ? data["type"].ToString() : null)
                .WithGcpCredentialJson(data.Keys.Contains("gcpCredentialJson") && data["gcpCredentialJson"] != null ? data["gcpCredentialJson"].ToString() : null)
                .WithBigQueryDatasetName(data.Keys.Contains("bigQueryDatasetName") && data["bigQueryDatasetName"] != null ? data["bigQueryDatasetName"].ToString() : null)
                .WithLogExpireDays(data.Keys.Contains("logExpireDays") && data["logExpireDays"] != null ? (int?)int.Parse(data["logExpireDays"].ToString()) : null)
                .WithAwsRegion(data.Keys.Contains("awsRegion") && data["awsRegion"] != null ? data["awsRegion"].ToString() : null)
                .WithAwsAccessKeyId(data.Keys.Contains("awsAccessKeyId") && data["awsAccessKeyId"] != null ? data["awsAccessKeyId"].ToString() : null)
                .WithAwsSecretAccessKey(data.Keys.Contains("awsSecretAccessKey") && data["awsSecretAccessKey"] != null ? data["awsSecretAccessKey"].ToString() : null)
                .WithFirehoseStreamName(data.Keys.Contains("firehoseStreamName") && data["firehoseStreamName"] != null ? data["firehoseStreamName"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}