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
using Gs2.Gs2Quest.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Quest.Model
{
	[Preserve]
	public class EzContents
	{
		/** クエストモデルのメタデータ */
		public string Metadata { get; set; }
		/** クエストクリア時の報酬 */
		public List<EzAcquireAction> CompleteAcquireActions { get; set; }

		public EzContents()
		{

		}

		public EzContents(Gs2.Gs2Quest.Model.Contents @contents)
		{
			Metadata = @contents.metadata;
			CompleteAcquireActions = @contents.completeAcquireActions != null ? @contents.completeAcquireActions.Select(value =>
                {
                    return new EzAcquireAction(value);
                }
			).ToList() : new List<EzAcquireAction>(new EzAcquireAction[] {});
		}

        public Contents ToModel()
        {
            return new Contents {
                metadata = Metadata,
                completeAcquireActions = CompleteAcquireActions != null ? CompleteAcquireActions.Select(Value0 =>
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
