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
using Gs2.Editor.ResourceTree.Gs2Inventory.Editor;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Gs2.Editor.ResourceTree.Gs2Inventory
{
    public sealed class OwnItemSet : AbstractTreeViewItem
    {
        private InventoryModel _parent;
        private Gs2.Gs2Inventory.Model.ItemModel _item;
        public string NamespaceName => _parent.NamespaceName;
        public string InventoryName => _parent.InventoryName;
        public string ItemName => _item.Name;

        public OwnItemSet(
                int id,
                InventoryModel parent,
                Gs2.Gs2Inventory.Model.ItemModel item
        ) {
            this.id = id = id * 100;
            this.depth = 7;
            this.icon = EditorGUIUtility.ObjectContent(null, typeof(GameObject)).image.ToTexture2D();
            this.displayName = "OwnItemSet";
            this.children = new TreeViewItem[] {
            }.ToList();
            this._parent = parent;
            this._item = item;
        }

        public override ScriptableObject ToScriptableObject() {
            Gs2.Unity.Gs2Inventory.ScriptableObject.OwnInventory parent = null;
            var guids = AssetDatabase.FindAssets("t:Gs2.Unity.Gs2Inventory.ScriptableObject.OwnInventory");
            foreach (var guid in guids) {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var item = AssetDatabase.LoadAssetAtPath<Gs2.Unity.Gs2Inventory.ScriptableObject.OwnInventory>(path);
                if (
                    item.NamespaceName == NamespaceName &&
                    item.InventoryName == InventoryName
                ) {
                    parent = item;
                }
            }
            if (parent == null) {
                Debug.LogError("Gs2.Unity.Gs2Inventory.ScriptableObject.OwnInventory not found.");
                return null;
            }
            var instance = Gs2.Unity.Gs2Inventory.ScriptableObject.OwnItemSet.New(
                parent,
                this._item.Name,
                null
            );
            instance.name = this._item.Name + "OwnItemSet";
            return instance;
        }

        public override void OnGUI() {
            if (GUILayout.Button("Create Reference Object")) {
                var directory = "Assets/Gs2/Resources/Inventory";
                directory += "/Namespace/" + NamespaceName;
                directory += "/InventoryModel/" + InventoryName;
                directory += "/ItemModel/" + ItemName;

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