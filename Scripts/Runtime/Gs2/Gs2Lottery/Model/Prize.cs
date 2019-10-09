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

namespace Gs2.Gs2Lottery.Model
{
	[Preserve]
	public class Prize
	{

        /** 景品ID */
        public string prizeId { set; get; }

        /**
         * 景品IDを設定
         *
         * @param prizeId 景品ID
         * @return this
         */
        public Prize WithPrizeId(string prizeId) {
            this.prizeId = prizeId;
            return this;
        }

        /** 景品の種類 */
        public string type { set; get; }

        /**
         * 景品の種類を設定
         *
         * @param type 景品の種類
         * @return this
         */
        public Prize WithType(string type) {
            this.type = type;
            return this;
        }

        /** 景品の入手アクションリスト */
        public List<AcquireAction> acquireActions { set; get; }

        /**
         * 景品の入手アクションリストを設定
         *
         * @param acquireActions 景品の入手アクションリスト
         * @return this
         */
        public Prize WithAcquireActions(List<AcquireAction> acquireActions) {
            this.acquireActions = acquireActions;
            return this;
        }

        /** 排出確率テーブルの名前 */
        public string prizeTableName { set; get; }

        /**
         * 排出確率テーブルの名前を設定
         *
         * @param prizeTableName 排出確率テーブルの名前
         * @return this
         */
        public Prize WithPrizeTableName(string prizeTableName) {
            this.prizeTableName = prizeTableName;
            return this;
        }

        /** 排出重み */
        public int? weight { set; get; }

        /**
         * 排出重みを設定
         *
         * @param weight 排出重み
         * @return this
         */
        public Prize WithWeight(int? weight) {
            this.weight = weight;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.prizeId != null)
            {
                writer.WritePropertyName("prizeId");
                writer.Write(this.prizeId);
            }
            if(this.type != null)
            {
                writer.WritePropertyName("type");
                writer.Write(this.type);
            }
            if(this.acquireActions != null)
            {
                writer.WritePropertyName("acquireActions");
                writer.WriteArrayStart();
                foreach(var item in this.acquireActions)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.prizeTableName != null)
            {
                writer.WritePropertyName("prizeTableName");
                writer.Write(this.prizeTableName);
            }
            if(this.weight.HasValue)
            {
                writer.WritePropertyName("weight");
                writer.Write(this.weight.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Prize FromDict(JsonData data)
        {
            return new Prize()
                .WithPrizeId(data.Keys.Contains("prizeId") && data["prizeId"] != null ? data["prizeId"].ToString() : null)
                .WithType(data.Keys.Contains("type") && data["type"] != null ? data["type"].ToString() : null)
                .WithAcquireActions(data.Keys.Contains("acquireActions") && data["acquireActions"] != null ? data["acquireActions"].Cast<JsonData>().Select(value =>
                    {
                        return AcquireAction.FromDict(value);
                    }
                ).ToList() : null)
                .WithPrizeTableName(data.Keys.Contains("prizeTableName") && data["prizeTableName"] != null ? data["prizeTableName"].ToString() : null)
                .WithWeight(data.Keys.Contains("weight") && data["weight"] != null ? (int?)int.Parse(data["weight"].ToString()) : null);
        }
	}
}