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
	public class EzEmitEvent
	{
		[SerializeField]
		public string Event;
		[SerializeField]
		public string Parameters;
		[SerializeField]
		public long Timestamp;

        public Gs2.Gs2StateMachine.Model.EmitEvent ToModel()
        {
            return new Gs2.Gs2StateMachine.Model.EmitEvent {
                Event = Event,
                Parameters = Parameters,
                Timestamp = Timestamp,
            };
        }

        public static EzEmitEvent FromModel(Gs2.Gs2StateMachine.Model.EmitEvent model)
        {
            return new EzEmitEvent {
                Event = model.Event == null ? null : model.Event,
                Parameters = model.Parameters == null ? null : model.Parameters,
                Timestamp = model.Timestamp ?? 0,
            };
        }
    }
}