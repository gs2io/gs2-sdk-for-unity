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
	public class EzTargetVersion
	{
		/** バージョンの名前 */
		public string VersionName { get; set; }
		/** バージョン */
		public EzVersion Version { get; set; }
		/** ボディ */
		public string Body { get; set; }
		/** 署名 */
		public string Signature { get; set; }

		public EzTargetVersion()
		{

		}

		public EzTargetVersion(Gs2.Gs2Version.Model.TargetVersion @targetVersion)
		{
			VersionName = @targetVersion.versionName;
			Version = @targetVersion.version != null ? new EzVersion(@targetVersion.version) : null;
			Body = @targetVersion.body;
			Signature = @targetVersion.signature;
		}

        public TargetVersion ToModel()
        {
            return new TargetVersion {
                versionName = VersionName,
                version = new Version_ {
                    major = Version.Major,
                    minor = Version.Minor,
                    micro = Version.Micro,
                },
                body = Body,
                signature = Signature,
            };
        }
	}
}
