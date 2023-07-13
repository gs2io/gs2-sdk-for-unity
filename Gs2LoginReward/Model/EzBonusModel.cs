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

using System;
using Gs2.Gs2LoginReward.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2LoginReward.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzBonusModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public string PeriodEventId;
		[SerializeField]
		public int ResetHour;
		[SerializeField]
		public List<Gs2.Unity.Gs2LoginReward.Model.EzReward> Rewards;
		[SerializeField]
		public string MissedReceiveRelief;
		[SerializeField]
		public List<Gs2.Unity.Core.Model.EzConsumeAction> MissedReceiveReliefConsumeActions;

        public Gs2.Gs2LoginReward.Model.BonusModel ToModel()
        {
            return new Gs2.Gs2LoginReward.Model.BonusModel {
                Name = Name,
                Metadata = Metadata,
                PeriodEventId = PeriodEventId,
                ResetHour = ResetHour,
                Rewards = Rewards?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                MissedReceiveRelief = MissedReceiveRelief,
                MissedReceiveReliefConsumeActions = MissedReceiveReliefConsumeActions?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
            };
        }

        public static EzBonusModel FromModel(Gs2.Gs2LoginReward.Model.BonusModel model)
        {
            return new EzBonusModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                PeriodEventId = model.PeriodEventId == null ? null : model.PeriodEventId,
                ResetHour = model.ResetHour ?? 0,
                Rewards = model.Rewards == null ? new List<Gs2.Unity.Gs2LoginReward.Model.EzReward>() : model.Rewards.Select(v => {
                    return Gs2.Unity.Gs2LoginReward.Model.EzReward.FromModel(v);
                }).ToList(),
                MissedReceiveRelief = model.MissedReceiveRelief == null ? null : model.MissedReceiveRelief,
                MissedReceiveReliefConsumeActions = model.MissedReceiveReliefConsumeActions == null ? new List<Gs2.Unity.Core.Model.EzConsumeAction>() : model.MissedReceiveReliefConsumeActions.Select(v => {
                    return Gs2.Unity.Core.Model.EzConsumeAction.FromModel(v);
                }).ToList(),
            };
        }
    }
}