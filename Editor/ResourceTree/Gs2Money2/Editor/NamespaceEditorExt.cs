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
    public static class NamespaceEditorExt
    {
        public static void OnGUI(Gs2.Gs2Money2.Model.Namespace item) {
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
            EditorGUILayout.TextField("CurrencyUsagePriority", item.CurrencyUsagePriority);
            EditorGUILayout.TextField("SharedFreeCurrency", item.SharedFreeCurrency?.ToString());
            if (item.PlatformSetting == null) {
                EditorGUILayout.TextField("PlatformSetting", "");
            }
            else {
                EditorGUILayout.LabelField("PlatformSetting");
                EditorGUI.indentLevel++;
                PlatformSettingEditorExt.OnGUI(item.PlatformSetting);
                EditorGUI.indentLevel--;
            }
            if (item.DepositBalanceScript == null) {
                EditorGUILayout.TextField("DepositBalanceScript", "");
            }
            else {
                EditorGUILayout.LabelField("DepositBalanceScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.DepositBalanceScript);
                EditorGUI.indentLevel--;
            }
            if (item.WithdrawBalanceScript == null) {
                EditorGUILayout.TextField("WithdrawBalanceScript", "");
            }
            else {
                EditorGUILayout.LabelField("WithdrawBalanceScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.WithdrawBalanceScript);
                EditorGUI.indentLevel--;
            }
            if (item.VerifyReceiptScript == null) {
                EditorGUILayout.TextField("VerifyReceiptScript", "");
            }
            else {
                EditorGUILayout.LabelField("VerifyReceiptScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.VerifyReceiptScript);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.TextField("SubscribeScript", item.SubscribeScript);
            EditorGUILayout.TextField("RenewScript", item.RenewScript);
            EditorGUILayout.TextField("UnsubscribeScript", item.UnsubscribeScript);
            if (item.TakeOverScript == null) {
                EditorGUILayout.TextField("TakeOverScript", "");
            }
            else {
                EditorGUILayout.LabelField("TakeOverScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.TakeOverScript);
                EditorGUI.indentLevel--;
            }
            if (item.ChangeSubscriptionStatusNotification == null) {
                EditorGUILayout.TextField("ChangeSubscriptionStatusNotification", "");
            }
            else {
                EditorGUILayout.LabelField("ChangeSubscriptionStatusNotification");
                EditorGUI.indentLevel++;
                NotificationSettingEditorExt.OnGUI(item.ChangeSubscriptionStatusNotification);
                EditorGUI.indentLevel--;
            }
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