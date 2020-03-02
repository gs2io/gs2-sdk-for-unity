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
using System.Collections.Generic;
using Gs2.Core.Model;
using Gs2.Unity.Gs2News.Model;
using Gs2.Gs2News.Result;
using UnityEngine.Scripting;

namespace Gs2.Unity.Gs2News.Result
{
	[Preserve]
	public class EzListNewsesResult
	{
        /** お知らせ記事のリスト */
        public List<EzNews> Items { get; private set; }

        /** お知らせ記事データのハッシュ値 */
        public string ContentHash { get; private set; }

        /** テンプレートデータのハッシュ値 */
        public string TemplateHash { get; private set; }


        public EzListNewsesResult(
            DescribeNewsResult result
        )
        {
            Items = new List<EzNews>();
            foreach (var item_ in result.items)
            {
                Items.Add(new EzNews(item_));
            }
            ContentHash = result.contentHash;
            TemplateHash = result.templateHash;
        }
	}
}