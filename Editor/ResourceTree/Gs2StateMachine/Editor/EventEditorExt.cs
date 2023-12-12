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

namespace Gs2.Editor.ResourceTree.Gs2StateMachine.Editor
{
    public static class EventEditorExt
    {
        public static void OnGUI(Gs2.Gs2StateMachine.Model.Event item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("EventType", item.EventType);
            if (item.ChangeStateEvent == null) {
                EditorGUILayout.TextField("ChangeStateEvent", "");
            }
            else {
                EditorGUILayout.LabelField("ChangeStateEvent");
                EditorGUI.indentLevel++;
                ChangeStateEventEditorExt.OnGUI(item.ChangeStateEvent);
                EditorGUI.indentLevel--;
            }
            if (item.EmitEvent == null) {
                EditorGUILayout.TextField("EmitEvent", "");
            }
            else {
                EditorGUILayout.LabelField("EmitEvent");
                EditorGUI.indentLevel++;
                EmitEventEditorExt.OnGUI(item.EmitEvent);
                EditorGUI.indentLevel--;
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}