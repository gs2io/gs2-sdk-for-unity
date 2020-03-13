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
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2JobQueue.Model
{
	[Preserve]
	[System.Serializable]
	public class EzJob
	{
		/** ジョブ */
		[UnityEngine.SerializeField]
		public string JobId;
		/** 現在のリトライ回数 */
		[UnityEngine.SerializeField]
		public int CurrentRetryCount;
		/** 最大試行回数 */
		[UnityEngine.SerializeField]
		public int MaxTryCount;

		public EzJob()
		{

		}

		public EzJob(Gs2.Gs2JobQueue.Model.Job @job)
		{
			JobId = @job.jobId;
			CurrentRetryCount = @job.currentRetryCount.HasValue ? @job.currentRetryCount.Value : 0;
			MaxTryCount = @job.maxTryCount.HasValue ? @job.maxTryCount.Value : 0;
		}

        public virtual Job ToModel()
        {
            return new Job {
                jobId = JobId,
                currentRetryCount = CurrentRetryCount,
                maxTryCount = MaxTryCount,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.JobId != null)
            {
                writer.WritePropertyName("jobId");
                writer.Write(this.JobId);
            }
            writer.WritePropertyName("currentRetryCount");
            writer.Write(this.CurrentRetryCount);
            writer.WritePropertyName("maxTryCount");
            writer.Write(this.MaxTryCount);
            writer.WriteObjectEnd();
        }
	}
}
