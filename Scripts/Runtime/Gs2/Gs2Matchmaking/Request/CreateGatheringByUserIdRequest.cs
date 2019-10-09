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
using Gs2.Gs2Matchmaking.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Matchmaking.Request
{
	[Preserve]
	public class CreateGatheringByUserIdRequest : Gs2Request<CreateGatheringByUserIdRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public CreateGatheringByUserIdRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
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
        public CreateGatheringByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** 自身のプレイヤー情報 */
        public Player player { set; get; }

        /**
         * 自身のプレイヤー情報を設定
         *
         * @param player 自身のプレイヤー情報
         * @return this
         */
        public CreateGatheringByUserIdRequest WithPlayer(Player player) {
            this.player = player;
            return this;
        }


        /** 募集条件 */
        public List<AttributeRange> attributeRanges { set; get; }

        /**
         * 募集条件を設定
         *
         * @param attributeRanges 募集条件
         * @return this
         */
        public CreateGatheringByUserIdRequest WithAttributeRanges(List<AttributeRange> attributeRanges) {
            this.attributeRanges = attributeRanges;
            return this;
        }


        /** 参加者 */
        public List<CapacityOfRole> capacityOfRoles { set; get; }

        /**
         * 参加者を設定
         *
         * @param capacityOfRoles 参加者
         * @return this
         */
        public CreateGatheringByUserIdRequest WithCapacityOfRoles(List<CapacityOfRole> capacityOfRoles) {
            this.capacityOfRoles = capacityOfRoles;
            return this;
        }


        /** 参加を許可するユーザIDリスト */
        public List<string> allowUserIds { set; get; }

        /**
         * 参加を許可するユーザIDリストを設定
         *
         * @param allowUserIds 参加を許可するユーザIDリスト
         * @return this
         */
        public CreateGatheringByUserIdRequest WithAllowUserIds(List<string> allowUserIds) {
            this.allowUserIds = allowUserIds;
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
        public CreateGatheringByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static CreateGatheringByUserIdRequest FromDict(JsonData data)
        {
            return new CreateGatheringByUserIdRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                userId = data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString(): null,
                player = data.Keys.Contains("player") && data["player"] != null ? Player.FromDict(data["player"]) : null,
                attributeRanges = data.Keys.Contains("attributeRanges") && data["attributeRanges"] != null ? data["attributeRanges"].Cast<JsonData>().Select(value =>
                    {
                        return AttributeRange.FromDict(value);
                    }
                ).ToList() : null,
                capacityOfRoles = data.Keys.Contains("capacityOfRoles") && data["capacityOfRoles"] != null ? data["capacityOfRoles"].Cast<JsonData>().Select(value =>
                    {
                        return CapacityOfRole.FromDict(value);
                    }
                ).ToList() : null,
                allowUserIds = data.Keys.Contains("allowUserIds") && data["allowUserIds"] != null ? data["allowUserIds"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}