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
	public class EzJobResultBody
	{
		/** 試行回数 */
		[UnityEngine.SerializeField]
		public int TryNumber;
		/** ステータスコード */
		[UnityEngine.SerializeField]
		public int StatusCode;
		/** レスポンスの内容 */
		[UnityEngine.SerializeField]
		public string Result;
		/** 実行日時 */
		[UnityEngine.SerializeField]
		public long TryAt;

		public EzJobResultBody()
		{

		}

		public EzJobResultBody(Gs2.Gs2JobQueue.Model.JobResultBody @jobResultBody)
		{
			TryNumber = @jobResultBody.tryNumber.HasValue ? @jobResultBody.tryNumber.Value : 0;
			StatusCode = @jobResultBody.statusCode.HasValue ? @jobResultBody.statusCode.Value : 0;
			Result = @jobResultBody.result;
			TryAt = @jobResultBody.tryAt.HasValue ? @jobResultBody.tryAt.Value : 0;
		}

        public virtual JobResultBody ToModel()
        {
            return new JobResultBody {
                tryNumber = TryNumber,
                statusCode = StatusCode,
                result = Result,
                tryAt = TryAt,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WritePropertyName("tryNumber");
            writer.Write(this.TryNumber);
            writer.WritePropertyName("statusCode");
            writer.Write(this.StatusCode);
            if(this.Result != null)
            {
                writer.WritePropertyName("result");
                writer.Write(this.Result);
            }
            writer.WritePropertyName("tryAt");
            writer.Write(this.TryAt);
            writer.WriteObjectEnd();
        }
	}
}
