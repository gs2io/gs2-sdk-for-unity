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
using Gs2.Gs2Schedule.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Schedule.Model
{
	[Preserve]
	[System.Serializable]
	public class EzTrigger
	{
		/** トリガー */
		[UnityEngine.SerializeField]
		public string TriggerId;
		/** トリガーの名前 */
		[UnityEngine.SerializeField]
		public string Name;
		/** 作成日時 */
		[UnityEngine.SerializeField]
		public long CreatedAt;
		/** トリガーの有効期限 */
		[UnityEngine.SerializeField]
		public long ExpiresAt;

		public EzTrigger()
		{

		}

		public EzTrigger(Gs2.Gs2Schedule.Model.Trigger @trigger)
		{
			TriggerId = @trigger.triggerId;
			Name = @trigger.name;
			CreatedAt = @trigger.createdAt.HasValue ? @trigger.createdAt.Value : 0;
			ExpiresAt = @trigger.expiresAt.HasValue ? @trigger.expiresAt.Value : 0;
		}

        public virtual Trigger ToModel()
        {
            return new Trigger {
                triggerId = TriggerId,
                name = Name,
                createdAt = CreatedAt,
                expiresAt = ExpiresAt,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.TriggerId != null)
            {
                writer.WritePropertyName("triggerId");
                writer.Write(this.TriggerId);
            }
            if(this.Name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.Name);
            }
            writer.WritePropertyName("createdAt");
            writer.Write(this.CreatedAt);
            writer.WritePropertyName("expiresAt");
            writer.Write(this.ExpiresAt);
            writer.WriteObjectEnd();
        }
	}
}
