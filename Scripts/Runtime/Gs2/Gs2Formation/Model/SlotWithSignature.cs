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

namespace Gs2.Gs2Formation.Model
{
	[Preserve]
	public class SlotWithSignature
	{

        /** スロットモデル名 */
        public string name { set; get; }

        /**
         * スロットモデル名を設定
         *
         * @param name スロットモデル名
         * @return this
         */
        public SlotWithSignature WithName(string name) {
            this.name = name;
            return this;
        }

        /** プロパティの種類 */
        public string propertyType { set; get; }

        /**
         * プロパティの種類を設定
         *
         * @param propertyType プロパティの種類
         * @return this
         */
        public SlotWithSignature WithPropertyType(string propertyType) {
            this.propertyType = propertyType;
            return this;
        }

        /** ペイロード */
        public string body { set; get; }

        /**
         * ペイロードを設定
         *
         * @param body ペイロード
         * @return this
         */
        public SlotWithSignature WithBody(string body) {
            this.body = body;
            return this;
        }

        /** プロパティIDのリソースを所有していることを証明する署名 */
        public string signature { set; get; }

        /**
         * プロパティIDのリソースを所有していることを証明する署名を設定
         *
         * @param signature プロパティIDのリソースを所有していることを証明する署名
         * @return this
         */
        public SlotWithSignature WithSignature(string signature) {
            this.signature = signature;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.propertyType != null)
            {
                writer.WritePropertyName("propertyType");
                writer.Write(this.propertyType);
            }
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
        public static SlotWithSignature FromDict(JsonData data)
        {
            return new SlotWithSignature()
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithPropertyType(data.Keys.Contains("propertyType") && data["propertyType"] != null ? data["propertyType"].ToString() : null)
                .WithBody(data.Keys.Contains("body") && data["body"] != null ? data["body"].ToString() : null)
                .WithSignature(data.Keys.Contains("signature") && data["signature"] != null ? data["signature"].ToString() : null);
        }
	}
}