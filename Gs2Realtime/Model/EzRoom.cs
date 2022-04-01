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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Realtime.Model
{
	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzRoom
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string IpAddress;
		[SerializeField]
		public int Port;
		[SerializeField]
		public string EncryptionKey;

        public Gs2.Gs2Realtime.Model.Room ToModel()
        {
            return new Gs2.Gs2Realtime.Model.Room {
                Name = Name,
                IpAddress = IpAddress,
                Port = Port,
                EncryptionKey = EncryptionKey,
            };
        }

        public static EzRoom FromModel(Gs2.Gs2Realtime.Model.Room model)
        {
            return new EzRoom {
                Name = model.Name == null ? null : model.Name,
                IpAddress = model.IpAddress == null ? null : model.IpAddress,
                Port = model.Port ?? 0,
                EncryptionKey = model.EncryptionKey == null ? null : model.EncryptionKey,
            };
        }
    }
}