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
using Gs2.Gs2Formation.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Formation.Request
{
	[Preserve]
	public class CreateFormModelMasterRequest : Gs2Request<CreateFormModelMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public CreateFormModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** フォーム名 */
        public string name { set; get; }

        /**
         * フォーム名を設定
         *
         * @param name フォーム名
         * @return this
         */
        public CreateFormModelMasterRequest WithName(string name) {
            this.name = name;
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
        public CreateFormModelMasterRequest WithDescription(string description) {
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
        public CreateFormModelMasterRequest WithMetadata(string metadata) {
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
        public CreateFormModelMasterRequest WithSlots(List<SlotModel> slots) {
            this.slots = slots;
            return this;
        }


    	[Preserve]
        public static CreateFormModelMasterRequest FromDict(JsonData data)
        {
            return new CreateFormModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                slots = data.Keys.Contains("slots") && data["slots"] != null ? data["slots"].Cast<JsonData>().Select(value =>
                    {
                        return SlotModel.FromDict(value);
                    }
                ).ToList() : null,
            };
        }

	}
}