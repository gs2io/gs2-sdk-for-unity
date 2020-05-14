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
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Version.Model
{
	[Preserve]
	[System.Serializable]
	public class EzTargetVersion
	{
		/** バージョンの名前 */
		[UnityEngine.SerializeField]
		public string VersionName;
		/** バージョン */
		[UnityEngine.SerializeField]
		public EzVersion Version;
		/** ボディ */
		[UnityEngine.SerializeField]
		public string Body;
		/** 署名 */
		[UnityEngine.SerializeField]
		public string Signature;

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

        public virtual TargetVersion ToModel()
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

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.VersionName != null)
            {
                writer.WritePropertyName("versionName");
                writer.Write(this.VersionName);
            }
            if(this.Version != null)
            {
                writer.WritePropertyName("version");
                this.Version.WriteJson(writer);
            }
            if(this.Body != null)
            {
                writer.WritePropertyName("body");
                writer.Write(this.Body);
            }
            if(this.Signature != null)
            {
                writer.WritePropertyName("signature");
                writer.Write(this.Signature);
            }
            writer.WriteObjectEnd();
        }
	}
}
