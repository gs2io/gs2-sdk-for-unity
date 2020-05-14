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
using Gs2.Gs2Mission.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Mission.Model
{
	[Preserve]
	[System.Serializable]
	public class EzCounter
	{
		/** カウンター名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** 値 */
		[UnityEngine.SerializeField]
		public List<EzScopedValue> Values;

		public EzCounter()
		{

		}

		public EzCounter(Gs2.Gs2Mission.Model.Counter @counter)
		{
			Name = @counter.name;
			Values = @counter.values != null ? @counter.values.Select(value =>
                {
                    return new EzScopedValue(value);
                }
			).ToList() : new List<EzScopedValue>(new EzScopedValue[] {});
		}

        public virtual Counter ToModel()
        {
            return new Counter {
                name = Name,
                values = Values != null ? Values.Select(Value0 =>
                        {
                            return new ScopedValue
                            {
                                resetType = Value0.ResetType,
                                value = Value0.Value,
                            };
                        }
                ).ToList() : new List<ScopedValue>(new ScopedValue[] {}),
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.Name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.Name);
            }
            if(this.Values != null)
            {
                writer.WritePropertyName("values");
                writer.WriteArrayStart();
                foreach(var item in this.Values)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
        }
	}
}
