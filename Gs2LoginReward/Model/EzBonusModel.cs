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
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2LoginReward.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzBonusModel
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Name;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Metadata;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Mode;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string PeriodEventId;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int ResetHour;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Repeat;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<Gs2.Unity.Gs2LoginReward.Model.EzReward> Rewards;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string MissedReceiveRelief;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<Gs2.Unity.Core.Model.EzVerifyAction> MissedReceiveReliefVerifyActions;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<Gs2.Unity.Core.Model.EzConsumeAction> MissedReceiveReliefConsumeActions;

        public Gs2.Gs2LoginReward.Model.BonusModel ToModel()
        {
            return new Gs2.Gs2LoginReward.Model.BonusModel {
                Name = Name,
                Metadata = Metadata,
                Mode = Mode,
                PeriodEventId = PeriodEventId,
                ResetHour = ResetHour,
                Repeat = Repeat,
                Rewards = Rewards?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                MissedReceiveRelief = MissedReceiveRelief,
                MissedReceiveReliefVerifyActions = MissedReceiveReliefVerifyActions?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
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
                Mode = model.Mode == null ? null : model.Mode,
                PeriodEventId = model.PeriodEventId == null ? null : model.PeriodEventId,
                ResetHour = model.ResetHour ?? 0,
                Repeat = model.Repeat == null ? null : model.Repeat,
                Rewards = model.Rewards == null ? new List<Gs2.Unity.Gs2LoginReward.Model.EzReward>() : model.Rewards.Select(v => {
                    return Gs2.Unity.Gs2LoginReward.Model.EzReward.FromModel(v);
                }).ToList(),
                MissedReceiveRelief = model.MissedReceiveRelief == null ? null : model.MissedReceiveRelief,
                MissedReceiveReliefVerifyActions = model.MissedReceiveReliefVerifyActions == null ? new List<Gs2.Unity.Core.Model.EzVerifyAction>() : model.MissedReceiveReliefVerifyActions.Select(v => {
                    return Gs2.Unity.Core.Model.EzVerifyAction.FromModel(v);
                }).ToList(),
                MissedReceiveReliefConsumeActions = model.MissedReceiveReliefConsumeActions == null ? new List<Gs2.Unity.Core.Model.EzConsumeAction>() : model.MissedReceiveReliefConsumeActions.Select(v => {
                    return Gs2.Unity.Core.Model.EzConsumeAction.FromModel(v);
                }).ToList(),
            };
        }
    }
}