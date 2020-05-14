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
using Gs2.Gs2Quest.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Quest.Model
{
	[Preserve]
	[System.Serializable]
	public class EzConfig
	{
		/** 名前 */
		[UnityEngine.SerializeField]
		public string Key;
		/** 値 */
		[UnityEngine.SerializeField]
		public string Value;

		public EzConfig()
		{

		}

		public EzConfig(Gs2.Gs2Quest.Model.Config @config)
		{
			Key = @config.key;
			Value = @config.value;
		}

        public virtual Config ToModel()
        {
            return new Config {
                key = Key,
                value = Value,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.Key != null)
            {
                writer.WritePropertyName("key");
                writer.Write(this.Key);
            }
            if(this.Value != null)
            {
                writer.WritePropertyName("value");
                writer.Write(this.Value);
            }
            writer.WriteObjectEnd();
        }
	}
}
