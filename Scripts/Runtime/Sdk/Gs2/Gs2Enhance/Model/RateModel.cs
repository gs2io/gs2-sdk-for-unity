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

namespace Gs2.Gs2Enhance.Model
{
	[Preserve]
	public class RateModel : IComparable
	{

        /** 強化レートモデル */
        public string rateModelId { set; get; }

        /**
         * 強化レートモデルを設定
         *
         * @param rateModelId 強化レートモデル
         * @return this
         */
        public RateModel WithRateModelId(string rateModelId) {
            this.rateModelId = rateModelId;
            return this;
        }

        /** 強化レート名 */
        public string name { set; get; }

        /**
         * 強化レート名を設定
         *
         * @param name 強化レート名
         * @return this
         */
        public RateModel WithName(string name) {
            this.name = name;
            return this;
        }

        /** 強化レートの説明 */
        public string description { set; get; }

        /**
         * 強化レートの説明を設定
         *
         * @param description 強化レートの説明
         * @return this
         */
        public RateModel WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** 強化レートのメタデータ */
        public string metadata { set; get; }

        /**
         * 強化レートのメタデータを設定
         *
         * @param metadata 強化レートのメタデータ
         * @return this
         */
        public RateModel WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** 強化対象に使用できるインベントリモデル のGRN */
        public string targetInventoryModelId { set; get; }

        /**
         * 強化対象に使用できるインベントリモデル のGRNを設定
         *
         * @param targetInventoryModelId 強化対象に使用できるインベントリモデル のGRN
         * @return this
         */
        public RateModel WithTargetInventoryModelId(string targetInventoryModelId) {
            this.targetInventoryModelId = targetInventoryModelId;
            return this;
        }

        /** GS2-Experience で入手した経験値を格納する プロパティID に付与するサフィックス */
        public string acquireExperienceSuffix { set; get; }

        /**
         * GS2-Experience で入手した経験値を格納する プロパティID に付与するサフィックスを設定
         *
         * @param acquireExperienceSuffix GS2-Experience で入手した経験値を格納する プロパティID に付与するサフィックス
         * @return this
         */
        public RateModel WithAcquireExperienceSuffix(string acquireExperienceSuffix) {
            this.acquireExperienceSuffix = acquireExperienceSuffix;
            return this;
        }

        /** 強化素材に使用できるインベントリモデル のGRN */
        public string materialInventoryModelId { set; get; }

        /**
         * 強化素材に使用できるインベントリモデル のGRNを設定
         *
         * @param materialInventoryModelId 強化素材に使用できるインベントリモデル のGRN
         * @return this
         */
        public RateModel WithMaterialInventoryModelId(string materialInventoryModelId) {
            this.materialInventoryModelId = materialInventoryModelId;
            return this;
        }

        /** 入手経験値を格納しているメタデータのJSON階層 */
        public List<string> acquireExperienceHierarchy { set; get; }

        /**
         * 入手経験値を格納しているメタデータのJSON階層を設定
         *
         * @param acquireExperienceHierarchy 入手経験値を格納しているメタデータのJSON階層
         * @return this
         */
        public RateModel WithAcquireExperienceHierarchy(List<string> acquireExperienceHierarchy) {
            this.acquireExperienceHierarchy = acquireExperienceHierarchy;
            return this;
        }

        /** 獲得できる経験値の種類マスター のGRN */
        public string experienceModelId { set; get; }

        /**
         * 獲得できる経験値の種類マスター のGRNを設定
         *
         * @param experienceModelId 獲得できる経験値の種類マスター のGRN
         * @return this
         */
        public RateModel WithExperienceModelId(string experienceModelId) {
            this.experienceModelId = experienceModelId;
            return this;
        }

        /** 経験値獲得量ボーナス */
        public List<BonusRate> bonusRates { set; get; }

        /**
         * 経験値獲得量ボーナスを設定
         *
         * @param bonusRates 経験値獲得量ボーナス
         * @return this
         */
        public RateModel WithBonusRates(List<BonusRate> bonusRates) {
            this.bonusRates = bonusRates;
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
            if(this.targetInventoryModelId != null)
            {
                writer.WritePropertyName("targetInventoryModelId");
                writer.Write(this.targetInventoryModelId);
            }
            if(this.acquireExperienceSuffix != null)
            {
                writer.WritePropertyName("acquireExperienceSuffix");
                writer.Write(this.acquireExperienceSuffix);
            }
            if(this.materialInventoryModelId != null)
            {
                writer.WritePropertyName("materialInventoryModelId");
                writer.Write(this.materialInventoryModelId);
            }
            if(this.acquireExperienceHierarchy != null)
            {
                writer.WritePropertyName("acquireExperienceHierarchy");
                writer.WriteArrayStart();
                foreach(var item in this.acquireExperienceHierarchy)
                {
                    writer.Write(item);
                }
                writer.WriteArrayEnd();
            }
            if(this.experienceModelId != null)
            {
                writer.WritePropertyName("experienceModelId");
                writer.Write(this.experienceModelId);
            }
            if(this.bonusRates != null)
            {
                writer.WritePropertyName("bonusRates");
                writer.WriteArrayStart();
                foreach(var item in this.bonusRates)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
        }

    public static string GetRateNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):enhance:(?<namespaceName>.*):");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):enhance:(?<namespaceName>.*):");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):enhance:(?<namespaceName>.*):");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):enhance:(?<namespaceName>.*):");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static RateModel FromDict(JsonData data)
        {
            return new RateModel()
                .WithRateModelId(data.Keys.Contains("rateModelId") && data["rateModelId"] != null ? data["rateModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithTargetInventoryModelId(data.Keys.Contains("targetInventoryModelId") && data["targetInventoryModelId"] != null ? data["targetInventoryModelId"].ToString() : null)
                .WithAcquireExperienceSuffix(data.Keys.Contains("acquireExperienceSuffix") && data["acquireExperienceSuffix"] != null ? data["acquireExperienceSuffix"].ToString() : null)
                .WithMaterialInventoryModelId(data.Keys.Contains("materialInventoryModelId") && data["materialInventoryModelId"] != null ? data["materialInventoryModelId"].ToString() : null)
                .WithAcquireExperienceHierarchy(data.Keys.Contains("acquireExperienceHierarchy") && data["acquireExperienceHierarchy"] != null ? data["acquireExperienceHierarchy"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null)
                .WithExperienceModelId(data.Keys.Contains("experienceModelId") && data["experienceModelId"] != null ? data["experienceModelId"].ToString() : null)
                .WithBonusRates(data.Keys.Contains("bonusRates") && data["bonusRates"] != null ? data["bonusRates"].Cast<JsonData>().Select(value =>
                    {
                        return Gs2.Gs2Enhance.Model.BonusRate.FromDict(value);
                    }
                ).ToList() : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as RateModel;
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
            if (targetInventoryModelId == null && targetInventoryModelId == other.targetInventoryModelId)
            {
                // null and null
            }
            else
            {
                diff += targetInventoryModelId.CompareTo(other.targetInventoryModelId);
            }
            if (acquireExperienceSuffix == null && acquireExperienceSuffix == other.acquireExperienceSuffix)
            {
                // null and null
            }
            else
            {
                diff += acquireExperienceSuffix.CompareTo(other.acquireExperienceSuffix);
            }
            if (materialInventoryModelId == null && materialInventoryModelId == other.materialInventoryModelId)
            {
                // null and null
            }
            else
            {
                diff += materialInventoryModelId.CompareTo(other.materialInventoryModelId);
            }
            if (acquireExperienceHierarchy == null && acquireExperienceHierarchy == other.acquireExperienceHierarchy)
            {
                // null and null
            }
            else
            {
                diff += acquireExperienceHierarchy.Count - other.acquireExperienceHierarchy.Count;
                for (var i = 0; i < acquireExperienceHierarchy.Count; i++)
                {
                    diff += acquireExperienceHierarchy[i].CompareTo(other.acquireExperienceHierarchy[i]);
                }
            }
            if (experienceModelId == null && experienceModelId == other.experienceModelId)
            {
                // null and null
            }
            else
            {
                diff += experienceModelId.CompareTo(other.experienceModelId);
            }
            if (bonusRates == null && bonusRates == other.bonusRates)
            {
                // null and null
            }
            else
            {
                diff += bonusRates.Count - other.bonusRates.Count;
                for (var i = 0; i < bonusRates.Count; i++)
                {
                    diff += bonusRates[i].CompareTo(other.bonusRates[i]);
                }
            }
            return diff;
        }
	}
}