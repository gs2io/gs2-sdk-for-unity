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
using Gs2.Gs2Exchange.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Exchange.Model
{
	[Preserve]
	[System.Serializable]
	public class EzRateModel
	{
		/** 交換レートの種類名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** 交換レートの種類のメタデータ */
		[UnityEngine.SerializeField]
		public string Metadata;
		/** 消費アクションリスト */
		[UnityEngine.SerializeField]
		public List<EzConsumeAction> ConsumeActions;
		/** 入手アクションリスト */
		[UnityEngine.SerializeField]
		public List<EzAcquireAction> AcquireActions;

		public EzRateModel()
		{

		}

		public EzRateModel(Gs2.Gs2Exchange.Model.RateModel @rateModel)
		{
			Name = @rateModel.name;
			Metadata = @rateModel.metadata;
			ConsumeActions = @rateModel.consumeActions != null ? @rateModel.consumeActions.Select(value =>
                {
                    return new EzConsumeAction(value);
                }
			).ToList() : new List<EzConsumeAction>(new EzConsumeAction[] {});
			AcquireActions = @rateModel.acquireActions != null ? @rateModel.acquireActions.Select(value =>
                {
                    return new EzAcquireAction(value);
                }
			).ToList() : new List<EzAcquireAction>(new EzAcquireAction[] {});
		}

        public virtual RateModel ToModel()
        {
            return new RateModel {
                name = Name,
                metadata = Metadata,
                consumeActions = ConsumeActions != null ? ConsumeActions.Select(Value0 =>
                        {
                            return new ConsumeAction
                            {
                                action = Value0.Action,
                                request = Value0.Request,
                            };
                        }
                ).ToList() : new List<ConsumeAction>(new ConsumeAction[] {}),
                acquireActions = AcquireActions != null ? AcquireActions.Select(Value0 =>
                        {
                            return new AcquireAction
                            {
                                action = Value0.Action,
                                request = Value0.Request,
                            };
                        }
                ).ToList() : new List<AcquireAction>(new AcquireAction[] {}),
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
            if(this.ConsumeActions != null)
            {
                writer.WritePropertyName("consumeActions");
                writer.WriteArrayStart();
                foreach(var item in this.ConsumeActions)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.AcquireActions != null)
            {
                writer.WritePropertyName("acquireActions");
                writer.WriteArrayStart();
                foreach(var item in this.AcquireActions)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
        }
	}
}
