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

namespace Gs2.Gs2Stamina.Model
{
	[Preserve]
	public class Stamina
	{

        /** スタミナ */
        public string staminaId { set; get; }

        /**
         * スタミナを設定
         *
         * @param staminaId スタミナ
         * @return this
         */
        public Stamina WithStaminaId(string staminaId) {
            this.staminaId = staminaId;
            return this;
        }

        /** スタミナモデルの名前 */
        public string staminaName { set; get; }

        /**
         * スタミナモデルの名前を設定
         *
         * @param staminaName スタミナモデルの名前
         * @return this
         */
        public Stamina WithStaminaName(string staminaName) {
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
        public Stamina WithUserId(string userId) {
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
        public Stamina WithValue(int? value) {
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
        public Stamina WithMaxValue(int? maxValue) {
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
        public Stamina WithRecoverIntervalMinutes(int? recoverIntervalMinutes) {
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
        public Stamina WithRecoverValue(int? recoverValue) {
            this.recoverValue = recoverValue;
            return this;
        }

        /** スタミナの最大値を超えて格納されているスタミナ値 */
        public int? overflowValue { set; get; }

        /**
         * スタミナの最大値を超えて格納されているスタミナ値を設定
         *
         * @param overflowValue スタミナの最大値を超えて格納されているスタミナ値
         * @return this
         */
        public Stamina WithOverflowValue(int? overflowValue) {
            this.overflowValue = overflowValue;
            return this;
        }

        /** 次回スタミナが回復する時間 */
        public long? nextRecoverAt { set; get; }

        /**
         * 次回スタミナが回復する時間を設定
         *
         * @param nextRecoverAt 次回スタミナが回復する時間
         * @return this
         */
        public Stamina WithNextRecoverAt(long? nextRecoverAt) {
            this.nextRecoverAt = nextRecoverAt;
            return this;
        }

        /** 作成日時 */
        public long? lastRecoveredAt { set; get; }

        /**
         * 作成日時を設定
         *
         * @param lastRecoveredAt 作成日時
         * @return this
         */
        public Stamina WithLastRecoveredAt(long? lastRecoveredAt) {
            this.lastRecoveredAt = lastRecoveredAt;
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
        public Stamina WithCreatedAt(long? createdAt) {
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
        public Stamina WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.staminaId != null)
            {
                writer.WritePropertyName("staminaId");
                writer.Write(this.staminaId);
            }
            if(this.staminaName != null)
            {
                writer.WritePropertyName("staminaName");
                writer.Write(this.staminaName);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.value.HasValue)
            {
                writer.WritePropertyName("value");
                writer.Write(this.value.Value);
            }
            if(this.maxValue.HasValue)
            {
                writer.WritePropertyName("maxValue");
                writer.Write(this.maxValue.Value);
            }
            if(this.recoverIntervalMinutes.HasValue)
            {
                writer.WritePropertyName("recoverIntervalMinutes");
                writer.Write(this.recoverIntervalMinutes.Value);
            }
            if(this.recoverValue.HasValue)
            {
                writer.WritePropertyName("recoverValue");
                writer.Write(this.recoverValue.Value);
            }
            if(this.overflowValue.HasValue)
            {
                writer.WritePropertyName("overflowValue");
                writer.Write(this.overflowValue.Value);
            }
            if(this.nextRecoverAt.HasValue)
            {
                writer.WritePropertyName("nextRecoverAt");
                writer.Write(this.nextRecoverAt.Value);
            }
            if(this.lastRecoveredAt.HasValue)
            {
                writer.WritePropertyName("lastRecoveredAt");
                writer.Write(this.lastRecoveredAt.Value);
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

    	[Preserve]
        public static Stamina FromDict(JsonData data)
        {
            return new Stamina()
                .WithStaminaId(data.Keys.Contains("staminaId") && data["staminaId"] != null ? data["staminaId"].ToString() : null)
                .WithStaminaName(data.Keys.Contains("staminaName") && data["staminaName"] != null ? data["staminaName"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithValue(data.Keys.Contains("value") && data["value"] != null ? (int?)int.Parse(data["value"].ToString()) : null)
                .WithMaxValue(data.Keys.Contains("maxValue") && data["maxValue"] != null ? (int?)int.Parse(data["maxValue"].ToString()) : null)
                .WithRecoverIntervalMinutes(data.Keys.Contains("recoverIntervalMinutes") && data["recoverIntervalMinutes"] != null ? (int?)int.Parse(data["recoverIntervalMinutes"].ToString()) : null)
                .WithRecoverValue(data.Keys.Contains("recoverValue") && data["recoverValue"] != null ? (int?)int.Parse(data["recoverValue"].ToString()) : null)
                .WithOverflowValue(data.Keys.Contains("overflowValue") && data["overflowValue"] != null ? (int?)int.Parse(data["overflowValue"].ToString()) : null)
                .WithNextRecoverAt(data.Keys.Contains("nextRecoverAt") && data["nextRecoverAt"] != null ? (long?)long.Parse(data["nextRecoverAt"].ToString()) : null)
                .WithLastRecoveredAt(data.Keys.Contains("lastRecoveredAt") && data["lastRecoveredAt"] != null ? (long?)long.Parse(data["lastRecoveredAt"].ToString()) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}