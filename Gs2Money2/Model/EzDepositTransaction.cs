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
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Money2.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzDepositTransaction
	{
		[SerializeField]
		public float Price;
		[SerializeField]
		public string Currency;
		[SerializeField]
		public int Count;

        public Gs2.Gs2Money2.Model.DepositTransaction ToModel()
        {
            return new Gs2.Gs2Money2.Model.DepositTransaction {
                Price = Price,
                Currency = Currency,
                Count = Count,
            };
        }

        public static EzDepositTransaction FromModel(Gs2.Gs2Money2.Model.DepositTransaction model)
        {
            return new EzDepositTransaction {
                Price = model.Price ?? 0,
                Currency = model.Currency == null ? null : model.Currency,
                Count = model.Count ?? 0,
            };
        }
    }
}