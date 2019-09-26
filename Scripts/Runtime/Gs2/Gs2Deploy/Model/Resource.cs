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
using Gs2.Core.Model;
using LitJson;

namespace Gs2.Gs2Deploy.Model
{
	public class Resource
	{

        /** 作成されたのリソース */
        public string resourceId { set; get; }

        /**
         * 作成されたのリソースを設定
         *
         * @param resourceId 作成されたのリソース
         * @return this
         */
        public Resource WithResourceId(string resourceId) {
            this.resourceId = resourceId;
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
        public Resource WithType(string type) {
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
        public Resource WithName(string name) {
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
        public Resource WithRequest(string request) {
            this.request = request;
            return this;
        }

        /** リソースの作成・更新のレスポンス */
        public string response { set; get; }

        /**
         * リソースの作成・更新のレスポンスを設定
         *
         * @param response リソースの作成・更新のレスポンス
         * @return this
         */
        public Resource WithResponse(string response) {
            this.response = response;
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
        public Resource WithRollbackContext(string rollbackContext) {
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
        public Resource WithRollbackRequest(string rollbackRequest) {
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
        public Resource WithRollbackAfter(List<string> rollbackAfter) {
            this.rollbackAfter = rollbackAfter;
            return this;
        }

        /** このリソースに関連するアウトプットに記録したキー名 */
        public List<string> outputKeys { set; get; }

        /**
         * このリソースに関連するアウトプットに記録したキー名を設定
         *
         * @param outputKeys このリソースに関連するアウトプットに記録したキー名
         * @return this
         */
        public Resource WithOutputKeys(List<string> outputKeys) {
            this.outputKeys = outputKeys;
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
        public Resource WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
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
            if(this.response != null)
            {
                writer.WritePropertyName("response");
                writer.Write(this.response);
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
            if(this.outputKeys != null)
            {
                writer.WritePropertyName("outputKeys");
                writer.WriteArrayStart();
                foreach(var item in this.outputKeys)
                {
                    writer.Write(item);
                }
                writer.WriteArrayEnd();
            }
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            writer.WriteObjectEnd();
        }
	}
}