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
	public class SendMessageByUserIdRequest : Gs2Request<SendMessageByUserIdRequest>
	{

        /** プレゼントボックス名 */
        public string namespaceName { set; get; }

        /**
         * プレゼントボックス名を設定
         *
         * @param namespaceName プレゼントボックス名
         * @return this
         */
        public SendMessageByUserIdRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** ユーザーID */
        public string userId { set; get; }

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
        public string metadata { set; get; }

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
        public List<AcquireAction> readAcquireActions { set; get; }

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


        /** 重複実行回避機能に使用するID */
        public string duplicationAvoider { set; get; }

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
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}