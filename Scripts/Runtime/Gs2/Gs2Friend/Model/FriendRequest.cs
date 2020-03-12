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

namespace Gs2.Gs2Friend.Model
{
	[Preserve]
	public class FriendRequest
	{

        /** ユーザーID */
        public string userId { set; get; }

        /**
         * ユーザーIDを設定
         *
         * @param userId ユーザーID
         * @return this
         */
        public FriendRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** ユーザーID */
        public string targetUserId { set; get; }

        /**
         * ユーザーIDを設定
         *
         * @param targetUserId ユーザーID
         * @return this
         */
        public FriendRequest WithTargetUserId(string targetUserId) {
            this.targetUserId = targetUserId;
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
            if(this.targetUserId != null)
            {
                writer.WritePropertyName("targetUserId");
                writer.Write(this.targetUserId);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static FriendRequest FromDict(JsonData data)
        {
            return new FriendRequest()
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithTargetUserId(data.Keys.Contains("targetUserId") && data["targetUserId"] != null ? data["targetUserId"].ToString() : null);
        }
	}
}