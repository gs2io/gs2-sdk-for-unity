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

using Gs2.Gs2Stamina.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Stamina.Model
{
	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzStamina
	{
		[SerializeField]
		public string StaminaName;
		[SerializeField]
		public int Value;
		[SerializeField]
		public int MaxValue;
		[SerializeField]
		public int RecoverIntervalMinutes;
		[SerializeField]
		public int RecoverValue;
		[SerializeField]
		public long NextRecoverAt;

        public Gs2.Gs2Stamina.Model.Stamina ToModel()
        {
            return new Gs2.Gs2Stamina.Model.Stamina {
                StaminaName = StaminaName,
                Value = Value,
                MaxValue = MaxValue,
                RecoverIntervalMinutes = RecoverIntervalMinutes,
                RecoverValue = RecoverValue,
                NextRecoverAt = NextRecoverAt,
            };
        }

        public static EzStamina FromModel(Gs2.Gs2Stamina.Model.Stamina model)
        {
            return new EzStamina {
                StaminaName = model.StaminaName == null ? null : model.StaminaName,
                Value = model.Value ?? 0,
                MaxValue = model.MaxValue ?? 0,
                RecoverIntervalMinutes = model.RecoverIntervalMinutes ?? 0,
                RecoverValue = model.RecoverValue ?? 0,
                NextRecoverAt = model.NextRecoverAt ?? 0,
            };
        }
    }
}