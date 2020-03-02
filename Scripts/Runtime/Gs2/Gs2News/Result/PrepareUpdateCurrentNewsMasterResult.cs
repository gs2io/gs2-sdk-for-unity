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
using Gs2.Gs2News.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2News.Result
{
	[Preserve]
	public class PrepareUpdateCurrentNewsMasterResult
	{
        /** アップロード後に結果を反映する際に使用するトークン */
        public string uploadToken { set; get; }

        /** テンプレートアップロード処理の実行に使用するURL */
        public string templateUploadUrl { set; get; }


    	[Preserve]
        public static PrepareUpdateCurrentNewsMasterResult FromDict(JsonData data)
        {
            return new PrepareUpdateCurrentNewsMasterResult {
                uploadToken = data.Keys.Contains("uploadToken") && data["uploadToken"] != null ? data["uploadToken"].ToString() : null,
                templateUploadUrl = data.Keys.Contains("templateUploadUrl") && data["templateUploadUrl"] != null ? data["templateUploadUrl"].ToString() : null,
            };
        }
	}
}