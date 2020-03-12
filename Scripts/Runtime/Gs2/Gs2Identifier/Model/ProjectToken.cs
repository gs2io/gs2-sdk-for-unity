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

namespace Gs2.Gs2Identifier.Model
{
	[Preserve]
	public class ProjectToken
	{

        /** プロジェクトトークン */
        public string token { set; get; }

        /**
         * プロジェクトトークンを設定
         *
         * @param token プロジェクトトークン
         * @return this
         */
        public ProjectToken WithToken(string token) {
            this.token = token;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.token != null)
            {
                writer.WritePropertyName("token");
                writer.Write(this.token);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static ProjectToken FromDict(JsonData data)
        {
            return new ProjectToken()
                .WithToken(data.Keys.Contains("token") && data["token"] != null ? data["token"].ToString() : null);
        }
	}
}