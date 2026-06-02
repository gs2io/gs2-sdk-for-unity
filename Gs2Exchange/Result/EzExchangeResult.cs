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

using Gs2.Gs2Exchange.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Exchange.Result
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzExchangeResult
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public Gs2.Unity.Gs2Exchange.Model.EzRateModel Item;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string TransactionId;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string StampSheet;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string StampSheetEncryptionKeyId;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public bool AutoRunStampSheet;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public bool AtomicCommit;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Transaction;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public Gs2.Unity.Core.Model.EzTransactionResult TransactionResult;

        public static EzExchangeResult FromModel(Gs2.Gs2Exchange.Result.ExchangeResult model)
        {
            return new EzExchangeResult {
                Item = model.Item == null ? null : Gs2.Unity.Gs2Exchange.Model.EzRateModel.FromModel(model.Item),
                TransactionId = model.TransactionId == null ? null : model.TransactionId,
                StampSheet = model.StampSheet == null ? null : model.StampSheet,
                StampSheetEncryptionKeyId = model.StampSheetEncryptionKeyId == null ? null : model.StampSheetEncryptionKeyId,
                AutoRunStampSheet = model.AutoRunStampSheet ?? false,
                AtomicCommit = model.AtomicCommit ?? false,
                Transaction = model.Transaction == null ? null : model.Transaction,
                TransactionResult = model.TransactionResult == null ? null : Gs2.Unity.Core.Model.EzTransactionResult.FromModel(model.TransactionResult),
            };
        }
    }
}