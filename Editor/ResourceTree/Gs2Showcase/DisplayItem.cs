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

using System.IO;
using System.Linq;
using Gs2.Editor.ResourceTree.Core;
using Gs2.Editor.ResourceTree.Gs2Showcase.Editor;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Gs2.Editor.ResourceTree.Gs2Showcase
{
    public sealed class DisplayItem : AbstractTreeViewItem
    {
        private Gs2.Gs2Showcase.Model.DisplayItem _item;
        private Showcase _parent;
        public string NamespaceName => _parent.NamespaceName;
        public string ShowcaseName => _parent.ShowcaseName;
        public string DisplayItemId => _item.DisplayItemId;

        public DisplayItem(
                int id,
                Showcase parent,
                Gs2.Gs2Showcase.Model.DisplayItem item
        ) {
            this.id = id = id * 100;
            this.depth = 6;
            this.icon = EditorGUIUtility.ObjectContent(null, typeof(GameObject)).image.ToTexture2D();
            this.displayName = item.DisplayItemId;
            this.children = new TreeViewItem[] {
            }.ToList();
            this._parent = parent;
            this._item = item;
        }

        public override ScriptableObject ToScriptableObject() {
            Gs2.Unity.Gs2Showcase.ScriptableObject.OwnShowcase parent = null;
            var guids = AssetDatabase.FindAssets("t:Gs2.Unity.Gs2Showcase.ScriptableObject.OwnShowcase");
            foreach (var guid in guids) {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var item = AssetDatabase.LoadAssetAtPath<Gs2.Unity.Gs2Showcase.ScriptableObject.OwnShowcase>(path);
                if (
                    item.NamespaceName == NamespaceName &&
                    item.ShowcaseName == ShowcaseName
                ) {
                    parent = item;
                }
            }
            if (parent == null) {
                Debug.LogError("Gs2.Unity.Gs2Showcase.ScriptableObject.OwnShowcase not found.");
                return null;
            }
            var instance = Gs2.Unity.Gs2Showcase.ScriptableObject.OwnDisplayItem.New(
                parent,
                this._item.DisplayItemId
            );
            instance.name = this._item.DisplayItemId + "DisplayItem";
            return instance;
        }

        public override void OnGUI() {
            DisplayItemEditorExt.OnGUI(this._item);
            
            if (GUILayout.Button("Create Reference Object")) {
                var directory = "Assets/Gs2/Resources";
                directory += "/Namespace" + "/" + NamespaceName;
                directory += "/Showcase" + "/" + ShowcaseName;
                directory += "/DisplayItem" + "/" + DisplayItemId;

                CreateFolder(directory);

                var instance = ToScriptableObject();
                if (instance != null) {
                    AssetDatabase.CreateAsset(instance, AssetDatabase.GenerateUniqueAssetPath(directory + "/" + instance.name + ".asset"));
                    AssetDatabase.SaveAssets();
                }
            }
        }
    }
}