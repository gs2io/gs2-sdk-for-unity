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
	public class EzVersionModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public Gs2.Unity.Gs2Version.Model.EzVersion WarningVersion;
		[SerializeField]
		public Gs2.Unity.Gs2Version.Model.EzVersion ErrorVersion;
		[SerializeField]
		public string Scope;
		[SerializeField]
		public Gs2.Unity.Gs2Version.Model.EzVersion CurrentVersion;
		[SerializeField]
		public bool NeedSignature;

        public Gs2.Gs2Version.Model.VersionModel ToModel()
        {
            return new Gs2.Gs2Version.Model.VersionModel {
                Name = Name,
                Metadata = Metadata,
                WarningVersion = WarningVersion?.ToModel(),
                ErrorVersion = ErrorVersion?.ToModel(),
                Scope = Scope,
                CurrentVersion = CurrentVersion?.ToModel(),
                NeedSignature = NeedSignature,
            };
        }

        public static EzVersionModel FromModel(Gs2.Gs2Version.Model.VersionModel model)
        {
            return new EzVersionModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                WarningVersion = model.WarningVersion == null ? null : Gs2.Unity.Gs2Version.Model.EzVersion.FromModel(model.WarningVersion),
                ErrorVersion = model.ErrorVersion == null ? null : Gs2.Unity.Gs2Version.Model.EzVersion.FromModel(model.ErrorVersion),
                Scope = model.Scope == null ? null : model.Scope,
                CurrentVersion = model.CurrentVersion == null ? null : Gs2.Unity.Gs2Version.Model.EzVersion.FromModel(model.CurrentVersion),
                NeedSignature = model.NeedSignature ?? false,
            };
        }
    }
}