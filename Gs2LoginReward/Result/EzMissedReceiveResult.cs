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

using Gs2.Gs2LoginReward.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2LoginReward.Result
{
	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzMissedReceiveResult
	{
		[SerializeField]
		public Gs2.Unity.Gs2LoginReward.Model.EzReceiveStatus Item;
		[SerializeField]
		public Gs2.Unity.Gs2LoginReward.Model.EzBonusModel BonusModel;
		[SerializeField]
		public string TransactionId;
		[SerializeField]
		public string StampSheet;
		[SerializeField]
		public string StampSheetEncryptionKeyId;
		[SerializeField]
		public bool AutoRunStampSheet;
		[SerializeField]
		public bool AtomicCommit;
		[SerializeField]
		public string Transaction;
		[SerializeField]
		public Gs2.Unity.Core.Model.EzTransactionResult TransactionResult;

        public static EzMissedReceiveResult FromModel(Gs2.Gs2LoginReward.Result.MissedReceiveResult model)
        {
            return new EzMissedReceiveResult {
                Item = model.Item == null ? null : Gs2.Unity.Gs2LoginReward.Model.EzReceiveStatus.FromModel(model.Item),
                BonusModel = model.BonusModel == null ? null : Gs2.Unity.Gs2LoginReward.Model.EzBonusModel.FromModel(model.BonusModel),
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