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
using Gs2.Gs2Realtime.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Realtime.Request
{
	[Preserve]
	public class WantRoomRequest : Gs2Request<WantRoomRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public WantRoomRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** ルーム名 */
        public string name { set; get; }

        /**
         * ルーム名を設定
         *
         * @param name ルーム名
         * @return this
         */
        public WantRoomRequest WithName(string name) {
            this.name = name;
            return this;
        }


        /** ルームの作成が終わったときに通知を受けるユーザIDリスト */
        public List<string> notificationUserIds { set; get; }

        /**
         * ルームの作成が終わったときに通知を受けるユーザIDリストを設定
         *
         * @param notificationUserIds ルームの作成が終わったときに通知を受けるユーザIDリスト
         * @return this
         */
        public WantRoomRequest WithNotificationUserIds(List<string> notificationUserIds) {
            this.notificationUserIds = notificationUserIds;
            return this;
        }


    	[Preserve]
        public static WantRoomRequest FromDict(JsonData data)
        {
            return new WantRoomRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
                notificationUserIds = data.Keys.Contains("notificationUserIds") && data["notificationUserIds"] != null ? data["notificationUserIds"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null,
            };
        }

	}
}