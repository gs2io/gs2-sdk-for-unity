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
using Gs2.Core.Model;
using LitJson;

namespace Gs2.Gs2Formation.Model
{
	public class Slot
	{

        /** スロットモデル名 */
        public string name { set; get; }

        /**
         * スロットモデル名を設定
         *
         * @param name スロットモデル名
         * @return this
         */
        public Slot WithName(string name) {
            this.name = name;
            return this;
        }

        /** プロパティID */
        public string propertyId { set; get; }

        /**
         * プロパティIDを設定
         *
         * @param propertyId プロパティID
         * @return this
         */
        public Slot WithPropertyId(string propertyId) {
            this.propertyId = propertyId;
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
            if(this.propertyId != null)
            {
                writer.WritePropertyName("propertyId");
                writer.Write(this.propertyId);
            }
            writer.WriteObjectEnd();
        }
	}
}