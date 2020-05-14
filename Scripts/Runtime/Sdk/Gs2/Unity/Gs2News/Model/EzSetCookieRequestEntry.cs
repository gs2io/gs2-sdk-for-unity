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
using Gs2.Gs2News.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2News.Model
{
	[Preserve]
	[System.Serializable]
	public class EzSetCookieRequestEntry
	{
		/** 記事を閲覧できるようにするために設定してほしい Cookie のキー値 */
		[UnityEngine.SerializeField]
		public string Key;
		/** 記事を閲覧できるようにするために設定してほしい Cookie の値 */
		[UnityEngine.SerializeField]
		public string Value;

		public EzSetCookieRequestEntry()
		{

		}

		public EzSetCookieRequestEntry(Gs2.Gs2News.Model.SetCookieRequestEntry @setCookieRequestEntry)
		{
			Key = @setCookieRequestEntry.key;
			Value = @setCookieRequestEntry.value;
		}

        public virtual SetCookieRequestEntry ToModel()
        {
            return new SetCookieRequestEntry {
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
