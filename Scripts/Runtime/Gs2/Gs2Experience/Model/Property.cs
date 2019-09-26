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

namespace Gs2.Gs2Experience.Model
{
	public class Property
	{

        /** 経験値の種類名 */
        public string experienceName { set; get; }

        /**
         * 経験値の種類名を設定
         *
         * @param experienceName 経験値の種類名
         * @return this
         */
        public Property WithExperienceName(string experienceName) {
            this.experienceName = experienceName;
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
        public Property WithPropertyId(string propertyId) {
            this.propertyId = propertyId;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.experienceName != null)
            {
                writer.WritePropertyName("experienceName");
                writer.Write(this.experienceName);
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