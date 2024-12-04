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
using Gs2.Gs2Enchant.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Enchant.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzTransactionResult
	{
		[SerializeField]
		public string TransactionId;
		[SerializeField]
		public List<Gs2.Unity.Core.Model.EzVerifyActionResult> VerifyResults;
		[SerializeField]
		public List<Gs2.Unity.Core.Model.EzConsumeActionResult> ConsumeResults;
		[SerializeField]
		public List<Gs2.Unity.Core.Model.EzAcquireActionResult> AcquireResults;

        public Gs2.Core.Model.TransactionResult ToModel()
        {
            return new Gs2.Core.Model.TransactionResult {
                TransactionId = TransactionId,
                VerifyResults = VerifyResults?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                ConsumeResults = ConsumeResults?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                AcquireResults = AcquireResults?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
            };
        }

        public static EzTransactionResult FromModel(Gs2.Core.Model.TransactionResult model)
        {
            return new EzTransactionResult {
                TransactionId = model.TransactionId == null ? null : model.TransactionId,
                VerifyResults = model.VerifyResults == null ? new List<Gs2.Unity.Core.Model.EzVerifyActionResult>() : model.VerifyResults.Select(v => {
                    return Gs2.Unity.Core.Model.EzVerifyActionResult.FromModel(v);
                }).ToList(),
                ConsumeResults = model.ConsumeResults == null ? new List<Gs2.Unity.Core.Model.EzConsumeActionResult>() : model.ConsumeResults.Select(v => {
                    return Gs2.Unity.Core.Model.EzConsumeActionResult.FromModel(v);
                }).ToList(),
                AcquireResults = model.AcquireResults == null ? new List<Gs2.Unity.Core.Model.EzAcquireActionResult>() : model.AcquireResults.Select(v => {
                    return Gs2.Unity.Core.Model.EzAcquireActionResult.FromModel(v);
                }).ToList(),
            };
        }
    }
}