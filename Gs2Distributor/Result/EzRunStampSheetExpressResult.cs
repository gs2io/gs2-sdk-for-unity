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

using Gs2.Gs2Distributor.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Distributor.Result
{
	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzRunStampSheetExpressResult
	{
		[SerializeField]
		public List<int> VerifyTaskResultCodes;
		[SerializeField]
		public List<string> VerifyTaskResults;
		[SerializeField]
		public List<int> TaskResultCodes;
		[SerializeField]
		public List<string> TaskResults;
		[SerializeField]
		public int SheetResultCode;
		[SerializeField]
		public string SheetResult;

        public static EzRunStampSheetExpressResult FromModel(Gs2.Gs2Distributor.Result.RunStampSheetExpressResult model)
        {
            return new EzRunStampSheetExpressResult {
                VerifyTaskResultCodes = model.VerifyTaskResultCodes == null ? new List<int>() : model.VerifyTaskResultCodes.Select(v => {
                    return v;
                }).ToList(),
                VerifyTaskResults = model.VerifyTaskResults == null ? new List<string>() : model.VerifyTaskResults.Select(v => {
                    return v;
                }).ToList(),
                TaskResultCodes = model.TaskResultCodes == null ? new List<int>() : model.TaskResultCodes.Select(v => {
                    return v;
                }).ToList(),
                TaskResults = model.TaskResults == null ? new List<string>() : model.TaskResults.Select(v => {
                    return v;
                }).ToList(),
                SheetResultCode = model.SheetResultCode ?? 0,
                SheetResult = model.SheetResult == null ? null : model.SheetResult,
            };
        }
    }
}