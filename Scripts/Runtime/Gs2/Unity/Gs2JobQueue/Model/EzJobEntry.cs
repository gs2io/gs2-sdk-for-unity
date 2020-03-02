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
	public class EzJobEntry
	{
		/** スクリプト のGRN */
		[UnityEngine.SerializeField]
		public string ScriptId;
		/** 引数 */
		[UnityEngine.SerializeField]
		public string Args;
		/** 最大試行回数 */
		[UnityEngine.SerializeField]
		public int MaxTryCount;

		public EzJobEntry()
		{

		}

		public EzJobEntry(Gs2.Gs2JobQueue.Model.JobEntry @jobEntry)
		{
			ScriptId = @jobEntry.scriptId;
			Args = @jobEntry.args;
			MaxTryCount = @jobEntry.maxTryCount.HasValue ? @jobEntry.maxTryCount.Value : 0;
		}

        public virtual JobEntry ToModel()
        {
            return new JobEntry {
                scriptId = ScriptId,
                args = Args,
                maxTryCount = MaxTryCount,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.ScriptId != null)
            {
                writer.WritePropertyName("scriptId");
                writer.Write(this.ScriptId);
            }
            if(this.Args != null)
            {
                writer.WritePropertyName("args");
                writer.Write(this.Args);
            }
            writer.WritePropertyName("maxTryCount");
            writer.Write(this.MaxTryCount);
            writer.WriteObjectEnd();
        }
	}
}
