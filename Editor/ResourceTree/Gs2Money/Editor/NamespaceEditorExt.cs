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

namespace Gs2.Editor.ResourceTree.Gs2Money.Editor
{
    public static class NamespaceEditorExt
    {
        public static void OnGUI(Gs2.Gs2Money.Model.Namespace item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("NamespaceId", item.NamespaceId);
            EditorGUILayout.TextField("Name", item.Name);
            EditorGUILayout.TextField("Description", item.Description);
            if (item.TransactionSetting == null) {
                EditorGUILayout.TextField("TransactionSetting", "");
            }
            else {
                EditorGUILayout.LabelField("TransactionSetting");
                EditorGUI.indentLevel++;
                TransactionSettingEditorExt.OnGUI(item.TransactionSetting);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.TextField("Priority", item.Priority);
            EditorGUILayout.TextField("ShareFree", item.ShareFree?.ToString());
            EditorGUILayout.TextField("Currency", item.Currency);
            EditorGUILayout.TextField("AppleKey", item.AppleKey);
            EditorGUILayout.TextField("GoogleKey", item.GoogleKey);
            EditorGUILayout.TextField("EnableFakeReceipt", item.EnableFakeReceipt?.ToString());
            if (item.CreateWalletScript == null) {
                EditorGUILayout.TextField("CreateWalletScript", "");
            }
            else {
                EditorGUILayout.LabelField("CreateWalletScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.CreateWalletScript);
                EditorGUI.indentLevel--;
            }
            if (item.DepositScript == null) {
                EditorGUILayout.TextField("DepositScript", "");
            }
            else {
                EditorGUILayout.LabelField("DepositScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.DepositScript);
                EditorGUI.indentLevel--;
            }
            if (item.WithdrawScript == null) {
                EditorGUILayout.TextField("WithdrawScript", "");
            }
            else {
                EditorGUILayout.LabelField("WithdrawScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.WithdrawScript);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.TextField("Balance", item.Balance?.ToString());
            if (item.LogSetting == null) {
                EditorGUILayout.TextField("LogSetting", "");
            }
            else {
                EditorGUILayout.LabelField("LogSetting");
                EditorGUI.indentLevel++;
                LogSettingEditorExt.OnGUI(item.LogSetting);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.TextField("CreatedAt", UnixTime.FromUnixTime(item.CreatedAt ?? 0).ToString(CultureInfo.CurrentUICulture));
            EditorGUILayout.TextField("UpdatedAt", UnixTime.FromUnixTime(item.UpdatedAt ?? 0).ToString(CultureInfo.CurrentUICulture));
            EditorGUILayout.TextField("Revision", item.Revision?.ToString());
            EditorGUI.EndDisabledGroup();
        }
    }
}