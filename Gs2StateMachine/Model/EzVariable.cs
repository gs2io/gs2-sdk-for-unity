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
	public class EzVariable
	{
		[SerializeField]
		public string StateMachineName;
		[SerializeField]
		public string Value;

        public Gs2.Gs2StateMachine.Model.Variable ToModel()
        {
            return new Gs2.Gs2StateMachine.Model.Variable {
                StateMachineName = StateMachineName,
                Value = Value,
            };
        }

        public static EzVariable FromModel(Gs2.Gs2StateMachine.Model.Variable model)
        {
            return new EzVariable {
                StateMachineName = model.StateMachineName == null ? null : model.StateMachineName,
                Value = model.Value == null ? null : model.Value,
            };
        }
    }
}