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

using System.Linq;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Mission.Model
{
    public static class EzCounterExt
    {
        public static EzScopedValue Value(this EzCounter self, string resetType)
        {
            return self.Values.FirstOrDefault(v => v.ResetType == resetType) ?? new EzScopedValue
            {
                ResetType = resetType,
                Value = 0,
            };
        }
        
        public static EzScopedValue ValueByConditionName(this EzCounter self, string conditionName)
        {
            return self.Values.FirstOrDefault(v => v.ConditionName == conditionName) ?? new EzScopedValue
            {
                ConditionName = conditionName,
                Value = 0,
            };
        }
    }
}