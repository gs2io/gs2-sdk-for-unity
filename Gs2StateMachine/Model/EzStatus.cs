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
using Gs2.Gs2StateMachine.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2StateMachine.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzStatus
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string StatusId;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Name;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string EnableSpeculativeExecution;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string StateMachineDefinition;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public Gs2.Unity.Gs2StateMachine.Model.EzRandomStatus RandomStatus;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<Gs2.Unity.Gs2StateMachine.Model.EzStackEntry> Stacks;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<Gs2.Unity.Gs2StateMachine.Model.EzVariable> Variables;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Status;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string LastError;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int TransitionCount;

        public Gs2.Gs2StateMachine.Model.Status ToModel()
        {
            return new Gs2.Gs2StateMachine.Model.Status {
                StatusId = StatusId,
                Name = Name,
                EnableSpeculativeExecution = EnableSpeculativeExecution,
                StateMachineDefinition = StateMachineDefinition,
                RandomStatus = RandomStatus?.ToModel(),
                Stacks = Stacks?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                Variables = Variables?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                Value = Status,
                LastError = LastError,
                TransitionCount = TransitionCount,
            };
        }

        public static EzStatus FromModel(Gs2.Gs2StateMachine.Model.Status model)
        {
            return new EzStatus {
                StatusId = model.StatusId == null ? null : model.StatusId,
                Name = model.Name == null ? null : model.Name,
                EnableSpeculativeExecution = model.EnableSpeculativeExecution == null ? null : model.EnableSpeculativeExecution,
                StateMachineDefinition = model.StateMachineDefinition == null ? null : model.StateMachineDefinition,
                RandomStatus = model.RandomStatus == null ? null : Gs2.Unity.Gs2StateMachine.Model.EzRandomStatus.FromModel(model.RandomStatus),
                Stacks = model.Stacks == null ? new List<Gs2.Unity.Gs2StateMachine.Model.EzStackEntry>() : model.Stacks.Select(v => {
                    return Gs2.Unity.Gs2StateMachine.Model.EzStackEntry.FromModel(v);
                }).ToList(),
                Variables = model.Variables == null ? new List<Gs2.Unity.Gs2StateMachine.Model.EzVariable>() : model.Variables.Select(v => {
                    return Gs2.Unity.Gs2StateMachine.Model.EzVariable.FromModel(v);
                }).ToList(),
                Status = model.Value == null ? null : model.Value,
                LastError = model.LastError == null ? null : model.LastError,
                TransitionCount = model.TransitionCount ?? 0,
            };
        }
    }
}