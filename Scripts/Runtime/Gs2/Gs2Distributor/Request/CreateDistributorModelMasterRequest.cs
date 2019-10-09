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
using Gs2.Gs2Distributor.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Distributor.Request
{
	[Preserve]
	public class CreateDistributorModelMasterRequest : Gs2Request<CreateDistributorModelMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public CreateDistributorModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
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
        public CreateDistributorModelMasterRequest WithName(string name) {
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
        public CreateDistributorModelMasterRequest WithDescription(string description) {
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
        public CreateDistributorModelMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
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
        public CreateDistributorModelMasterRequest WithInboxNamespaceId(string inboxNamespaceId) {
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
        public CreateDistributorModelMasterRequest WithWhiteListTargetIds(List<string> whiteListTargetIds) {
            this.whiteListTargetIds = whiteListTargetIds;
            return this;
        }


    	[Preserve]
        public static CreateDistributorModelMasterRequest FromDict(JsonData data)
        {
            return new CreateDistributorModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                inboxNamespaceId = data.Keys.Contains("inboxNamespaceId") && data["inboxNamespaceId"] != null ? data["inboxNamespaceId"].ToString(): null,
                whiteListTargetIds = data.Keys.Contains("whiteListTargetIds") && data["whiteListTargetIds"] != null ? data["whiteListTargetIds"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null,
            };
        }

	}
}