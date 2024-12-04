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
using Gs2.Gs2SkillTree.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2SkillTree.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzVerifyActionResult
	{
		[SerializeField]
		public string Action;
		[SerializeField]
		public string VerifyRequest;
		[SerializeField]
		public int StatusCode;
		[SerializeField]
		public string VerifyResult;

        public Gs2.Core.Model.VerifyActionResult ToModel()
        {
            return new Gs2.Core.Model.VerifyActionResult {
                Action = Action,
                VerifyRequest = VerifyRequest,
                StatusCode = StatusCode,
                VerifyResult = VerifyResult,
            };
        }

        public static EzVerifyActionResult FromModel(Gs2.Core.Model.VerifyActionResult model)
        {
            return new EzVerifyActionResult {
                Action = model.Action == null ? null : model.Action,
                VerifyRequest = model.VerifyRequest == null ? null : model.VerifyRequest,
                StatusCode = model.StatusCode ?? 0,
                VerifyResult = model.VerifyResult == null ? null : model.VerifyResult,
            };
        }
    }
}