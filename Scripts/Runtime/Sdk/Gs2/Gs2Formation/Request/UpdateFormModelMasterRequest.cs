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
	[System.Serializable]
	public class UpdateFormModelMasterRequest : Gs2Request<UpdateFormModelMasterRequest>
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
        public UpdateFormModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** フォーム名 */
		[UnityEngine.SerializeField]
        public string formModelName;

        /**
         * フォーム名を設定
         *
         * @param formModelName フォーム名
         * @return this
         */
        public UpdateFormModelMasterRequest WithFormModelName(string formModelName) {
            this.formModelName = formModelName;
            return this;
        }


        /** フォームマスターの説明 */
		[UnityEngine.SerializeField]
        public string description;

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
		[UnityEngine.SerializeField]
        public string metadata;

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
		[UnityEngine.SerializeField]
        public List<SlotModel> slots;

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


    	[Preserve]
        public static UpdateFormModelMasterRequest FromDict(JsonData data)
        {
            return new UpdateFormModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                formModelName = data.Keys.Contains("formModelName") && data["formModelName"] != null ? data["formModelName"].ToString(): null,
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