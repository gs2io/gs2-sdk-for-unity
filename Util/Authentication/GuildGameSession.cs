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

using System.Collections;
using Gs2.Core.Domain;
using Gs2.Gs2Auth.Model;
using Gs2.Gs2Guild;
using Gs2.Gs2Guild.Request;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Gs2.Unity.Util
{
    public class GuildGameSession : IGameSession
    {
        private readonly IGuildAuthenticator _authenticator;
        public AccessToken AccessToken { get; protected set; }

        private Gs2Connection _connection;
        private IGameSession _userGameSession;
        private string _guildName;
        
        public GuildGameSession(
            IGuildAuthenticator authenticator,
            Gs2Connection connection,
            IGameSession userGameSession,
            string guildName
        ) {
            this._authenticator = authenticator;
            this._connection = connection;
            this._userGameSession = userGameSession;
            this._guildName = guildName;
        }

        public string UserId => this._userGameSession.UserId;
        public string GuildName => this.AccessToken.UserId;

        public Gs2Future RefreshFuture() {
            IEnumerator Impl(Gs2Future self) {
                var future = this._authenticator.AuthenticationFuture(
                    this._connection,
                    this._userGameSession,
                    this._guildName
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                AccessToken = future.Result;
            }
            return new Gs2InlineFuture(Impl);
        }

#if GS2_ENABLE_UNITASK
        public async UniTask RefreshAsync() {
            AccessToken = await this._authenticator.AuthenticationAsync(
                this._connection,
                this._userGameSession,
                this._guildName
            );
        }
#endif

        public Gs2Future<bool> RefreshIfNeedRefreshFuture() {
            IEnumerator Impl(Gs2Future<bool> self) {
                if (this._authenticator == null || !this._authenticator.NeedReAuthentication) {
                    self.OnComplete(false);
                    yield break;
                }
                var future = RefreshFuture();
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(true);
            }
            return new Gs2InlineFuture<bool>(Impl);
        }

#if GS2_ENABLE_UNITASK
        public async UniTask<bool> RefreshIfNeedRefreshAsync() {
            if (this._authenticator == null || !this._authenticator.NeedReAuthentication) {
                return false;
            }
            await RefreshAsync();
            return true;
        }
#endif

        public override int GetHashCode() {
            unchecked {
                int hash = 17;
                hash = hash * 23 + (this.AccessToken?.TimeOffset?.GetHashCode() ?? 0);
                hash = hash * 23 + (this.GuildName?.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}