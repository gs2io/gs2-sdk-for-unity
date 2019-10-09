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
	public class BoxItems
	{

        /** ボックス */
        public string boxId { set; get; }

        /**
         * ボックスを設定
         *
         * @param boxId ボックス
         * @return this
         */
        public BoxItems WithBoxId(string boxId) {
            this.boxId = boxId;
            return this;
        }

        /** 排出確率テーブル名 */
        public string prizeTableName { set; get; }

        /**
         * 排出確率テーブル名を設定
         *
         * @param prizeTableName 排出確率テーブル名
         * @return this
         */
        public BoxItems WithPrizeTableName(string prizeTableName) {
            this.prizeTableName = prizeTableName;
            return this;
        }

        /** ユーザーID */
        public string userId { set; get; }

        /**
         * ユーザーIDを設定
         *
         * @param userId ユーザーID
         * @return this
         */
        public BoxItems WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** ボックスから取り出したアイテムのリスト */
        public List<BoxItem> items { set; get; }

        /**
         * ボックスから取り出したアイテムのリストを設定
         *
         * @param items ボックスから取り出したアイテムのリスト
         * @return this
         */
        public BoxItems WithItems(List<BoxItem> items) {
            this.items = items;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.boxId != null)
            {
                writer.WritePropertyName("boxId");
                writer.Write(this.boxId);
            }
            if(this.prizeTableName != null)
            {
                writer.WritePropertyName("prizeTableName");
                writer.Write(this.prizeTableName);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.items != null)
            {
                writer.WritePropertyName("items");
                writer.WriteArrayStart();
                foreach(var item in this.items)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static BoxItems FromDict(JsonData data)
        {
            return new BoxItems()
                .WithBoxId(data.Keys.Contains("boxId") && data["boxId"] != null ? data["boxId"].ToString() : null)
                .WithPrizeTableName(data.Keys.Contains("prizeTableName") && data["prizeTableName"] != null ? data["prizeTableName"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithItems(data.Keys.Contains("items") && data["items"] != null ? data["items"].Cast<JsonData>().Select(value =>
                    {
                        return BoxItem.FromDict(value);
                    }
                ).ToList() : null);
        }
	}
}