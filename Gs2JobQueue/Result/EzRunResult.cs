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
using Gs2.Unity.Gs2JobQueue.Model;
using Gs2.Gs2JobQueue.Result;
using UnityEngine.Scripting;

namespace Gs2.Unity.Gs2JobQueue.Result
{
	[Preserve]
	public class EzRunResult
	{
        /** ジョブ */
        public EzJob Item { get; private set; }

        /** ジョブの実行結果 */
        public EzJobResultBody Result { get; private set; }

        /** None */
        public bool IsLastJob { get; private set; }


        public EzRunResult(
            RunResult result
        )
        {
            if(result.item != null)
            {
                Item = new EzJob(result.item);
            }
            if(result.result != null)
            {
                Result = new EzJobResultBody(result.result);
            }
            if(result.isLastJob.HasValue)
            {
                IsLastJob = result.isLastJob.Value;
            }
        }
	}
}