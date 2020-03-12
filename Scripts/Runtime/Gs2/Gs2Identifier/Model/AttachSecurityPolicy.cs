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

namespace Gs2.Gs2Identifier.Model
{
	[Preserve]
	public class AttachSecurityPolicy
	{

        /** ユーザ のGRN */
        public string userId { set; get; }

        /**
         * ユーザ のGRNを設定
         *
         * @param userId ユーザ のGRN
         * @return this
         */
        public AttachSecurityPolicy WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** セキュリティポリシー のGRNのリスト */
        public List<string> securityPolicyIds { set; get; }

        /**
         * セキュリティポリシー のGRNのリストを設定
         *
         * @param securityPolicyIds セキュリティポリシー のGRNのリスト
         * @return this
         */
        public AttachSecurityPolicy WithSecurityPolicyIds(List<string> securityPolicyIds) {
            this.securityPolicyIds = securityPolicyIds;
            return this;
        }

        /** 作成日時 */
        public long? attachedAt { set; get; }

        /**
         * 作成日時を設定
         *
         * @param attachedAt 作成日時
         * @return this
         */
        public AttachSecurityPolicy WithAttachedAt(long? attachedAt) {
            this.attachedAt = attachedAt;
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
            if(this.securityPolicyIds != null)
            {
                writer.WritePropertyName("securityPolicyIds");
                writer.WriteArrayStart();
                foreach(var item in this.securityPolicyIds)
                {
                    writer.Write(item);
                }
                writer.WriteArrayEnd();
            }
            if(this.attachedAt.HasValue)
            {
                writer.WritePropertyName("attachedAt");
                writer.Write(this.attachedAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static AttachSecurityPolicy FromDict(JsonData data)
        {
            return new AttachSecurityPolicy()
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithSecurityPolicyIds(data.Keys.Contains("securityPolicyIds") && data["securityPolicyIds"] != null ? data["securityPolicyIds"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null)
                .WithAttachedAt(data.Keys.Contains("attachedAt") && data["attachedAt"] != null ? (long?)long.Parse(data["attachedAt"].ToString()) : null);
        }
	}
}