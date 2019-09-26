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
using Gs2.Gs2Experience.Model;

namespace Gs2.Gs2Experience.Request
{
	public class CreateThresholdMasterRequest : Gs2Request<CreateThresholdMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public CreateThresholdMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** ランクアップ閾値名 */
        public string name { set; get; }

        /**
         * ランクアップ閾値名を設定
         *
         * @param name ランクアップ閾値名
         * @return this
         */
        public CreateThresholdMasterRequest WithName(string name) {
            this.name = name;
            return this;
        }


        /** ランクアップ閾値マスターの説明 */
        public string description { set; get; }

        /**
         * ランクアップ閾値マスターの説明を設定
         *
         * @param description ランクアップ閾値マスターの説明
         * @return this
         */
        public CreateThresholdMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** ランクアップ閾値のメタデータ */
        public string metadata { set; get; }

        /**
         * ランクアップ閾値のメタデータを設定
         *
         * @param metadata ランクアップ閾値のメタデータ
         * @return this
         */
        public CreateThresholdMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** ランクアップ経験値閾値リスト */
        public List<long?> values { set; get; }

        /**
         * ランクアップ経験値閾値リストを設定
         *
         * @param values ランクアップ経験値閾値リスト
         * @return this
         */
        public CreateThresholdMasterRequest WithValues(List<long?> values) {
            this.values = values;
            return this;
        }


	}
}