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

namespace Gs2.Gs2Distributor.Model
{
	[Preserve]
	public class DistributorModelMaster
	{

        /** 配信設定マスター */
        public string distributorModelId { set; get; }

        /**
         * 配信設定マスターを設定
         *
         * @param distributorModelId 配信設定マスター
         * @return this
         */
        public DistributorModelMaster WithDistributorModelId(string distributorModelId) {
            this.distributorModelId = distributorModelId;
            return this;
        }

        /** 配信設定名 */
        public string name { set; get; }

        /**
         * 配信設定名を設定
         *
         * @param name 配信設定名
         * @return this
         */
        public DistributorModelMaster WithName(string name) {
            this.name = name;
            return this;
        }

        /** 配信設定マスターの説明 */
        public string description { set; get; }

        /**
         * 配信設定マスターの説明を設定
         *
         * @param description 配信設定マスターの説明
         * @return this
         */
        public DistributorModelMaster WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** 配信設定のメタデータ */
        public string metadata { set; get; }

        /**
         * 配信設定のメタデータを設定
         *
         * @param metadata 配信設定のメタデータ
         * @return this
         */
        public DistributorModelMaster WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** 所持品の配布処理の権限判定に使用する ユーザ のGRN */
        public string assumeUserId { set; get; }

        /**
         * 所持品の配布処理の権限判定に使用する ユーザ のGRNを設定
         *
         * @param assumeUserId 所持品の配布処理の権限判定に使用する ユーザ のGRN
         * @return this
         */
        public DistributorModelMaster WithAssumeUserId(string assumeUserId) {
            this.assumeUserId = assumeUserId;
            return this;
        }

        /** 所持品がキャパシティをオーバーしたときに転送するプレゼントボックスのネームスペース のGRN */
        public string inboxNamespaceId { set; get; }

        /**
         * 所持品がキャパシティをオーバーしたときに転送するプレゼントボックスのネームスペース のGRNを設定
         *
         * @param inboxNamespaceId 所持品がキャパシティをオーバーしたときに転送するプレゼントボックスのネームスペース のGRN
         * @return this
         */
        public DistributorModelMaster WithInboxNamespaceId(string inboxNamespaceId) {
            this.inboxNamespaceId = inboxNamespaceId;
            return this;
        }

        /** ディストリビューターを通して処理出来る対象のリソースGRNのホワイトリスト */
        public List<string> whiteListTargetIds { set; get; }

        /**
         * ディストリビューターを通して処理出来る対象のリソースGRNのホワイトリストを設定
         *
         * @param whiteListTargetIds ディストリビューターを通して処理出来る対象のリソースGRNのホワイトリスト
         * @return this
         */
        public DistributorModelMaster WithWhiteListTargetIds(List<string> whiteListTargetIds) {
            this.whiteListTargetIds = whiteListTargetIds;
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
        public DistributorModelMaster WithCreatedAt(long? createdAt) {
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
        public DistributorModelMaster WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.distributorModelId != null)
            {
                writer.WritePropertyName("distributorModelId");
                writer.Write(this.distributorModelId);
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
            if(this.assumeUserId != null)
            {
                writer.WritePropertyName("assumeUserId");
                writer.Write(this.assumeUserId);
            }
            if(this.inboxNamespaceId != null)
            {
                writer.WritePropertyName("inboxNamespaceId");
                writer.Write(this.inboxNamespaceId);
            }
            if(this.whiteListTargetIds != null)
            {
                writer.WritePropertyName("whiteListTargetIds");
                writer.WriteArrayStart();
                foreach(var item in this.whiteListTargetIds)
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
            if(this.updatedAt.HasValue)
            {
                writer.WritePropertyName("updatedAt");
                writer.Write(this.updatedAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static DistributorModelMaster FromDict(JsonData data)
        {
            return new DistributorModelMaster()
                .WithDistributorModelId(data.Keys.Contains("distributorModelId") && data["distributorModelId"] != null ? (string) data["distributorModelId"] : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? (string) data["name"] : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? (string) data["description"] : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? (string) data["metadata"] : null)
                .WithAssumeUserId(data.Keys.Contains("assumeUserId") && data["assumeUserId"] != null ? (string) data["assumeUserId"] : null)
                .WithInboxNamespaceId(data.Keys.Contains("inboxNamespaceId") && data["inboxNamespaceId"] != null ? (string) data["inboxNamespaceId"] : null)
                .WithWhiteListTargetIds(data.Keys.Contains("whiteListTargetIds") && data["whiteListTargetIds"] != null ? data["whiteListTargetIds"].Cast<JsonData>().Select(value =>
                    {
                        return (string) value;
                    }
                ).ToList() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?) data["createdAt"] : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?) data["updatedAt"] : null);
        }
	}
}