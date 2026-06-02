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

using System;
using Gs2.Gs2Realtime.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Realtime.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzRoom
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Name;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string IpAddress;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int Port;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
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