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
using Gs2.Gs2Version.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Version.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzStatus
	{
		[SerializeField]
		public Gs2.Unity.Gs2Version.Model.EzVersionModel VersionModel;
		[SerializeField]
		public Gs2.Unity.Gs2Version.Model.EzVersion CurrentVersion;

        public Gs2.Gs2Version.Model.Status ToModel()
        {
            return new Gs2.Gs2Version.Model.Status {
                VersionModel = VersionModel?.ToModel(),
                CurrentVersion = CurrentVersion?.ToModel(),
            };
        }

        public static EzStatus FromModel(Gs2.Gs2Version.Model.Status model)
        {
            return new EzStatus {
                VersionModel = model.VersionModel == null ? null : Gs2.Unity.Gs2Version.Model.EzVersionModel.FromModel(model.VersionModel),
                CurrentVersion = model.CurrentVersion == null ? null : Gs2.Unity.Gs2Version.Model.EzVersion.FromModel(model.CurrentVersion),
            };
        }
    }
}