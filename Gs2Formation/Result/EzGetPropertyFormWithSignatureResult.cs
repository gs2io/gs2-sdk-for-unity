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
	public class EzGetPropertyFormWithSignatureResult
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public Gs2.Unity.Gs2Formation.Model.EzPropertyForm Item;
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
		public Gs2.Unity.Gs2Formation.Model.EzPropertyFormModel PropertyFormModel;

        public static EzGetPropertyFormWithSignatureResult FromModel(Gs2.Gs2Formation.Result.GetPropertyFormWithSignatureResult model)
        {
            return new EzGetPropertyFormWithSignatureResult {
                Item = model.Item == null ? null : Gs2.Unity.Gs2Formation.Model.EzPropertyForm.FromModel(model.Item),
                Body = model.Body == null ? null : model.Body,
                Signature = model.Signature == null ? null : model.Signature,
                PropertyFormModel = model.PropertyFormModel == null ? null : Gs2.Unity.Gs2Formation.Model.EzPropertyFormModel.FromModel(model.PropertyFormModel),
            };
        }
    }
}