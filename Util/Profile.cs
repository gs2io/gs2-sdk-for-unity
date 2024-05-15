/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0(the "License").
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
using Gs2.Core;
using Gs2.Core.Domain;
using Gs2.Core.Exception;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Core.Result;
using Gs2.Gs2Auth.Model;
using Gs2.Unity.Core;
using UnityEngine.Events;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Gs2.Unity.Util
{
    [Obsolete("The method of initializing the SDK has changed; use Gs2Client.Create().")]
    public class Profile
    {
        private Gs2Connection _connection;
        
        private readonly IReopener _reopener;
        private string _distributorNamespaceName;

        public IReopener Reopener => _reopener;

        public string DistributorNamespaceName => _distributorNamespaceName;
        public Gs2WebSocketSession Gs2Session => _connection.WebSocketSession;
        public Gs2RestSession Gs2RestSession => _connection.RestSession;

        [Obsolete("The method of initializing the SDK has changed; use Gs2Client.Create().")]
        public Profile(
            string clientId,
            string clientSecret,
            IReopener reopener,
            Region region = Region.ApNortheast1,
            string distributorNamespaceName = null,
            bool checkCertificateRevocation = true
        ) {
            _connection = new Gs2Connection(
                new BasicGs2Credential(
                    clientId,
                    clientSecret
                ),
                region
            );
            this._reopener = reopener;
            this._distributorNamespaceName = distributorNamespaceName;
        }

#if GS2_ENABLE_UNITASK

        [Obsolete("The method of initializing the SDK has changed; use Gs2Client.Create().")]
        public async UniTask<Gs2Domain> InitializeAsync()
        {
            await _reopener.ReOpenAsync(
                Gs2Session,
                Gs2RestSession
            );

            return new Gs2Domain(this._connection, _distributorNamespaceName);
        }

#endif

        [Obsolete("The method of initializing the SDK has changed; use Gs2Client.Create().")]
        public Gs2Future<Gs2Domain> InitializeFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2Domain> self)
            {
                var future = _reopener.ReOpenFuture(
                    Gs2Session,
                    Gs2RestSession
                );
                yield return future;
                
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(
                    new Gs2Domain(this._connection, _distributorNamespaceName)
                );
            }

            return new Gs2InlineFuture<Gs2Domain>(Impl);
        }
        
        [Obsolete("The method of initializing the SDK has changed; use Gs2Client.Create().")]
        public IEnumerator Initialize(
            UnityAction<AsyncResult<Gs2Domain>> callback
        )
        {
            var future = InitializeFuture();
            yield return future;
            callback.Invoke(new AsyncResult<Gs2Domain>(future.Result, future.Error));
        }

#if GS2_ENABLE_UNITASK
        public async UniTask FinalizeAsync()
        {
            await Gs2Session.CloseAsync();
            await Gs2RestSession.CloseAsync();
        }
#endif

        [Obsolete("The method of initializing the SDK has changed; use Gs2Client.Create().")]
        public Gs2Future Finalize()
        {
            IEnumerator Impl(Gs2Future self)
            {
                yield return Gs2Session.Close(() => {});
                yield return Gs2RestSession.Close(() => {});
            }

            return new Gs2InlineFuture(Impl);
        }

#if GS2_ENABLE_UNITASK
        public async UniTask<IGameSession> LoginAsync(
            IAuthenticator authenticator
        )
        {
            var gameSession = new GameSession(
                authenticator,
                this._connection,
                (authenticator as Gs2AccountAuthenticator)?.userId,
                (authenticator as Gs2AccountAuthenticator)?.password
            );
            await gameSession.RefreshAsync();
            return gameSession;
        }
#endif

        [Obsolete("The method of initializing the SDK has changed; use Gs2Client.Create().")]
        public Gs2Future<IGameSession> LoginFuture(
            IAuthenticator authenticator
        )
        {
            IEnumerator Impl(Gs2Future<IGameSession> self) {
                var gameSession = new GameSession(
                    authenticator,
                    this._connection,
                    (authenticator as Gs2AccountAuthenticator)?.userId,
                    (authenticator as Gs2AccountAuthenticator)?.password
                );
                var future = gameSession.RefreshFuture();
                yield return future;
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(gameSession);
            }
            return new Gs2InlineFuture<IGameSession>(Impl);
        }

        [Obsolete("The method of initializing the SDK has changed; use Gs2Client.Create().")]
        public IEnumerator Login(
            IAuthenticator authenticator,
            UnityAction<AsyncResult<IGameSession>> callback
        ) {
            var future = LoginFuture(
                authenticator
            );
            yield return future;
            callback.Invoke(new AsyncResult<IGameSession>(
                future.Result,
                future.Error
            ));
        }
    }
}