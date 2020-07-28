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
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Distributor.Model
{
	[Preserve]
	public class LogSetting : IComparable
	{

        /** ログの記録に使用する GS2-Log のネームスペース のGRN */
        public string loggingNamespaceId { set; get; }

        /**
         * ログの記録に使用する GS2-Log のネームスペース のGRNを設定
         *
         * @param loggingNamespaceId ログの記録に使用する GS2-Log のネームスペース のGRN
         * @return this
         */
        public LogSetting WithLoggingNamespaceId(string loggingNamespaceId) {
            this.loggingNamespaceId = loggingNamespaceId;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.loggingNamespaceId != null)
            {
                writer.WritePropertyName("loggingNamespaceId");
                writer.Write(this.loggingNamespaceId);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static LogSetting FromDict(JsonData data)
        {
            return new LogSetting()
                .WithLoggingNamespaceId(data.Keys.Contains("loggingNamespaceId") && data["loggingNamespaceId"] != null ? data["loggingNamespaceId"].ToString() : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as LogSetting;
            var diff = 0;
            if (loggingNamespaceId == null && loggingNamespaceId == other.loggingNamespaceId)
            {
                // null and null
            }
            else
            {
                diff += loggingNamespaceId.CompareTo(other.loggingNamespaceId);
            }
            return diff;
        }
	}
}