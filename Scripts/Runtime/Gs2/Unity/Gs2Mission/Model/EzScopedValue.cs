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
	public class EzScopedValue
	{
		/** リセットタイミング */
		[UnityEngine.SerializeField]
		public string ResetType;
		/** カウント */
		[UnityEngine.SerializeField]
		public long Value;

		public EzScopedValue()
		{

		}

		public EzScopedValue(Gs2.Gs2Mission.Model.ScopedValue @scopedValue)
		{
			ResetType = @scopedValue.resetType;
			Value = @scopedValue.value.HasValue ? @scopedValue.value.Value : 0;
		}

        public virtual ScopedValue ToModel()
        {
            return new ScopedValue {
                resetType = ResetType,
                value = Value,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.ResetType != null)
            {
                writer.WritePropertyName("resetType");
                writer.Write(this.ResetType);
            }
            writer.WritePropertyName("value");
            writer.Write(this.Value);
            writer.WriteObjectEnd();
        }
	}
}
