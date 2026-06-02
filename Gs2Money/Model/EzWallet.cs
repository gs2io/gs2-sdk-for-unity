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
using Gs2.Gs2Money.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Money.Model
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
		public int Paid;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int Free;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public bool ShareFree;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public long UpdatedAt;

        public Gs2.Gs2Money.Model.Wallet ToModel()
        {
            return new Gs2.Gs2Money.Model.Wallet {
                Slot = Slot,
                Paid = Paid,
                Free = Free,
                ShareFree = ShareFree,
                UpdatedAt = UpdatedAt,
            };
        }

        public static EzWallet FromModel(Gs2.Gs2Money.Model.Wallet model)
        {
            return new EzWallet {
                Slot = model.Slot ?? 0,
                Paid = model.Paid ?? 0,
                Free = model.Free ?? 0,
                ShareFree = model.ShareFree ?? false,
                UpdatedAt = model.UpdatedAt ?? 0,
            };
        }
    }
}