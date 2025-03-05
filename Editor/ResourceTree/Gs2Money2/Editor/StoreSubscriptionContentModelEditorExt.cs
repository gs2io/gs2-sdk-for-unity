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
    public static class StoreSubscriptionContentModelEditorExt
    {
        public static void OnGUI(Gs2.Gs2Money2.Model.StoreSubscriptionContentModel item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("StoreSubscriptionContentModelId", item.StoreSubscriptionContentModelId);
            EditorGUILayout.TextField("Name", item.Name);
            EditorGUILayout.TextField("Metadata", item.Metadata);
            EditorGUILayout.TextField("ScheduleNamespaceId", item.ScheduleNamespaceId);
            EditorGUILayout.TextField("TriggerName", item.TriggerName);
            EditorGUILayout.TextField("TriggerExtendMode", item.TriggerExtendMode);
            EditorGUILayout.TextField("RollupHour", item.RollupHour?.ToString());
            EditorGUILayout.TextField("ReallocateSpanDays", item.ReallocateSpanDays?.ToString());
            if (item.AppleAppStore == null) {
                EditorGUILayout.TextField("AppleAppStore", "");
            }
            else {
                EditorGUILayout.LabelField("AppleAppStore");
                EditorGUI.indentLevel++;
                AppleAppStoreSubscriptionContentEditorExt.OnGUI(item.AppleAppStore);
                EditorGUI.indentLevel--;
            }
            if (item.GooglePlay == null) {
                EditorGUILayout.TextField("GooglePlay", "");
            }
            else {
                EditorGUILayout.LabelField("GooglePlay");
                EditorGUI.indentLevel++;
                GooglePlaySubscriptionContentEditorExt.OnGUI(item.GooglePlay);
                EditorGUI.indentLevel--;
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}