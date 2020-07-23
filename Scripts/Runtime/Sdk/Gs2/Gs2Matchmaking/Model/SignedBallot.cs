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

namespace Gs2.Gs2Matchmaking.Model
{
	[Preserve]
	public class SignedBallot : IComparable
	{

        /** 投票用紙の署名対象のデータ */
        public string body { set; get; }

        /**
         * 投票用紙の署名対象のデータを設定
         *
         * @param body 投票用紙の署名対象のデータ
         * @return this
         */
        public SignedBallot WithBody(string body) {
            this.body = body;
            return this;
        }

        /** 投票用紙の署名 */
        public string signature { set; get; }

        /**
         * 投票用紙の署名を設定
         *
         * @param signature 投票用紙の署名
         * @return this
         */
        public SignedBallot WithSignature(string signature) {
            this.signature = signature;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.body != null)
            {
                writer.WritePropertyName("body");
                writer.Write(this.body);
            }
            if(this.signature != null)
            {
                writer.WritePropertyName("signature");
                writer.Write(this.signature);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static SignedBallot FromDict(JsonData data)
        {
            return new SignedBallot()
                .WithBody(data.Keys.Contains("body") && data["body"] != null ? data["body"].ToString() : null)
                .WithSignature(data.Keys.Contains("signature") && data["signature"] != null ? data["signature"].ToString() : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as SignedBallot;
            var diff = 0;
            if (body == null && body == other.body)
            {
                // null and null
            }
            else
            {
                diff += body.CompareTo(other.body);
            }
            if (signature == null && signature == other.signature)
            {
                // null and null
            }
            else
            {
                diff += signature.CompareTo(other.signature);
            }
            return diff;
        }
	}
}