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

using Gs2.Gs2Formation.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Formation.Model
{
	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzMoldModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public Gs2.Unity.Gs2Formation.Model.EzFormModel FormModel;
		[SerializeField]
		public int InitialMaxCapacity;
		[SerializeField]
		public int MaxCapacity;

        public Gs2.Gs2Formation.Model.MoldModel ToModel()
        {
            return new Gs2.Gs2Formation.Model.MoldModel {
                Name = Name,
                Metadata = Metadata,
                FormModel = FormModel?.ToModel(),
                InitialMaxCapacity = InitialMaxCapacity,
                MaxCapacity = MaxCapacity,
            };
        }

        public static EzMoldModel FromModel(Gs2.Gs2Formation.Model.MoldModel model)
        {
            return new EzMoldModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                FormModel = model.FormModel == null ? null : Gs2.Unity.Gs2Formation.Model.EzFormModel.FromModel(model.FormModel),
                InitialMaxCapacity = model.InitialMaxCapacity ?? 0,
                MaxCapacity = model.MaxCapacity ?? 0,
            };
        }
    }
}