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
using Gs2.Gs2Idle.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Idle.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzCategoryModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public int RewardIntervalMinutes;
		[SerializeField]
		public int DefaultMaximumIdleMinutes;
		[SerializeField]
		public List<Gs2.Unity.Gs2Idle.Model.EzAcquireActionList> AcquireActions;
		[SerializeField]
		public string IdlePeriodScheduleId;
		[SerializeField]
		public string ReceivePeriodScheduleId;

        public Gs2.Gs2Idle.Model.CategoryModel ToModel()
        {
            return new Gs2.Gs2Idle.Model.CategoryModel {
                Name = Name,
                Metadata = Metadata,
                RewardIntervalMinutes = RewardIntervalMinutes,
                DefaultMaximumIdleMinutes = DefaultMaximumIdleMinutes,
                AcquireActions = AcquireActions?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                IdlePeriodScheduleId = IdlePeriodScheduleId,
                ReceivePeriodScheduleId = ReceivePeriodScheduleId,
            };
        }

        public static EzCategoryModel FromModel(Gs2.Gs2Idle.Model.CategoryModel model)
        {
            return new EzCategoryModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                RewardIntervalMinutes = model.RewardIntervalMinutes ?? 0,
                DefaultMaximumIdleMinutes = model.DefaultMaximumIdleMinutes ?? 0,
                AcquireActions = model.AcquireActions == null ? new List<Gs2.Unity.Gs2Idle.Model.EzAcquireActionList>() : model.AcquireActions.Select(v => {
                    return Gs2.Unity.Gs2Idle.Model.EzAcquireActionList.FromModel(v);
                }).ToList(),
                IdlePeriodScheduleId = model.IdlePeriodScheduleId == null ? null : model.IdlePeriodScheduleId,
                ReceivePeriodScheduleId = model.ReceivePeriodScheduleId == null ? null : model.ReceivePeriodScheduleId,
            };
        }
    }
}