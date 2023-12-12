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
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2StateMachine.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzEvent
	{
		[SerializeField]
		public string EventType;
		[SerializeField]
		public Gs2.Unity.Gs2StateMachine.Model.EzChangeStateEvent ChangeStateEvent;
		[SerializeField]
		public Gs2.Unity.Gs2StateMachine.Model.EzEmitEvent EmitEvent;

        public Gs2.Gs2StateMachine.Model.Event ToModel()
        {
            return new Gs2.Gs2StateMachine.Model.Event {
                EventType = EventType,
                ChangeStateEvent = ChangeStateEvent?.ToModel(),
                EmitEvent = EmitEvent?.ToModel(),
            };
        }

        public static EzEvent FromModel(Gs2.Gs2StateMachine.Model.Event model)
        {
            return new EzEvent {
                EventType = model.EventType == null ? null : model.EventType,
                ChangeStateEvent = model.ChangeStateEvent == null ? null : Gs2.Unity.Gs2StateMachine.Model.EzChangeStateEvent.FromModel(model.ChangeStateEvent),
                EmitEvent = model.EmitEvent == null ? null : Gs2.Unity.Gs2StateMachine.Model.EzEmitEvent.FromModel(model.EmitEvent),
            };
        }
    }
}