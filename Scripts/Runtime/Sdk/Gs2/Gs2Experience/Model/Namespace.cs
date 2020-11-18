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

namespace Gs2.Gs2Experience.Model
{
	[Preserve]
	public class Namespace : IComparable
	{

        /** ネームスペース */
        public string namespaceId { set; get; }

        /**
         * ネームスペースを設定
         *
         * @param namespaceId ネームスペース
         * @return this
         */
        public Namespace WithNamespaceId(string namespaceId) {
            this.namespaceId = namespaceId;
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
        public Namespace WithOwnerId(string ownerId) {
            this.ownerId = ownerId;
            return this;
        }

        /** ネームスペース名 */
        public string name { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param name ネームスペース名
         * @return this
         */
        public Namespace WithName(string name) {
            this.name = name;
            return this;
        }

        /** ネームスペースの説明 */
        public string description { set; get; }

        /**
         * ネームスペースの説明を設定
         *
         * @param description ネームスペースの説明
         * @return this
         */
        public Namespace WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** ランクキャップ取得時 に実行されるスクリプト のGRN */
        public string experienceCapScriptId { set; get; }

        /**
         * ランクキャップ取得時 に実行されるスクリプト のGRNを設定
         *
         * @param experienceCapScriptId ランクキャップ取得時 に実行されるスクリプト のGRN
         * @return this
         */
        public Namespace WithExperienceCapScriptId(string experienceCapScriptId) {
            this.experienceCapScriptId = experienceCapScriptId;
            return this;
        }

        /** 経験値変化したときに実行するスクリプト */
        public Gs2.Gs2Experience.Model.ScriptSetting changeExperienceScript { set; get; }

        /**
         * 経験値変化したときに実行するスクリプトを設定
         *
         * @param changeExperienceScript 経験値変化したときに実行するスクリプト
         * @return this
         */
        public Namespace WithChangeExperienceScript(Gs2.Gs2Experience.Model.ScriptSetting changeExperienceScript) {
            this.changeExperienceScript = changeExperienceScript;
            return this;
        }

        /** ランク変化したときに実行するスクリプト */
        public Gs2.Gs2Experience.Model.ScriptSetting changeRankScript { set; get; }

        /**
         * ランク変化したときに実行するスクリプトを設定
         *
         * @param changeRankScript ランク変化したときに実行するスクリプト
         * @return this
         */
        public Namespace WithChangeRankScript(Gs2.Gs2Experience.Model.ScriptSetting changeRankScript) {
            this.changeRankScript = changeRankScript;
            return this;
        }

        /** ランクキャップ変化したときに実行するスクリプト */
        public Gs2.Gs2Experience.Model.ScriptSetting changeRankCapScript { set; get; }

        /**
         * ランクキャップ変化したときに実行するスクリプトを設定
         *
         * @param changeRankCapScript ランクキャップ変化したときに実行するスクリプト
         * @return this
         */
        public Namespace WithChangeRankCapScript(Gs2.Gs2Experience.Model.ScriptSetting changeRankCapScript) {
            this.changeRankCapScript = changeRankCapScript;
            return this;
        }

        /** 経験値あふれしたときに実行するスクリプト */
        public Gs2.Gs2Experience.Model.ScriptSetting overflowExperienceScript { set; get; }

        /**
         * 経験値あふれしたときに実行するスクリプトを設定
         *
         * @param overflowExperienceScript 経験値あふれしたときに実行するスクリプト
         * @return this
         */
        public Namespace WithOverflowExperienceScript(Gs2.Gs2Experience.Model.ScriptSetting overflowExperienceScript) {
            this.overflowExperienceScript = overflowExperienceScript;
            return this;
        }

        /** ログの出力設定 */
        public Gs2.Gs2Experience.Model.LogSetting logSetting { set; get; }

        /**
         * ログの出力設定を設定
         *
         * @param logSetting ログの出力設定
         * @return this
         */
        public Namespace WithLogSetting(Gs2.Gs2Experience.Model.LogSetting logSetting) {
            this.logSetting = logSetting;
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
        public Namespace WithCreatedAt(long? createdAt) {
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
        public Namespace WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.namespaceId != null)
            {
                writer.WritePropertyName("namespaceId");
                writer.Write(this.namespaceId);
            }
            if(this.ownerId != null)
            {
                writer.WritePropertyName("ownerId");
                writer.Write(this.ownerId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.description != null)
            {
                writer.WritePropertyName("description");
                writer.Write(this.description);
            }
            if(this.experienceCapScriptId != null)
            {
                writer.WritePropertyName("experienceCapScriptId");
                writer.Write(this.experienceCapScriptId);
            }
            if(this.changeExperienceScript != null)
            {
                writer.WritePropertyName("changeExperienceScript");
                this.changeExperienceScript.WriteJson(writer);
            }
            if(this.changeRankScript != null)
            {
                writer.WritePropertyName("changeRankScript");
                this.changeRankScript.WriteJson(writer);
            }
            if(this.changeRankCapScript != null)
            {
                writer.WritePropertyName("changeRankCapScript");
                this.changeRankCapScript.WriteJson(writer);
            }
            if(this.overflowExperienceScript != null)
            {
                writer.WritePropertyName("overflowExperienceScript");
                this.overflowExperienceScript.WriteJson(writer);
            }
            if(this.logSetting != null)
            {
                writer.WritePropertyName("logSetting");
                this.logSetting.WriteJson(writer);
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

    public static string GetNamespaceNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):experience:(?<namespaceName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):experience:(?<namespaceName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):experience:(?<namespaceName>.*)");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static Namespace FromDict(JsonData data)
        {
            return new Namespace()
                .WithNamespaceId(data.Keys.Contains("namespaceId") && data["namespaceId"] != null ? data["namespaceId"].ToString() : null)
                .WithOwnerId(data.Keys.Contains("ownerId") && data["ownerId"] != null ? data["ownerId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithExperienceCapScriptId(data.Keys.Contains("experienceCapScriptId") && data["experienceCapScriptId"] != null ? data["experienceCapScriptId"].ToString() : null)
                .WithChangeExperienceScript(data.Keys.Contains("changeExperienceScript") && data["changeExperienceScript"] != null ? Gs2.Gs2Experience.Model.ScriptSetting.FromDict(data["changeExperienceScript"]) : null)
                .WithChangeRankScript(data.Keys.Contains("changeRankScript") && data["changeRankScript"] != null ? Gs2.Gs2Experience.Model.ScriptSetting.FromDict(data["changeRankScript"]) : null)
                .WithChangeRankCapScript(data.Keys.Contains("changeRankCapScript") && data["changeRankCapScript"] != null ? Gs2.Gs2Experience.Model.ScriptSetting.FromDict(data["changeRankCapScript"]) : null)
                .WithOverflowExperienceScript(data.Keys.Contains("overflowExperienceScript") && data["overflowExperienceScript"] != null ? Gs2.Gs2Experience.Model.ScriptSetting.FromDict(data["overflowExperienceScript"]) : null)
                .WithLogSetting(data.Keys.Contains("logSetting") && data["logSetting"] != null ? Gs2.Gs2Experience.Model.LogSetting.FromDict(data["logSetting"]) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as Namespace;
            var diff = 0;
            if (namespaceId == null && namespaceId == other.namespaceId)
            {
                // null and null
            }
            else
            {
                diff += namespaceId.CompareTo(other.namespaceId);
            }
            if (ownerId == null && ownerId == other.ownerId)
            {
                // null and null
            }
            else
            {
                diff += ownerId.CompareTo(other.ownerId);
            }
            if (name == null && name == other.name)
            {
                // null and null
            }
            else
            {
                diff += name.CompareTo(other.name);
            }
            if (description == null && description == other.description)
            {
                // null and null
            }
            else
            {
                diff += description.CompareTo(other.description);
            }
            if (experienceCapScriptId == null && experienceCapScriptId == other.experienceCapScriptId)
            {
                // null and null
            }
            else
            {
                diff += experienceCapScriptId.CompareTo(other.experienceCapScriptId);
            }
            if (changeExperienceScript == null && changeExperienceScript == other.changeExperienceScript)
            {
                // null and null
            }
            else
            {
                diff += changeExperienceScript.CompareTo(other.changeExperienceScript);
            }
            if (changeRankScript == null && changeRankScript == other.changeRankScript)
            {
                // null and null
            }
            else
            {
                diff += changeRankScript.CompareTo(other.changeRankScript);
            }
            if (changeRankCapScript == null && changeRankCapScript == other.changeRankCapScript)
            {
                // null and null
            }
            else
            {
                diff += changeRankCapScript.CompareTo(other.changeRankCapScript);
            }
            if (overflowExperienceScript == null && overflowExperienceScript == other.overflowExperienceScript)
            {
                // null and null
            }
            else
            {
                diff += overflowExperienceScript.CompareTo(other.overflowExperienceScript);
            }
            if (logSetting == null && logSetting == other.logSetting)
            {
                // null and null
            }
            else
            {
                diff += logSetting.CompareTo(other.logSetting);
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