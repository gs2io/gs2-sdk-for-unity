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

using Gs2.Gs2Experience.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Experience.Model
{
	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzThreshold
	{
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public List<long> Values;

        public Gs2.Gs2Experience.Model.Threshold ToModel()
        {
            return new Gs2.Gs2Experience.Model.Threshold {
                Metadata = Metadata,
                Values = Values?.Select(v => {
                    return v;
                }).ToArray(),
            };
        }

        public static EzThreshold FromModel(Gs2.Gs2Experience.Model.Threshold model)
        {
            return new EzThreshold {
                Metadata = model.Metadata == null ? null : model.Metadata,
                Values = model.Values == null ? new List<long>() : model.Values.Select(v => {
                    return v;
                }).ToList(),
            };
        }
    }
}