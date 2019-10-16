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
	public class EzQuestModel
	{
		/** クエストモデル */
		public string QuestModelId { get; set; }
		/** クエストモデル名 */
		public string Name { get; set; }
		/** クエストモデルのメタデータ */
		public string Metadata { get; set; }
		/** クエストの内容 */
		public List<EzContents> Contents { get; set; }
		/** 挑戦可能な期間を指定するイベントマスター のGRN */
		public string ChallengePeriodEventId { get; set; }
		/** クエストの参加料 */
		public List<EzConsumeAction> ConsumeActions { get; set; }
		/** クエスト失敗時の報酬 */
		public List<EzAcquireAction> FailedAcquireActions { get; set; }
		/** クエストに挑戦するためにクリアしておく必要のあるクエスト名 */
		public List<string> PremiseQuestNames { get; set; }

		public EzQuestModel()
		{

		}

		public EzQuestModel(Gs2.Gs2Quest.Model.QuestModel @questModel)
		{
			QuestModelId = @questModel.questModelId;
			Name = @questModel.name;
			Metadata = @questModel.metadata;
			Contents = @questModel.contents != null ? @questModel.contents.Select(value =>
                {
                    return new EzContents(value);
                }
			).ToList() : new List<EzContents>(new EzContents[] {});
			ChallengePeriodEventId = @questModel.challengePeriodEventId;
			ConsumeActions = @questModel.consumeActions != null ? @questModel.consumeActions.Select(value =>
                {
                    return new EzConsumeAction(value);
                }
			).ToList() : new List<EzConsumeAction>(new EzConsumeAction[] {});
			FailedAcquireActions = @questModel.failedAcquireActions != null ? @questModel.failedAcquireActions.Select(value =>
                {
                    return new EzAcquireAction(value);
                }
			).ToList() : new List<EzAcquireAction>(new EzAcquireAction[] {});
			PremiseQuestNames = @questModel.premiseQuestNames != null ? @questModel.premiseQuestNames.Select(value =>
                {
                    return value;
                }
			).ToList() : new List<string>(new string[] {});
		}

        public QuestModel ToModel()
        {
            return new QuestModel {
                questModelId = QuestModelId,
                name = Name,
                metadata = Metadata,
                contents = Contents != null ? Contents.Select(Value0 =>
                        {
                            return new Contents
                            {
                                metadata = Value0.Metadata,
                                completeAcquireActions = Value0.CompleteAcquireActions != null ? Value0.CompleteAcquireActions.Select(Value1 =>
                                        {
                                            return new AcquireAction
                                            {
                                                action = Value1.Action,
                                                request = Value1.Request,
                                            };
                                        }
                                ).ToList() : new List<AcquireAction>(new AcquireAction[] {}),
                            };
                        }
                ).ToList() : new List<Contents>(new Contents[] {}),
                challengePeriodEventId = ChallengePeriodEventId,
                consumeActions = ConsumeActions != null ? ConsumeActions.Select(Value0 =>
                        {
                            return new ConsumeAction
                            {
                                action = Value0.Action,
                                request = Value0.Request,
                            };
                        }
                ).ToList() : new List<ConsumeAction>(new ConsumeAction[] {}),
                failedAcquireActions = FailedAcquireActions != null ? FailedAcquireActions.Select(Value0 =>
                        {
                            return new AcquireAction
                            {
                                action = Value0.Action,
                                request = Value0.Request,
                            };
                        }
                ).ToList() : new List<AcquireAction>(new AcquireAction[] {}),
                premiseQuestNames = PremiseQuestNames != null ? PremiseQuestNames.Select(Value0 =>
                        {
                            return Value0;
                        }
                ).ToList() : new List<string>(new string[] {}),
            };
        }
	}
}
