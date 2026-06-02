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

using Gs2.Gs2News.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2News.Result
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzListNewsesResult
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<Gs2.Unity.Gs2News.Model.EzNews> Items;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string ContentHash;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string TemplateHash;

        public static EzListNewsesResult FromModel(Gs2.Gs2News.Result.DescribeNewsResult model)
        {
            return new EzListNewsesResult {
                Items = model.Items == null ? new List<Gs2.Unity.Gs2News.Model.EzNews>() : model.Items.Select(v => {
                    return Gs2.Unity.Gs2News.Model.EzNews.FromModel(v);
                }).ToList(),
                ContentHash = model.ContentHash == null ? null : model.ContentHash,
                TemplateHash = model.TemplateHash == null ? null : model.TemplateHash,
            };
        }
    }
}