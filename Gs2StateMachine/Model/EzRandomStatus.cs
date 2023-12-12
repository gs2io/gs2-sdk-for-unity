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
using Gs2.Gs2StateMachine.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2StateMachine.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzRandomStatus
	{
		[SerializeField]
		public long Seed;
		[SerializeField]
		public List<Gs2.Unity.Gs2StateMachine.Model.EzRandomUsed> Used;

        public Gs2.Gs2StateMachine.Model.RandomStatus ToModel()
        {
            return new Gs2.Gs2StateMachine.Model.RandomStatus {
                Seed = Seed,
                Used = Used?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
            };
        }

        public static EzRandomStatus FromModel(Gs2.Gs2StateMachine.Model.RandomStatus model)
        {
            return new EzRandomStatus {
                Seed = model.Seed ?? 0,
                Used = model.Used == null ? new List<Gs2.Unity.Gs2StateMachine.Model.EzRandomUsed>() : model.Used.Select(v => {
                    return Gs2.Unity.Gs2StateMachine.Model.EzRandomUsed.FromModel(v);
                }).ToList(),
            };
        }
    }
}