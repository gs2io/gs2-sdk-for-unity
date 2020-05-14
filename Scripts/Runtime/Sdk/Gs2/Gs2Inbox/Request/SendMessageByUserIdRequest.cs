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
	public class SendMessageByUserIdRequest : Gs2Request<SendMessageByUserIdRequest>
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
        public SendMessageByUserIdRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** ユーザーID */
		[UnityEngine.SerializeField]
        public string userId;

        /**
         * ユーザーIDを設定
         *
         * @param userId ユーザーID
         * @return this
         */
        public SendMessageByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** メッセージの内容に相当するメタデータ */
		[UnityEngine.SerializeField]
        public string metadata;

        /**
         * メッセージの内容に相当するメタデータを設定
         *
         * @param metadata メッセージの内容に相当するメタデータ
         * @return this
         */
        public SendMessageByUserIdRequest WithMetadata(string metadata) {
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
        public SendMessageByUserIdRequest WithReadAcquireActions(List<AcquireAction> readAcquireActions) {
            this.readAcquireActions = readAcquireActions;
            return this;
        }


        /** メッセージの有効期限 */
		[UnityEngine.SerializeField]
        public long? expiresAt;

        /**
         * メッセージの有効期限を設定
         *
         * @param expiresAt メッセージの有効期限
         * @return this
         */
        public SendMessageByUserIdRequest WithExpiresAt(long? expiresAt) {
            this.expiresAt = expiresAt;
            return this;
        }


        /** メッセージの有効期限までの差分 */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Inbox.Model.TimeSpan expiresTimeSpan;

        /**
         * メッセージの有効期限までの差分を設定
         *
         * @param expiresTimeSpan メッセージの有効期限までの差分
         * @return this
         */
        public SendMessageByUserIdRequest WithExpiresTimeSpan(global::Gs2.Gs2Inbox.Model.TimeSpan expiresTimeSpan) {
            this.expiresTimeSpan = expiresTimeSpan;
            return this;
        }


        /** 重複実行回避機能に使用するID */
		[UnityEngine.SerializeField]
        public string duplicationAvoider;

        /**
         * 重複実行回避機能に使用するIDを設定
         *
         * @param duplicationAvoider 重複実行回避機能に使用するID
         * @return this
         */
        public SendMessageByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static SendMessageByUserIdRequest FromDict(JsonData data)
        {
            return new SendMessageByUserIdRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                userId = data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                readAcquireActions = data.Keys.Contains("readAcquireActions") && data["readAcquireActions"] != null ? data["readAcquireActions"].Cast<JsonData>().Select(value =>
                    {
                        return AcquireAction.FromDict(value);
                    }
                ).ToList() : null,
                expiresAt = data.Keys.Contains("expiresAt") && data["expiresAt"] != null ? (long?)long.Parse(data["expiresAt"].ToString()) : null,
                expiresTimeSpan = data.Keys.Contains("expiresTimeSpan") && data["expiresTimeSpan"] != null ? global::Gs2.Gs2Inbox.Model.TimeSpan.FromDict(data["expiresTimeSpan"]) : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}