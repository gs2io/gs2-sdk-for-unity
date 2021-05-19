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
using Gs2.Gs2Matchmaking.Model;
using System.Collections.Generic;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Matchmaking.Model
{
	[Preserve]
	[System.Serializable]
	public class EzSignedBallot
	{
		/** 投票用紙の署名対象のデータ */
		[UnityEngine.SerializeField]
		public string Body;
		/** 投票用紙の署名 */
		[UnityEngine.SerializeField]
		public string Signature;

		public EzSignedBallot()
		{

		}

		public EzSignedBallot(Gs2.Gs2Matchmaking.Model.SignedBallot @signedBallot)
		{
			Body = @signedBallot.body;
			Signature = @signedBallot.signature;
		}

        public virtual SignedBallot ToModel()
        {
            return new SignedBallot {
                body = Body,
                signature = Signature,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
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
