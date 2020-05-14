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
using Gs2.Gs2Exchange.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Exchange.Request
{
	[Preserve]
	[System.Serializable]
	public class UpdateRateModelMasterRequest : Gs2Request<UpdateRateModelMasterRequest>
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
        public UpdateRateModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 交換レート名 */
		[UnityEngine.SerializeField]
        public string rateName;

        /**
         * 交換レート名を設定
         *
         * @param rateName 交換レート名
         * @return this
         */
        public UpdateRateModelMasterRequest WithRateName(string rateName) {
            this.rateName = rateName;
            return this;
        }


        /** 交換レートマスターの説明 */
		[UnityEngine.SerializeField]
        public string description;

        /**
         * 交換レートマスターの説明を設定
         *
         * @param description 交換レートマスターの説明
         * @return this
         */
        public UpdateRateModelMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** 交換レートのメタデータ */
		[UnityEngine.SerializeField]
        public string metadata;

        /**
         * 交換レートのメタデータを設定
         *
         * @param metadata 交換レートのメタデータ
         * @return this
         */
        public UpdateRateModelMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** 入手アクションリスト */
		[UnityEngine.SerializeField]
        public List<AcquireAction> acquireActions;

        /**
         * 入手アクションリストを設定
         *
         * @param acquireActions 入手アクションリスト
         * @return this
         */
        public UpdateRateModelMasterRequest WithAcquireActions(List<AcquireAction> acquireActions) {
            this.acquireActions = acquireActions;
            return this;
        }


        /** 消費アクションリスト */
		[UnityEngine.SerializeField]
        public List<ConsumeAction> consumeActions;

        /**
         * 消費アクションリストを設定
         *
         * @param consumeActions 消費アクションリスト
         * @return this
         */
        public UpdateRateModelMasterRequest WithConsumeActions(List<ConsumeAction> consumeActions) {
            this.consumeActions = consumeActions;
            return this;
        }


    	[Preserve]
        public static UpdateRateModelMasterRequest FromDict(JsonData data)
        {
            return new UpdateRateModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                rateName = data.Keys.Contains("rateName") && data["rateName"] != null ? data["rateName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                acquireActions = data.Keys.Contains("acquireActions") && data["acquireActions"] != null ? data["acquireActions"].Cast<JsonData>().Select(value =>
                    {
                        return AcquireAction.FromDict(value);
                    }
                ).ToList() : null,
                consumeActions = data.Keys.Contains("consumeActions") && data["consumeActions"] != null ? data["consumeActions"].Cast<JsonData>().Select(value =>
                    {
                        return ConsumeAction.FromDict(value);
                    }
                ).ToList() : null,
            };
        }

	}
}