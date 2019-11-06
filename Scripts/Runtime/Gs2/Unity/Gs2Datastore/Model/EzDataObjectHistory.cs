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
using Gs2.Gs2Datastore.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Datastore.Model
{
	[Preserve]
	public class EzDataObjectHistory
	{
		/** データオブジェクト履歴 */
		public string DataObjectHistoryId { get; set; }
		/** 世代ID */
		public string Generation { get; set; }
		/** データサイズ */
		public long ContentLength { get; set; }
		/** 作成日時 */
		public long CreatedAt { get; set; }

		public EzDataObjectHistory()
		{

		}

		public EzDataObjectHistory(Gs2.Gs2Datastore.Model.DataObjectHistory @dataObjectHistory)
		{
			DataObjectHistoryId = @dataObjectHistory.dataObjectHistoryId;
			Generation = @dataObjectHistory.generation;
			ContentLength = @dataObjectHistory.contentLength.HasValue ? @dataObjectHistory.contentLength.Value : 0;
			CreatedAt = @dataObjectHistory.createdAt.HasValue ? @dataObjectHistory.createdAt.Value : 0;
		}

        public DataObjectHistory ToModel()
        {
            return new DataObjectHistory {
                dataObjectHistoryId = DataObjectHistoryId,
                generation = Generation,
                contentLength = ContentLength,
                createdAt = CreatedAt,
            };
        }
	}
}
