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

namespace Gs2.Gs2Gateway.Model
{
	[Preserve]
	public class Session : IComparable
	{

        /** WebSocketセッション のGRN */
        public string sessionId { set; get; }

        /**
         * WebSocketセッション のGRNを設定
         *
         * @param sessionId WebSocketセッション のGRN
         * @return this
         */
        public Session WithSessionId(string sessionId) {
            this.sessionId = sessionId;
            return this;
        }

        /** オーナーID */
        public string ownerId { set; get; }

        /**
         * オーナーIDを設定
         *
         * @param ownerId オーナーID
         * @return this
         */
        public Session WithOwnerId(string ownerId) {
            this.ownerId = ownerId;
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
        public Session WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** WebSocketセッション名 */
        public string sessionName { set; get; }

        /**
         * WebSocketセッション名を設定
         *
         * @param sessionName WebSocketセッション名
         * @return this
         */
        public Session WithSessionName(string sessionName) {
            this.sessionName = sessionName;
            return this;
        }

        /** API Gateway の APIID */
        public string apiId { set; get; }

        /**
         * API Gateway の APIIDを設定
         *
         * @param apiId API Gateway の APIID
         * @return this
         */
        public Session WithApiId(string apiId) {
            this.apiId = apiId;
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
        public Session WithCreatedAt(long? createdAt) {
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
        public Session WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.sessionId != null)
            {
                writer.WritePropertyName("sessionId");
                writer.Write(this.sessionId);
            }
            if(this.ownerId != null)
            {
                writer.WritePropertyName("ownerId");
                writer.Write(this.ownerId);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.sessionName != null)
            {
                writer.WritePropertyName("sessionName");
                writer.Write(this.sessionName);
            }
            if(this.apiId != null)
            {
                writer.WritePropertyName("apiId");
                writer.Write(this.apiId);
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

    public static string GetSessionNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):gateway:(?<namespaceName>.*):user:(?<userId>.*):session:(?<sessionName>.*)");
        if (!match.Groups["sessionName"].Success)
        {
            return null;
        }
        return match.Groups["sessionName"].Value;
    }

    public static string GetUserIdFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):gateway:(?<namespaceName>.*):user:(?<userId>.*):session:(?<sessionName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):gateway:(?<namespaceName>.*):user:(?<userId>.*):session:(?<sessionName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):gateway:(?<namespaceName>.*):user:(?<userId>.*):session:(?<sessionName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):gateway:(?<namespaceName>.*):user:(?<userId>.*):session:(?<sessionName>.*)");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static Session FromDict(JsonData data)
        {
            return new Session()
                .WithSessionId(data.Keys.Contains("sessionId") && data["sessionId"] != null ? data["sessionId"].ToString() : null)
                .WithOwnerId(data.Keys.Contains("ownerId") && data["ownerId"] != null ? data["ownerId"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithSessionName(data.Keys.Contains("sessionName") && data["sessionName"] != null ? data["sessionName"].ToString() : null)
                .WithApiId(data.Keys.Contains("apiId") && data["apiId"] != null ? data["apiId"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as Session;
            var diff = 0;
            if (sessionId == null && sessionId == other.sessionId)
            {
                // null and null
            }
            else
            {
                diff += sessionId.CompareTo(other.sessionId);
            }
            if (ownerId == null && ownerId == other.ownerId)
            {
                // null and null
            }
            else
            {
                diff += ownerId.CompareTo(other.ownerId);
            }
            if (userId == null && userId == other.userId)
            {
                // null and null
            }
            else
            {
                diff += userId.CompareTo(other.userId);
            }
            if (sessionName == null && sessionName == other.sessionName)
            {
                // null and null
            }
            else
            {
                diff += sessionName.CompareTo(other.sessionName);
            }
            if (apiId == null && apiId == other.apiId)
            {
                // null and null
            }
            else
            {
                diff += apiId.CompareTo(other.apiId);
            }
            if (createdAt == null && createdAt == other.createdAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(createdAt - other.createdAt);
            }
            if (updatedAt == null && updatedAt == other.updatedAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(updatedAt - other.updatedAt);
            }
            return diff;
        }
	}
}