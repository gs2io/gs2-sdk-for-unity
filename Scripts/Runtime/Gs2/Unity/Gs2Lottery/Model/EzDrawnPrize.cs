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
using Gs2.Gs2Lottery.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Lottery.Model
{
	[Preserve]
	public class EzDrawnPrize
	{
		/** 入手アクションのリスト */
		public List<EzAcquireAction> AcquireActions { get; set; }

		public EzDrawnPrize()
		{

		}

		public EzDrawnPrize(Gs2.Gs2Lottery.Model.DrawnPrize @drawnPrize)
		{
			AcquireActions = @drawnPrize.acquireActions != null ? @drawnPrize.acquireActions.Select(value =>
                {
                    return new EzAcquireAction(value);
                }
			).ToList() : new List<EzAcquireAction>(new EzAcquireAction[] {});
		}

        public DrawnPrize ToModel()
        {
            return new DrawnPrize {
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
