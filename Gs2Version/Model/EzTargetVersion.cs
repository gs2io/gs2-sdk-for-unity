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
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Version.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzTargetVersion
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string VersionName;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public Gs2.Unity.Gs2Version.Model.EzVersion Version;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Body;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Signature;

        public Gs2.Gs2Version.Model.TargetVersion ToModel()
        {
            return new Gs2.Gs2Version.Model.TargetVersion {
                VersionName = VersionName,
                Version = Version?.ToModel(),
                Body = Body,
                Signature = Signature,
            };
        }

        public static EzTargetVersion FromModel(Gs2.Gs2Version.Model.TargetVersion model)
        {
            return new EzTargetVersion {
                VersionName = model.VersionName == null ? null : model.VersionName,
                Version = model.Version == null ? null : Gs2.Unity.Gs2Version.Model.EzVersion.FromModel(model.Version),
                Body = model.Body == null ? null : model.Body,
                Signature = model.Signature == null ? null : model.Signature,
            };
        }
    }
}