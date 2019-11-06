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

namespace Gs2.Gs2Deploy.Model
{
	[Preserve]
	public class WorkingResource
	{

        /** 作成中のリソース */
        public string resourceId { set; get; }

        /**
         * 作成中のリソースを設定
         *
         * @param resourceId 作成中のリソース
         * @return this
         */
        public WorkingResource WithResourceId(string resourceId) {
            this.resourceId = resourceId;
            return this;
        }

        /** 操作の種類 */
        public string context { set; get; }

        /**
         * 操作の種類を設定
         *
         * @param context 操作の種類
         * @return this
         */
        public WorkingResource WithContext(string context) {
            this.context = context;
            return this;
        }

        /** 操作対象のリソース */
        public string type { set; get; }

        /**
         * 操作対象のリソースを設定
         *
         * @param type 操作対象のリソース
         * @return this
         */
        public WorkingResource WithType(string type) {
            this.type = type;
            return this;
        }

        /** 作成中のリソース名 */
        public string name { set; get; }

        /**
         * 作成中のリソース名を設定
         *
         * @param name 作成中のリソース名
         * @return this
         */
        public WorkingResource WithName(string name) {
            this.name = name;
            return this;
        }

        /** リクエストパラメータ */
        public string request { set; get; }

        /**
         * リクエストパラメータを設定
         *
         * @param request リクエストパラメータ
         * @return this
         */
        public WorkingResource WithRequest(string request) {
            this.request = request;
            return this;
        }

        /** 依存しているリソースの名前 */
        public List<string> after { set; get; }

        /**
         * 依存しているリソースの名前を設定
         *
         * @param after 依存しているリソースの名前
         * @return this
         */
        public WorkingResource WithAfter(List<string> after) {
            this.after = after;
            return this;
        }

        /** ロールバック操作の種類 */
        public string rollbackContext { set; get; }

        /**
         * ロールバック操作の種類を設定
         *
         * @param rollbackContext ロールバック操作の種類
         * @return this
         */
        public WorkingResource WithRollbackContext(string rollbackContext) {
            this.rollbackContext = rollbackContext;
            return this;
        }

        /** ロールバック用のリクエストパラメータ */
        public string rollbackRequest { set; get; }

        /**
         * ロールバック用のリクエストパラメータを設定
         *
         * @param rollbackRequest ロールバック用のリクエストパラメータ
         * @return this
         */
        public WorkingResource WithRollbackRequest(string rollbackRequest) {
            this.rollbackRequest = rollbackRequest;
            return this;
        }

        /** ロールバック時に依存しているリソースの名前 */
        public List<string> rollbackAfter { set; get; }

        /**
         * ロールバック時に依存しているリソースの名前を設定
         *
         * @param rollbackAfter ロールバック時に依存しているリソースの名前
         * @return this
         */
        public WorkingResource WithRollbackAfter(List<string> rollbackAfter) {
            this.rollbackAfter = rollbackAfter;
            return this;
        }

        /** リソースを作成したときに Output に記録するフィールド */
        public List<OutputField> outputFields { set; get; }

        /**
         * リソースを作成したときに Output に記録するフィールドを設定
         *
         * @param outputFields リソースを作成したときに Output に記録するフィールド
         * @return this
         */
        public WorkingResource WithOutputFields(List<OutputField> outputFields) {
            this.outputFields = outputFields;
            return this;
        }

        /** 実行に対して割り振られる一意な ID */
        public string workId { set; get; }

        /**
         * 実行に対して割り振られる一意な IDを設定
         *
         * @param workId 実行に対して割り振られる一意な ID
         * @return this
         */
        public WorkingResource WithWorkId(string workId) {
            this.workId = workId;
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
        public WorkingResource WithCreatedAt(long? createdAt) {
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
        public WorkingResource WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.resourceId != null)
            {
                writer.WritePropertyName("resourceId");
                writer.Write(this.resourceId);
            }
            if(this.context != null)
            {
                writer.WritePropertyName("context");
                writer.Write(this.context);
            }
            if(this.type != null)
            {
                writer.WritePropertyName("type");
                writer.Write(this.type);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.request != null)
            {
                writer.WritePropertyName("request");
                writer.Write(this.request);
            }
            if(this.after != null)
            {
                writer.WritePropertyName("after");
                writer.WriteArrayStart();
                foreach(var item in this.after)
                {
                    writer.Write(item);
                }
                writer.WriteArrayEnd();
            }
            if(this.rollbackContext != null)
            {
                writer.WritePropertyName("rollbackContext");
                writer.Write(this.rollbackContext);
            }
            if(this.rollbackRequest != null)
            {
                writer.WritePropertyName("rollbackRequest");
                writer.Write(this.rollbackRequest);
            }
            if(this.rollbackAfter != null)
            {
                writer.WritePropertyName("rollbackAfter");
                writer.WriteArrayStart();
                foreach(var item in this.rollbackAfter)
                {
                    writer.Write(item);
                }
                writer.WriteArrayEnd();
            }
            if(this.outputFields != null)
            {
                writer.WritePropertyName("outputFields");
                writer.WriteArrayStart();
                foreach(var item in this.outputFields)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.workId != null)
            {
                writer.WritePropertyName("workId");
                writer.Write(this.workId);
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
        public static WorkingResource FromDict(JsonData data)
        {
            return new WorkingResource()
                .WithResourceId(data.Keys.Contains("resourceId") && data["resourceId"] != null ? data["resourceId"].ToString() : null)
                .WithContext(data.Keys.Contains("context") && data["context"] != null ? data["context"].ToString() : null)
                .WithType(data.Keys.Contains("type") && data["type"] != null ? data["type"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithRequest(data.Keys.Contains("request") && data["request"] != null ? data["request"].ToString() : null)
                .WithAfter(data.Keys.Contains("after") && data["after"] != null ? data["after"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null)
                .WithRollbackContext(data.Keys.Contains("rollbackContext") && data["rollbackContext"] != null ? data["rollbackContext"].ToString() : null)
                .WithRollbackRequest(data.Keys.Contains("rollbackRequest") && data["rollbackRequest"] != null ? data["rollbackRequest"].ToString() : null)
                .WithRollbackAfter(data.Keys.Contains("rollbackAfter") && data["rollbackAfter"] != null ? data["rollbackAfter"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null)
                .WithOutputFields(data.Keys.Contains("outputFields") && data["outputFields"] != null ? data["outputFields"].Cast<JsonData>().Select(value =>
                    {
                        return OutputField.FromDict(value);
                    }
                ).ToList() : null)
                .WithWorkId(data.Keys.Contains("workId") && data["workId"] != null ? data["workId"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}