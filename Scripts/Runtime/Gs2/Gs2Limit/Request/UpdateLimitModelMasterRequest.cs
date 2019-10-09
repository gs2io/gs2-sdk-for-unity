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
using Gs2.Gs2Limit.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Limit.Request
{
	[Preserve]
	public class UpdateLimitModelMasterRequest : Gs2Request<UpdateLimitModelMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateLimitModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 回数制限の種類名 */
        public string limitName { set; get; }

        /**
         * 回数制限の種類名を設定
         *
         * @param limitName 回数制限の種類名
         * @return this
         */
        public UpdateLimitModelMasterRequest WithLimitName(string limitName) {
            this.limitName = limitName;
            return this;
        }


        /** 回数制限の種類マスターの説明 */
        public string description { set; get; }

        /**
         * 回数制限の種類マスターの説明を設定
         *
         * @param description 回数制限の種類マスターの説明
         * @return this
         */
        public UpdateLimitModelMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** 回数制限の種類のメタデータ */
        public string metadata { set; get; }

        /**
         * 回数制限の種類のメタデータを設定
         *
         * @param metadata 回数制限の種類のメタデータ
         * @return this
         */
        public UpdateLimitModelMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** リセットタイミング */
        public string resetType { set; get; }

        /**
         * リセットタイミングを設定
         *
         * @param resetType リセットタイミング
         * @return this
         */
        public UpdateLimitModelMasterRequest WithResetType(string resetType) {
            this.resetType = resetType;
            return this;
        }


        /** リセットをする日にち */
        public int? resetDayOfMonth { set; get; }

        /**
         * リセットをする日にちを設定
         *
         * @param resetDayOfMonth リセットをする日にち
         * @return this
         */
        public UpdateLimitModelMasterRequest WithResetDayOfMonth(int? resetDayOfMonth) {
            this.resetDayOfMonth = resetDayOfMonth;
            return this;
        }


        /** リセットする曜日 */
        public string resetDayOfWeek { set; get; }

        /**
         * リセットする曜日を設定
         *
         * @param resetDayOfWeek リセットする曜日
         * @return this
         */
        public UpdateLimitModelMasterRequest WithResetDayOfWeek(string resetDayOfWeek) {
            this.resetDayOfWeek = resetDayOfWeek;
            return this;
        }


        /** リセット時刻 */
        public int? resetHour { set; get; }

        /**
         * リセット時刻を設定
         *
         * @param resetHour リセット時刻
         * @return this
         */
        public UpdateLimitModelMasterRequest WithResetHour(int? resetHour) {
            this.resetHour = resetHour;
            return this;
        }


    	[Preserve]
        public static UpdateLimitModelMasterRequest FromDict(JsonData data)
        {
            return new UpdateLimitModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                limitName = data.Keys.Contains("limitName") && data["limitName"] != null ? data["limitName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                resetType = data.Keys.Contains("resetType") && data["resetType"] != null ? data["resetType"].ToString(): null,
                resetDayOfMonth = data.Keys.Contains("resetDayOfMonth") && data["resetDayOfMonth"] != null ? (int?)int.Parse(data["resetDayOfMonth"].ToString()) : null,
                resetDayOfWeek = data.Keys.Contains("resetDayOfWeek") && data["resetDayOfWeek"] != null ? data["resetDayOfWeek"].ToString(): null,
                resetHour = data.Keys.Contains("resetHour") && data["resetHour"] != null ? (int?)int.Parse(data["resetHour"].ToString()) : null,
            };
        }

	}
}