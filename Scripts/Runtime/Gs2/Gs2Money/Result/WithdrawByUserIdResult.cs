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
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Model;
using Gs2.Gs2Money.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Money.Result
{
	[Preserve]
	public class WithdrawByUserIdResult
	{
        /** 消費後のウォレット */
        public Wallet item { set; get; }

        /** 消費した通貨の価格 */
        public float? price { set; get; }


    	[Preserve]
        public static WithdrawByUserIdResult FromDict(JsonData data)
        {
            return new WithdrawByUserIdResult {
                item = data.Keys.Contains("item") && data["item"] != null ? Wallet.FromDict(data["item"]) : null,
                price = data.Keys.Contains("price") && data["price"] != null ? (float?)float.Parse(data["price"].ToString()) : null,
            };
        }
	}
}