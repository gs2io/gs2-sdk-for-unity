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

using Gs2.Gs2Schedule;
using Gs2.Unity.Gs2Schedule.Model;
using Gs2.Unity.Gs2Schedule.Result;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Gs2Quest;
using Gs2.Gs2Quest.Model;
using Gs2.Gs2Quest.Request;
using Gs2.Gs2Quest.Result;
using Gs2.Unity.Gs2Quest.Model;
using Gs2.Unity.Gs2Quest.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Schedule
{
	public class DisabledCertificateHandler : CertificateHandler {
		protected override bool ValidateCertificate(byte[] certificateData)
		{
			return true;
		}
	}

	[Preserve]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public partial class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2ScheduleWebSocketClient _client;
		private readonly Gs2ScheduleRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2ScheduleWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2ScheduleRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2ScheduleRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

        public IEnumerator GetTrigger(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Schedule.Result.EzGetTriggerResult>> callback,
		        GameSession session,
                string namespaceName,
                string triggerName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetTrigger(
                    new Gs2.Gs2Schedule.Request.GetTriggerRequest()
                        .WithNamespaceName(namespaceName)
                        .WithTriggerName(triggerName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Schedule.Result.EzGetTriggerResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Schedule.Result.EzGetTriggerResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListTriggers(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Schedule.Result.EzListTriggersResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeTriggers(
                    new Gs2.Gs2Schedule.Request.DescribeTriggersRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Schedule.Result.EzListTriggersResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Schedule.Result.EzListTriggersResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetEvent(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Schedule.Result.EzGetEventResult>> callback,
		        GameSession session,
                string namespaceName,
                string eventName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetEvent(
                    new Gs2.Gs2Schedule.Request.GetEventRequest()
                        .WithNamespaceName(namespaceName)
                        .WithEventName(eventName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Schedule.Result.EzGetEventResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Schedule.Result.EzGetEventResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListEvents(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Schedule.Result.EzListEventsResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeEvents(
                    new Gs2.Gs2Schedule.Request.DescribeEventsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Schedule.Result.EzListEventsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Schedule.Result.EzListEventsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}