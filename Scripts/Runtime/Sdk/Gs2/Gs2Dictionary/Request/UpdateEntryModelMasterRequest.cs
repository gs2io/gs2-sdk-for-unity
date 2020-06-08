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
using Gs2.Gs2Dictionary.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Dictionary.Request
{
	[Preserve]
	[System.Serializable]
	public class UpdateEntryModelMasterRequest : Gs2Request<UpdateEntryModelMasterRequest>
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
        public UpdateEntryModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** エントリーモデル名 */
		[UnityEngine.SerializeField]
        public string entryName;

        /**
         * エントリーモデル名を設定
         *
         * @param entryName エントリーモデル名
         * @return this
         */
        public UpdateEntryModelMasterRequest WithEntryName(string entryName) {
            this.entryName = entryName;
            return this;
        }


        /** エントリーモデルマスターの説明 */
		[UnityEngine.SerializeField]
        public string description;

        /**
         * エントリーモデルマスターの説明を設定
         *
         * @param description エントリーモデルマスターの説明
         * @return this
         */
        public UpdateEntryModelMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** エントリーモデルのメタデータ */
		[UnityEngine.SerializeField]
        public string metadata;

        /**
         * エントリーモデルのメタデータを設定
         *
         * @param metadata エントリーモデルのメタデータ
         * @return this
         */
        public UpdateEntryModelMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


    	[Preserve]
        public static UpdateEntryModelMasterRequest FromDict(JsonData data)
        {
            return new UpdateEntryModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                entryName = data.Keys.Contains("entryName") && data["entryName"] != null ? data["entryName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
            };
        }

	}
}