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
	[System.Serializable]
	public class RunStampSheetWithoutNamespaceRequest : Gs2Request<RunStampSheetWithoutNamespaceRequest>
	{

        /** 実行するスタンプタスク */
		[UnityEngine.SerializeField]
        public string stampSheet;

        /**
         * 実行するスタンプタスクを設定
         *
         * @param stampSheet 実行するスタンプタスク
         * @return this
         */
        public RunStampSheetWithoutNamespaceRequest WithStampSheet(string stampSheet) {
            this.stampSheet = stampSheet;
            return this;
        }


        /** スタンプシートの暗号化に使用した暗号鍵GRN */
		[UnityEngine.SerializeField]
        public string keyId;

        /**
         * スタンプシートの暗号化に使用した暗号鍵GRNを設定
         *
         * @param keyId スタンプシートの暗号化に使用した暗号鍵GRN
         * @return this
         */
        public RunStampSheetWithoutNamespaceRequest WithKeyId(string keyId) {
            this.keyId = keyId;
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
        public RunStampSheetWithoutNamespaceRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static RunStampSheetWithoutNamespaceRequest FromDict(JsonData data)
        {
            return new RunStampSheetWithoutNamespaceRequest {
                stampSheet = data.Keys.Contains("stampSheet") && data["stampSheet"] != null ? data["stampSheet"].ToString(): null,
                keyId = data.Keys.Contains("keyId") && data["keyId"] != null ? data["keyId"].ToString(): null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}