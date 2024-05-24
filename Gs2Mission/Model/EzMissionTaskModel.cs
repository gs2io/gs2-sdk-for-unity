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
using Gs2.Gs2Mission.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Mission.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzMissionTaskModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public string VerifyCompleteType;
		[SerializeField]
		public Gs2.Unity.Gs2Mission.Model.EzTargetCounterModel TargetCounter;
		[SerializeField]
		public List<Gs2.Unity.Core.Model.EzConsumeAction> VerifyCompleteConsumeActions;
		[SerializeField]
		public List<Gs2.Unity.Core.Model.EzAcquireAction> CompleteAcquireActions;
		[SerializeField]
		public string ChallengePeriodEventId;
		[SerializeField]
		public string PremiseMissionTaskName;
		[SerializeField]
		public string CounterName;
		[SerializeField]
		public string TargetResetType;
		[SerializeField]
		public long TargetValue;

        public Gs2.Gs2Mission.Model.MissionTaskModel ToModel()
        {
            return new Gs2.Gs2Mission.Model.MissionTaskModel {
                Name = Name,
                Metadata = Metadata,
                VerifyCompleteType = VerifyCompleteType,
                TargetCounter = TargetCounter?.ToModel(),
                VerifyCompleteConsumeActions = VerifyCompleteConsumeActions?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                CompleteAcquireActions = CompleteAcquireActions?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                ChallengePeriodEventId = ChallengePeriodEventId,
                PremiseMissionTaskName = PremiseMissionTaskName,
                CounterName = CounterName,
                TargetResetType = TargetResetType,
                TargetValue = TargetValue,
            };
        }

        public static EzMissionTaskModel FromModel(Gs2.Gs2Mission.Model.MissionTaskModel model)
        {
            return new EzMissionTaskModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                VerifyCompleteType = model.VerifyCompleteType == null ? null : model.VerifyCompleteType,
                TargetCounter = model.TargetCounter == null ? null : Gs2.Unity.Gs2Mission.Model.EzTargetCounterModel.FromModel(model.TargetCounter),
                VerifyCompleteConsumeActions = model.VerifyCompleteConsumeActions == null ? new List<Gs2.Unity.Core.Model.EzConsumeAction>() : model.VerifyCompleteConsumeActions.Select(v => {
                    return Gs2.Unity.Core.Model.EzConsumeAction.FromModel(v);
                }).ToList(),
                CompleteAcquireActions = model.CompleteAcquireActions == null ? new List<Gs2.Unity.Core.Model.EzAcquireAction>() : model.CompleteAcquireActions.Select(v => {
                    return Gs2.Unity.Core.Model.EzAcquireAction.FromModel(v);
                }).ToList(),
                ChallengePeriodEventId = model.ChallengePeriodEventId == null ? null : model.ChallengePeriodEventId,
                PremiseMissionTaskName = model.PremiseMissionTaskName == null ? null : model.PremiseMissionTaskName,
                CounterName = model.CounterName == null ? null : model.CounterName,
                TargetResetType = model.TargetResetType == null ? null : model.TargetResetType,
                TargetValue = model.TargetValue ?? 0,
            };
        }
    }
}