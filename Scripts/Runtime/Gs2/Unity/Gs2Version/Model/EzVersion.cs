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
using Gs2.Gs2Version.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Version.Model
{
	[Preserve]
	public class EzVersion
	{
		/** メジャーバージョン */
		public int Major { get; set; }
		/** マイナーバージョン */
		public int Minor { get; set; }
		/** マイクロバージョン */
		public int Micro { get; set; }

		public EzVersion()
		{

		}

		public EzVersion(Gs2.Gs2Version.Model.Version_ @version)
		{
			Major = @version.major.HasValue ? @version.major.Value : 0;
			Minor = @version.minor.HasValue ? @version.minor.Value : 0;
			Micro = @version.micro.HasValue ? @version.micro.Value : 0;
		}

        public Version_ ToModel()
        {
            return new Version_ {
                major = Major,
                minor = Minor,
                micro = Micro,
            };
        }
	}
}
