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
using Gs2.Gs2Lottery.Model;
using System.Collections.Generic;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Lottery.Model
{
	[Preserve]
	[System.Serializable]
	public class EzProbability
	{
		/** 景品の種類 */
		[UnityEngine.SerializeField]
		public EzDrawnPrize Prize;
		/** 排出確率(0.0〜1.0) */
		[UnityEngine.SerializeField]
		public float Rate;

		public EzProbability()
		{

		}

		public EzProbability(Gs2.Gs2Lottery.Model.Probability @probability)
		{
			Prize = @probability.prize != null ? new EzDrawnPrize(@probability.prize) : null;
			Rate = @probability.rate.HasValue ? @probability.rate.Value : 0;
		}

        public virtual Probability ToModel()
        {
            return new Probability {
                prize = new DrawnPrize {
                    acquireActions = Prize.AcquireActions != null ? Prize.AcquireActions.Select(Value1 =>
                            {
                                return new AcquireAction
                                {
                                    action = Value1.Action,
                                    request = Value1.Request,
                                };
                            }
                    ).ToList() : new List<AcquireAction>(new AcquireAction[] {}),
                },
                rate = Rate,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.Prize != null)
            {
                writer.WritePropertyName("prize");
                this.Prize.WriteJson(writer);
            }
            writer.WritePropertyName("rate");
            writer.Write(this.Rate);
            writer.WriteObjectEnd();
        }
	}
}
