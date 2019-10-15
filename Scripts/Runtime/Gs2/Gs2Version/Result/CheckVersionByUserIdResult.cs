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
using Gs2.Gs2Version.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Version.Result
{
	[Preserve]
	public class CheckVersionByUserIdResult
	{
        /** プロジェクトトークン */
        public string projectToken { set; get; }

        /** バージョンの検証結果のリスト */
        public List<Status> warnings { set; get; }

        /** バージョンの検証結果のリスト */
        public List<Status> errors { set; get; }


    	[Preserve]
        public static CheckVersionByUserIdResult FromDict(JsonData data)
        {
            return new CheckVersionByUserIdResult {
                projectToken = data.Keys.Contains("projectToken") && data["projectToken"] != null ? data["projectToken"].ToString() : null,
                warnings = data.Keys.Contains("warnings") && data["warnings"] != null ? data["warnings"].Cast<JsonData>().Select(value =>
                    {
                        return Status.FromDict(value);
                    }
                ).ToList() : null,
                errors = data.Keys.Contains("errors") && data["errors"] != null ? data["errors"].Cast<JsonData>().Select(value =>
                    {
                        return Status.FromDict(value);
                    }
                ).ToList() : null,
            };
        }
	}
}