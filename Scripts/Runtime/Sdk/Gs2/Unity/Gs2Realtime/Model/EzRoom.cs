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
using Gs2.Gs2Realtime.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Realtime.Model
{
	[Preserve]
	[System.Serializable]
	public class EzRoom
	{
		/** ルーム名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** IPアドレス */
		[UnityEngine.SerializeField]
		public string IpAddress;
		/** 待受ポート */
		[UnityEngine.SerializeField]
		public int Port;
		/** 暗号鍵 */
		[UnityEngine.SerializeField]
		public string EncryptionKey;

		public EzRoom()
		{

		}

		public EzRoom(Gs2.Gs2Realtime.Model.Room @room)
		{
			Name = @room.name;
			IpAddress = @room.ipAddress;
			Port = @room.port.HasValue ? @room.port.Value : 0;
			EncryptionKey = @room.encryptionKey;
		}

        public virtual Room ToModel()
        {
            return new Room {
                name = Name,
                ipAddress = IpAddress,
                port = Port,
                encryptionKey = EncryptionKey,
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
            if(this.IpAddress != null)
            {
                writer.WritePropertyName("ipAddress");
                writer.Write(this.IpAddress);
            }
            writer.WritePropertyName("port");
            writer.Write(this.Port);
            if(this.EncryptionKey != null)
            {
                writer.WritePropertyName("encryptionKey");
                writer.Write(this.EncryptionKey);
            }
            writer.WriteObjectEnd();
        }
	}
}
