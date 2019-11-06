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

namespace Gs2.Gs2Realtime.Model
{
	[Preserve]
	public class Room
	{

        /** ルーム */
        public string roomId { set; get; }

        /**
         * ルームを設定
         *
         * @param roomId ルーム
         * @return this
         */
        public Room WithRoomId(string roomId) {
            this.roomId = roomId;
            return this;
        }

        /** ルーム名 */
        public string name { set; get; }

        /**
         * ルーム名を設定
         *
         * @param name ルーム名
         * @return this
         */
        public Room WithName(string name) {
            this.name = name;
            return this;
        }

        /** IPアドレス */
        public string ipAddress { set; get; }

        /**
         * IPアドレスを設定
         *
         * @param ipAddress IPアドレス
         * @return this
         */
        public Room WithIpAddress(string ipAddress) {
            this.ipAddress = ipAddress;
            return this;
        }

        /** 待受ポート */
        public int? port { set; get; }

        /**
         * 待受ポートを設定
         *
         * @param port 待受ポート
         * @return this
         */
        public Room WithPort(int? port) {
            this.port = port;
            return this;
        }

        /** 暗号鍵 */
        public string encryptionKey { set; get; }

        /**
         * 暗号鍵を設定
         *
         * @param encryptionKey 暗号鍵
         * @return this
         */
        public Room WithEncryptionKey(string encryptionKey) {
            this.encryptionKey = encryptionKey;
            return this;
        }

        /** ルームの作成が終わったときに通知を受けるユーザIDリスト */
        public List<string> notificationUserIds { set; get; }

        /**
         * ルームの作成が終わったときに通知を受けるユーザIDリストを設定
         *
         * @param notificationUserIds ルームの作成が終わったときに通知を受けるユーザIDリスト
         * @return this
         */
        public Room WithNotificationUserIds(List<string> notificationUserIds) {
            this.notificationUserIds = notificationUserIds;
            return this;
        }

        /** 作成日時 */
        public long? createdAt { set; get; }

        /**
         * 作成日時を設定
         *
         * @param createdAt 作成日時
         * @return this
         */
        public Room WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        /** 最終更新日時 */
        public long? updatedAt { set; get; }

        /**
         * 最終更新日時を設定
         *
         * @param updatedAt 最終更新日時
         * @return this
         */
        public Room WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.roomId != null)
            {
                writer.WritePropertyName("roomId");
                writer.Write(this.roomId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.ipAddress != null)
            {
                writer.WritePropertyName("ipAddress");
                writer.Write(this.ipAddress);
            }
            if(this.port.HasValue)
            {
                writer.WritePropertyName("port");
                writer.Write(this.port.Value);
            }
            if(this.encryptionKey != null)
            {
                writer.WritePropertyName("encryptionKey");
                writer.Write(this.encryptionKey);
            }
            if(this.notificationUserIds != null)
            {
                writer.WritePropertyName("notificationUserIds");
                writer.WriteArrayStart();
                foreach(var item in this.notificationUserIds)
                {
                    writer.Write(item);
                }
                writer.WriteArrayEnd();
            }
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            if(this.updatedAt.HasValue)
            {
                writer.WritePropertyName("updatedAt");
                writer.Write(this.updatedAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Room FromDict(JsonData data)
        {
            return new Room()
                .WithRoomId(data.Keys.Contains("roomId") && data["roomId"] != null ? data["roomId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithIpAddress(data.Keys.Contains("ipAddress") && data["ipAddress"] != null ? data["ipAddress"].ToString() : null)
                .WithPort(data.Keys.Contains("port") && data["port"] != null ? (int?)int.Parse(data["port"].ToString()) : null)
                .WithEncryptionKey(data.Keys.Contains("encryptionKey") && data["encryptionKey"] != null ? data["encryptionKey"].ToString() : null)
                .WithNotificationUserIds(data.Keys.Contains("notificationUserIds") && data["notificationUserIds"] != null ? data["notificationUserIds"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}