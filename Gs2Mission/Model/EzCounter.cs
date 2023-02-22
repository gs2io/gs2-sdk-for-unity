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
using Gs2.Gs2Mission.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Mission.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzCounter
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public List<Gs2.Unity.Gs2Mission.Model.EzScopedValue> Values;

        public Gs2.Gs2Mission.Model.Counter ToModel()
        {
            return new Gs2.Gs2Mission.Model.Counter {
                Name = Name,
                Values = Values?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
            };
        }

        public static EzCounter FromModel(Gs2.Gs2Mission.Model.Counter model)
        {
            return new EzCounter {
                Name = model.Name == null ? null : model.Name,
                Values = model.Values == null ? new List<Gs2.Unity.Gs2Mission.Model.EzScopedValue>() : model.Values.Select(v => {
                    return Gs2.Unity.Gs2Mission.Model.EzScopedValue.FromModel(v);
                }).ToList(),
            };
        }
    }
}