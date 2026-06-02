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
using Gs2.Gs2LoginReward.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2LoginReward.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzReceiveStatus
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string BonusModelName;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<bool> ReceivedSteps;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public long LastReceivedAt;

        public Gs2.Gs2LoginReward.Model.ReceiveStatus ToModel()
        {
            return new Gs2.Gs2LoginReward.Model.ReceiveStatus {
                BonusModelName = BonusModelName,
                ReceivedSteps = ReceivedSteps?.Select(v => {
                    return v;
                }).ToArray(),
                LastReceivedAt = LastReceivedAt,
            };
        }

        public static EzReceiveStatus FromModel(Gs2.Gs2LoginReward.Model.ReceiveStatus model)
        {
            return new EzReceiveStatus {
                BonusModelName = model.BonusModelName == null ? null : model.BonusModelName,
                ReceivedSteps = model.ReceivedSteps == null ? new List<bool>() : model.ReceivedSteps.Select(v => {
                    return v;
                }).ToList(),
                LastReceivedAt = model.LastReceivedAt ?? 0,
            };
        }
    }
}