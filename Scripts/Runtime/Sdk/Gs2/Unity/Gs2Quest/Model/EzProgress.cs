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
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Quest.Model
{
	[Preserve]
	[System.Serializable]
	public class EzProgress
	{
		/** クエスト挑戦 */
		[UnityEngine.SerializeField]
		public string ProgressId;
		/** トランザクションID */
		[UnityEngine.SerializeField]
		public string TransactionId;
		/** クエストモデル */
		[UnityEngine.SerializeField]
		public string QuestModelId;
		/** 乱数シード */
		[UnityEngine.SerializeField]
		public long RandomSeed;
		/** クエストで得られる報酬の上限 */
		[UnityEngine.SerializeField]
		public List<EzReward> Rewards;

		public EzProgress()
		{

		}

		public EzProgress(Gs2.Gs2Quest.Model.Progress @progress)
		{
			ProgressId = @progress.progressId;
			TransactionId = @progress.transactionId;
			QuestModelId = @progress.questModelId;
			RandomSeed = @progress.randomSeed.HasValue ? @progress.randomSeed.Value : 0;
			Rewards = @progress.rewards != null ? @progress.rewards.Select(value =>
                {
                    return new EzReward(value);
                }
			).ToList() : new List<EzReward>(new EzReward[] {});
		}

        public virtual Progress ToModel()
        {
            return new Progress {
                progressId = ProgressId,
                transactionId = TransactionId,
                questModelId = QuestModelId,
                randomSeed = RandomSeed,
                rewards = Rewards != null ? Rewards.Select(Value0 =>
                        {
                            return new Reward
                            {
                                action = Value0.Action,
                                request = Value0.Request,
                                itemId = Value0.ItemId,
                                value = Value0.Value,
                            };
                        }
                ).ToList() : new List<Reward>(new Reward[] {}),
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.ProgressId != null)
            {
                writer.WritePropertyName("progressId");
                writer.Write(this.ProgressId);
            }
            if(this.TransactionId != null)
            {
                writer.WritePropertyName("transactionId");
                writer.Write(this.TransactionId);
            }
            if(this.QuestModelId != null)
            {
                writer.WritePropertyName("questModelId");
                writer.Write(this.QuestModelId);
            }
            writer.WritePropertyName("randomSeed");
            writer.Write(this.RandomSeed);
            if(this.Rewards != null)
            {
                writer.WritePropertyName("rewards");
                writer.WriteArrayStart();
                foreach(var item in this.Rewards)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
        }
	}
}
