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

using Gs2.Gs2Lottery;
using Gs2.Unity.Gs2Lottery.Model;
using Gs2.Unity.Gs2Lottery.Result;
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
namespace Gs2.Unity.Gs2Lottery
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
		private readonly Gs2LotteryWebSocketClient _client;
		private readonly Gs2LotteryRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2LotteryWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2LotteryRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2LotteryRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

        public IEnumerator DescribeBoxes(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Lottery.Result.EzDescribeBoxesResult>> callback,
		        GameSession session,
                string namespaceName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeBoxes(
                    new Gs2.Gs2Lottery.Request.DescribeBoxesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Lottery.Result.EzDescribeBoxesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Lottery.Result.EzDescribeBoxesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetBox(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Lottery.Result.EzGetBoxResult>> callback,
		        GameSession session,
                string namespaceName,
                string prizeTableName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.GetBox(
                    new Gs2.Gs2Lottery.Request.GetBoxRequest()
                        .WithNamespaceName(namespaceName)
                        .WithPrizeTableName(prizeTableName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Lottery.Result.EzGetBoxResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Lottery.Result.EzGetBoxResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ResetBox(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Lottery.Result.EzResetBoxResult>> callback,
		        GameSession session,
                string namespaceName,
                string prizeTableName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.ResetBox(
                    new Gs2.Gs2Lottery.Request.ResetBoxRequest()
                        .WithNamespaceName(namespaceName)
                        .WithPrizeTableName(prizeTableName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Lottery.Result.EzResetBoxResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Lottery.Result.EzResetBoxResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListProbabilities(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Lottery.Result.EzListProbabilitiesResult>> callback,
		        GameSession session,
                string namespaceName,
                string lotteryName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeProbabilities(
                    new Gs2.Gs2Lottery.Request.DescribeProbabilitiesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithLotteryName(lotteryName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Lottery.Result.EzListProbabilitiesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Lottery.Result.EzListProbabilitiesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetLotteryModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Lottery.Result.EzGetLotteryModelResult>> callback,
                string namespaceName,
                string lotteryName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetLotteryModel(
                    new Gs2.Gs2Lottery.Request.GetLotteryModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithLotteryName(lotteryName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Lottery.Result.EzGetLotteryModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Lottery.Result.EzGetLotteryModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListLotteryModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Lottery.Result.EzListLotteryModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeLotteryModels(
                    new Gs2.Gs2Lottery.Request.DescribeLotteryModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Lottery.Result.EzListLotteryModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Lottery.Result.EzListLotteryModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}