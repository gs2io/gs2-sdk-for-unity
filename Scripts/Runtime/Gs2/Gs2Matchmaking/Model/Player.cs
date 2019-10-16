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

namespace Gs2.Gs2Matchmaking.Model
{
	[Preserve]
	public class Player
	{

        /** ユーザーID */
        public string userId { set; get; }

        /**
         * ユーザーIDを設定
         *
         * @param userId ユーザーID
         * @return this
         */
        public Player WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** 属性値のリスト */
        public List<Attribute_> attributes { set; get; }

        /**
         * 属性値のリストを設定
         *
         * @param attributes 属性値のリスト
         * @return this
         */
        public Player WithAttributes(List<Attribute_> attributes) {
            this.attributes = attributes;
            return this;
        }

        /** ロール名 */
        public string roleName { set; get; }

        /**
         * ロール名を設定
         *
         * @param roleName ロール名
         * @return this
         */
        public Player WithRoleName(string roleName) {
            this.roleName = roleName;
            return this;
        }

        /** 参加を拒否するユーザIDリスト */
        public List<string> denyUserIds { set; get; }

        /**
         * 参加を拒否するユーザIDリストを設定
         *
         * @param denyUserIds 参加を拒否するユーザIDリスト
         * @return this
         */
        public Player WithDenyUserIds(List<string> denyUserIds) {
            this.denyUserIds = denyUserIds;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.attributes != null)
            {
                writer.WritePropertyName("attributes");
                writer.WriteArrayStart();
                foreach(var item in this.attributes)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.roleName != null)
            {
                writer.WritePropertyName("roleName");
                writer.Write(this.roleName);
            }
            if(this.denyUserIds != null)
            {
                writer.WritePropertyName("denyUserIds");
                writer.WriteArrayStart();
                foreach(var item in this.denyUserIds)
                {
                    writer.Write(item);
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Player FromDict(JsonData data)
        {
            return new Player()
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithAttributes(data.Keys.Contains("attributes") && data["attributes"] != null ? data["attributes"].Cast<JsonData>().Select(value =>
                    {
                        return Attribute_.FromDict(value);
                    }
                ).ToList() : null)
                .WithRoleName(data.Keys.Contains("roleName") && data["roleName"] != null ? data["roleName"].ToString() : null)
                .WithDenyUserIds(data.Keys.Contains("denyUserIds") && data["denyUserIds"] != null ? data["denyUserIds"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null);
        }
	}
}