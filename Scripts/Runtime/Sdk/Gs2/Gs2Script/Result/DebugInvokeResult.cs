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
using Gs2.Gs2Script.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Script.Result
{
	[Preserve]
	public class DebugInvokeResult
	{
        /** ステータスコード */
        public int? code { set; get; }

        /** 戻り値 */
        public string result { set; get; }

        /** スクリプトの実行時間(ミリ秒) */
        public int? executeTime { set; get; }

        /** 費用の計算対象となった時間(秒) */
        public int? charged { set; get; }

        /** 標準出力の内容のリスト */
        public List<string> output { set; get; }


    	[Preserve]
        public static DebugInvokeResult FromDict(JsonData data)
        {
            return new DebugInvokeResult {
                code = data.Keys.Contains("code") && data["code"] != null ? (int?)int.Parse(data["code"].ToString()) : null,
                result = data.Keys.Contains("result") && data["result"] != null ? data["result"].ToString() : null,
                executeTime = data.Keys.Contains("executeTime") && data["executeTime"] != null ? (int?)int.Parse(data["executeTime"].ToString()) : null,
                charged = data.Keys.Contains("charged") && data["charged"] != null ? (int?)int.Parse(data["charged"].ToString()) : null,
                output = data.Keys.Contains("output") && data["output"] != null ? data["output"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null,
            };
        }
	}
}