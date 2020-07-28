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
using Gs2.Gs2Exchange.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Exchange.Request
{
	[Preserve]
	[System.Serializable]
	public class UpdateNamespaceRequest : Gs2Request<UpdateNamespaceRequest>
	{

        /** ネームスペース名 */
		[UnityEngine.SerializeField]
        public string namespaceName;

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateNamespaceRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
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
        public UpdateNamespaceRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** 交換結果の受け取りに待ち時間の発生する交換機能を利用するか */
		[UnityEngine.SerializeField]
        public bool? enableAwaitExchange;

        /**
         * 交換結果の受け取りに待ち時間の発生する交換機能を利用するかを設定
         *
         * @param enableAwaitExchange 交換結果の受け取りに待ち時間の発生する交換機能を利用するか
         * @return this
         */
        public UpdateNamespaceRequest WithEnableAwaitExchange(bool? enableAwaitExchange) {
            this.enableAwaitExchange = enableAwaitExchange;
            return this;
        }


        /** 直接交換APIの呼び出しを許可する。許可しない場合はスタンプシート経由でしか交換できない */
		[UnityEngine.SerializeField]
        public bool? enableDirectExchange;

        /**
         * 直接交換APIの呼び出しを許可する。許可しない場合はスタンプシート経由でしか交換できないを設定
         *
         * @param enableDirectExchange 直接交換APIの呼び出しを許可する。許可しない場合はスタンプシート経由でしか交換できない
         * @return this
         */
        public UpdateNamespaceRequest WithEnableDirectExchange(bool? enableDirectExchange) {
            this.enableDirectExchange = enableDirectExchange;
            return this;
        }


        /** 交換処理をジョブとして追加するキューのネームスペース のGRN */
		[UnityEngine.SerializeField]
        public string queueNamespaceId;

        /**
         * 交換処理をジョブとして追加するキューのネームスペース のGRNを設定
         *
         * @param queueNamespaceId 交換処理をジョブとして追加するキューのネームスペース のGRN
         * @return this
         */
        public UpdateNamespaceRequest WithQueueNamespaceId(string queueNamespaceId) {
            this.queueNamespaceId = queueNamespaceId;
            return this;
        }


        /** 交換処理のスタンプシートで使用する暗号鍵GRN */
		[UnityEngine.SerializeField]
        public string keyId;

        /**
         * 交換処理のスタンプシートで使用する暗号鍵GRNを設定
         *
         * @param keyId 交換処理のスタンプシートで使用する暗号鍵GRN
         * @return this
         */
        public UpdateNamespaceRequest WithKeyId(string keyId) {
            this.keyId = keyId;
            return this;
        }


        /** ログの出力設定 */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Exchange.Model.LogSetting logSetting;

        /**
         * ログの出力設定を設定
         *
         * @param logSetting ログの出力設定
         * @return this
         */
        public UpdateNamespaceRequest WithLogSetting(global::Gs2.Gs2Exchange.Model.LogSetting logSetting) {
            this.logSetting = logSetting;
            return this;
        }


    	[Preserve]
        public static UpdateNamespaceRequest FromDict(JsonData data)
        {
            return new UpdateNamespaceRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                enableAwaitExchange = data.Keys.Contains("enableAwaitExchange") && data["enableAwaitExchange"] != null ? (bool?)bool.Parse(data["enableAwaitExchange"].ToString()) : null,
                enableDirectExchange = data.Keys.Contains("enableDirectExchange") && data["enableDirectExchange"] != null ? (bool?)bool.Parse(data["enableDirectExchange"].ToString()) : null,
                queueNamespaceId = data.Keys.Contains("queueNamespaceId") && data["queueNamespaceId"] != null ? data["queueNamespaceId"].ToString(): null,
                keyId = data.Keys.Contains("keyId") && data["keyId"] != null ? data["keyId"].ToString(): null,
                logSetting = data.Keys.Contains("logSetting") && data["logSetting"] != null ? global::Gs2.Gs2Exchange.Model.LogSetting.FromDict(data["logSetting"]) : null,
            };
        }

	}
}