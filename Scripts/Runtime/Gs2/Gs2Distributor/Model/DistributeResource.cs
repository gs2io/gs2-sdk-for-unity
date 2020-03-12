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
using Gs2.Core.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Distributor.Model
{
	[Preserve]
	public class DistributeResource
	{

        /** スタンプシートで実行するアクションの種類 */
        public string action { set; get; }

        /**
         * スタンプシートで実行するアクションの種類を設定
         *
         * @param action スタンプシートで実行するアクションの種類
         * @return this
         */
        public DistributeResource WithAction(string action) {
            this.action = action;
            return this;
        }

        /** 加算リクエストのJSON */
        public string request { set; get; }

        /**
         * 加算リクエストのJSONを設定
         *
         * @param request 加算リクエストのJSON
         * @return this
         */
        public DistributeResource WithRequest(string request) {
            this.request = request;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.action != null)
            {
                writer.WritePropertyName("action");
                writer.Write(this.action);
            }
            if(this.request != null)
            {
                writer.WritePropertyName("request");
                writer.Write(this.request);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static DistributeResource FromDict(JsonData data)
        {
            return new DistributeResource()
                .WithAction(data.Keys.Contains("action") && data["action"] != null ? data["action"].ToString() : null)
                .WithRequest(data.Keys.Contains("request") && data["request"] != null ? data["request"].ToString() : null);
        }
	}
}