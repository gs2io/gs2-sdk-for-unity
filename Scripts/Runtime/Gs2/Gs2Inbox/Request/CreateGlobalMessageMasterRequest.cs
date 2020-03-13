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
using Gs2.Gs2Inbox.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Inbox.Request
{
	[Preserve]
	[System.Serializable]
	public class CreateGlobalMessageMasterRequest : Gs2Request<CreateGlobalMessageMasterRequest>
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
        public CreateGlobalMessageMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 全ユーザに向けたメッセージ名 */
		[UnityEngine.SerializeField]
        public string name;

        /**
         * 全ユーザに向けたメッセージ名を設定
         *
         * @param name 全ユーザに向けたメッセージ名
         * @return this
         */
        public CreateGlobalMessageMasterRequest WithName(string name) {
            this.name = name;
            return this;
        }


        /** 全ユーザに向けたメッセージの内容に相当するメタデータ */
		[UnityEngine.SerializeField]
        public string metadata;

        /**
         * 全ユーザに向けたメッセージの内容に相当するメタデータを設定
         *
         * @param metadata 全ユーザに向けたメッセージの内容に相当するメタデータ
         * @return this
         */
        public CreateGlobalMessageMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** 開封時に実行する入手アクション */
		[UnityEngine.SerializeField]
        public List<AcquireAction> readAcquireActions;

        /**
         * 開封時に実行する入手アクションを設定
         *
         * @param readAcquireActions 開封時に実行する入手アクション
         * @return this
         */
        public CreateGlobalMessageMasterRequest WithReadAcquireActions(List<AcquireAction> readAcquireActions) {
            this.readAcquireActions = readAcquireActions;
            return this;
        }


        /** メッセージを受信したあとメッセージが削除されるまでの期間 */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Inbox.Model.TimeSpan_ expiresTimeSpan;

        /**
         * メッセージを受信したあとメッセージが削除されるまでの期間を設定
         *
         * @param expiresTimeSpan メッセージを受信したあとメッセージが削除されるまでの期間
         * @return this
         */
        public CreateGlobalMessageMasterRequest WithExpiresTimeSpan(global::Gs2.Gs2Inbox.Model.TimeSpan_ expiresTimeSpan) {
            this.expiresTimeSpan = expiresTimeSpan;
            return this;
        }


        /** 全ユーザに向けたメッセージの受信期限 */
		[UnityEngine.SerializeField]
        public long? expiresAt;

        /**
         * 全ユーザに向けたメッセージの受信期限を設定
         *
         * @param expiresAt 全ユーザに向けたメッセージの受信期限
         * @return this
         */
        public CreateGlobalMessageMasterRequest WithExpiresAt(long? expiresAt) {
            this.expiresAt = expiresAt;
            return this;
        }


    	[Preserve]
        public static CreateGlobalMessageMasterRequest FromDict(JsonData data)
        {
            return new CreateGlobalMessageMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                readAcquireActions = data.Keys.Contains("readAcquireActions") && data["readAcquireActions"] != null ? data["readAcquireActions"].Cast<JsonData>().Select(value =>
                    {
                        return AcquireAction.FromDict(value);
                    }
                ).ToList() : null,
                expiresTimeSpan = data.Keys.Contains("expiresTimeSpan") && data["expiresTimeSpan"] != null ? global::Gs2.Gs2Inbox.Model.TimeSpan_.FromDict(data["expiresTimeSpan"]) : null,
                expiresAt = data.Keys.Contains("expiresAt") && data["expiresAt"] != null ? (long?)long.Parse(data["expiresAt"].ToString()) : null,
            };
        }

	}
}