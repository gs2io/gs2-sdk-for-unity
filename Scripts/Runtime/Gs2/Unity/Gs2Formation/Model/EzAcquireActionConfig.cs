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
using Gs2.Gs2Formation.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Formation.Model
{
	[Preserve]
	public class EzAcquireActionConfig
	{
		/** スロット名 */
		public string Name { get; set; }
		/** スタンプシートに使用するコンフィグ */
		public List<EzConfig> Config { get; set; }

		public EzAcquireActionConfig()
		{

		}

		public EzAcquireActionConfig(Gs2.Gs2Formation.Model.AcquireActionConfig @acquireActionConfig)
		{
			Name = @acquireActionConfig.name;
			Config = @acquireActionConfig.config != null ? @acquireActionConfig.config.Select(value =>
                {
                    return new EzConfig(value);
                }
			).ToList() : new List<EzConfig>(new EzConfig[] {});
		}

        public AcquireActionConfig ToModel()
        {
            return new AcquireActionConfig {
                name = Name,
                config = Config != null ? Config.Select(Value0 =>
                        {
                            return new Config
                            {
                                key = Value0.Key,
                                value = Value0.Value,
                            };
                        }
                ).ToList() : new List<Config>(new Config[] {}),
            };
        }
	}
}
