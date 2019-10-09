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

namespace Gs2.Gs2Showcase.Model
{
	[Preserve]
	public class SalesItem
	{

        /** 商品名 */
        public string name { set; get; }

        /**
         * 商品名を設定
         *
         * @param name 商品名
         * @return this
         */
        public SalesItem WithName(string name) {
            this.name = name;
            return this;
        }

        /** 商品のメタデータ */
        public string metadata { set; get; }

        /**
         * 商品のメタデータを設定
         *
         * @param metadata 商品のメタデータ
         * @return this
         */
        public SalesItem WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** 消費アクションリスト */
        public List<ConsumeAction> consumeActions { set; get; }

        /**
         * 消費アクションリストを設定
         *
         * @param consumeActions 消費アクションリスト
         * @return this
         */
        public SalesItem WithConsumeActions(List<ConsumeAction> consumeActions) {
            this.consumeActions = consumeActions;
            return this;
        }

        /** 入手アクションリスト */
        public List<AcquireAction> acquireActions { set; get; }

        /**
         * 入手アクションリストを設定
         *
         * @param acquireActions 入手アクションリスト
         * @return this
         */
        public SalesItem WithAcquireActions(List<AcquireAction> acquireActions) {
            this.acquireActions = acquireActions;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.metadata);
            }
            if(this.consumeActions != null)
            {
                writer.WritePropertyName("consumeActions");
                writer.WriteArrayStart();
                foreach(var item in this.consumeActions)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
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
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static SalesItem FromDict(JsonData data)
        {
            return new SalesItem()
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithConsumeActions(data.Keys.Contains("consumeActions") && data["consumeActions"] != null ? data["consumeActions"].Cast<JsonData>().Select(value =>
                    {
                        return ConsumeAction.FromDict(value);
                    }
                ).ToList() : null)
                .WithAcquireActions(data.Keys.Contains("acquireActions") && data["acquireActions"] != null ? data["acquireActions"].Cast<JsonData>().Select(value =>
                    {
                        return AcquireAction.FromDict(value);
                    }
                ).ToList() : null);
        }
	}
}