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
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantUsingDirective
// ReSharper disable CheckNamespace
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable ArrangeThisQualifier
// ReSharper disable NotAccessedField.Local

using Gs2.Unity.Util;

namespace Gs2.Unity.Gs2Guild.Domain.Model
{
    public static class EzNamespaceDomainExt
    {
        public static EzGuildGameSessionDomain GuildGameSession(
            this EzNamespaceDomain self,
            string guildModelName,
            GuildGameSession guildGameSession
        ) {
            return new Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain(
                self._domain.GuildAccessToken(
                    guildModelName,
                    guildGameSession.AccessToken
                ),
                guildGameSession,
                self._connection
            );
        }
    }
}
