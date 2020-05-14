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
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Formation.Model
{
	[Preserve]
	public class Form
	{

        /** フォーム */
        public string formId { set; get; }

        /**
         * フォームを設定
         *
         * @param formId フォーム
         * @return this
         */
        public Form WithFormId(string formId) {
            this.formId = formId;
            return this;
        }

        /** フォームの保存領域の名前 */
        public string name { set; get; }

        /**
         * フォームの保存領域の名前を設定
         *
         * @param name フォームの保存領域の名前
         * @return this
         */
        public Form WithName(string name) {
            this.name = name;
            return this;
        }

        /** 保存領域のインデックス */
        public int? index { set; get; }

        /**
         * 保存領域のインデックスを設定
         *
         * @param index 保存領域のインデックス
         * @return this
         */
        public Form WithIndex(int? index) {
            this.index = index;
            return this;
        }

        /** スロットリスト */
        public List<Slot> slots { set; get; }

        /**
         * スロットリストを設定
         *
         * @param slots スロットリスト
         * @return this
         */
        public Form WithSlots(List<Slot> slots) {
            this.slots = slots;
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
        public Form WithCreatedAt(long? createdAt) {
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
        public Form WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.formId != null)
            {
                writer.WritePropertyName("formId");
                writer.Write(this.formId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.index.HasValue)
            {
                writer.WritePropertyName("index");
                writer.Write(this.index.Value);
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

    public static string GetIndexFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):formation:(?<namespaceName>.*):user:(?<userId>.*):mold:(?<moldName>.*):form:(?<index>.*)");
        if (!match.Groups["index"].Success)
        {
            return null;
        }
        return match.Groups["index"].Value;
    }

    public static string GetMoldNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):formation:(?<namespaceName>.*):user:(?<userId>.*):mold:(?<moldName>.*):form:(?<index>.*)");
        if (!match.Groups["moldName"].Success)
        {
            return null;
        }
        return match.Groups["moldName"].Value;
    }

    public static string GetUserIdFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):formation:(?<namespaceName>.*):user:(?<userId>.*):mold:(?<moldName>.*):form:(?<index>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):formation:(?<namespaceName>.*):user:(?<userId>.*):mold:(?<moldName>.*):form:(?<index>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):formation:(?<namespaceName>.*):user:(?<userId>.*):mold:(?<moldName>.*):form:(?<index>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):formation:(?<namespaceName>.*):user:(?<userId>.*):mold:(?<moldName>.*):form:(?<index>.*)");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static Form FromDict(JsonData data)
        {
            return new Form()
                .WithFormId(data.Keys.Contains("formId") && data["formId"] != null ? data["formId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithIndex(data.Keys.Contains("index") && data["index"] != null ? (int?)int.Parse(data["index"].ToString()) : null)
                .WithSlots(data.Keys.Contains("slots") && data["slots"] != null ? data["slots"].Cast<JsonData>().Select(value =>
                    {
                        return Gs2.Gs2Formation.Model.Slot.FromDict(value);
                    }
                ).ToList() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}