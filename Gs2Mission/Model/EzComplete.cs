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
using Gs2.Gs2Mission.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Mission.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzComplete
	{
		[SerializeField]
		public string MissionGroupName;
		[SerializeField]
		public List<string> CompletedMissionTaskNames;
		[SerializeField]
		public List<string> ReceivedMissionTaskNames;

        public Gs2.Gs2Mission.Model.Complete ToModel()
        {
            return new Gs2.Gs2Mission.Model.Complete {
                MissionGroupName = MissionGroupName,
                CompletedMissionTaskNames = CompletedMissionTaskNames?.Select(v => {
                    return v;
                }).ToArray(),
                ReceivedMissionTaskNames = ReceivedMissionTaskNames?.Select(v => {
                    return v;
                }).ToArray(),
            };
        }

        public static EzComplete FromModel(Gs2.Gs2Mission.Model.Complete model)
        {
            return new EzComplete {
                MissionGroupName = model.MissionGroupName == null ? null : model.MissionGroupName,
                CompletedMissionTaskNames = model.CompletedMissionTaskNames == null ? new List<string>() : model.CompletedMissionTaskNames.Select(v => {
                    return v;
                }).ToList(),
                ReceivedMissionTaskNames = model.ReceivedMissionTaskNames == null ? new List<string>() : model.ReceivedMissionTaskNames.Select(v => {
                    return v;
                }).ToList(),
            };
        }
    }
}