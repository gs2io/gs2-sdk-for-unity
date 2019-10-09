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

namespace Gs2.Gs2Quest.Model
{
	[Preserve]
	public class Contents
	{

        /** クエストモデルのメタデータ */
        public string metadata { set; get; }

        /**
         * クエストモデルのメタデータを設定
         *
         * @param metadata クエストモデルのメタデータ
         * @return this
         */
        public Contents WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** クエストクリア時の報酬 */
        public List<AcquireAction> completeAcquireActions { set; get; }

        /**
         * クエストクリア時の報酬を設定
         *
         * @param completeAcquireActions クエストクリア時の報酬
         * @return this
         */
        public Contents WithCompleteAcquireActions(List<AcquireAction> completeAcquireActions) {
            this.completeAcquireActions = completeAcquireActions;
            return this;
        }

        /** 抽選する重み */
        public int? weight { set; get; }

        /**
         * 抽選する重みを設定
         *
         * @param weight 抽選する重み
         * @return this
         */
        public Contents WithWeight(int? weight) {
            this.weight = weight;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.metadata);
            }
            if(this.completeAcquireActions != null)
            {
                writer.WritePropertyName("completeAcquireActions");
                writer.WriteArrayStart();
                foreach(var item in this.completeAcquireActions)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.weight.HasValue)
            {
                writer.WritePropertyName("weight");
                writer.Write(this.weight.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Contents FromDict(JsonData data)
        {
            return new Contents()
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithCompleteAcquireActions(data.Keys.Contains("completeAcquireActions") && data["completeAcquireActions"] != null ? data["completeAcquireActions"].Cast<JsonData>().Select(value =>
                    {
                        return AcquireAction.FromDict(value);
                    }
                ).ToList() : null)
                .WithWeight(data.Keys.Contains("weight") && data["weight"] != null ? (int?)int.Parse(data["weight"].ToString()) : null);
        }
	}
}