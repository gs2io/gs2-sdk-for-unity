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

#pragma warning disable 1998

using System;
using System.Linq;
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Gs2Schedule.Domain.Iterator;
using Gs2.Gs2Schedule.Request;
using Gs2.Gs2Schedule.Result;
using Gs2.Gs2Auth.Model;
using Gs2.Util.LitJson;
using Gs2.Core;
using Gs2.Core.Domain;
using Gs2.Core.Util;
using UnityEngine.Scripting;
using System.Collections;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Collections.Generic;
#endif

namespace Gs2.Unity.Gs2Schedule.Domain.Model
{

    public partial class EzUserDomain {
        private readonly Gs2.Gs2Schedule.Domain.Model.UserDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserDomain(
            Gs2.Gs2Schedule.Domain.Model.UserDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public class EzTriggersIterator : Gs2Iterator<Gs2.Unity.Gs2Schedule.Model.EzTrigger>
        {
            private Gs2Iterator<Gs2.Gs2Schedule.Model.Trigger> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Schedule.Domain.Model.UserDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzTriggersIterator(
                Gs2Iterator<Gs2.Gs2Schedule.Model.Trigger> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Schedule.Domain.Model.UserDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Schedule.Model.EzTrigger> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        _it = _domain.Triggers(
                        );
                    }
                );
        #endif
                callback.Invoke(_it.Current == null ? null : Gs2.Unity.Gs2Schedule.Model.EzTrigger.FromModel(_it.Current));
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Schedule.Model.EzTrigger> Triggers(
        )
        {
            return new EzTriggersIterator(
                _domain.Triggers(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Schedule.Model.EzTrigger> TriggersAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Schedule.Model.EzTrigger> Triggers(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Schedule.Model.EzTrigger>(async (writer, token) =>
            {
                var it = _domain.TriggersAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.TriggersAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Schedule.Model.EzTrigger.FromModel(it.Current));
                }
            });
        #else
            return new EzTriggersIterator(
                _domain.Triggers(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public Gs2.Unity.Gs2Schedule.Domain.Model.EzTriggerDomain Trigger(
            string triggerName
        ) {
            return new Gs2.Unity.Gs2Schedule.Domain.Model.EzTriggerDomain(
                _domain.Trigger(
                    triggerName
                ),
                _profile
            );
        }

        public class EzEventsIterator : Gs2Iterator<Gs2.Unity.Gs2Schedule.Model.EzEvent>
        {
            private Gs2Iterator<Gs2.Gs2Schedule.Model.Event> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Schedule.Domain.Model.UserDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzEventsIterator(
                Gs2Iterator<Gs2.Gs2Schedule.Model.Event> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Schedule.Domain.Model.UserDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Schedule.Model.EzEvent> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        _it = _domain.Events(
                        );
                    }
                );
        #endif
                callback.Invoke(_it.Current == null ? null : Gs2.Unity.Gs2Schedule.Model.EzEvent.FromModel(_it.Current));
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Schedule.Model.EzEvent> Events(
        )
        {
            return new EzEventsIterator(
                _domain.Events(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Schedule.Model.EzEvent> EventsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Schedule.Model.EzEvent> Events(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Schedule.Model.EzEvent>(async (writer, token) =>
            {
                var it = _domain.EventsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.EventsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Schedule.Model.EzEvent.FromModel(it.Current));
                }
            });
        #else
            return new EzEventsIterator(
                _domain.Events(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public Gs2.Unity.Gs2Schedule.Domain.Model.EzEventDomain Event(
            string eventName
        ) {
            return new Gs2.Unity.Gs2Schedule.Domain.Model.EzEventDomain(
                _domain.Event(
                    eventName
                ),
                _profile
            );
        }

    }
}
