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
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Realtime.Model
{
	[Preserve]
	public class EzRoom
	{
		/** ルーム名 */
		public string Name { get; set; }
		/** IPアドレス */
		public string IpAddress { get; set; }
		/** 待受ポート */
		public int Port { get; set; }
		/** 暗号鍵 */
		public string EncryptionKey { get; set; }

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

        public Room ToModel()
        {
            return new Room {
                name = Name,
                ipAddress = IpAddress,
                port = Port,
                encryptionKey = EncryptionKey,
            };
        }
	}
}
