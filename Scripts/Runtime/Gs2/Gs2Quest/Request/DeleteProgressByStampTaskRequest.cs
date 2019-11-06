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
using Gs2.Gs2Quest.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Quest.Request
{
	[Preserve]
	public class DeleteProgressByStampTaskRequest : Gs2Request<DeleteProgressByStampTaskRequest>
	{

        /** スタンプタスク */
        public string stampTask { set; get; }

        /**
         * スタンプタスクを設定
         *
         * @param stampTask スタンプタスク
         * @return this
         */
        public DeleteProgressByStampTaskRequest WithStampTask(string stampTask) {
            this.stampTask = stampTask;
            return this;
        }


        /** スタンプタスクの署名検証に使用する 暗号鍵 のGRN */
        public string keyId { set; get; }

        /**
         * スタンプタスクの署名検証に使用する 暗号鍵 のGRNを設定
         *
         * @param keyId スタンプタスクの署名検証に使用する 暗号鍵 のGRN
         * @return this
         */
        public DeleteProgressByStampTaskRequest WithKeyId(string keyId) {
            this.keyId = keyId;
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
        public DeleteProgressByStampTaskRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static DeleteProgressByStampTaskRequest FromDict(JsonData data)
        {
            return new DeleteProgressByStampTaskRequest {
                stampTask = data.Keys.Contains("stampTask") && data["stampTask"] != null ? data["stampTask"].ToString(): null,
                keyId = data.Keys.Contains("keyId") && data["keyId"] != null ? data["keyId"].ToString(): null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}