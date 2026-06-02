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
using Gs2.Gs2Ranking2.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Ranking2.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzSubscribeRankingModel
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string SubscribeRankingModelId;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Name;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Metadata;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string EntryPeriodEventId;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string AccessPeriodEventId;

        public Gs2.Gs2Ranking2.Model.SubscribeRankingModel ToModel()
        {
            return new Gs2.Gs2Ranking2.Model.SubscribeRankingModel {
                SubscribeRankingModelId = SubscribeRankingModelId,
                Name = Name,
                Metadata = Metadata,
                EntryPeriodEventId = EntryPeriodEventId,
                AccessPeriodEventId = AccessPeriodEventId,
            };
        }

        public static EzSubscribeRankingModel FromModel(Gs2.Gs2Ranking2.Model.SubscribeRankingModel model)
        {
            return new EzSubscribeRankingModel {
                SubscribeRankingModelId = model.SubscribeRankingModelId == null ? null : model.SubscribeRankingModelId,
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                EntryPeriodEventId = model.EntryPeriodEventId == null ? null : model.EntryPeriodEventId,
                AccessPeriodEventId = model.AccessPeriodEventId == null ? null : model.AccessPeriodEventId,
            };
        }
    }
}