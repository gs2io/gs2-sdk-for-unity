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
using Gs2.Gs2Distributor.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Distributor.Model
{
	[Preserve]
	[System.Serializable]
	public class EzDistributorModel
	{
		/** ディストリビューターの種類名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** ディストリビューターの種類のメタデータ */
		[UnityEngine.SerializeField]
		public string Metadata;
		/** 所持品がキャパシティをオーバーしたときに転送するプレゼントボックスのネームスペース のGRN */
		[UnityEngine.SerializeField]
		public string InboxNamespaceId;
		/** ディストリビューターを通して処理出来る対象のリソースGRNのホワイトリスト */
		[UnityEngine.SerializeField]
		public List<string> WhiteListTargetIds;

		public EzDistributorModel()
		{

		}

		public EzDistributorModel(Gs2.Gs2Distributor.Model.DistributorModel @distributorModel)
		{
			Name = @distributorModel.name;
			Metadata = @distributorModel.metadata;
			InboxNamespaceId = @distributorModel.inboxNamespaceId;
			WhiteListTargetIds = @distributorModel.whiteListTargetIds != null ? @distributorModel.whiteListTargetIds.Select(value =>
                {
                    return value;
                }
			).ToList() : new List<string>(new string[] {});
		}

        public virtual DistributorModel ToModel()
        {
            return new DistributorModel {
                name = Name,
                metadata = Metadata,
                inboxNamespaceId = InboxNamespaceId,
                whiteListTargetIds = WhiteListTargetIds != null ? WhiteListTargetIds.Select(Value0 =>
                        {
                            return Value0;
                        }
                ).ToList() : new List<string>(new string[] {}),
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.Name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.Name);
            }
            if(this.Metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.Metadata);
            }
            if(this.InboxNamespaceId != null)
            {
                writer.WritePropertyName("inboxNamespaceId");
                writer.Write(this.InboxNamespaceId);
            }
            if(this.WhiteListTargetIds != null)
            {
                writer.WritePropertyName("whiteListTargetIds");
                writer.WriteArrayStart();
                foreach(var item in this.WhiteListTargetIds)
                {
                    writer.Write(item);
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
        }
	}
}
