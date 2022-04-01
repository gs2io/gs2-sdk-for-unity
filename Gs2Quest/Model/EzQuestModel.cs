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
	public class EzQuestModel
	{
		[SerializeField]
		public string QuestModelId;
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public List<Gs2.Unity.Gs2Quest.Model.EzContents> Contents;
		[SerializeField]
		public string ChallengePeriodEventId;
		[SerializeField]
		public List<Gs2.Unity.Gs2Quest.Model.EzConsumeAction> ConsumeActions;
		[SerializeField]
		public List<Gs2.Unity.Gs2Quest.Model.EzAcquireAction> FailedAcquireActions;
		[SerializeField]
		public List<string> PremiseQuestNames;

        public Gs2.Gs2Quest.Model.QuestModel ToModel()
        {
            return new Gs2.Gs2Quest.Model.QuestModel {
                QuestModelId = QuestModelId,
                Name = Name,
                Metadata = Metadata,
                Contents = Contents?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                ChallengePeriodEventId = ChallengePeriodEventId,
                ConsumeActions = ConsumeActions?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                FailedAcquireActions = FailedAcquireActions?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                PremiseQuestNames = PremiseQuestNames?.Select(v => {
                    return v;
                }).ToArray(),
            };
        }

        public static EzQuestModel FromModel(Gs2.Gs2Quest.Model.QuestModel model)
        {
            return new EzQuestModel {
                QuestModelId = model.QuestModelId == null ? null : model.QuestModelId,
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                Contents = model.Contents == null ? new List<Gs2.Unity.Gs2Quest.Model.EzContents>() : model.Contents.Select(v => {
                    return Gs2.Unity.Gs2Quest.Model.EzContents.FromModel(v);
                }).ToList(),
                ChallengePeriodEventId = model.ChallengePeriodEventId == null ? null : model.ChallengePeriodEventId,
                ConsumeActions = model.ConsumeActions == null ? new List<Gs2.Unity.Gs2Quest.Model.EzConsumeAction>() : model.ConsumeActions.Select(v => {
                    return Gs2.Unity.Gs2Quest.Model.EzConsumeAction.FromModel(v);
                }).ToList(),
                FailedAcquireActions = model.FailedAcquireActions == null ? new List<Gs2.Unity.Gs2Quest.Model.EzAcquireAction>() : model.FailedAcquireActions.Select(v => {
                    return Gs2.Unity.Gs2Quest.Model.EzAcquireAction.FromModel(v);
                }).ToList(),
                PremiseQuestNames = model.PremiseQuestNames == null ? new List<string>() : model.PremiseQuestNames.Select(v => {
                    return v;
                }).ToList(),
            };
        }
    }
}