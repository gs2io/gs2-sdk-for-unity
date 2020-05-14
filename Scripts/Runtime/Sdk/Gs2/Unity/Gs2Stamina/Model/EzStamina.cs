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
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Stamina.Model
{
	[Preserve]
	[System.Serializable]
	public class EzStamina
	{
		/** スタミナモデルの名前 */
		[UnityEngine.SerializeField]
		public string StaminaName;
		/** 最終更新時におけるスタミナ値 */
		[UnityEngine.SerializeField]
		public int Value;
		/** スタミナの最大値 */
		[UnityEngine.SerializeField]
		public int MaxValue;
		/** スタミナの回復間隔(分) */
		[UnityEngine.SerializeField]
		public int RecoverIntervalMinutes;
		/** スタミナの回復量 */
		[UnityEngine.SerializeField]
		public int RecoverValue;
		/** 次回スタミナが回復する時間 */
		[UnityEngine.SerializeField]
		public long NextRecoverAt;

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

        public virtual Stamina ToModel()
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

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.StaminaName != null)
            {
                writer.WritePropertyName("staminaName");
                writer.Write(this.StaminaName);
            }
            writer.WritePropertyName("value");
            writer.Write(this.Value);
            writer.WritePropertyName("maxValue");
            writer.Write(this.MaxValue);
            writer.WritePropertyName("recoverIntervalMinutes");
            writer.Write(this.RecoverIntervalMinutes);
            writer.WritePropertyName("recoverValue");
            writer.Write(this.RecoverValue);
            writer.WritePropertyName("nextRecoverAt");
            writer.Write(this.NextRecoverAt);
            writer.WriteObjectEnd();
        }
	}
}
