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

using System.Globalization;
using Gs2.Core.Util;
using Gs2.Editor.ResourceTree.Core.Editor;
using UnityEditor;

namespace Gs2.Editor.ResourceTree.Gs2Money2.Editor
{
    public static class DailyTransactionHistoryEditorExt
    {
        public static void OnGUI(Gs2.Gs2Money2.Model.DailyTransactionHistory item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("DailyTransactionHistoryId", item.DailyTransactionHistoryId);
            EditorGUILayout.TextField("Year", item.Year?.ToString());
            EditorGUILayout.TextField("Month", item.Month?.ToString());
            EditorGUILayout.TextField("Day", item.Day?.ToString());
            EditorGUILayout.TextField("Currency", item.Currency);
            EditorGUILayout.TextField("DepositAmount", item.DepositAmount?.ToString());
            EditorGUILayout.TextField("WithdrawAmount", item.WithdrawAmount?.ToString());
            EditorGUILayout.TextField("UpdatedAt", UnixTime.FromUnixTime(item.UpdatedAt ?? 0).ToString(CultureInfo.CurrentUICulture));
            EditorGUILayout.TextField("Revision", item.Revision?.ToString());
            EditorGUI.EndDisabledGroup();
        }
    }
}