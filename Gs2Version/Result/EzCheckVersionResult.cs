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

using Gs2.Gs2Version.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Version.Result
{
	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzCheckVersionResult
	{
		[SerializeField]
		public string ProjectToken;
		[SerializeField]
		public List<Gs2.Unity.Gs2Version.Model.EzStatus> Warnings;
		[SerializeField]
		public List<Gs2.Unity.Gs2Version.Model.EzStatus> Errors;

        public static EzCheckVersionResult FromModel(Gs2.Gs2Version.Result.CheckVersionResult model)
        {
            return new EzCheckVersionResult {
                ProjectToken = model.ProjectToken == null ? null : model.ProjectToken,
                Warnings = model.Warnings == null ? new List<Gs2.Unity.Gs2Version.Model.EzStatus>() : model.Warnings.Select(v => {
                    return Gs2.Unity.Gs2Version.Model.EzStatus.FromModel(v);
                }).ToList(),
                Errors = model.Errors == null ? new List<Gs2.Unity.Gs2Version.Model.EzStatus>() : model.Errors.Select(v => {
                    return Gs2.Unity.Gs2Version.Model.EzStatus.FromModel(v);
                }).ToList(),
            };
        }
    }
}