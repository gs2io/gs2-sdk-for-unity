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
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2JobQueue.Model
{
	[Preserve]
	public class JobResultBody
	{

        /** 試行回数 */
        public int? tryNumber { set; get; }

        /**
         * 試行回数を設定
         *
         * @param tryNumber 試行回数
         * @return this
         */
        public JobResultBody WithTryNumber(int? tryNumber) {
            this.tryNumber = tryNumber;
            return this;
        }

        /** ステータスコード */
        public int? statusCode { set; get; }

        /**
         * ステータスコードを設定
         *
         * @param statusCode ステータスコード
         * @return this
         */
        public JobResultBody WithStatusCode(int? statusCode) {
            this.statusCode = statusCode;
            return this;
        }

        /** レスポンスの内容 */
        public string result { set; get; }

        /**
         * レスポンスの内容を設定
         *
         * @param result レスポンスの内容
         * @return this
         */
        public JobResultBody WithResult(string result) {
            this.result = result;
            return this;
        }

        /** 実行日時 */
        public long? tryAt { set; get; }

        /**
         * 実行日時を設定
         *
         * @param tryAt 実行日時
         * @return this
         */
        public JobResultBody WithTryAt(long? tryAt) {
            this.tryAt = tryAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.tryNumber.HasValue)
            {
                writer.WritePropertyName("tryNumber");
                writer.Write(this.tryNumber.Value);
            }
            if(this.statusCode.HasValue)
            {
                writer.WritePropertyName("statusCode");
                writer.Write(this.statusCode.Value);
            }
            if(this.result != null)
            {
                writer.WritePropertyName("result");
                writer.Write(this.result);
            }
            if(this.tryAt.HasValue)
            {
                writer.WritePropertyName("tryAt");
                writer.Write(this.tryAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static JobResultBody FromDict(JsonData data)
        {
            return new JobResultBody()
                .WithTryNumber(data.Keys.Contains("tryNumber") && data["tryNumber"] != null ? (int?)int.Parse(data["tryNumber"].ToString()) : null)
                .WithStatusCode(data.Keys.Contains("statusCode") && data["statusCode"] != null ? (int?)int.Parse(data["statusCode"].ToString()) : null)
                .WithResult(data.Keys.Contains("result") && data["result"] != null ? data["result"].ToString() : null)
                .WithTryAt(data.Keys.Contains("tryAt") && data["tryAt"] != null ? (long?)long.Parse(data["tryAt"].ToString()) : null);
        }
	}
}