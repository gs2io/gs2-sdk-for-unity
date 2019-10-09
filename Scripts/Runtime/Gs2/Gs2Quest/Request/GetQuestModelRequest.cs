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
using Gs2.Gs2Quest.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Quest.Request
{
	[Preserve]
	public class GetQuestModelRequest : Gs2Request<GetQuestModelRequest>
	{

        /** カテゴリ名 */
        public string namespaceName { set; get; }

        /**
         * カテゴリ名を設定
         *
         * @param namespaceName カテゴリ名
         * @return this
         */
        public GetQuestModelRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** クエストグループモデル名 */
        public string questGroupName { set; get; }

        /**
         * クエストグループモデル名を設定
         *
         * @param questGroupName クエストグループモデル名
         * @return this
         */
        public GetQuestModelRequest WithQuestGroupName(string questGroupName) {
            this.questGroupName = questGroupName;
            return this;
        }


        /** クエスト名 */
        public string questName { set; get; }

        /**
         * クエスト名を設定
         *
         * @param questName クエスト名
         * @return this
         */
        public GetQuestModelRequest WithQuestName(string questName) {
            this.questName = questName;
            return this;
        }


    	[Preserve]
        public static GetQuestModelRequest FromDict(JsonData data)
        {
            return new GetQuestModelRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                questGroupName = data.Keys.Contains("questGroupName") && data["questGroupName"] != null ? data["questGroupName"].ToString(): null,
                questName = data.Keys.Contains("questName") && data["questName"] != null ? data["questName"].ToString(): null,
            };
        }

	}
}