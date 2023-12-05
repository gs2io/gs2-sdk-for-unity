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

using System;
using System.Globalization;
using UnityEngine;

namespace Gs2.Unity.Core
{
    public class Gs2DateTime
    {
        private static readonly DateTimeOffset UnixEpoch = DateTimeOffset.Parse("1970-01-01T00:00:00Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);

        private DateTimeOffset _epoch;
        private TimeSpan _offset;

        private Gs2DateTime(long millisecondsFromUnitEpoch)
        {
            _epoch = UnixEpoch.AddMilliseconds(millisecondsFromUnitEpoch - (long)(Time.realtimeSinceStartup * 1000));
        }

        public static Gs2DateTime FromMilliseconds(long fromUnixEpoch)
        {
            return new Gs2DateTime(fromUnixEpoch);
        }

        public static Gs2DateTime FromSeconds(long fromUnixEpoch)
        {
            return new Gs2DateTime(fromUnixEpoch * 1000);
        }

        public DateTimeOffset Current => _epoch.AddMilliseconds((long)(Time.realtimeSinceStartup * 1000));

        public TimeSpan Offset
        {
            get => _offset;
            set
            {
                _epoch = _epoch - _offset + value;
                _offset = value;
            }
        }
    }
}
