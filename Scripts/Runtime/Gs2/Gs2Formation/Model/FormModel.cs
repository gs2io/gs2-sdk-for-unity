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
	public class FormModel
	{

        /** フォームマスター */
        public string formModelId { set; get; }

        /**
         * フォームマスターを設定
         *
         * @param formModelId フォームマスター
         * @return this
         */
        public FormModel WithFormModelId(string formModelId) {
            this.formModelId = formModelId;
            return this;
        }

        /** フォームの種類名 */
        public string name { set; get; }

        /**
         * フォームの種類名を設定
         *
         * @param name フォームの種類名
         * @return this
         */
        public FormModel WithName(string name) {
            this.name = name;
            return this;
        }

        /** フォームの種類のメタデータ */
        public string metadata { set; get; }

        /**
         * フォームの種類のメタデータを設定
         *
         * @param metadata フォームの種類のメタデータ
         * @return this
         */
        public FormModel WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** スリットリスト */
        public List<SlotModel> slots { set; get; }

        /**
         * スリットリストを設定
         *
         * @param slots スリットリスト
         * @return this
         */
        public FormModel WithSlots(List<SlotModel> slots) {
            this.slots = slots;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.formModelId != null)
            {
                writer.WritePropertyName("formModelId");
                writer.Write(this.formModelId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.metadata);
            }
            if(this.slots != null)
            {
                writer.WritePropertyName("slots");
                writer.WriteArrayStart();
                foreach(var item in this.slots)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static FormModel FromDict(JsonData data)
        {
            return new FormModel()
                .WithFormModelId(data.Keys.Contains("formModelId") && data["formModelId"] != null ? data["formModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithSlots(data.Keys.Contains("slots") && data["slots"] != null ? data["slots"].Cast<JsonData>().Select(value =>
                    {
                        return SlotModel.FromDict(value);
                    }
                ).ToList() : null);
        }
	}
}