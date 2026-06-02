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
using Gs2.Gs2Mission.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Mission.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzAcquireActionResult
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Action;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string AcquireRequest;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int StatusCode;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string AcquireResult;

        public Gs2.Core.Model.AcquireActionResult ToModel()
        {
            return new Gs2.Core.Model.AcquireActionResult {
                Action = Action,
                AcquireRequest = AcquireRequest,
                StatusCode = StatusCode,
                AcquireResult = AcquireResult,
            };
        }

        public static EzAcquireActionResult FromModel(Gs2.Core.Model.AcquireActionResult model)
        {
            return new EzAcquireActionResult {
                Action = model.Action == null ? null : model.Action,
                AcquireRequest = model.AcquireRequest == null ? null : model.AcquireRequest,
                StatusCode = model.StatusCode ?? 0,
                AcquireResult = model.AcquireResult == null ? null : model.AcquireResult,
            };
        }
    }
}