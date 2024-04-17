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
using Gs2.Gs2Buff.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Buff.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzBuffEntryModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public string TargetType;
		[SerializeField]
		public Gs2.Unity.Gs2Buff.Model.EzBuffTargetModel TargetModel;
		[SerializeField]
		public Gs2.Unity.Gs2Buff.Model.EzBuffTargetAction TargetAction;
		[SerializeField]
		public string Expression;
		[SerializeField]
		public string ApplyPeriodScheduleEventId;

        public Gs2.Gs2Buff.Model.BuffEntryModel ToModel()
        {
            return new Gs2.Gs2Buff.Model.BuffEntryModel {
                Name = Name,
                Metadata = Metadata,
                TargetType = TargetType,
                TargetModel = TargetModel?.ToModel(),
                TargetAction = TargetAction?.ToModel(),
                Expression = Expression,
                ApplyPeriodScheduleEventId = ApplyPeriodScheduleEventId,
            };
        }

        public static EzBuffEntryModel FromModel(Gs2.Gs2Buff.Model.BuffEntryModel model)
        {
            return new EzBuffEntryModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                TargetType = model.TargetType == null ? null : model.TargetType,
                TargetModel = model.TargetModel == null ? null : Gs2.Unity.Gs2Buff.Model.EzBuffTargetModel.FromModel(model.TargetModel),
                TargetAction = model.TargetAction == null ? null : Gs2.Unity.Gs2Buff.Model.EzBuffTargetAction.FromModel(model.TargetAction),
                Expression = model.Expression == null ? null : model.Expression,
                ApplyPeriodScheduleEventId = model.ApplyPeriodScheduleEventId == null ? null : model.ApplyPeriodScheduleEventId,
            };
        }
    }
}