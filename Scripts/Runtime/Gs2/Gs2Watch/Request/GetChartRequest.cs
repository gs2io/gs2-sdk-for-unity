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
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Watch.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Watch.Request
{
	[Preserve]
	public class GetChartRequest : Gs2Request<GetChartRequest>
	{

        /** 指標 */
        public string metrics { set; get; }

        /**
         * 指標を設定
         *
         * @param metrics 指標
         * @return this
         */
        public GetChartRequest WithMetrics(string metrics) {
            this.metrics = metrics;
            return this;
        }


        /** リソースのGRN */
        public string grn { set; get; }

        /**
         * リソースのGRNを設定
         *
         * @param grn リソースのGRN
         * @return this
         */
        public GetChartRequest WithGrn(string grn) {
            this.grn = grn;
            return this;
        }


        /** クエリリスト */
        public List<string> queries { set; get; }

        /**
         * クエリリストを設定
         *
         * @param queries クエリリスト
         * @return this
         */
        public GetChartRequest WithQueries(List<string> queries) {
            this.queries = queries;
            return this;
        }


        /** グルーピング対象 */
        public string by { set; get; }

        /**
         * グルーピング対象を設定
         *
         * @param by グルーピング対象
         * @return this
         */
        public GetChartRequest WithBy(string by) {
            this.by = by;
            return this;
        }


        /** データの取得期間 */
        public string timeframe { set; get; }

        /**
         * データの取得期間を設定
         *
         * @param timeframe データの取得期間
         * @return this
         */
        public GetChartRequest WithTimeframe(string timeframe) {
            this.timeframe = timeframe;
            return this;
        }


        /** グラフのサイズ */
        public string size { set; get; }

        /**
         * グラフのサイズを設定
         *
         * @param size グラフのサイズ
         * @return this
         */
        public GetChartRequest WithSize(string size) {
            this.size = size;
            return this;
        }


        /** フォーマット */
        public string format { set; get; }

        /**
         * フォーマットを設定
         *
         * @param format フォーマット
         * @return this
         */
        public GetChartRequest WithFormat(string format) {
            this.format = format;
            return this;
        }


        /** 集計方針 */
        public string aggregator { set; get; }

        /**
         * 集計方針を設定
         *
         * @param aggregator 集計方針
         * @return this
         */
        public GetChartRequest WithAggregator(string aggregator) {
            this.aggregator = aggregator;
            return this;
        }


        /** スタイル */
        public string style { set; get; }

        /**
         * スタイルを設定
         *
         * @param style スタイル
         * @return this
         */
        public GetChartRequest WithStyle(string style) {
            this.style = style;
            return this;
        }


        /** タイトル */
        public string title { set; get; }

        /**
         * タイトルを設定
         *
         * @param title タイトル
         * @return this
         */
        public GetChartRequest WithTitle(string title) {
            this.title = title;
            return this;
        }


    	[Preserve]
        public static GetChartRequest FromDict(JsonData data)
        {
            return new GetChartRequest {
                metrics = data.Keys.Contains("metrics") && data["metrics"] != null ? data["metrics"].ToString(): null,
                grn = data.Keys.Contains("grn") && data["grn"] != null ? data["grn"].ToString(): null,
                queries = data.Keys.Contains("queries") && data["queries"] != null ? data["queries"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null,
                by = data.Keys.Contains("by") && data["by"] != null ? data["by"].ToString(): null,
                timeframe = data.Keys.Contains("timeframe") && data["timeframe"] != null ? data["timeframe"].ToString(): null,
                size = data.Keys.Contains("size") && data["size"] != null ? data["size"].ToString(): null,
                format = data.Keys.Contains("format") && data["format"] != null ? data["format"].ToString(): null,
                aggregator = data.Keys.Contains("aggregator") && data["aggregator"] != null ? data["aggregator"].ToString(): null,
                style = data.Keys.Contains("style") && data["style"] != null ? data["style"].ToString(): null,
                title = data.Keys.Contains("title") && data["title"] != null ? data["title"].ToString(): null,
            };
        }

	}
}