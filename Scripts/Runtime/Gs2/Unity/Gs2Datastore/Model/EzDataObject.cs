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
	public class EzDataObject
	{
		/** データオブジェクト */
		public string DataObjectId { get; set; }
		/** データの名前 */
		public string Name { get; set; }
		/** ユーザーID */
		public string UserId { get; set; }
		/** ファイルのアクセス権 */
		public string Scope { get; set; }
		/** 公開するユーザIDリスト */
		public List<string> AllowUserIds { get; set; }
		/** 状態 */
		public string Status { get; set; }
		/** データの世代 */
		public string Generation { get; set; }
		/** 作成日時 */
		public long CreatedAt { get; set; }
		/** 最終更新日時 */
		public long UpdatedAt { get; set; }

		public EzDataObject()
		{

		}

		public EzDataObject(Gs2.Gs2Datastore.Model.DataObject @dataObject)
		{
			DataObjectId = @dataObject.dataObjectId;
			Name = @dataObject.name;
			UserId = @dataObject.userId;
			Scope = @dataObject.scope;
			AllowUserIds = @dataObject.allowUserIds != null ? @dataObject.allowUserIds.Select(value =>
                {
                    return value;
                }
			).ToList() : new List<string>(new string[] {});
			Status = @dataObject.status;
			Generation = @dataObject.generation;
			CreatedAt = @dataObject.createdAt.HasValue ? @dataObject.createdAt.Value : 0;
			UpdatedAt = @dataObject.updatedAt.HasValue ? @dataObject.updatedAt.Value : 0;
		}

        public DataObject ToModel()
        {
            return new DataObject {
                dataObjectId = DataObjectId,
                name = Name,
                userId = UserId,
                scope = Scope,
                allowUserIds = AllowUserIds != null ? AllowUserIds.Select(Value0 =>
                        {
                            return Value0;
                        }
                ).ToList() : new List<string>(new string[] {}),
                status = Status,
                generation = Generation,
                createdAt = CreatedAt,
                updatedAt = UpdatedAt,
            };
        }
	}
}
