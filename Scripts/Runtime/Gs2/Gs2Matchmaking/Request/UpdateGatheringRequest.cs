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
using Gs2.Gs2Matchmaking.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Matchmaking.Request
{
	[Preserve]
	public class UpdateGatheringRequest : Gs2Request<UpdateGatheringRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateGatheringRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** ギャザリング名 */
        public string gatheringName { set; get; }

        /**
         * ギャザリング名を設定
         *
         * @param gatheringName ギャザリング名
         * @return this
         */
        public UpdateGatheringRequest WithGatheringName(string gatheringName) {
            this.gatheringName = gatheringName;
            return this;
        }


        /** 募集条件 */
        public List<AttributeRange> attributeRanges { set; get; }

        /**
         * 募集条件を設定
         *
         * @param attributeRanges 募集条件
         * @return this
         */
        public UpdateGatheringRequest WithAttributeRanges(List<AttributeRange> attributeRanges) {
            this.attributeRanges = attributeRanges;
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
        public UpdateGatheringRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


        /** アクセストークン */
        public string accessToken { set; get; }

        /**
         * アクセストークンを設定
         *
         * @param accessToken アクセストークン
         * @return this
         */
        public UpdateGatheringRequest WithAccessToken(string accessToken) {
            this.accessToken = accessToken;
            return this;
        }

    	[Preserve]
        public static UpdateGatheringRequest FromDict(JsonData data)
        {
            return new UpdateGatheringRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                gatheringName = data.Keys.Contains("gatheringName") && data["gatheringName"] != null ? data["gatheringName"].ToString(): null,
                attributeRanges = data.Keys.Contains("attributeRanges") && data["attributeRanges"] != null ? data["attributeRanges"].Cast<JsonData>().Select(value =>
                    {
                        return AttributeRange.FromDict(value);
                    }
                ).ToList() : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}