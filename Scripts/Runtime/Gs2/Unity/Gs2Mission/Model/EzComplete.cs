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
using Gs2.Gs2Mission.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Mission.Model
{
	[Preserve]
	public class EzComplete
	{
		/** ミッショングループ名 */
		public string MissionGroupName { get; set; }
		/** 達成済みのタスク名リスト */
		public List<string> CompletedMissionTaskNames { get; set; }
		/** 報酬の受け取り済みのタスク名リスト */
		public List<string> ReceivedMissionTaskNames { get; set; }

		public EzComplete()
		{

		}

		public EzComplete(Gs2.Gs2Mission.Model.Complete @complete)
		{
			MissionGroupName = @complete.missionGroupName;
			CompletedMissionTaskNames = @complete.completedMissionTaskNames != null ? @complete.completedMissionTaskNames.Select(value =>
                {
                    return value;
                }
			).ToList() : new List<string>(new string[] {});
			ReceivedMissionTaskNames = @complete.receivedMissionTaskNames != null ? @complete.receivedMissionTaskNames.Select(value =>
                {
                    return value;
                }
			).ToList() : new List<string>(new string[] {});
		}

        public Complete ToModel()
        {
            return new Complete {
                missionGroupName = MissionGroupName,
                completedMissionTaskNames = CompletedMissionTaskNames != null ? CompletedMissionTaskNames.Select(Value0 =>
                        {
                            return Value0;
                        }
                ).ToList() : new List<string>(new string[] {}),
                receivedMissionTaskNames = ReceivedMissionTaskNames != null ? ReceivedMissionTaskNames.Select(Value0 =>
                        {
                            return Value0;
                        }
                ).ToList() : new List<string>(new string[] {}),
            };
        }
	}
}
