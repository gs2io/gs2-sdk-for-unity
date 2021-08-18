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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Quest.Model
{
	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzQuestGroupModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public List<Gs2.Unity.Gs2Quest.Model.EzQuestModel> Quests;
		[SerializeField]
		public string ChallengePeriodEventId;

        public Gs2.Gs2Quest.Model.QuestGroupModel ToModel()
        {
            return new Gs2.Gs2Quest.Model.QuestGroupModel {
                Name = Name,
                Metadata = Metadata,
                Quests = Quests?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                ChallengePeriodEventId = ChallengePeriodEventId,
            };
        }

        public static EzQuestGroupModel FromModel(Gs2.Gs2Quest.Model.QuestGroupModel model)
        {
            return new EzQuestGroupModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                Quests = model.Quests == null ? new List<Gs2.Unity.Gs2Quest.Model.EzQuestModel>() : model.Quests.Select(v => {
                    return Gs2.Unity.Gs2Quest.Model.EzQuestModel.FromModel(v);
                }).ToList(),
                ChallengePeriodEventId = model.ChallengePeriodEventId == null ? null : model.ChallengePeriodEventId,
            };
        }
    }
}