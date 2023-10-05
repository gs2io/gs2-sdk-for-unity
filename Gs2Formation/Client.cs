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

using Gs2.Gs2Formation;
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Unity.Gs2Formation.Result;
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
namespace Gs2.Unity.Gs2Formation
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
		private readonly Gs2FormationWebSocketClient _client;
		private readonly Gs2FormationRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2FormationWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2FormationRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2FormationRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

        public IEnumerator GetMoldModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Formation.Result.EzGetMoldModelResult>> callback,
                string namespaceName,
                string moldModelName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetMoldModel(
                    new Gs2.Gs2Formation.Request.GetMoldModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMoldModelName(moldModelName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Formation.Result.EzGetMoldModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Formation.Result.EzGetMoldModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListMoldModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Formation.Result.EzListMoldModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeMoldModels(
                    new Gs2.Gs2Formation.Request.DescribeMoldModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Formation.Result.EzListMoldModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Formation.Result.EzListMoldModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetMold(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Formation.Result.EzGetMoldResult>> callback,
		        GameSession session,
                string namespaceName,
                string moldModelName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetMold(
                    new Gs2.Gs2Formation.Request.GetMoldRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMoldModelName(moldModelName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Formation.Result.EzGetMoldResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Formation.Result.EzGetMoldResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListMolds(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Formation.Result.EzListMoldsResult>> callback,
		        GameSession session,
                string namespaceName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeMolds(
                    new Gs2.Gs2Formation.Request.DescribeMoldsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Formation.Result.EzListMoldsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Formation.Result.EzListMoldsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetFormModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Formation.Result.EzGetFormModelResult>> callback,
                string namespaceName,
                string moldModelName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetFormModel(
                    new Gs2.Gs2Formation.Request.GetFormModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMoldModelName(moldModelName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Formation.Result.EzGetFormModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Formation.Result.EzGetFormModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DeleteForm(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Formation.Result.EzDeleteFormResult>> callback,
		        GameSession session,
                string namespaceName,
                string moldModelName,
                int index
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DeleteForm(
                    new Gs2.Gs2Formation.Request.DeleteFormRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMoldModelName(moldModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithIndex(index),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Formation.Result.EzDeleteFormResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Formation.Result.EzDeleteFormResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetForm(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Formation.Result.EzGetFormResult>> callback,
		        GameSession session,
                string namespaceName,
                string moldModelName,
                int index
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.GetForm(
                    new Gs2.Gs2Formation.Request.GetFormRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMoldModelName(moldModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithIndex(index),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Formation.Result.EzGetFormResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Formation.Result.EzGetFormResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetFormWithSignature(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Formation.Result.EzGetFormWithSignatureResult>> callback,
		        GameSession session,
                string namespaceName,
                string moldModelName,
                int index,
                string keyId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.GetFormWithSignature(
                    new Gs2.Gs2Formation.Request.GetFormWithSignatureRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMoldModelName(moldModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithIndex(index)
                        .WithKeyId(keyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Formation.Result.EzGetFormWithSignatureResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Formation.Result.EzGetFormWithSignatureResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListForms(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Formation.Result.EzListFormsResult>> callback,
		        GameSession session,
                string namespaceName,
                string moldModelName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeForms(
                    new Gs2.Gs2Formation.Request.DescribeFormsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMoldModelName(moldModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Formation.Result.EzListFormsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Formation.Result.EzListFormsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator SetForm(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Formation.Result.EzSetFormResult>> callback,
		        GameSession session,
                string namespaceName,
                string moldModelName,
                int index,
                List<Gs2.Unity.Gs2Formation.Model.EzSlotWithSignature> slots,
                string keyId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.SetFormWithSignature(
                    new Gs2.Gs2Formation.Request.SetFormWithSignatureRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMoldModelName(moldModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithIndex(index)
                        .WithSlots(slots?.Select(v => {
                            return v?.ToModel();
                        }).ToArray())
                        .WithKeyId(keyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Formation.Result.EzSetFormResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Formation.Result.EzSetFormResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetPropertyFormModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Formation.Result.EzGetPropertyFormModelResult>> callback,
                string namespaceName,
                string propertyFormModelName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetPropertyFormModel(
                    new Gs2.Gs2Formation.Request.GetPropertyFormModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithPropertyFormModelName(propertyFormModelName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Formation.Result.EzGetPropertyFormModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Formation.Result.EzGetPropertyFormModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListPropertyFormModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Formation.Result.EzListPropertyFormModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribePropertyFormModels(
                    new Gs2.Gs2Formation.Request.DescribePropertyFormModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Formation.Result.EzListPropertyFormModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Formation.Result.EzListPropertyFormModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DeletePropertyForm(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Formation.Result.EzDeletePropertyFormResult>> callback,
		        GameSession session,
                string namespaceName,
                string propertyId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DeletePropertyForm(
                    new Gs2.Gs2Formation.Request.DeletePropertyFormRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPropertyId(propertyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Formation.Result.EzDeletePropertyFormResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Formation.Result.EzDeletePropertyFormResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetPropertyForm(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Formation.Result.EzGetPropertyFormResult>> callback,
		        GameSession session,
                string namespaceName,
                string propertyFormModelName,
                string propertyId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.GetPropertyForm(
                    new Gs2.Gs2Formation.Request.GetPropertyFormRequest()
                        .WithNamespaceName(namespaceName)
                        .WithPropertyFormModelName(propertyFormModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPropertyId(propertyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Formation.Result.EzGetPropertyFormResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Formation.Result.EzGetPropertyFormResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetPropertyFormWithSignature(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Formation.Result.EzGetPropertyFormWithSignatureResult>> callback,
		        GameSession session,
                string namespaceName,
                string propertyFormModelName,
                string propertyId,
                string keyId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.GetPropertyFormWithSignature(
                    new Gs2.Gs2Formation.Request.GetPropertyFormWithSignatureRequest()
                        .WithNamespaceName(namespaceName)
                        .WithPropertyFormModelName(propertyFormModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPropertyId(propertyId)
                        .WithKeyId(keyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Formation.Result.EzGetPropertyFormWithSignatureResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Formation.Result.EzGetPropertyFormWithSignatureResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListPropertyForms(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Formation.Result.EzListPropertyFormsResult>> callback,
		        GameSession session,
                string namespaceName,
                string propertyFormModelName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribePropertyForms(
                    new Gs2.Gs2Formation.Request.DescribePropertyFormsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithPropertyFormModelName(propertyFormModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Formation.Result.EzListPropertyFormsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Formation.Result.EzListPropertyFormsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator SetPropertyForm(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Formation.Result.EzSetPropertyFormResult>> callback,
		        GameSession session,
                string namespaceName,
                string propertyFormModelName,
                string propertyId,
                List<Gs2.Unity.Gs2Formation.Model.EzSlotWithSignature> slots,
                string keyId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.SetPropertyFormWithSignature(
                    new Gs2.Gs2Formation.Request.SetPropertyFormWithSignatureRequest()
                        .WithNamespaceName(namespaceName)
                        .WithPropertyFormModelName(propertyFormModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPropertyId(propertyId)
                        .WithSlots(slots?.Select(v => {
                            return v?.ToModel();
                        }).ToArray())
                        .WithKeyId(keyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Formation.Result.EzSetPropertyFormResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Formation.Result.EzSetPropertyFormResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}