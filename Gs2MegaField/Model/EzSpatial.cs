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
	public class EzSpatial
	{
		[SerializeField]
		public string UserId;
		[SerializeField]
		public string AreaModelName;
		[SerializeField]
		public string LayerModelName;
		[SerializeField]
		public Gs2.Unity.Gs2MegaField.Model.EzPosition Position;
		[SerializeField]
		public Gs2.Unity.Gs2MegaField.Model.EzVector Vector;

        public Gs2.Gs2MegaField.Model.Spatial ToModel()
        {
            return new Gs2.Gs2MegaField.Model.Spatial {
                UserId = UserId,
                AreaModelName = AreaModelName,
                LayerModelName = LayerModelName,
                Position = Position?.ToModel(),
                Vector = Vector?.ToModel(),
            };
        }

        public static EzSpatial FromModel(Gs2.Gs2MegaField.Model.Spatial model)
        {
            return new EzSpatial {
                UserId = model.UserId == null ? null : model.UserId,
                AreaModelName = model.AreaModelName == null ? null : model.AreaModelName,
                LayerModelName = model.LayerModelName == null ? null : model.LayerModelName,
                Position = model.Position == null ? null : Gs2.Unity.Gs2MegaField.Model.EzPosition.FromModel(model.Position),
                Vector = model.Vector == null ? null : Gs2.Unity.Gs2MegaField.Model.EzVector.FromModel(model.Vector),
            };
        }
    }
}