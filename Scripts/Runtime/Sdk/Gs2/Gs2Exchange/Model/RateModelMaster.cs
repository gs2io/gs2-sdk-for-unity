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

namespace Gs2.Gs2Exchange.Model
{
	[Preserve]
	public class RateModelMaster : IComparable
	{

        /** 交換レートマスター */
        public string rateModelId { set; get; }

        /**
         * 交換レートマスターを設定
         *
         * @param rateModelId 交換レートマスター
         * @return this
         */
        public RateModelMaster WithRateModelId(string rateModelId) {
            this.rateModelId = rateModelId;
            return this;
        }

        /** 交換レート名 */
        public string name { set; get; }

        /**
         * 交換レート名を設定
         *
         * @param name 交換レート名
         * @return this
         */
        public RateModelMaster WithName(string name) {
            this.name = name;
            return this;
        }

        /** 交換レートマスターの説明 */
        public string description { set; get; }

        /**
         * 交換レートマスターの説明を設定
         *
         * @param description 交換レートマスターの説明
         * @return this
         */
        public RateModelMaster WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** 交換レートのメタデータ */
        public string metadata { set; get; }

        /**
         * 交換レートのメタデータを設定
         *
         * @param metadata 交換レートのメタデータ
         * @return this
         */
        public RateModelMaster WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** 消費アクションリスト */
        public List<ConsumeAction> consumeActions { set; get; }

        /**
         * 消費アクションリストを設定
         *
         * @param consumeActions 消費アクションリスト
         * @return this
         */
        public RateModelMaster WithConsumeActions(List<ConsumeAction> consumeActions) {
            this.consumeActions = consumeActions;
            return this;
        }

        /** 交換の種類 */
        public string timingType { set; get; }

        /**
         * 交換の種類を設定
         *
         * @param timingType 交換の種類
         * @return this
         */
        public RateModelMaster WithTimingType(string timingType) {
            this.timingType = timingType;
            return this;
        }

        /** 交換実行から実際に報酬を受け取れるようになるまでの待ち時間（分） */
        public int? lockTime { set; get; }

        /**
         * 交換実行から実際に報酬を受け取れるようになるまでの待ち時間（分）を設定
         *
         * @param lockTime 交換実行から実際に報酬を受け取れるようになるまでの待ち時間（分）
         * @return this
         */
        public RateModelMaster WithLockTime(int? lockTime) {
            this.lockTime = lockTime;
            return this;
        }

        /** スキップをすることができるか */
        public bool? enableSkip { set; get; }

        /**
         * スキップをすることができるかを設定
         *
         * @param enableSkip スキップをすることができるか
         * @return this
         */
        public RateModelMaster WithEnableSkip(bool? enableSkip) {
            this.enableSkip = enableSkip;
            return this;
        }

        /** 時短消費アクションリスト */
        public List<ConsumeAction> skipConsumeActions { set; get; }

        /**
         * 時短消費アクションリストを設定
         *
         * @param skipConsumeActions 時短消費アクションリスト
         * @return this
         */
        public RateModelMaster WithSkipConsumeActions(List<ConsumeAction> skipConsumeActions) {
            this.skipConsumeActions = skipConsumeActions;
            return this;
        }

        /** 入手アクションリスト */
        public List<AcquireAction> acquireActions { set; get; }

        /**
         * 入手アクションリストを設定
         *
         * @param acquireActions 入手アクションリスト
         * @return this
         */
        public RateModelMaster WithAcquireActions(List<AcquireAction> acquireActions) {
            this.acquireActions = acquireActions;
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
        public RateModelMaster WithCreatedAt(long? createdAt) {
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
        public RateModelMaster WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.rateModelId != null)
            {
                writer.WritePropertyName("rateModelId");
                writer.Write(this.rateModelId);
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
            if(this.metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.metadata);
            }
            if(this.consumeActions != null)
            {
                writer.WritePropertyName("consumeActions");
                writer.WriteArrayStart();
                foreach(var item in this.consumeActions)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.timingType != null)
            {
                writer.WritePropertyName("timingType");
                writer.Write(this.timingType);
            }
            if(this.lockTime.HasValue)
            {
                writer.WritePropertyName("lockTime");
                writer.Write(this.lockTime.Value);
            }
            if(this.enableSkip.HasValue)
            {
                writer.WritePropertyName("enableSkip");
                writer.Write(this.enableSkip.Value);
            }
            if(this.skipConsumeActions != null)
            {
                writer.WritePropertyName("skipConsumeActions");
                writer.WriteArrayStart();
                foreach(var item in this.skipConsumeActions)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.acquireActions != null)
            {
                writer.WritePropertyName("acquireActions");
                writer.WriteArrayStart();
                foreach(var item in this.acquireActions)
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

    public static string GetRateNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):exchange:(?<namespaceName>.*):model:(?<rateName>.*)");
        if (!match.Groups["rateName"].Success)
        {
            return null;
        }
        return match.Groups["rateName"].Value;
    }

    public static string GetNamespaceNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):exchange:(?<namespaceName>.*):model:(?<rateName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):exchange:(?<namespaceName>.*):model:(?<rateName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):exchange:(?<namespaceName>.*):model:(?<rateName>.*)");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static RateModelMaster FromDict(JsonData data)
        {
            return new RateModelMaster()
                .WithRateModelId(data.Keys.Contains("rateModelId") && data["rateModelId"] != null ? data["rateModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithConsumeActions(data.Keys.Contains("consumeActions") && data["consumeActions"] != null ? data["consumeActions"].Cast<JsonData>().Select(value =>
                    {
                        return Gs2.Gs2Exchange.Model.ConsumeAction.FromDict(value);
                    }
                ).ToList() : null)
                .WithTimingType(data.Keys.Contains("timingType") && data["timingType"] != null ? data["timingType"].ToString() : null)
                .WithLockTime(data.Keys.Contains("lockTime") && data["lockTime"] != null ? (int?)int.Parse(data["lockTime"].ToString()) : null)
                .WithEnableSkip(data.Keys.Contains("enableSkip") && data["enableSkip"] != null ? (bool?)bool.Parse(data["enableSkip"].ToString()) : null)
                .WithSkipConsumeActions(data.Keys.Contains("skipConsumeActions") && data["skipConsumeActions"] != null ? data["skipConsumeActions"].Cast<JsonData>().Select(value =>
                    {
                        return Gs2.Gs2Exchange.Model.ConsumeAction.FromDict(value);
                    }
                ).ToList() : null)
                .WithAcquireActions(data.Keys.Contains("acquireActions") && data["acquireActions"] != null ? data["acquireActions"].Cast<JsonData>().Select(value =>
                    {
                        return Gs2.Gs2Exchange.Model.AcquireAction.FromDict(value);
                    }
                ).ToList() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as RateModelMaster;
            var diff = 0;
            if (rateModelId == null && rateModelId == other.rateModelId)
            {
                // null and null
            }
            else
            {
                diff += rateModelId.CompareTo(other.rateModelId);
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
            if (metadata == null && metadata == other.metadata)
            {
                // null and null
            }
            else
            {
                diff += metadata.CompareTo(other.metadata);
            }
            if (consumeActions == null && consumeActions == other.consumeActions)
            {
                // null and null
            }
            else
            {
                diff += consumeActions.Count - other.consumeActions.Count;
                for (var i = 0; i < consumeActions.Count; i++)
                {
                    diff += consumeActions[i].CompareTo(other.consumeActions[i]);
                }
            }
            if (timingType == null && timingType == other.timingType)
            {
                // null and null
            }
            else
            {
                diff += timingType.CompareTo(other.timingType);
            }
            if (lockTime == null && lockTime == other.lockTime)
            {
                // null and null
            }
            else
            {
                diff += (int)(lockTime - other.lockTime);
            }
            if (enableSkip == null && enableSkip == other.enableSkip)
            {
                // null and null
            }
            else
            {
                diff += enableSkip == other.enableSkip ? 0 : 1;
            }
            if (skipConsumeActions == null && skipConsumeActions == other.skipConsumeActions)
            {
                // null and null
            }
            else
            {
                diff += skipConsumeActions.Count - other.skipConsumeActions.Count;
                for (var i = 0; i < skipConsumeActions.Count; i++)
                {
                    diff += skipConsumeActions[i].CompareTo(other.skipConsumeActions[i]);
                }
            }
            if (acquireActions == null && acquireActions == other.acquireActions)
            {
                // null and null
            }
            else
            {
                diff += acquireActions.Count - other.acquireActions.Count;
                for (var i = 0; i < acquireActions.Count; i++)
                {
                    diff += acquireActions[i].CompareTo(other.acquireActions[i]);
                }
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