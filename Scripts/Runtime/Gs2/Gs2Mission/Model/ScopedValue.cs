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

namespace Gs2.Gs2Mission.Model
{
	[Preserve]
	public class ScopedValue
	{

        /** リセットタイミング */
        public string resetType { set; get; }

        /**
         * リセットタイミングを設定
         *
         * @param resetType リセットタイミング
         * @return this
         */
        public ScopedValue WithResetType(string resetType) {
            this.resetType = resetType;
            return this;
        }

        /** カウント */
        public long? value { set; get; }

        /**
         * カウントを設定
         *
         * @param value カウント
         * @return this
         */
        public ScopedValue WithValue(long? value) {
            this.value = value;
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
        public ScopedValue WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.resetType != null)
            {
                writer.WritePropertyName("resetType");
                writer.Write(this.resetType);
            }
            if(this.value.HasValue)
            {
                writer.WritePropertyName("value");
                writer.Write(this.value.Value);
            }
            if(this.updatedAt.HasValue)
            {
                writer.WritePropertyName("updatedAt");
                writer.Write(this.updatedAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static ScopedValue FromDict(JsonData data)
        {
            return new ScopedValue()
                .WithResetType(data.Keys.Contains("resetType") && data["resetType"] != null ? data["resetType"].ToString() : null)
                .WithValue(data.Keys.Contains("value") && data["value"] != null ? (long?)long.Parse(data["value"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}