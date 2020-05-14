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
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Datastore.Model
{
	[Preserve]
	[System.Serializable]
	public class EzDataObject
	{
		/** データオブジェクト */
		[UnityEngine.SerializeField]
		public string DataObjectId;
		/** データの名前 */
		[UnityEngine.SerializeField]
		public string Name;
		/** ユーザーID */
		[UnityEngine.SerializeField]
		public string UserId;
		/** ファイルのアクセス権 */
		[UnityEngine.SerializeField]
		public string Scope;
		/** 公開するユーザIDリスト */
		[UnityEngine.SerializeField]
		public List<string> AllowUserIds;
		/** 状態 */
		[UnityEngine.SerializeField]
		public string Status;
		/** データの世代 */
		[UnityEngine.SerializeField]
		public string Generation;
		/** 作成日時 */
		[UnityEngine.SerializeField]
		public long CreatedAt;
		/** 最終更新日時 */
		[UnityEngine.SerializeField]
		public long UpdatedAt;

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

        public virtual DataObject ToModel()
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

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.DataObjectId != null)
            {
                writer.WritePropertyName("dataObjectId");
                writer.Write(this.DataObjectId);
            }
            if(this.Name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.Name);
            }
            if(this.UserId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.UserId);
            }
            if(this.Scope != null)
            {
                writer.WritePropertyName("scope");
                writer.Write(this.Scope);
            }
            if(this.AllowUserIds != null)
            {
                writer.WritePropertyName("allowUserIds");
                writer.WriteArrayStart();
                foreach(var item in this.AllowUserIds)
                {
                    writer.Write(item);
                }
                writer.WriteArrayEnd();
            }
            if(this.Status != null)
            {
                writer.WritePropertyName("status");
                writer.Write(this.Status);
            }
            if(this.Generation != null)
            {
                writer.WritePropertyName("generation");
                writer.Write(this.Generation);
            }
            writer.WritePropertyName("createdAt");
            writer.Write(this.CreatedAt);
            writer.WritePropertyName("updatedAt");
            writer.Write(this.UpdatedAt);
            writer.WriteObjectEnd();
        }
	}
}
