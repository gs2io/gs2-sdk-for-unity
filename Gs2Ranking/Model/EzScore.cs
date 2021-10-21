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
	public class EzScore
	{
		[SerializeField]
		public string CategoryName;
		[SerializeField]
		public string UserId;
		[SerializeField]
		public string UniqueId;
		[SerializeField]
		public string ScorerUserId;
		[SerializeField]
		public long Score;
		[SerializeField]
		public string Metadata;

        public Gs2.Gs2Ranking.Model.Score ToModel()
        {
            return new Gs2.Gs2Ranking.Model.Score {
                CategoryName = CategoryName,
                UserId = UserId,
                UniqueId = UniqueId,
                ScorerUserId = ScorerUserId,
                Value = Score,
                Metadata = Metadata,
            };
        }

        public static EzScore FromModel(Gs2.Gs2Ranking.Model.Score model)
        {
            return new EzScore {
                CategoryName = model.CategoryName == null ? null : model.CategoryName,
                UserId = model.UserId == null ? null : model.UserId,
                UniqueId = model.UniqueId == null ? null : model.UniqueId,
                ScorerUserId = model.ScorerUserId == null ? null : model.ScorerUserId,
                Score = model.Value ?? 0,
                Metadata = model.Metadata == null ? null : model.Metadata,
            };
        }
    }
}