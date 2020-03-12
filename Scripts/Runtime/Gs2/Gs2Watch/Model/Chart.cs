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

namespace Gs2.Gs2Watch.Model
{
	[Preserve]
	public class Chart
	{

        /** Datadog のJSON 形式のグラフ定義 */
        public string chartId { set; get; }

        /**
         * Datadog のJSON 形式のグラフ定義を設定
         *
         * @param chartId Datadog のJSON 形式のグラフ定義
         * @return this
         */
        public Chart WithChartId(string chartId) {
            this.chartId = chartId;
            return this;
        }

        /** オーナーID */
        public string ownerId { set; get; }

        /**
         * オーナーIDを設定
         *
         * @param ownerId オーナーID
         * @return this
         */
        public Chart WithOwnerId(string ownerId) {
            this.ownerId = ownerId;
            return this;
        }

        /** Datadog から払い出された組み込みID */
        public string embedId { set; get; }

        /**
         * Datadog から払い出された組み込みIDを設定
         *
         * @param embedId Datadog から払い出された組み込みID
         * @return this
         */
        public Chart WithEmbedId(string embedId) {
            this.embedId = embedId;
            return this;
        }

        /** Datadog から払い出された組み込み用HTML */
        public string html { set; get; }

        /**
         * Datadog から払い出された組み込み用HTMLを設定
         *
         * @param html Datadog から払い出された組み込み用HTML
         * @return this
         */
        public Chart WithHtml(string html) {
            this.html = html;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.chartId != null)
            {
                writer.WritePropertyName("chartId");
                writer.Write(this.chartId);
            }
            if(this.ownerId != null)
            {
                writer.WritePropertyName("ownerId");
                writer.Write(this.ownerId);
            }
            if(this.embedId != null)
            {
                writer.WritePropertyName("embedId");
                writer.Write(this.embedId);
            }
            if(this.html != null)
            {
                writer.WritePropertyName("html");
                writer.Write(this.html);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Chart FromDict(JsonData data)
        {
            return new Chart()
                .WithChartId(data.Keys.Contains("chartId") && data["chartId"] != null ? data["chartId"].ToString() : null)
                .WithOwnerId(data.Keys.Contains("ownerId") && data["ownerId"] != null ? data["ownerId"].ToString() : null)
                .WithEmbedId(data.Keys.Contains("embedId") && data["embedId"] != null ? data["embedId"].ToString() : null)
                .WithHtml(data.Keys.Contains("html") && data["html"] != null ? data["html"].ToString() : null);
        }
	}
}