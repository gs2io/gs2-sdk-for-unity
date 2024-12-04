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
using Gs2.Gs2Grade.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Grade.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzConsumeActionResult
	{
		[SerializeField]
		public string Action;
		[SerializeField]
		public string ConsumeRequest;
		[SerializeField]
		public int StatusCode;
		[SerializeField]
		public string ConsumeResult;

        public Gs2.Core.Model.ConsumeActionResult ToModel()
        {
            return new Gs2.Core.Model.ConsumeActionResult {
                Action = Action,
                ConsumeRequest = ConsumeRequest,
                StatusCode = StatusCode,
                ConsumeResult = ConsumeResult,
            };
        }

        public static EzConsumeActionResult FromModel(Gs2.Core.Model.ConsumeActionResult model)
        {
            return new EzConsumeActionResult {
                Action = model.Action == null ? null : model.Action,
                ConsumeRequest = model.ConsumeRequest == null ? null : model.ConsumeRequest,
                StatusCode = model.StatusCode ?? 0,
                ConsumeResult = model.ConsumeResult == null ? null : model.ConsumeResult,
            };
        }
    }
}