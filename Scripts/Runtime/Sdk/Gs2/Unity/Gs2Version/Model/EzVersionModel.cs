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
	public class EzVersionModel
	{
		/** バージョンの種類名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** バージョンの種類のメタデータ */
		[UnityEngine.SerializeField]
		public string Metadata;
		/** バージョンアップを促すバージョン */
		[UnityEngine.SerializeField]
		public EzVersion WarningVersion;
		/** バージョンチェックを蹴るバージョン */
		[UnityEngine.SerializeField]
		public EzVersion ErrorVersion;
		/** 判定に使用するバージョン値の種類 */
		[UnityEngine.SerializeField]
		public string Scope;
		/** 現在のバージョン */
		[UnityEngine.SerializeField]
		public EzVersion CurrentVersion;
		/** 判定するバージョン値に署名検証を必要とするか */
		[UnityEngine.SerializeField]
		public bool NeedSignature;

		public EzVersionModel()
		{

		}

		public EzVersionModel(Gs2.Gs2Version.Model.VersionModel @versionModel)
		{
			Name = @versionModel.name;
			Metadata = @versionModel.metadata;
			WarningVersion = @versionModel.warningVersion != null ? new EzVersion(@versionModel.warningVersion) : null;
			ErrorVersion = @versionModel.errorVersion != null ? new EzVersion(@versionModel.errorVersion) : null;
			Scope = @versionModel.scope;
			CurrentVersion = @versionModel.currentVersion != null ? new EzVersion(@versionModel.currentVersion) : null;
			NeedSignature = @versionModel.needSignature.HasValue ? @versionModel.needSignature.Value : false;
		}

        public virtual VersionModel ToModel()
        {
            return new VersionModel {
                name = Name,
                metadata = Metadata,
                warningVersion = new Version_ {
                    major = WarningVersion.Major,
                    minor = WarningVersion.Minor,
                    micro = WarningVersion.Micro,
                },
                errorVersion = new Version_ {
                    major = ErrorVersion.Major,
                    minor = ErrorVersion.Minor,
                    micro = ErrorVersion.Micro,
                },
                scope = Scope,
                currentVersion = new Version_ {
                    major = CurrentVersion.Major,
                    minor = CurrentVersion.Minor,
                    micro = CurrentVersion.Micro,
                },
                needSignature = NeedSignature,
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
            if(this.WarningVersion != null)
            {
                writer.WritePropertyName("warningVersion");
                this.WarningVersion.WriteJson(writer);
            }
            if(this.ErrorVersion != null)
            {
                writer.WritePropertyName("errorVersion");
                this.ErrorVersion.WriteJson(writer);
            }
            if(this.Scope != null)
            {
                writer.WritePropertyName("scope");
                writer.Write(this.Scope);
            }
            if(this.CurrentVersion != null)
            {
                writer.WritePropertyName("currentVersion");
                this.CurrentVersion.WriteJson(writer);
            }
            writer.WritePropertyName("needSignature");
            writer.Write(this.NeedSignature);
            writer.WriteObjectEnd();
        }
	}
}
