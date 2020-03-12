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
	public class UpdateMoldModelMasterRequest : Gs2Request<UpdateMoldModelMasterRequest>
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
        public UpdateMoldModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** フォームの保存領域名 */
		[UnityEngine.SerializeField]
        public string moldName;

        /**
         * フォームの保存領域名を設定
         *
         * @param moldName フォームの保存領域名
         * @return this
         */
        public UpdateMoldModelMasterRequest WithMoldName(string moldName) {
            this.moldName = moldName;
            return this;
        }


        /** フォームの保存領域マスターの説明 */
		[UnityEngine.SerializeField]
        public string description;

        /**
         * フォームの保存領域マスターの説明を設定
         *
         * @param description フォームの保存領域マスターの説明
         * @return this
         */
        public UpdateMoldModelMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** フォームの保存領域のメタデータ */
		[UnityEngine.SerializeField]
        public string metadata;

        /**
         * フォームの保存領域のメタデータを設定
         *
         * @param metadata フォームの保存領域のメタデータ
         * @return this
         */
        public UpdateMoldModelMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
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
        public UpdateMoldModelMasterRequest WithFormModelName(string formModelName) {
            this.formModelName = formModelName;
            return this;
        }


        /** フォームを保存できる初期キャパシティ */
		[UnityEngine.SerializeField]
        public int? initialMaxCapacity;

        /**
         * フォームを保存できる初期キャパシティを設定
         *
         * @param initialMaxCapacity フォームを保存できる初期キャパシティ
         * @return this
         */
        public UpdateMoldModelMasterRequest WithInitialMaxCapacity(int? initialMaxCapacity) {
            this.initialMaxCapacity = initialMaxCapacity;
            return this;
        }


        /** フォームを保存できるキャパシティ */
		[UnityEngine.SerializeField]
        public int? maxCapacity;

        /**
         * フォームを保存できるキャパシティを設定
         *
         * @param maxCapacity フォームを保存できるキャパシティ
         * @return this
         */
        public UpdateMoldModelMasterRequest WithMaxCapacity(int? maxCapacity) {
            this.maxCapacity = maxCapacity;
            return this;
        }


    	[Preserve]
        public static UpdateMoldModelMasterRequest FromDict(JsonData data)
        {
            return new UpdateMoldModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                moldName = data.Keys.Contains("moldName") && data["moldName"] != null ? data["moldName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                formModelName = data.Keys.Contains("formModelName") && data["formModelName"] != null ? data["formModelName"].ToString(): null,
                initialMaxCapacity = data.Keys.Contains("initialMaxCapacity") && data["initialMaxCapacity"] != null ? (int?)int.Parse(data["initialMaxCapacity"].ToString()) : null,
                maxCapacity = data.Keys.Contains("maxCapacity") && data["maxCapacity"] != null ? (int?)int.Parse(data["maxCapacity"].ToString()) : null,
            };
        }

	}
}