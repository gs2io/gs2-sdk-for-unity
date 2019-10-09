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
using Gs2.Gs2Stamina.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Stamina.Request
{
	[Preserve]
	public class UpdateStaminaByUserIdRequest : Gs2Request<UpdateStaminaByUserIdRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateStaminaByUserIdRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** スタミナの種類名 */
        public string staminaName { set; get; }

        /**
         * スタミナの種類名を設定
         *
         * @param staminaName スタミナの種類名
         * @return this
         */
        public UpdateStaminaByUserIdRequest WithStaminaName(string staminaName) {
            this.staminaName = staminaName;
            return this;
        }


        /** ユーザーID */
        public string userId { set; get; }

        /**
         * ユーザーIDを設定
         *
         * @param userId ユーザーID
         * @return this
         */
        public UpdateStaminaByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** 最終更新時におけるスタミナ値 */
        public int? value { set; get; }

        /**
         * 最終更新時におけるスタミナ値を設定
         *
         * @param value 最終更新時におけるスタミナ値
         * @return this
         */
        public UpdateStaminaByUserIdRequest WithValue(int? value) {
            this.value = value;
            return this;
        }


        /** スタミナの最大値 */
        public int? maxValue { set; get; }

        /**
         * スタミナの最大値を設定
         *
         * @param maxValue スタミナの最大値
         * @return this
         */
        public UpdateStaminaByUserIdRequest WithMaxValue(int? maxValue) {
            this.maxValue = maxValue;
            return this;
        }


        /** スタミナの回復間隔(分) */
        public int? recoverIntervalMinutes { set; get; }

        /**
         * スタミナの回復間隔(分)を設定
         *
         * @param recoverIntervalMinutes スタミナの回復間隔(分)
         * @return this
         */
        public UpdateStaminaByUserIdRequest WithRecoverIntervalMinutes(int? recoverIntervalMinutes) {
            this.recoverIntervalMinutes = recoverIntervalMinutes;
            return this;
        }


        /** スタミナの回復量 */
        public int? recoverValue { set; get; }

        /**
         * スタミナの回復量を設定
         *
         * @param recoverValue スタミナの回復量
         * @return this
         */
        public UpdateStaminaByUserIdRequest WithRecoverValue(int? recoverValue) {
            this.recoverValue = recoverValue;
            return this;
        }


        /** 重複実行回避機能に使用するID */
        public string duplicationAvoider { set; get; }

        /**
         * 重複実行回避機能に使用するIDを設定
         *
         * @param duplicationAvoider 重複実行回避機能に使用するID
         * @return this
         */
        public UpdateStaminaByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static UpdateStaminaByUserIdRequest FromDict(JsonData data)
        {
            return new UpdateStaminaByUserIdRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                staminaName = data.Keys.Contains("staminaName") && data["staminaName"] != null ? data["staminaName"].ToString(): null,
                userId = data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString(): null,
                value = data.Keys.Contains("value") && data["value"] != null ? (int?)int.Parse(data["value"].ToString()) : null,
                maxValue = data.Keys.Contains("maxValue") && data["maxValue"] != null ? (int?)int.Parse(data["maxValue"].ToString()) : null,
                recoverIntervalMinutes = data.Keys.Contains("recoverIntervalMinutes") && data["recoverIntervalMinutes"] != null ? (int?)int.Parse(data["recoverIntervalMinutes"].ToString()) : null,
                recoverValue = data.Keys.Contains("recoverValue") && data["recoverValue"] != null ? (int?)int.Parse(data["recoverValue"].ToString()) : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}