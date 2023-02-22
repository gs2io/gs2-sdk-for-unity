
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
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Domain;
using Gs2.Core.Util;
using Gs2.Gs2Auth.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;
#if GS2_ENABLE_UNITASK
using System.Threading;
using System.Collections.Generic;
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
#else
using System.Collections;
using UnityEngine.Events;
using Gs2.Core.Exception;
#endif

namespace Gs2.Unity.Gs2Quest.Domain.Iterator
{

    #if GS2_ENABLE_UNITASK
    public class EzDescribeCompletedQuestListsIterator {
    #else
    public class EzDescribeCompletedQuestListsIterator : Gs2Iterator<Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList> {
    #endif
        private readonly Gs2.Gs2Quest.Domain.Iterator.DescribeCompletedQuestListsIterator _iterator;

        public EzDescribeCompletedQuestListsIterator(
            Gs2.Gs2Quest.Domain.Iterator.DescribeCompletedQuestListsIterator iterator
        ) {
            this._iterator = iterator;
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList> GetAsyncEnumerator(
            CancellationToken cancellationToken = new CancellationToken()
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList>(async (writer, token) =>
            {
            });
        }

        #else

        public override bool HasNext()
        {
            return _iterator.HasNext();
        }

        protected override IEnumerator Next(
            Action<AsyncResult<Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList>> callback
        )
        {
            yield return _iterator;
            callback.Invoke(new AsyncResult<Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList>(
                _iterator.Current == null ? null : Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList.FromModel(_iterator.Current),
                _iterator.Error
            ));
        }

        #endif
    }
}
