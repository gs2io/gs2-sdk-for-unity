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
	public class EzAcceptVersion
	{
		/** 承認したバージョン名 */
		public string VersionName { get; set; }
		/** ユーザーID */
		public string UserId { get; set; }
		/** 承認したバージョン */
		public EzVersion Version { get; set; }

		public EzAcceptVersion()
		{

		}

		public EzAcceptVersion(Gs2.Gs2Version.Model.AcceptVersion @acceptVersion)
		{
			VersionName = @acceptVersion.versionName;
			UserId = @acceptVersion.userId;
			Version = @acceptVersion.version != null ? new EzVersion(@acceptVersion.version) : null;
		}

        public AcceptVersion ToModel()
        {
            return new AcceptVersion {
                versionName = VersionName,
                userId = UserId,
                version = new Version_ {
                    major = Version.Major,
                    minor = Version.Minor,
                    micro = Version.Micro,
                },
            };
        }
	}
}
