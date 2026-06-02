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
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Formation.Result
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzGetFormWithSignatureResult
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public Gs2.Unity.Gs2Formation.Model.EzForm Item;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Body;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Signature;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public Gs2.Unity.Gs2Formation.Model.EzMold Mold;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public Gs2.Unity.Gs2Formation.Model.EzMoldModel MoldModel;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public Gs2.Unity.Gs2Formation.Model.EzFormModel FormModel;

        public static EzGetFormWithSignatureResult FromModel(Gs2.Gs2Formation.Result.GetFormWithSignatureResult model)
        {
            return new EzGetFormWithSignatureResult {
                Item = model.Item == null ? null : Gs2.Unity.Gs2Formation.Model.EzForm.FromModel(model.Item),
                Body = model.Body == null ? null : model.Body,
                Signature = model.Signature == null ? null : model.Signature,
                Mold = model.Mold == null ? null : Gs2.Unity.Gs2Formation.Model.EzMold.FromModel(model.Mold),
                MoldModel = model.MoldModel == null ? null : Gs2.Unity.Gs2Formation.Model.EzMoldModel.FromModel(model.MoldModel),
                FormModel = model.FormModel == null ? null : Gs2.Unity.Gs2Formation.Model.EzFormModel.FromModel(model.FormModel),
            };
        }
    }
}