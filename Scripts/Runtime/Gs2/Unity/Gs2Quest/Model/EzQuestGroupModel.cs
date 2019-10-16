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
	public class EzQuestGroupModel
	{
		/** クエストグループ名 */
		public string Name { get; set; }
		/** クエストグループのメタデータ */
		public string Metadata { get; set; }
		/** グループに属するクエスト */
		public List<EzQuestModel> Quests { get; set; }
		/** 挑戦可能な期間を指定するイベントマスター のGRN */
		public string ChallengePeriodEventId { get; set; }

		public EzQuestGroupModel()
		{

		}

		public EzQuestGroupModel(Gs2.Gs2Quest.Model.QuestGroupModel @questGroupModel)
		{
			Name = @questGroupModel.name;
			Metadata = @questGroupModel.metadata;
			Quests = @questGroupModel.quests != null ? @questGroupModel.quests.Select(value =>
                {
                    return new EzQuestModel(value);
                }
			).ToList() : new List<EzQuestModel>(new EzQuestModel[] {});
			ChallengePeriodEventId = @questGroupModel.challengePeriodEventId;
		}

        public QuestGroupModel ToModel()
        {
            return new QuestGroupModel {
                name = Name,
                metadata = Metadata,
                quests = Quests != null ? Quests.Select(Value0 =>
                        {
                            return new QuestModel
                            {
                                questModelId = Value0.QuestModelId,
                                name = Value0.Name,
                                metadata = Value0.Metadata,
                                contents = Value0.Contents != null ? Value0.Contents.Select(Value1 =>
                                        {
                                            return new Contents
                                            {
                                                metadata = Value1.Metadata,
                                                completeAcquireActions = Value1.CompleteAcquireActions != null ? Value1.CompleteAcquireActions.Select(Value2 =>
                                                        {
                                                            return new AcquireAction
                                                            {
                                                                action = Value2.Action,
                                                                request = Value2.Request,
                                                            };
                                                        }
                                                ).ToList() : new List<AcquireAction>(new AcquireAction[] {}),
                                            };
                                        }
                                ).ToList() : new List<Contents>(new Contents[] {}),
                                challengePeriodEventId = Value0.ChallengePeriodEventId,
                                consumeActions = Value0.ConsumeActions != null ? Value0.ConsumeActions.Select(Value1 =>
                                        {
                                            return new ConsumeAction
                                            {
                                                action = Value1.Action,
                                                request = Value1.Request,
                                            };
                                        }
                                ).ToList() : new List<ConsumeAction>(new ConsumeAction[] {}),
                                failedAcquireActions = Value0.FailedAcquireActions != null ? Value0.FailedAcquireActions.Select(Value1 =>
                                        {
                                            return new AcquireAction
                                            {
                                                action = Value1.Action,
                                                request = Value1.Request,
                                            };
                                        }
                                ).ToList() : new List<AcquireAction>(new AcquireAction[] {}),
                                premiseQuestNames = Value0.PremiseQuestNames != null ? Value0.PremiseQuestNames.Select(Value1 =>
                                        {
                                            return Value1;
                                        }
                                ).ToList() : new List<string>(new string[] {}),
                            };
                        }
                ).ToList() : new List<QuestModel>(new QuestModel[] {}),
                challengePeriodEventId = ChallengePeriodEventId,
            };
        }
	}
}
