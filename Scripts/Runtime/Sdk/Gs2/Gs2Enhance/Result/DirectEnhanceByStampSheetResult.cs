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
using Gs2.Gs2Enhance.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Enhance.Result
{
	[Preserve]
	public class DirectEnhanceByStampSheetResult
	{
        /** 強化レートモデル */
        public RateModel item { set; get; }

        /** 強化処理の実行に使用するスタンプシート */
        public string stampSheet { set; get; }

        /** スタンプシートの署名計算に使用した暗号鍵GRN */
        public string stampSheetEncryptionKeyId { set; get; }

        /** 獲得経験値量 */
        public long? acquireExperience { set; get; }

        /** 経験値ボーナスの倍率(1.0=ボーナスなし) */
        public float? bonusRate { set; get; }


    	[Preserve]
        public static DirectEnhanceByStampSheetResult FromDict(JsonData data)
        {
            return new DirectEnhanceByStampSheetResult {
                item = data.Keys.Contains("item") && data["item"] != null ? Gs2.Gs2Enhance.Model.RateModel.FromDict(data["item"]) : null,
                stampSheet = data.Keys.Contains("stampSheet") && data["stampSheet"] != null ? data["stampSheet"].ToString() : null,
                stampSheetEncryptionKeyId = data.Keys.Contains("stampSheetEncryptionKeyId") && data["stampSheetEncryptionKeyId"] != null ? data["stampSheetEncryptionKeyId"].ToString() : null,
                acquireExperience = data.Keys.Contains("acquireExperience") && data["acquireExperience"] != null ? (long?)long.Parse(data["acquireExperience"].ToString()) : null,
                bonusRate = data.Keys.Contains("bonusRate") && data["bonusRate"] != null ? (float?)float.Parse(data["bonusRate"].ToString()) : null,
            };
        }
	}
}