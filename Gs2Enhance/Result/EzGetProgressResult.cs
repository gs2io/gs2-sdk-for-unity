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
using Gs2.Core.Model;
using Gs2.Unity.Gs2Enhance.Model;
using Gs2.Gs2Enhance.Result;
using UnityEngine.Scripting;

namespace Gs2.Unity.Gs2Enhance.Result
{
	[Preserve]
	public class EzGetProgressResult
	{
        /** 強化実行 */
        public EzProgress Item { get; private set; }

        /** 強化レートモデル */
        public EzRateModel RateModel { get; private set; }


        public EzGetProgressResult(
            GetProgressResult result
        )
        {
            if(result.item != null)
            {
                Item = new EzProgress(result.item);
            }
            if(result.rateModel != null)
            {
                RateModel = new EzRateModel(result.rateModel);
            }
        }
	}
}