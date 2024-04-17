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
using Gs2.Gs2Buff.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Buff.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzBuffTargetModel
	{
		[SerializeField]
		public string TargetModelName;
		[SerializeField]
		public string TargetFieldName;
		[SerializeField]
		public List<Gs2.Unity.Gs2Buff.Model.EzBuffTargetGrn> ConditionGrns;
		[SerializeField]
		public float Rate;

        public Gs2.Gs2Buff.Model.BuffTargetModel ToModel()
        {
            return new Gs2.Gs2Buff.Model.BuffTargetModel {
                TargetModelName = TargetModelName,
                TargetFieldName = TargetFieldName,
                ConditionGrns = ConditionGrns?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                Rate = Rate,
            };
        }

        public static EzBuffTargetModel FromModel(Gs2.Gs2Buff.Model.BuffTargetModel model)
        {
            return new EzBuffTargetModel {
                TargetModelName = model.TargetModelName == null ? null : model.TargetModelName,
                TargetFieldName = model.TargetFieldName == null ? null : model.TargetFieldName,
                ConditionGrns = model.ConditionGrns == null ? new List<Gs2.Unity.Gs2Buff.Model.EzBuffTargetGrn>() : model.ConditionGrns.Select(v => {
                    return Gs2.Unity.Gs2Buff.Model.EzBuffTargetGrn.FromModel(v);
                }).ToList(),
                Rate = model.Rate ?? 0,
            };
        }
    }
}