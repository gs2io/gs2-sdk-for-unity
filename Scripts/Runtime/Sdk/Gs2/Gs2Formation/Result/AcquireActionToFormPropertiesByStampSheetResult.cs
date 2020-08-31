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
using Gs2.Gs2Formation.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Formation.Result
{
	[Preserve]
	public class AcquireActionToFormPropertiesByStampSheetResult
	{
        /** フォーム */
        public Form item { set; get; }

        /** 保存したフォーム */
        public Mold mold { set; get; }

        /** スタンプシート */
        public string stampSheet { set; get; }

        /** スタンプシートの署名計算に使用した暗号鍵GRN */
        public string stampSheetEncryptionKeyId { set; get; }


    	[Preserve]
        public static AcquireActionToFormPropertiesByStampSheetResult FromDict(JsonData data)
        {
            return new AcquireActionToFormPropertiesByStampSheetResult {
                item = data.Keys.Contains("item") && data["item"] != null ? Gs2.Gs2Formation.Model.Form.FromDict(data["item"]) : null,
                mold = data.Keys.Contains("mold") && data["mold"] != null ? Gs2.Gs2Formation.Model.Mold.FromDict(data["mold"]) : null,
                stampSheet = data.Keys.Contains("stampSheet") && data["stampSheet"] != null ? data["stampSheet"].ToString() : null,
                stampSheetEncryptionKeyId = data.Keys.Contains("stampSheetEncryptionKeyId") && data["stampSheetEncryptionKeyId"] != null ? data["stampSheetEncryptionKeyId"].ToString() : null,
            };
        }
	}
}