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
using Gs2.Gs2Mission.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Mission.Request
{
	[Preserve]
	public class GetMissionTaskModelRequest : Gs2Request<GetMissionTaskModelRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public GetMissionTaskModelRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** グループ名 */
        public string missionGroupName { set; get; }

        /**
         * グループ名を設定
         *
         * @param missionGroupName グループ名
         * @return this
         */
        public GetMissionTaskModelRequest WithMissionGroupName(string missionGroupName) {
            this.missionGroupName = missionGroupName;
            return this;
        }


        /** タスク名 */
        public string missionTaskName { set; get; }

        /**
         * タスク名を設定
         *
         * @param missionTaskName タスク名
         * @return this
         */
        public GetMissionTaskModelRequest WithMissionTaskName(string missionTaskName) {
            this.missionTaskName = missionTaskName;
            return this;
        }


    	[Preserve]
        public static GetMissionTaskModelRequest FromDict(JsonData data)
        {
            return new GetMissionTaskModelRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                missionGroupName = data.Keys.Contains("missionGroupName") && data["missionGroupName"] != null ? data["missionGroupName"].ToString(): null,
                missionTaskName = data.Keys.Contains("missionTaskName") && data["missionTaskName"] != null ? data["missionTaskName"].ToString(): null,
            };
        }

	}
}