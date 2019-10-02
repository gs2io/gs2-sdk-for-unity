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
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Stamina.Model
{
	[Preserve]
	public class EzStamina
	{
		/** スタミナモデルの名前 */
		public string StaminaName { get; set; }
		/** 最終更新時におけるスタミナ値 */
		public int Value { get; set; }
		/** スタミナの最大値 */
		public int MaxValue { get; set; }
		/** スタミナの回復間隔(分) */
		public int RecoverIntervalMinutes { get; set; }
		/** スタミナの回復量 */
		public int RecoverValue { get; set; }
		/** 次回スタミナが回復する時間 */
		public long NextRecoverAt { get; set; }

		public EzStamina()
		{

		}

		public EzStamina(Gs2.Gs2Stamina.Model.Stamina @stamina)
		{
			StaminaName = @stamina.staminaName;
			Value = @stamina.value.HasValue ? @stamina.value.Value : 0;
			MaxValue = @stamina.maxValue.HasValue ? @stamina.maxValue.Value : 0;
			RecoverIntervalMinutes = @stamina.recoverIntervalMinutes.HasValue ? @stamina.recoverIntervalMinutes.Value : 0;
			RecoverValue = @stamina.recoverValue.HasValue ? @stamina.recoverValue.Value : 0;
			NextRecoverAt = @stamina.nextRecoverAt.HasValue ? @stamina.nextRecoverAt.Value : 0;
		}

        public Stamina ToModel()
        {
            return new Stamina {
                staminaName = StaminaName,
                value = Value,
                maxValue = MaxValue,
                recoverIntervalMinutes = RecoverIntervalMinutes,
                recoverValue = RecoverValue,
                nextRecoverAt = NextRecoverAt,
            };
        }
	}
}
