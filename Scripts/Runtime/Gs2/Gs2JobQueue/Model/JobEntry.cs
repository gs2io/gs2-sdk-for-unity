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
using LitJson;

namespace Gs2.Gs2JobQueue.Model
{
	public class JobEntry
	{

        /** スクリプト のGRN */
        public string scriptId { set; get; }

        /**
         * スクリプト のGRNを設定
         *
         * @param scriptId スクリプト のGRN
         * @return this
         */
        public JobEntry WithScriptId(string scriptId) {
            this.scriptId = scriptId;
            return this;
        }

        /** 引数 */
        public string args { set; get; }

        /**
         * 引数を設定
         *
         * @param args 引数
         * @return this
         */
        public JobEntry WithArgs(string args) {
            this.args = args;
            return this;
        }

        /** 最大試行回数 */
        public int? maxTryCount { set; get; }

        /**
         * 最大試行回数を設定
         *
         * @param maxTryCount 最大試行回数
         * @return this
         */
        public JobEntry WithMaxTryCount(int? maxTryCount) {
            this.maxTryCount = maxTryCount;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.scriptId != null)
            {
                writer.WritePropertyName("scriptId");
                writer.Write(this.scriptId);
            }
            if(this.args != null)
            {
                writer.WritePropertyName("args");
                writer.Write(this.args);
            }
            if(this.maxTryCount.HasValue)
            {
                writer.WritePropertyName("maxTryCount");
                writer.Write(this.maxTryCount.Value);
            }
            writer.WriteObjectEnd();
        }
	}
}