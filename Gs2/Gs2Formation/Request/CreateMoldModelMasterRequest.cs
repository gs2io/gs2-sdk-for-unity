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
using Gs2.Gs2Formation.Model;

namespace Gs2.Gs2Formation.Request
{
	public class CreateMoldModelMasterRequest : Gs2Request<CreateMoldModelMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public CreateMoldModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** フォームの保存領域名 */
        public string name { set; get; }

        /**
         * フォームの保存領域名を設定
         *
         * @param name フォームの保存領域名
         * @return this
         */
        public CreateMoldModelMasterRequest WithName(string name) {
            this.name = name;
            return this;
        }


        /** フォームの保存領域マスターの説明 */
        public string description { set; get; }

        /**
         * フォームの保存領域マスターの説明を設定
         *
         * @param description フォームの保存領域マスターの説明
         * @return this
         */
        public CreateMoldModelMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** フォームの保存領域のメタデータ */
        public string metadata { set; get; }

        /**
         * フォームの保存領域のメタデータを設定
         *
         * @param metadata フォームの保存領域のメタデータ
         * @return this
         */
        public CreateMoldModelMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** フォームマスター */
        public string formModelId { set; get; }

        /**
         * フォームマスターを設定
         *
         * @param formModelId フォームマスター
         * @return this
         */
        public CreateMoldModelMasterRequest WithFormModelId(string formModelId) {
            this.formModelId = formModelId;
            return this;
        }


        /** フォームを保存できる初期キャパシティ */
        public int? initialMaxCapacity { set; get; }

        /**
         * フォームを保存できる初期キャパシティを設定
         *
         * @param initialMaxCapacity フォームを保存できる初期キャパシティ
         * @return this
         */
        public CreateMoldModelMasterRequest WithInitialMaxCapacity(int? initialMaxCapacity) {
            this.initialMaxCapacity = initialMaxCapacity;
            return this;
        }


        /** フォームを保存できるキャパシティ */
        public int? maxCapacity { set; get; }

        /**
         * フォームを保存できるキャパシティを設定
         *
         * @param maxCapacity フォームを保存できるキャパシティ
         * @return this
         */
        public CreateMoldModelMasterRequest WithMaxCapacity(int? maxCapacity) {
            this.maxCapacity = maxCapacity;
            return this;
        }


	}
}