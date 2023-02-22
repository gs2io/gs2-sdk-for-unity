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
using Gs2.Gs2Formation.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Formation.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzForm
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public int Index;
		[SerializeField]
		public List<Gs2.Unity.Gs2Formation.Model.EzSlot> Slots;

        public Gs2.Gs2Formation.Model.Form ToModel()
        {
            return new Gs2.Gs2Formation.Model.Form {
                Name = Name,
                Index = Index,
                Slots = Slots?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
            };
        }

        public static EzForm FromModel(Gs2.Gs2Formation.Model.Form model)
        {
            return new EzForm {
                Name = model.Name == null ? null : model.Name,
                Index = model.Index ?? 0,
                Slots = model.Slots == null ? new List<Gs2.Unity.Gs2Formation.Model.EzSlot>() : model.Slots.Select(v => {
                    return Gs2.Unity.Gs2Formation.Model.EzSlot.FromModel(v);
                }).ToList(),
            };
        }
    }
}