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
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Chat.Model
{
	[Preserve]
	public class Subscribe : IComparable
	{

        /** 購読 */
        public string subscribeId { set; get; }

        /**
         * 購読を設定
         *
         * @param subscribeId 購読
         * @return this
         */
        public Subscribe WithSubscribeId(string subscribeId) {
            this.subscribeId = subscribeId;
            return this;
        }

        /** 購読するユーザID */
        public string userId { set; get; }

        /**
         * 購読するユーザIDを設定
         *
         * @param userId 購読するユーザID
         * @return this
         */
        public Subscribe WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** 購読するルーム名 */
        public string roomName { set; get; }

        /**
         * 購読するルーム名を設定
         *
         * @param roomName 購読するルーム名
         * @return this
         */
        public Subscribe WithRoomName(string roomName) {
            this.roomName = roomName;
            return this;
        }

        /** 新着メッセージ通知を受け取るカテゴリリスト */
        public List<NotificationType> notificationTypes { set; get; }

        /**
         * 新着メッセージ通知を受け取るカテゴリリストを設定
         *
         * @param notificationTypes 新着メッセージ通知を受け取るカテゴリリスト
         * @return this
         */
        public Subscribe WithNotificationTypes(List<NotificationType> notificationTypes) {
            this.notificationTypes = notificationTypes;
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
        public Subscribe WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.subscribeId != null)
            {
                writer.WritePropertyName("subscribeId");
                writer.Write(this.subscribeId);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.roomName != null)
            {
                writer.WritePropertyName("roomName");
                writer.Write(this.roomName);
            }
            if(this.notificationTypes != null)
            {
                writer.WritePropertyName("notificationTypes");
                writer.WriteArrayStart();
                foreach(var item in this.notificationTypes)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            writer.WriteObjectEnd();
        }

    public static string GetRoomNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):chat:(?<namespaceName>.*):user:(?<userId>.*):room:(?<roomName>.*):subscribe");
        if (!match.Groups["roomName"].Success)
        {
            return null;
        }
        return match.Groups["roomName"].Value;
    }

    public static string GetUserIdFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):chat:(?<namespaceName>.*):user:(?<userId>.*):room:(?<roomName>.*):subscribe");
        if (!match.Groups["userId"].Success)
        {
            return null;
        }
        return match.Groups["userId"].Value;
    }

    public static string GetNamespaceNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):chat:(?<namespaceName>.*):user:(?<userId>.*):room:(?<roomName>.*):subscribe");
        if (!match.Groups["namespaceName"].Success)
        {
            return null;
        }
        return match.Groups["namespaceName"].Value;
    }

    public static string GetOwnerIdFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):chat:(?<namespaceName>.*):user:(?<userId>.*):room:(?<roomName>.*):subscribe");
        if (!match.Groups["ownerId"].Success)
        {
            return null;
        }
        return match.Groups["ownerId"].Value;
    }

    public static string GetRegionFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):chat:(?<namespaceName>.*):user:(?<userId>.*):room:(?<roomName>.*):subscribe");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static Subscribe FromDict(JsonData data)
        {
            return new Subscribe()
                .WithSubscribeId(data.Keys.Contains("subscribeId") && data["subscribeId"] != null ? data["subscribeId"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithRoomName(data.Keys.Contains("roomName") && data["roomName"] != null ? data["roomName"].ToString() : null)
                .WithNotificationTypes(data.Keys.Contains("notificationTypes") && data["notificationTypes"] != null ? data["notificationTypes"].Cast<JsonData>().Select(value =>
                    {
                        return Gs2.Gs2Chat.Model.NotificationType.FromDict(value);
                    }
                ).ToList() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as Subscribe;
            var diff = 0;
            if (subscribeId == null && subscribeId == other.subscribeId)
            {
                // null and null
            }
            else
            {
                diff += subscribeId.CompareTo(other.subscribeId);
            }
            if (userId == null && userId == other.userId)
            {
                // null and null
            }
            else
            {
                diff += userId.CompareTo(other.userId);
            }
            if (roomName == null && roomName == other.roomName)
            {
                // null and null
            }
            else
            {
                diff += roomName.CompareTo(other.roomName);
            }
            if (notificationTypes == null && notificationTypes == other.notificationTypes)
            {
                // null and null
            }
            else
            {
                diff += notificationTypes.Count - other.notificationTypes.Count;
                for (var i = 0; i < notificationTypes.Count; i++)
                {
                    diff += notificationTypes[i].CompareTo(other.notificationTypes[i]);
                }
            }
            if (createdAt == null && createdAt == other.createdAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(createdAt - other.createdAt);
            }
            return diff;
        }
	}
}