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
	public class Reward
	{

        /** スタンプシートで実行するアクションの種類 */
        public string action { set; get; }

        /**
         * スタンプシートで実行するアクションの種類を設定
         *
         * @param action スタンプシートで実行するアクションの種類
         * @return this
         */
        public Reward WithAction(string action) {
            this.action = action;
            return this;
        }

        /** リクエストモデル */
        public string request { set; get; }

        /**
         * リクエストモデルを設定
         *
         * @param request リクエストモデル
         * @return this
         */
        public Reward WithRequest(string request) {
            this.request = request;
            return this;
        }

        /** 入手するリソースGRN */
        public string itemId { set; get; }

        /**
         * 入手するリソースGRNを設定
         *
         * @param itemId 入手するリソースGRN
         * @return this
         */
        public Reward WithItemId(string itemId) {
            this.itemId = itemId;
            return this;
        }

        /** 入手する数量 */
        public int? value { set; get; }

        /**
         * 入手する数量を設定
         *
         * @param value 入手する数量
         * @return this
         */
        public Reward WithValue(int? value) {
            this.value = value;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.action != null)
            {
                writer.WritePropertyName("action");
                writer.Write(this.action);
            }
            if(this.request != null)
            {
                writer.WritePropertyName("request");
                writer.Write(this.request);
            }
            if(this.itemId != null)
            {
                writer.WritePropertyName("itemId");
                writer.Write(this.itemId);
            }
            if(this.value.HasValue)
            {
                writer.WritePropertyName("value");
                writer.Write(this.value.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Reward FromDict(JsonData data)
        {
            return new Reward()
                .WithAction(data.Keys.Contains("action") && data["action"] != null ? data["action"].ToString() : null)
                .WithRequest(data.Keys.Contains("request") && data["request"] != null ? data["request"].ToString() : null)
                .WithItemId(data.Keys.Contains("itemId") && data["itemId"] != null ? data["itemId"].ToString() : null)
                .WithValue(data.Keys.Contains("value") && data["value"] != null ? (int?)int.Parse(data["value"].ToString()) : null);
        }
	}
}