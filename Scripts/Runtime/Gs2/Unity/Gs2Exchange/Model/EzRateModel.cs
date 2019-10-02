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
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Exchange.Model
{
	[Preserve]
	public class EzRateModel
	{
		/** 交換レートの種類名 */
		public string Name { get; set; }
		/** 交換レートの種類のメタデータ */
		public string Metadata { get; set; }
		/** 消費アクションリスト */
		public List<EzConsumeAction> ConsumeActions { get; set; }
		/** 入手アクションリスト */
		public List<EzAcquireAction> AcquireActions { get; set; }

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

        public RateModel ToModel()
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
	}
}
