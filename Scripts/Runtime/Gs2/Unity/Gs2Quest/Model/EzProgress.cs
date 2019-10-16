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
using Gs2.Gs2Quest.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Quest.Model
{
	[Preserve]
	public class EzProgress
	{
		/** クエスト挑戦 */
		public string ProgressId { get; set; }
		/** トランザクションID */
		public string TransactionId { get; set; }
		/** クエストモデル */
		public string QuestModelId { get; set; }
		/** 乱数シード */
		public long RandomSeed { get; set; }

		public EzProgress()
		{

		}

		public EzProgress(Gs2.Gs2Quest.Model.Progress @progress)
		{
			ProgressId = @progress.progressId;
			TransactionId = @progress.transactionId;
			QuestModelId = @progress.questModelId;
			RandomSeed = @progress.randomSeed.HasValue ? @progress.randomSeed.Value : 0;
		}

        public Progress ToModel()
        {
            return new Progress {
                progressId = ProgressId,
                transactionId = TransactionId,
                questModelId = QuestModelId,
                randomSeed = RandomSeed,
            };
        }
	}
}
