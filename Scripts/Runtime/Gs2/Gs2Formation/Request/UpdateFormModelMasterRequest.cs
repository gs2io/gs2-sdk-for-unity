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
	public class UpdateFormModelMasterRequest : Gs2Request<UpdateFormModelMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateFormModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** フォーム名 */
        public string formName { set; get; }

        /**
         * フォーム名を設定
         *
         * @param formName フォーム名
         * @return this
         */
        public UpdateFormModelMasterRequest WithFormName(string formName) {
            this.formName = formName;
            return this;
        }


        /** フォームマスターの説明 */
        public string description { set; get; }

        /**
         * フォームマスターの説明を設定
         *
         * @param description フォームマスターの説明
         * @return this
         */
        public UpdateFormModelMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** フォームのメタデータ */
        public string metadata { set; get; }

        /**
         * フォームのメタデータを設定
         *
         * @param metadata フォームのメタデータ
         * @return this
         */
        public UpdateFormModelMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** スロットリスト */
        public List<SlotModel> slots { set; get; }

        /**
         * スロットリストを設定
         *
         * @param slots スロットリスト
         * @return this
         */
        public UpdateFormModelMasterRequest WithSlots(List<SlotModel> slots) {
            this.slots = slots;
            return this;
        }


	}
}