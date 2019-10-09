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
	public class CapacityOfRole
	{

        /** ロール名 */
        public string roleName { set; get; }

        /**
         * ロール名を設定
         *
         * @param roleName ロール名
         * @return this
         */
        public CapacityOfRole WithRoleName(string roleName) {
            this.roleName = roleName;
            return this;
        }

        /** ロール名の別名リスト */
        public List<string> roleAliases { set; get; }

        /**
         * ロール名の別名リストを設定
         *
         * @param roleAliases ロール名の別名リスト
         * @return this
         */
        public CapacityOfRole WithRoleAliases(List<string> roleAliases) {
            this.roleAliases = roleAliases;
            return this;
        }

        /** 募集人数 */
        public int? capacity { set; get; }

        /**
         * 募集人数を設定
         *
         * @param capacity 募集人数
         * @return this
         */
        public CapacityOfRole WithCapacity(int? capacity) {
            this.capacity = capacity;
            return this;
        }

        /** 参加者のプレイヤー情報リスト */
        public List<Player> participants { set; get; }

        /**
         * 参加者のプレイヤー情報リストを設定
         *
         * @param participants 参加者のプレイヤー情報リスト
         * @return this
         */
        public CapacityOfRole WithParticipants(List<Player> participants) {
            this.participants = participants;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.roleName != null)
            {
                writer.WritePropertyName("roleName");
                writer.Write(this.roleName);
            }
            if(this.roleAliases != null)
            {
                writer.WritePropertyName("roleAliases");
                writer.WriteArrayStart();
                foreach(var item in this.roleAliases)
                {
                    writer.Write(item);
                }
                writer.WriteArrayEnd();
            }
            if(this.capacity.HasValue)
            {
                writer.WritePropertyName("capacity");
                writer.Write(this.capacity.Value);
            }
            if(this.participants != null)
            {
                writer.WritePropertyName("participants");
                writer.WriteArrayStart();
                foreach(var item in this.participants)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static CapacityOfRole FromDict(JsonData data)
        {
            return new CapacityOfRole()
                .WithRoleName(data.Keys.Contains("roleName") && data["roleName"] != null ? data["roleName"].ToString() : null)
                .WithRoleAliases(data.Keys.Contains("roleAliases") && data["roleAliases"] != null ? data["roleAliases"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null)
                .WithCapacity(data.Keys.Contains("capacity") && data["capacity"] != null ? (int?)int.Parse(data["capacity"].ToString()) : null)
                .WithParticipants(data.Keys.Contains("participants") && data["participants"] != null ? data["participants"].Cast<JsonData>().Select(value =>
                    {
                        return Player.FromDict(value);
                    }
                ).ToList() : null);
        }
	}
}