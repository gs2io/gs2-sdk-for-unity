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

using Gs2.Gs2Datastore.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Datastore.Model
{
	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzDataObjectHistory
	{
		[SerializeField]
		public string DataObjectHistoryId;
		[SerializeField]
		public string Generation;
		[SerializeField]
		public long ContentLength;
		[SerializeField]
		public long CreatedAt;

        public Gs2.Gs2Datastore.Model.DataObjectHistory ToModel()
        {
            return new Gs2.Gs2Datastore.Model.DataObjectHistory {
                DataObjectHistoryId = DataObjectHistoryId,
                Generation = Generation,
                ContentLength = ContentLength,
                CreatedAt = CreatedAt,
            };
        }

        public static EzDataObjectHistory FromModel(Gs2.Gs2Datastore.Model.DataObjectHistory model)
        {
            return new EzDataObjectHistory {
                DataObjectHistoryId = model.DataObjectHistoryId == null ? null : model.DataObjectHistoryId,
                Generation = model.Generation == null ? null : model.Generation,
                ContentLength = model.ContentLength ?? 0,
                CreatedAt = model.CreatedAt ?? 0,
            };
        }
    }
}