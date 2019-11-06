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
using Gs2.Gs2Datastore.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Datastore.Result
{
	[Preserve]
	public class PrepareDownloadByGenerationAndUserIdResult
	{
        /** データオブジェクト */
        public DataObject item { set; get; }

        /** ファイルをダウンロードするためのURL */
        public string fileUrl { set; get; }

        /** ファイルの容量 */
        public long? contentLength { set; get; }


    	[Preserve]
        public static PrepareDownloadByGenerationAndUserIdResult FromDict(JsonData data)
        {
            return new PrepareDownloadByGenerationAndUserIdResult {
                item = data.Keys.Contains("item") && data["item"] != null ? DataObject.FromDict(data["item"]) : null,
                fileUrl = data.Keys.Contains("fileUrl") && data["fileUrl"] != null ? data["fileUrl"].ToString() : null,
                contentLength = data.Keys.Contains("contentLength") && data["contentLength"] != null ? (long?)long.Parse(data["contentLength"].ToString()) : null,
            };
        }
	}
}