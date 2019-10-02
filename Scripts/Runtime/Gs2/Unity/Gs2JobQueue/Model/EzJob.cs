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
using Gs2.Gs2JobQueue.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2JobQueue.Model
{
	[Preserve]
	public class EzJob
	{
		/** ジョブ */
		public string JobId { get; set; }
		/** 現在のリトライ回数 */
		public int CurrentRetryCount { get; set; }
		/** 最大試行回数 */
		public int MaxTryCount { get; set; }

		public EzJob()
		{

		}

		public EzJob(Gs2.Gs2JobQueue.Model.Job @job)
		{
			JobId = @job.jobId;
			CurrentRetryCount = @job.currentRetryCount.HasValue ? @job.currentRetryCount.Value : 0;
			MaxTryCount = @job.maxTryCount.HasValue ? @job.maxTryCount.Value : 0;
		}

        public Job ToModel()
        {
            return new Job {
                jobId = JobId,
                currentRetryCount = CurrentRetryCount,
                maxTryCount = MaxTryCount,
            };
        }
	}
}
