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
using Gs2.Gs2Experience.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Experience.Request
{
	[Preserve]
	[System.Serializable]
	public class CreateNamespaceRequest : Gs2Request<CreateNamespaceRequest>
	{

        /** ネームスペース名 */
		[UnityEngine.SerializeField]
        public string name;

        /**
         * ネームスペース名を設定
         *
         * @param name ネームスペース名
         * @return this
         */
        public CreateNamespaceRequest WithName(string name) {
            this.name = name;
            return this;
        }


        /** ネームスペースの説明 */
		[UnityEngine.SerializeField]
        public string description;

        /**
         * ネームスペースの説明を設定
         *
         * @param description ネームスペースの説明
         * @return this
         */
        public CreateNamespaceRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** ランクキャップ取得時 に実行されるスクリプト のGRN */
		[UnityEngine.SerializeField]
        public string experienceCapScriptId;

        /**
         * ランクキャップ取得時 に実行されるスクリプト のGRNを設定
         *
         * @param experienceCapScriptId ランクキャップ取得時 に実行されるスクリプト のGRN
         * @return this
         */
        public CreateNamespaceRequest WithExperienceCapScriptId(string experienceCapScriptId) {
            this.experienceCapScriptId = experienceCapScriptId;
            return this;
        }


        /** 経験値変化したときに実行するスクリプト */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Experience.Model.ScriptSetting changeExperienceScript;

        /**
         * 経験値変化したときに実行するスクリプトを設定
         *
         * @param changeExperienceScript 経験値変化したときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithChangeExperienceScript(global::Gs2.Gs2Experience.Model.ScriptSetting changeExperienceScript) {
            this.changeExperienceScript = changeExperienceScript;
            return this;
        }


        /** ランク変化したときに実行するスクリプト */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Experience.Model.ScriptSetting changeRankScript;

        /**
         * ランク変化したときに実行するスクリプトを設定
         *
         * @param changeRankScript ランク変化したときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithChangeRankScript(global::Gs2.Gs2Experience.Model.ScriptSetting changeRankScript) {
            this.changeRankScript = changeRankScript;
            return this;
        }


        /** ランクキャップ変化したときに実行するスクリプト */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Experience.Model.ScriptSetting changeRankCapScript;

        /**
         * ランクキャップ変化したときに実行するスクリプトを設定
         *
         * @param changeRankCapScript ランクキャップ変化したときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithChangeRankCapScript(global::Gs2.Gs2Experience.Model.ScriptSetting changeRankCapScript) {
            this.changeRankCapScript = changeRankCapScript;
            return this;
        }


        /** ログの出力設定 */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Experience.Model.LogSetting logSetting;

        /**
         * ログの出力設定を設定
         *
         * @param logSetting ログの出力設定
         * @return this
         */
        public CreateNamespaceRequest WithLogSetting(global::Gs2.Gs2Experience.Model.LogSetting logSetting) {
            this.logSetting = logSetting;
            return this;
        }


    	[Preserve]
        public static CreateNamespaceRequest FromDict(JsonData data)
        {
            return new CreateNamespaceRequest {
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                experienceCapScriptId = data.Keys.Contains("experienceCapScriptId") && data["experienceCapScriptId"] != null ? data["experienceCapScriptId"].ToString(): null,
                changeExperienceScript = data.Keys.Contains("changeExperienceScript") && data["changeExperienceScript"] != null ? global::Gs2.Gs2Experience.Model.ScriptSetting.FromDict(data["changeExperienceScript"]) : null,
                changeRankScript = data.Keys.Contains("changeRankScript") && data["changeRankScript"] != null ? global::Gs2.Gs2Experience.Model.ScriptSetting.FromDict(data["changeRankScript"]) : null,
                changeRankCapScript = data.Keys.Contains("changeRankCapScript") && data["changeRankCapScript"] != null ? global::Gs2.Gs2Experience.Model.ScriptSetting.FromDict(data["changeRankCapScript"]) : null,
                logSetting = data.Keys.Contains("logSetting") && data["logSetting"] != null ? global::Gs2.Gs2Experience.Model.LogSetting.FromDict(data["logSetting"]) : null,
            };
        }

	}
}