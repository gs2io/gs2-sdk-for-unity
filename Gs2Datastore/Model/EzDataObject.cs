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
	public class EzDataObject
	{
		[SerializeField]
		public string DataObjectId;
		[SerializeField]
		public string Name;
		[SerializeField]
		public string UserId;
		[SerializeField]
		public string Scope;
		[SerializeField]
		public List<string> AllowUserIds;
		[SerializeField]
		public string Status;
		[SerializeField]
		public string Generation;
		[SerializeField]
		public long CreatedAt;
		[SerializeField]
		public long UpdatedAt;

        public Gs2.Gs2Datastore.Model.DataObject ToModel()
        {
            return new Gs2.Gs2Datastore.Model.DataObject {
                DataObjectId = DataObjectId,
                Name = Name,
                UserId = UserId,
                Scope = Scope,
                AllowUserIds = AllowUserIds?.Select(v => {
                    return v;
                }).ToArray(),
                Status = Status,
                Generation = Generation,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
            };
        }

        public static EzDataObject FromModel(Gs2.Gs2Datastore.Model.DataObject model)
        {
            return new EzDataObject {
                DataObjectId = model.DataObjectId == null ? null : model.DataObjectId,
                Name = model.Name == null ? null : model.Name,
                UserId = model.UserId == null ? null : model.UserId,
                Scope = model.Scope == null ? null : model.Scope,
                AllowUserIds = model.AllowUserIds == null ? new List<string>() : model.AllowUserIds.Select(v => {
                    return v;
                }).ToList(),
                Status = model.Status == null ? null : model.Status,
                Generation = model.Generation == null ? null : model.Generation,
                CreatedAt = model.CreatedAt ?? 0,
                UpdatedAt = model.UpdatedAt ?? 0,
            };
        }
    }
}