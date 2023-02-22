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
using Gs2.Gs2SerialKey.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2SerialKey.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzSerialKey
	{
		[SerializeField]
		public string CampaignModelName;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public string Code;
		[SerializeField]
		public string Status;

        public Gs2.Gs2SerialKey.Model.SerialKey ToModel()
        {
            return new Gs2.Gs2SerialKey.Model.SerialKey {
                CampaignModelName = CampaignModelName,
                Metadata = Metadata,
                Code = Code,
                Status = Status,
            };
        }

        public static EzSerialKey FromModel(Gs2.Gs2SerialKey.Model.SerialKey model)
        {
            return new EzSerialKey {
                CampaignModelName = model.CampaignModelName == null ? null : model.CampaignModelName,
                Metadata = model.Metadata == null ? null : model.Metadata,
                Code = model.Code == null ? null : model.Code,
                Status = model.Status == null ? null : model.Status,
            };
        }
    }
}