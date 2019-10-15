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
using Gs2.Gs2Version.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Version.Request
{
	[Preserve]
	public class UpdateVersionModelMasterRequest : Gs2Request<UpdateVersionModelMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateVersionModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** バージョン名 */
        public string versionName { set; get; }

        /**
         * バージョン名を設定
         *
         * @param versionName バージョン名
         * @return this
         */
        public UpdateVersionModelMasterRequest WithVersionName(string versionName) {
            this.versionName = versionName;
            return this;
        }


        /** バージョンマスターの説明 */
        public string description { set; get; }

        /**
         * バージョンマスターの説明を設定
         *
         * @param description バージョンマスターの説明
         * @return this
         */
        public UpdateVersionModelMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** バージョンのメタデータ */
        public string metadata { set; get; }

        /**
         * バージョンのメタデータを設定
         *
         * @param metadata バージョンのメタデータ
         * @return this
         */
        public UpdateVersionModelMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** バージョンアップを促すバージョン */
        public Version_ warningVersion { set; get; }

        /**
         * バージョンアップを促すバージョンを設定
         *
         * @param warningVersion バージョンアップを促すバージョン
         * @return this
         */
        public UpdateVersionModelMasterRequest WithWarningVersion(Version_ warningVersion) {
            this.warningVersion = warningVersion;
            return this;
        }


        /** バージョンチェックを蹴るバージョン */
        public Version_ errorVersion { set; get; }

        /**
         * バージョンチェックを蹴るバージョンを設定
         *
         * @param errorVersion バージョンチェックを蹴るバージョン
         * @return this
         */
        public UpdateVersionModelMasterRequest WithErrorVersion(Version_ errorVersion) {
            this.errorVersion = errorVersion;
            return this;
        }


        /** 判定に使用するバージョン値の種類 */
        public string scope { set; get; }

        /**
         * 判定に使用するバージョン値の種類を設定
         *
         * @param scope 判定に使用するバージョン値の種類
         * @return this
         */
        public UpdateVersionModelMasterRequest WithScope(string scope) {
            this.scope = scope;
            return this;
        }


        /** 現在のバージョン */
        public Version_ currentVersion { set; get; }

        /**
         * 現在のバージョンを設定
         *
         * @param currentVersion 現在のバージョン
         * @return this
         */
        public UpdateVersionModelMasterRequest WithCurrentVersion(Version_ currentVersion) {
            this.currentVersion = currentVersion;
            return this;
        }


        /** 判定するバージョン値に署名検証を必要とするか */
        public bool? needSignature { set; get; }

        /**
         * 判定するバージョン値に署名検証を必要とするかを設定
         *
         * @param needSignature 判定するバージョン値に署名検証を必要とするか
         * @return this
         */
        public UpdateVersionModelMasterRequest WithNeedSignature(bool? needSignature) {
            this.needSignature = needSignature;
            return this;
        }


        /** 署名検証に使用する暗号鍵 のGRN */
        public string signatureKeyId { set; get; }

        /**
         * 署名検証に使用する暗号鍵 のGRNを設定
         *
         * @param signatureKeyId 署名検証に使用する暗号鍵 のGRN
         * @return this
         */
        public UpdateVersionModelMasterRequest WithSignatureKeyId(string signatureKeyId) {
            this.signatureKeyId = signatureKeyId;
            return this;
        }


    	[Preserve]
        public static UpdateVersionModelMasterRequest FromDict(JsonData data)
        {
            return new UpdateVersionModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                versionName = data.Keys.Contains("versionName") && data["versionName"] != null ? data["versionName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                warningVersion = data.Keys.Contains("warningVersion") && data["warningVersion"] != null ? Version_.FromDict(data["warningVersion"]) : null,
                errorVersion = data.Keys.Contains("errorVersion") && data["errorVersion"] != null ? Version_.FromDict(data["errorVersion"]) : null,
                scope = data.Keys.Contains("scope") && data["scope"] != null ? data["scope"].ToString(): null,
                currentVersion = data.Keys.Contains("currentVersion") && data["currentVersion"] != null ? Version_.FromDict(data["currentVersion"]) : null,
                needSignature = data.Keys.Contains("needSignature") && data["needSignature"] != null ? (bool?)bool.Parse(data["needSignature"].ToString()) : null,
                signatureKeyId = data.Keys.Contains("signatureKeyId") && data["signatureKeyId"] != null ? data["signatureKeyId"].ToString(): null,
            };
        }

	}
}