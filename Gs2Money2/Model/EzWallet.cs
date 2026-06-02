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
using Gs2.Gs2Money2.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Money2.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzWallet
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int Slot;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public Gs2.Unity.Gs2Money2.Model.EzWalletSummary Summary;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public bool SharedFreeCurrency;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public long UpdatedAt;

        public Gs2.Gs2Money2.Model.Wallet ToModel()
        {
            return new Gs2.Gs2Money2.Model.Wallet {
                Slot = Slot,
                Summary = Summary?.ToModel(),
                SharedFreeCurrency = SharedFreeCurrency,
                UpdatedAt = UpdatedAt,
            };
        }

        public static EzWallet FromModel(Gs2.Gs2Money2.Model.Wallet model)
        {
            return new EzWallet {
                Slot = model.Slot ?? 0,
                Summary = model.Summary == null ? null : Gs2.Unity.Gs2Money2.Model.EzWalletSummary.FromModel(model.Summary),
                SharedFreeCurrency = model.SharedFreeCurrency ?? false,
                UpdatedAt = model.UpdatedAt ?? 0,
            };
        }
    }
}