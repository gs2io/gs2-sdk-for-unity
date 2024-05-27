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
using Gs2.Gs2Ranking.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Ranking.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzCategoryModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public string Scope;
		[SerializeField]
		public Gs2.Unity.Gs2Ranking.Model.EzGlobalRankingSetting GlobalRankingSetting;
		[SerializeField]
		public string EntryPeriodEventId;
		[SerializeField]
		public string AccessPeriodEventId;

        public Gs2.Gs2Ranking.Model.CategoryModel ToModel()
        {
            return new Gs2.Gs2Ranking.Model.CategoryModel {
                Name = Name,
                Metadata = Metadata,
                Scope = Scope,
                GlobalRankingSetting = GlobalRankingSetting?.ToModel(),
                EntryPeriodEventId = EntryPeriodEventId,
                AccessPeriodEventId = AccessPeriodEventId,
            };
        }

        public static EzCategoryModel FromModel(Gs2.Gs2Ranking.Model.CategoryModel model)
        {
            return new EzCategoryModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                Scope = model.Scope == null ? null : model.Scope,
                GlobalRankingSetting = model.GlobalRankingSetting == null ? null : Gs2.Unity.Gs2Ranking.Model.EzGlobalRankingSetting.FromModel(model.GlobalRankingSetting),
                EntryPeriodEventId = model.EntryPeriodEventId == null ? null : model.EntryPeriodEventId,
                AccessPeriodEventId = model.AccessPeriodEventId == null ? null : model.AccessPeriodEventId,
            };
        }
    }
}