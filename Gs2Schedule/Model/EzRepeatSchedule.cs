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
using Gs2.Gs2Schedule.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Schedule.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzRepeatSchedule
	{
		[SerializeField]
		public int RepeatCount;
		[SerializeField]
		public long CurrentRepeatStartAt;
		[SerializeField]
		public long CurrentRepeatEndAt;
		[SerializeField]
		public long LastRepeatEndAt;
		[SerializeField]
		public long NextRepeatStartAt;

        public Gs2.Gs2Schedule.Model.RepeatSchedule ToModel()
        {
            return new Gs2.Gs2Schedule.Model.RepeatSchedule {
                RepeatCount = RepeatCount,
                CurrentRepeatStartAt = CurrentRepeatStartAt,
                CurrentRepeatEndAt = CurrentRepeatEndAt,
                LastRepeatEndAt = LastRepeatEndAt,
                NextRepeatStartAt = NextRepeatStartAt,
            };
        }

        public static EzRepeatSchedule FromModel(Gs2.Gs2Schedule.Model.RepeatSchedule model)
        {
            return new EzRepeatSchedule {
                RepeatCount = model.RepeatCount ?? 0,
                CurrentRepeatStartAt = model.CurrentRepeatStartAt ?? 0,
                CurrentRepeatEndAt = model.CurrentRepeatEndAt ?? 0,
                LastRepeatEndAt = model.LastRepeatEndAt ?? 0,
                NextRepeatStartAt = model.NextRepeatStartAt ?? 0,
            };
        }
    }
}