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
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Log.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Log.Request
{
	[Preserve]
	[System.Serializable]
	public class CreateNamespaceRequest : Gs2Request<CreateNamespaceRequest>
	{

        /** カテゴリー名 */
		[UnityEngine.SerializeField]
        public string name;

        /**
         * カテゴリー名を設定
         *
         * @param name カテゴリー名
         * @return this
         */
        public CreateNamespaceRequest WithName(string name) {
            this.name = name;
            return this;
        }


        /** ネームスペースの説明 */
		[UnityEngine.SerializeField]
        public string description;

        /**
         * ネームスペースの説明を設定
         *
         * @param description ネームスペースの説明
         * @return this
         */
        public CreateNamespaceRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** ログの書き出し方法 */
		[UnityEngine.SerializeField]
        public string type;

        /**
         * ログの書き出し方法を設定
         *
         * @param type ログの書き出し方法
         * @return this
         */
        public CreateNamespaceRequest WithType(string type) {
            this.type = type;
            return this;
        }


        /** GCPのクレデンシャル */
		[UnityEngine.SerializeField]
        public string gcpCredentialJson;

        /**
         * GCPのクレデンシャルを設定
         *
         * @param gcpCredentialJson GCPのクレデンシャル
         * @return this
         */
        public CreateNamespaceRequest WithGcpCredentialJson(string gcpCredentialJson) {
            this.gcpCredentialJson = gcpCredentialJson;
            return this;
        }


        /** BigQueryのデータセット名 */
		[UnityEngine.SerializeField]
        public string bigQueryDatasetName;

        /**
         * BigQueryのデータセット名を設定
         *
         * @param bigQueryDatasetName BigQueryのデータセット名
         * @return this
         */
        public CreateNamespaceRequest WithBigQueryDatasetName(string bigQueryDatasetName) {
            this.bigQueryDatasetName = bigQueryDatasetName;
            return this;
        }


        /** ログの保存期間(日) */
		[UnityEngine.SerializeField]
        public int? logExpireDays;

        /**
         * ログの保存期間(日)を設定
         *
         * @param logExpireDays ログの保存期間(日)
         * @return this
         */
        public CreateNamespaceRequest WithLogExpireDays(int? logExpireDays) {
            this.logExpireDays = logExpireDays;
            return this;
        }


        /** AWSのリージョン */
		[UnityEngine.SerializeField]
        public string awsRegion;

        /**
         * AWSのリージョンを設定
         *
         * @param awsRegion AWSのリージョン
         * @return this
         */
        public CreateNamespaceRequest WithAwsRegion(string awsRegion) {
            this.awsRegion = awsRegion;
            return this;
        }


        /** AWSのアクセスキーID */
		[UnityEngine.SerializeField]
        public string awsAccessKeyId;

        /**
         * AWSのアクセスキーIDを設定
         *
         * @param awsAccessKeyId AWSのアクセスキーID
         * @return this
         */
        public CreateNamespaceRequest WithAwsAccessKeyId(string awsAccessKeyId) {
            this.awsAccessKeyId = awsAccessKeyId;
            return this;
        }


        /** AWSのシークレットアクセスキー */
		[UnityEngine.SerializeField]
        public string awsSecretAccessKey;

        /**
         * AWSのシークレットアクセスキーを設定
         *
         * @param awsSecretAccessKey AWSのシークレットアクセスキー
         * @return this
         */
        public CreateNamespaceRequest WithAwsSecretAccessKey(string awsSecretAccessKey) {
            this.awsSecretAccessKey = awsSecretAccessKey;
            return this;
        }


        /** Kinesis Firehose のストリーム名 */
		[UnityEngine.SerializeField]
        public string firehoseStreamName;

        /**
         * Kinesis Firehose のストリーム名を設定
         *
         * @param firehoseStreamName Kinesis Firehose のストリーム名
         * @return this
         */
        public CreateNamespaceRequest WithFirehoseStreamName(string firehoseStreamName) {
            this.firehoseStreamName = firehoseStreamName;
            return this;
        }


    	[Preserve]
        public static CreateNamespaceRequest FromDict(JsonData data)
        {
            return new CreateNamespaceRequest {
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                type = data.Keys.Contains("type") && data["type"] != null ? data["type"].ToString(): null,
                gcpCredentialJson = data.Keys.Contains("gcpCredentialJson") && data["gcpCredentialJson"] != null ? data["gcpCredentialJson"].ToString(): null,
                bigQueryDatasetName = data.Keys.Contains("bigQueryDatasetName") && data["bigQueryDatasetName"] != null ? data["bigQueryDatasetName"].ToString(): null,
                logExpireDays = data.Keys.Contains("logExpireDays") && data["logExpireDays"] != null ? (int?)int.Parse(data["logExpireDays"].ToString()) : null,
                awsRegion = data.Keys.Contains("awsRegion") && data["awsRegion"] != null ? data["awsRegion"].ToString(): null,
                awsAccessKeyId = data.Keys.Contains("awsAccessKeyId") && data["awsAccessKeyId"] != null ? data["awsAccessKeyId"].ToString(): null,
                awsSecretAccessKey = data.Keys.Contains("awsSecretAccessKey") && data["awsSecretAccessKey"] != null ? data["awsSecretAccessKey"].ToString(): null,
                firehoseStreamName = data.Keys.Contains("firehoseStreamName") && data["firehoseStreamName"] != null ? data["firehoseStreamName"].ToString(): null,
            };
        }

	}
}