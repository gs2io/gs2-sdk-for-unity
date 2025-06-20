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
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Gs2.Unity.Util
{
    public class GameSession : IGameSession
    {
        private readonly IAuthenticator _authenticator;
        private readonly Gs2Connection _connection;
        private readonly string _userId;
        private readonly string _password;
        public AccessToken AccessToken { get; set; }

        public string UserId => this.AccessToken.UserId;
        public string LoginUserId => this._userId;
        
        public GameSession(
            IAuthenticator authenticator,
            Gs2Connection connection,
            string userId,
            string password
        ) {
            this._authenticator = authenticator;
            this._connection = connection;
            this._userId = userId;
            this._password = password;
        }

        public Gs2Future RefreshFuture() {
            IEnumerator Impl(Gs2Future self) {
                if (this._authenticator == null) {
                    self.OnComplete(null);
                    yield break;
                }
                var future = this._authenticator.AuthenticationFuture(
                    this._connection,
                    this._userId,
                    this._password
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                if (this.AccessToken == null) {
                    this.AccessToken = future.Result;
                }
                else {
                    this.AccessToken
                        .WithToken(future.Result.Token)
                        .WithUserId(future.Result.UserId)
                        .WithExpire(future.Result.Expire)
                        .WithTimeOffset(future.Result.TimeOffset);
                }
                self.OnComplete(null);
            }
            return new Gs2InlineFuture(Impl);
        }
        
#if GS2_ENABLE_UNITASK
        public async UniTask RefreshAsync() {
            if (this._authenticator == null) {
                return;
            }
            var result = await this._authenticator.AuthenticationAsync(
                this._connection,
                this._userId,
                this._password
            );
            if (this.AccessToken == null) {
                this.AccessToken = result;
            }
            else {
                this.AccessToken
                    .WithToken(result.Token)
                    .WithUserId(result.UserId)
                    .WithExpire(result.Expire)
                    .WithTimeOffset(result.TimeOffset);
            }
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
                hash = hash * 23 + (this.UserId?.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}