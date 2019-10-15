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
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Gs2Formation.Result;
using UnityEngine.Scripting;

namespace Gs2.Unity.Gs2Formation.Result
{
	[Preserve]
	public class EzGetMoldResult
	{
        /** 保存したフォーム */
        public EzMold Item { get; private set; }

        /** フォームの保存領域 */
        public EzMoldModel MoldModel { get; private set; }


        public EzGetMoldResult(
            GetMoldResult result
        )
        {
            if(result.item != null)
            {
                Item = new EzMold(result.item);
            }
            if(result.moldModel != null)
            {
                MoldModel = new EzMoldModel(result.moldModel);
            }
        }
	}
}