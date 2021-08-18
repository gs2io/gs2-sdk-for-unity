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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2JobQueue.Model
{
	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzJob
	{
		[SerializeField]
		public string JobId;
		[SerializeField]
		public string ScriptId;
		[SerializeField]
		public string Args;
		[SerializeField]
		public int CurrentRetryCount;
		[SerializeField]
		public int MaxTryCount;

        public Gs2.Gs2JobQueue.Model.Job ToModel()
        {
            return new Gs2.Gs2JobQueue.Model.Job {
                JobId = JobId,
                ScriptId = ScriptId,
                Args = Args,
                CurrentRetryCount = CurrentRetryCount,
                MaxTryCount = MaxTryCount,
            };
        }

        public static EzJob FromModel(Gs2.Gs2JobQueue.Model.Job model)
        {
            return new EzJob {
                JobId = model.JobId == null ? null : model.JobId,
                ScriptId = model.ScriptId == null ? null : model.ScriptId,
                Args = model.Args == null ? null : model.Args,
                CurrentRetryCount = model.CurrentRetryCount ?? 0,
                MaxTryCount = model.MaxTryCount ?? 0,
            };
        }
    }
}