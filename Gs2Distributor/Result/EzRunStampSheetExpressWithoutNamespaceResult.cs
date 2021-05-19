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
using Gs2.Core.Model;
using Gs2.Unity.Gs2Distributor.Model;
using Gs2.Gs2Distributor.Result;
using UnityEngine.Scripting;

namespace Gs2.Unity.Gs2Distributor.Result
{
	[Preserve]
	public class EzRunStampSheetExpressWithoutNamespaceResult
	{
        /** スタンプタスクの実行結果 */
        public List<string> TaskResults { get; private set; }

        /** スタンプシートの実行結果レスポンス内容 */
        public string SheetResult { get; private set; }


        public EzRunStampSheetExpressWithoutNamespaceResult(
            RunStampSheetExpressWithoutNamespaceResult result
        )
        {
            TaskResults = new List<string>();
            foreach (var item_ in result.taskResults)
            {
                TaskResults.Add(item_);
            }
            SheetResult = result.sheetResult;
        }
	}
}