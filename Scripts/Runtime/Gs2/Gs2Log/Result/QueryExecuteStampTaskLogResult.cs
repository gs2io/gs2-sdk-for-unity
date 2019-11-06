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
using Gs2.Gs2Log.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Log.Result
{
	[Preserve]
	public class QueryExecuteStampTaskLogResult
	{
        /** スタンプタスク実行ログのリスト */
        public List<ExecuteStampTaskLog> items { set; get; }

        /** リストの続きを取得するためのページトークン */
        public string nextPageToken { set; get; }

        /** クエリ結果の総件数 */
        public long? totalCount { set; get; }

        /** 検索時にスキャンした総容量 */
        public long? scanSize { set; get; }


    	[Preserve]
        public static QueryExecuteStampTaskLogResult FromDict(JsonData data)
        {
            return new QueryExecuteStampTaskLogResult {
                items = data.Keys.Contains("items") && data["items"] != null ? data["items"].Cast<JsonData>().Select(value =>
                    {
                        return ExecuteStampTaskLog.FromDict(value);
                    }
                ).ToList() : null,
                nextPageToken = data.Keys.Contains("nextPageToken") && data["nextPageToken"] != null ? data["nextPageToken"].ToString() : null,
                totalCount = data.Keys.Contains("totalCount") && data["totalCount"] != null ? (long?)long.Parse(data["totalCount"].ToString()) : null,
                scanSize = data.Keys.Contains("scanSize") && data["scanSize"] != null ? (long?)long.Parse(data["scanSize"].ToString()) : null,
            };
        }
	}
}