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
	public class EzStatus
	{
		/** バージョン設定 */
		[UnityEngine.SerializeField]
		public EzVersionModel VersionModel;
		/** 現在のバージョン */
		[UnityEngine.SerializeField]
		public EzVersion CurrentVersion;

		public EzStatus()
		{

		}

		public EzStatus(Gs2.Gs2Version.Model.Status @status)
		{
			VersionModel = @status.versionModel != null ? new EzVersionModel(@status.versionModel) : null;
			CurrentVersion = @status.currentVersion != null ? new EzVersion(@status.currentVersion) : null;
		}

        public virtual Status ToModel()
        {
            return new Status {
                versionModel = new VersionModel {
                    name = VersionModel.Name,
                    metadata = VersionModel.Metadata,
                    warningVersion = new Version_ {
                        major = VersionModel.WarningVersion.Major,
                        minor = VersionModel.WarningVersion.Minor,
                        micro = VersionModel.WarningVersion.Micro,
                    },
                    errorVersion = new Version_ {
                        major = VersionModel.ErrorVersion.Major,
                        minor = VersionModel.ErrorVersion.Minor,
                        micro = VersionModel.ErrorVersion.Micro,
                    },
                    scope = VersionModel.Scope,
                    currentVersion = new Version_ {
                        major = VersionModel.CurrentVersion.Major,
                        minor = VersionModel.CurrentVersion.Minor,
                        micro = VersionModel.CurrentVersion.Micro,
                    },
                    needSignature = VersionModel.NeedSignature,
                },
                currentVersion = new Version_ {
                    major = CurrentVersion.Major,
                    minor = CurrentVersion.Minor,
                    micro = CurrentVersion.Micro,
                },
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.VersionModel != null)
            {
                writer.WritePropertyName("versionModel");
                this.VersionModel.WriteJson(writer);
            }
            if(this.CurrentVersion != null)
            {
                writer.WritePropertyName("currentVersion");
                this.CurrentVersion.WriteJson(writer);
            }
            writer.WriteObjectEnd();
        }
	}
}
