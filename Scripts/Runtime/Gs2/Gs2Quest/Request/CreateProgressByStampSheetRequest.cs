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
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Quest.Model;

namespace Gs2.Gs2Quest.Request
{
	public class CreateProgressByStampSheetRequest : Gs2Request<CreateProgressByStampSheetRequest>
	{

        /** スタンプシート */
        public string stampSheet { set; get; }

        /**
         * スタンプシートを設定
         *
         * @param stampSheet スタンプシート
         * @return this
         */
        public CreateProgressByStampSheetRequest WithStampSheet(string stampSheet) {
            this.stampSheet = stampSheet;
            return this;
        }


        /** スタンプシートの署名検証に使用する 暗号鍵 のGRN */
        public string keyId { set; get; }

        /**
         * スタンプシートの署名検証に使用する 暗号鍵 のGRNを設定
         *
         * @param keyId スタンプシートの署名検証に使用する 暗号鍵 のGRN
         * @return this
         */
        public CreateProgressByStampSheetRequest WithKeyId(string keyId) {
            this.keyId = keyId;
            return this;
        }


        /** スタンプシートの変数に適用する設定値 */
        public List<Config> config { set; get; }

        /**
         * スタンプシートの変数に適用する設定値を設定
         *
         * @param config スタンプシートの変数に適用する設定値
         * @return this
         */
        public CreateProgressByStampSheetRequest WithConfig(List<Config> config) {
            this.config = config;
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
        public CreateProgressByStampSheetRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


	}
}