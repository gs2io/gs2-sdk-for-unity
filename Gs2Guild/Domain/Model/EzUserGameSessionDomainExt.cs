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
using Gs2.Core.Net;
using Gs2.Unity.Util;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Gs2.Unity.Gs2Guild.Domain.Model
{
    public static class EzUserGameSessionDomainExt
    {
#if GS2_ENABLE_UNITASK
        public static async UniTask<GuildGameSession> AssumeAsync(
            this EzUserGameSessionDomain self,
            GatewaySetting gatewaySetting,
            string guildModelName,
            string guildName
        ) {
            var gameSession = new GuildGameSession(
                new Gs2GuildGuildAuthenticator(
                    new GuildSetting {
                        guildNamespaceName = self.NamespaceName,
                        guildModelName = guildModelName
                    },
                    gatewaySetting
                ),
                self.Connection,
                self.GameSession,
                guildName
            );
            await gameSession.RefreshAsync();
            return gameSession;
        }
#endif
        
        public static IFuture<GuildGameSession> AssumeFuture(
            this EzUserGameSessionDomain self,
            GatewaySetting gatewaySetting,
            string guildModelName,
            string guildName
        ) {
            IEnumerator Impl(Gs2Future<GuildGameSession> f) {
                var gameSession = new GuildGameSession(
                    new Gs2GuildGuildAuthenticator(
                        new GuildSetting {
                            guildNamespaceName = self.NamespaceName,
                            guildModelName = guildModelName
                        },
                        gatewaySetting
                    ),
                    self.Connection,
                    self.GameSession,
                    guildName
                );
                var future = gameSession.RefreshFuture();
                yield return future;
                if (future.Error != null) {
                    f.OnError(future.Error);
                    yield break;
                }
                f.OnComplete(gameSession);
            }

            return new Gs2InlineFuture<GuildGameSession>(Impl);
        }
    }
}
