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
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using UnityEngine.Events;

namespace Gs2.Unity.Util
{
    public class Profile
    {
        private readonly IReopener _reopener;
        
        public Profile(
            string clientId,
            string clientSecret,
            IReopener reopener
        )
        {
            BasicGs2Credential credential = new BasicGs2Credential(
                clientId,
                clientSecret
            );
            Gs2Session = new Gs2WebSocketSession(
                credential
            );
            _reopener = reopener;
        }

        public IEnumerator Initialize(
            UnityAction<AsyncResult<object>> callback
        )
        {
            yield return _reopener.ReOpen(
                Gs2Session,
                r => {
                    if (r.Error != null)
                    {
                        callback.Invoke(
                            new AsyncResult<object>(
                                null,
                                r.Error
                            )
                        );
                    }
                    else
                    {
                        callback.Invoke(
                            new AsyncResult<object>(
                                null,
                                r.Error
                            )
                        );
                    }
                }
            );
        }

        public IEnumerator Finalize()
        {
            yield return Gs2Session.Close(() => {});
        }

        public IEnumerator Login(
            IAuthenticator authenticator,
            UnityAction<AsyncResult<GameSession>> callback
        )
        {
            yield return authenticator.Authentication(
                r =>
                {
                    if (r.Error != null)
                    {
                        callback.Invoke(
                            new AsyncResult<GameSession>(
                                null,
                                r.Error
                            )
                        );
                    }
                    else
                    {
                        callback.Invoke(
                            new AsyncResult<GameSession>(
                                new GameSession(
                                    r.Result
                                ),
                                r.Error
                            )
                        );
                    }
                }
            );
        }

        public Gs2WebSocketSession Gs2Session { get; }
    }
}