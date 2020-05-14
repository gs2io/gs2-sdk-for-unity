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
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Project.Model
{
	[Preserve]
	public class Project
	{

        /** プロジェクト */
        public string projectId { set; get; }

        /**
         * プロジェクトを設定
         *
         * @param projectId プロジェクト
         * @return this
         */
        public Project WithProjectId(string projectId) {
            this.projectId = projectId;
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
        public Project WithAccountName(string accountName) {
            this.accountName = accountName;
            return this;
        }

        /** プロジェクト名 */
        public string name { set; get; }

        /**
         * プロジェクト名を設定
         *
         * @param name プロジェクト名
         * @return this
         */
        public Project WithName(string name) {
            this.name = name;
            return this;
        }

        /** プロジェクトの説明 */
        public string description { set; get; }

        /**
         * プロジェクトの説明を設定
         *
         * @param description プロジェクトの説明
         * @return this
         */
        public Project WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** 契約プラン */
        public string plan { set; get; }

        /**
         * 契約プランを設定
         *
         * @param plan 契約プラン
         * @return this
         */
        public Project WithPlan(string plan) {
            this.plan = plan;
            return this;
        }

        /** 支払い方法名 */
        public string billingMethodName { set; get; }

        /**
         * 支払い方法名を設定
         *
         * @param billingMethodName 支払い方法名
         * @return this
         */
        public Project WithBillingMethodName(string billingMethodName) {
            this.billingMethodName = billingMethodName;
            return this;
        }

        /** AWS EventBridge の設定 */
        public string enableEventBridge { set; get; }

        /**
         * AWS EventBridge の設定を設定
         *
         * @param enableEventBridge AWS EventBridge の設定
         * @return this
         */
        public Project WithEnableEventBridge(string enableEventBridge) {
            this.enableEventBridge = enableEventBridge;
            return this;
        }

        /** 通知に使用するAWSアカウントのID */
        public string eventBridgeAwsAccountId { set; get; }

        /**
         * 通知に使用するAWSアカウントのIDを設定
         *
         * @param eventBridgeAwsAccountId 通知に使用するAWSアカウントのID
         * @return this
         */
        public Project WithEventBridgeAwsAccountId(string eventBridgeAwsAccountId) {
            this.eventBridgeAwsAccountId = eventBridgeAwsAccountId;
            return this;
        }

        /** 通知に使用するAWSリージョン */
        public string eventBridgeAwsRegion { set; get; }

        /**
         * 通知に使用するAWSリージョンを設定
         *
         * @param eventBridgeAwsRegion 通知に使用するAWSリージョン
         * @return this
         */
        public Project WithEventBridgeAwsRegion(string eventBridgeAwsRegion) {
            this.eventBridgeAwsRegion = eventBridgeAwsRegion;
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
        public Project WithCreatedAt(long? createdAt) {
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
        public Project WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.projectId != null)
            {
                writer.WritePropertyName("projectId");
                writer.Write(this.projectId);
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
            if(this.description != null)
            {
                writer.WritePropertyName("description");
                writer.Write(this.description);
            }
            if(this.plan != null)
            {
                writer.WritePropertyName("plan");
                writer.Write(this.plan);
            }
            if(this.billingMethodName != null)
            {
                writer.WritePropertyName("billingMethodName");
                writer.Write(this.billingMethodName);
            }
            if(this.enableEventBridge != null)
            {
                writer.WritePropertyName("enableEventBridge");
                writer.Write(this.enableEventBridge);
            }
            if(this.eventBridgeAwsAccountId != null)
            {
                writer.WritePropertyName("eventBridgeAwsAccountId");
                writer.Write(this.eventBridgeAwsAccountId);
            }
            if(this.eventBridgeAwsRegion != null)
            {
                writer.WritePropertyName("eventBridgeAwsRegion");
                writer.Write(this.eventBridgeAwsRegion);
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

    public static string GetProjectNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:::gs2:account:(?<accountName>.*):project:(?<projectName>.*)");
        if (!match.Groups["projectName"].Success)
        {
            return null;
        }
        return match.Groups["projectName"].Value;
    }

    public static string GetAccountNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:::gs2:account:(?<accountName>.*):project:(?<projectName>.*)");
        if (!match.Groups["accountName"].Success)
        {
            return null;
        }
        return match.Groups["accountName"].Value;
    }

    	[Preserve]
        public static Project FromDict(JsonData data)
        {
            return new Project()
                .WithProjectId(data.Keys.Contains("projectId") && data["projectId"] != null ? data["projectId"].ToString() : null)
                .WithAccountName(data.Keys.Contains("accountName") && data["accountName"] != null ? data["accountName"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithPlan(data.Keys.Contains("plan") && data["plan"] != null ? data["plan"].ToString() : null)
                .WithBillingMethodName(data.Keys.Contains("billingMethodName") && data["billingMethodName"] != null ? data["billingMethodName"].ToString() : null)
                .WithEnableEventBridge(data.Keys.Contains("enableEventBridge") && data["enableEventBridge"] != null ? data["enableEventBridge"].ToString() : null)
                .WithEventBridgeAwsAccountId(data.Keys.Contains("eventBridgeAwsAccountId") && data["eventBridgeAwsAccountId"] != null ? data["eventBridgeAwsAccountId"].ToString() : null)
                .WithEventBridgeAwsRegion(data.Keys.Contains("eventBridgeAwsRegion") && data["eventBridgeAwsRegion"] != null ? data["eventBridgeAwsRegion"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}