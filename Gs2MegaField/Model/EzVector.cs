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

using Gs2.Gs2MegaField.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2MegaField.Model
{
	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzVector
	{
		[SerializeField]
		public double X;
		[SerializeField]
		public double Y;
		[SerializeField]
		public double Z;

        public Gs2.Gs2MegaField.Model.Vector ToModel()
        {
            return new Gs2.Gs2MegaField.Model.Vector {
                X = X,
                Y = Y,
                Z = Z,
            };
        }

        public static EzVector FromModel(Gs2.Gs2MegaField.Model.Vector model)
        {
            return new EzVector {
                X = model.X ?? 0,
                Y = model.Y ?? 0,
                Z = model.Z ?? 0,
            };
        }
    }
}