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

using System;
using System.Collections;
using System.Text;
using System.Threading;
using Gs2.Core.Exception;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Core.Util;
using LitJson;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gs2.Core
{
	public abstract class AbstractGs2Client {

		/** セッション */
		protected readonly Gs2Session Gs2Session;

		protected AbstractGs2Client(Gs2Session session)
		{
			Gs2Session = session;
			JsonMapper.RegisterExporter<float>( (obj, writer) => writer.Write( Convert.ToDouble( obj ) ) );
			JsonMapper.RegisterImporter<double, float>( Convert.ToSingle );
			JsonMapper.RegisterImporter<int, long>( Convert.ToInt64 );
		}
	}
}
