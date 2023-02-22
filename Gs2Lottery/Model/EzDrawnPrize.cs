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
using Gs2.Gs2Lottery.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Lottery.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzDrawnPrize
	{
		[SerializeField]
		public string PrizeId;
		[SerializeField]
		public List<Gs2.Unity.Core.Model.EzAcquireAction> AcquireActions;

        public Gs2.Gs2Lottery.Model.DrawnPrize ToModel()
        {
            return new Gs2.Gs2Lottery.Model.DrawnPrize {
                PrizeId = PrizeId,
                AcquireActions = AcquireActions?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
            };
        }

        public static EzDrawnPrize FromModel(Gs2.Gs2Lottery.Model.DrawnPrize model)
        {
            return new EzDrawnPrize {
                PrizeId = model.PrizeId == null ? null : model.PrizeId,
                AcquireActions = model.AcquireActions == null ? new List<Gs2.Unity.Core.Model.EzAcquireAction>() : model.AcquireActions.Select(v => {
                    return Gs2.Unity.Core.Model.EzAcquireAction.FromModel(v);
                }).ToList(),
            };
        }
    }
}