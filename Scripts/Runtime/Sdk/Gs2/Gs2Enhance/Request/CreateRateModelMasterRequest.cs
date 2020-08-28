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
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Enhance.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Enhance.Request
{
	[Preserve]
	[System.Serializable]
	public class CreateRateModelMasterRequest : Gs2Request<CreateRateModelMasterRequest>
	{

        /** ネームスペース名 */
		[UnityEngine.SerializeField]
        public string namespaceName;

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public CreateRateModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 強化レート名 */
		[UnityEngine.SerializeField]
        public string name;

        /**
         * 強化レート名を設定
         *
         * @param name 強化レート名
         * @return this
         */
        public CreateRateModelMasterRequest WithName(string name) {
            this.name = name;
            return this;
        }


        /** 強化レートマスターの説明 */
		[UnityEngine.SerializeField]
        public string description;

        /**
         * 強化レートマスターの説明を設定
         *
         * @param description 強化レートマスターの説明
         * @return this
         */
        public CreateRateModelMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** 強化レートのメタデータ */
		[UnityEngine.SerializeField]
        public string metadata;

        /**
         * 強化レートのメタデータを設定
         *
         * @param metadata 強化レートのメタデータ
         * @return this
         */
        public CreateRateModelMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** 強化対象に使用できるインベントリモデル のGRN */
		[UnityEngine.SerializeField]
        public string targetInventoryModelId;

        /**
         * 強化対象に使用できるインベントリモデル のGRNを設定
         *
         * @param targetInventoryModelId 強化対象に使用できるインベントリモデル のGRN
         * @return this
         */
        public CreateRateModelMasterRequest WithTargetInventoryModelId(string targetInventoryModelId) {
            this.targetInventoryModelId = targetInventoryModelId;
            return this;
        }


        /** GS2-Experience で入手した経験値を格納する プロパティID に付与するサフィックス */
		[UnityEngine.SerializeField]
        public string acquireExperienceSuffix;

        /**
         * GS2-Experience で入手した経験値を格納する プロパティID に付与するサフィックスを設定
         *
         * @param acquireExperienceSuffix GS2-Experience で入手した経験値を格納する プロパティID に付与するサフィックス
         * @return this
         */
        public CreateRateModelMasterRequest WithAcquireExperienceSuffix(string acquireExperienceSuffix) {
            this.acquireExperienceSuffix = acquireExperienceSuffix;
            return this;
        }


        /** 強化素材に使用できるインベントリモデル のGRN */
		[UnityEngine.SerializeField]
        public string materialInventoryModelId;

        /**
         * 強化素材に使用できるインベントリモデル のGRNを設定
         *
         * @param materialInventoryModelId 強化素材に使用できるインベントリモデル のGRN
         * @return this
         */
        public CreateRateModelMasterRequest WithMaterialInventoryModelId(string materialInventoryModelId) {
            this.materialInventoryModelId = materialInventoryModelId;
            return this;
        }


        /** 入手経験値を格納しているメタデータのJSON階層 */
		[UnityEngine.SerializeField]
        public List<string> acquireExperienceHierarchy;

        /**
         * 入手経験値を格納しているメタデータのJSON階層を設定
         *
         * @param acquireExperienceHierarchy 入手経験値を格納しているメタデータのJSON階層
         * @return this
         */
        public CreateRateModelMasterRequest WithAcquireExperienceHierarchy(List<string> acquireExperienceHierarchy) {
            this.acquireExperienceHierarchy = acquireExperienceHierarchy;
            return this;
        }


        /** 獲得できる経験値の種類マスター のGRN */
		[UnityEngine.SerializeField]
        public string experienceModelId;

        /**
         * 獲得できる経験値の種類マスター のGRNを設定
         *
         * @param experienceModelId 獲得できる経験値の種類マスター のGRN
         * @return this
         */
        public CreateRateModelMasterRequest WithExperienceModelId(string experienceModelId) {
            this.experienceModelId = experienceModelId;
            return this;
        }


        /** 経験値獲得量ボーナス */
		[UnityEngine.SerializeField]
        public List<BonusRate> bonusRates;

        /**
         * 経験値獲得量ボーナスを設定
         *
         * @param bonusRates 経験値獲得量ボーナス
         * @return this
         */
        public CreateRateModelMasterRequest WithBonusRates(List<BonusRate> bonusRates) {
            this.bonusRates = bonusRates;
            return this;
        }


    	[Preserve]
        public static CreateRateModelMasterRequest FromDict(JsonData data)
        {
            return new CreateRateModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                targetInventoryModelId = data.Keys.Contains("targetInventoryModelId") && data["targetInventoryModelId"] != null ? data["targetInventoryModelId"].ToString(): null,
                acquireExperienceSuffix = data.Keys.Contains("acquireExperienceSuffix") && data["acquireExperienceSuffix"] != null ? data["acquireExperienceSuffix"].ToString(): null,
                materialInventoryModelId = data.Keys.Contains("materialInventoryModelId") && data["materialInventoryModelId"] != null ? data["materialInventoryModelId"].ToString(): null,
                acquireExperienceHierarchy = data.Keys.Contains("acquireExperienceHierarchy") && data["acquireExperienceHierarchy"] != null ? data["acquireExperienceHierarchy"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null,
                experienceModelId = data.Keys.Contains("experienceModelId") && data["experienceModelId"] != null ? data["experienceModelId"].ToString(): null,
                bonusRates = data.Keys.Contains("bonusRates") && data["bonusRates"] != null ? data["bonusRates"].Cast<JsonData>().Select(value =>
                    {
                        return BonusRate.FromDict(value);
                    }
                ).ToList() : null,
            };
        }

	}
}