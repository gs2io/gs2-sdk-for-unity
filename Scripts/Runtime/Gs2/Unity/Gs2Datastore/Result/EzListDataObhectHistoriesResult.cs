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
using Gs2.Unity.Gs2Datastore.Model;
using Gs2.Gs2Datastore.Result;
using UnityEngine.Scripting;

namespace Gs2.Unity.Gs2Datastore.Result
{
	[Preserve]
	public class EzListDataObhectHistoriesResult
	{
        /** データオブジェクト履歴のリスト */
        public List<EzDataObjectHistory> Items { get; private set; }

        /** リストの続きを取得するためのページトークン */
        public string NextPageToken { get; private set; }


        public EzListDataObhectHistoriesResult(
            DescribeDataObjectHistoriesResult result
        )
        {
            Items = new List<EzDataObjectHistory>();
            foreach (var item_ in result.items)
            {
                Items.Add(new EzDataObjectHistory(item_));
            }
            NextPageToken = result.nextPageToken;
        }
	}
}