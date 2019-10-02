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
	public class EzCompletedQuestList
	{
		/** クエストグループ名 */
		public string QuestGroupName { get; set; }
		/** 攻略済みのクエスト名一覧のリスト */
		public List<string> CompleteQuestNames { get; set; }

		public EzCompletedQuestList()
		{

		}

		public EzCompletedQuestList(Gs2.Gs2Quest.Model.CompletedQuestList @completedQuestList)
		{
			QuestGroupName = @completedQuestList.questGroupName;
			CompleteQuestNames = @completedQuestList.completeQuestNames != null ? @completedQuestList.completeQuestNames.Select(value =>
                {
                    return value;
                }
			).ToList() : new List<string>(new string[] {});
		}

        public CompletedQuestList ToModel()
        {
            return new CompletedQuestList {
                questGroupName = QuestGroupName,
                completeQuestNames = CompleteQuestNames != null ? CompleteQuestNames.Select(Value0 =>
                        {
                            return Value0;
                        }
                ).ToList() : new List<string>(new string[] {}),
            };
        }
	}
}
