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
	public class BoxItem
	{

        /** 入手アクションのリスト */
        public List<AcquireAction> acquireActions { set; get; }

        /**
         * 入手アクションのリストを設定
         *
         * @param acquireActions 入手アクションのリスト
         * @return this
         */
        public BoxItem WithAcquireActions(List<AcquireAction> acquireActions) {
            this.acquireActions = acquireActions;
            return this;
        }

        /** 残り数量 */
        public int? remaining { set; get; }

        /**
         * 残り数量を設定
         *
         * @param remaining 残り数量
         * @return this
         */
        public BoxItem WithRemaining(int? remaining) {
            this.remaining = remaining;
            return this;
        }

        /** 初期数量 */
        public int? initial { set; get; }

        /**
         * 初期数量を設定
         *
         * @param initial 初期数量
         * @return this
         */
        public BoxItem WithInitial(int? initial) {
            this.initial = initial;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
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
            if(this.remaining.HasValue)
            {
                writer.WritePropertyName("remaining");
                writer.Write(this.remaining.Value);
            }
            if(this.initial.HasValue)
            {
                writer.WritePropertyName("initial");
                writer.Write(this.initial.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static BoxItem FromDict(JsonData data)
        {
            return new BoxItem()
                .WithAcquireActions(data.Keys.Contains("acquireActions") && data["acquireActions"] != null ? data["acquireActions"].Cast<JsonData>().Select(value =>
                    {
                        return AcquireAction.FromDict(value);
                    }
                ).ToList() : null)
                .WithRemaining(data.Keys.Contains("remaining") && data["remaining"] != null ? (int?)int.Parse(data["remaining"].ToString()) : null)
                .WithInitial(data.Keys.Contains("initial") && data["initial"] != null ? (int?)int.Parse(data["initial"].ToString()) : null);
        }
	}
}