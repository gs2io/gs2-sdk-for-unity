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

namespace Gs2.Gs2Exchange.Model
{
	[Preserve]
	public class RateModelMaster
	{

        /** 交換レートマスター */
        public string rateModelId { set; get; }

        /**
         * 交換レートマスターを設定
         *
         * @param rateModelId 交換レートマスター
         * @return this
         */
        public RateModelMaster WithRateModelId(string rateModelId) {
            this.rateModelId = rateModelId;
            return this;
        }

        /** 交換レート名 */
        public string name { set; get; }

        /**
         * 交換レート名を設定
         *
         * @param name 交換レート名
         * @return this
         */
        public RateModelMaster WithName(string name) {
            this.name = name;
            return this;
        }

        /** 交換レートマスターの説明 */
        public string description { set; get; }

        /**
         * 交換レートマスターの説明を設定
         *
         * @param description 交換レートマスターの説明
         * @return this
         */
        public RateModelMaster WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** 交換レートのメタデータ */
        public string metadata { set; get; }

        /**
         * 交換レートのメタデータを設定
         *
         * @param metadata 交換レートのメタデータ
         * @return this
         */
        public RateModelMaster WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** 消費アクションリスト */
        public List<ConsumeAction> consumeActions { set; get; }

        /**
         * 消費アクションリストを設定
         *
         * @param consumeActions 消費アクションリスト
         * @return this
         */
        public RateModelMaster WithConsumeActions(List<ConsumeAction> consumeActions) {
            this.consumeActions = consumeActions;
            return this;
        }

        /** 入手アクションリスト */
        public List<AcquireAction> acquireActions { set; get; }

        /**
         * 入手アクションリストを設定
         *
         * @param acquireActions 入手アクションリスト
         * @return this
         */
        public RateModelMaster WithAcquireActions(List<AcquireAction> acquireActions) {
            this.acquireActions = acquireActions;
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
        public RateModelMaster WithCreatedAt(long? createdAt) {
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
        public RateModelMaster WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.rateModelId != null)
            {
                writer.WritePropertyName("rateModelId");
                writer.Write(this.rateModelId);
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
            if(this.metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.metadata);
            }
            if(this.consumeActions != null)
            {
                writer.WritePropertyName("consumeActions");
                writer.WriteArrayStart();
                foreach(var item in this.consumeActions)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.acquireActions != null)
            {
                writer.WritePropertyName("acquireActions");
                writer.WriteArrayStart();
                foreach(var item in this.acquireActions)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
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
        public static RateModelMaster FromDict(JsonData data)
        {
            return new RateModelMaster()
                .WithRateModelId(data.Keys.Contains("rateModelId") && data["rateModelId"] != null ? data["rateModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithConsumeActions(data.Keys.Contains("consumeActions") && data["consumeActions"] != null ? data["consumeActions"].Cast<JsonData>().Select(value =>
                    {
                        return ConsumeAction.FromDict(value);
                    }
                ).ToList() : null)
                .WithAcquireActions(data.Keys.Contains("acquireActions") && data["acquireActions"] != null ? data["acquireActions"].Cast<JsonData>().Select(value =>
                    {
                        return AcquireAction.FromDict(value);
                    }
                ).ToList() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}