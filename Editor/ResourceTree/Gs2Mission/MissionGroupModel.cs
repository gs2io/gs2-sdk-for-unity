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
using Gs2.Editor.ResourceTree.Gs2Mission.Editor;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Gs2.Editor.ResourceTree.Gs2Mission
{
    public sealed class MissionGroupModel : AbstractTreeViewItem
    {
        private Gs2.Gs2Mission.Model.MissionGroupModel _item;
        private Namespace _parent;
        public string NamespaceName => _parent.NamespaceName;
        public string MissionGroupName => _item.Name;

        public MissionGroupModel(
                int id,
                Namespace parent,
                Gs2.Gs2Mission.Model.MissionGroupModel item
        ) {
            this.id = id = id * 100;
            this.depth = 4;
            this.icon = EditorGUIUtility.ObjectContent(null, typeof(GameObject)).image.ToTexture2D();
            this.displayName = item.Name;
            this.children = new TreeViewItem[] {
                new MissionTaskModelHolder(++id, this)
            }.ToList();
            this._parent = parent;
            this._item = item;
        }

        public override ScriptableObject ToScriptableObject() {
            Gs2.Unity.Gs2Mission.ScriptableObject.Namespace parent = null;
            var guids = AssetDatabase.FindAssets("t:Gs2.Unity.Gs2Mission.ScriptableObject.Namespace");
            foreach (var guid in guids) {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var item = AssetDatabase.LoadAssetAtPath<Gs2.Unity.Gs2Mission.ScriptableObject.Namespace>(path);
                if (
                    item.NamespaceName == NamespaceName
                ) {
                    parent = item;
                }
            }
            if (parent == null) {
                Debug.LogError("Gs2.Unity.Gs2Mission.ScriptableObject.Namespace not found.");
                return null;
            }
            var instance = Gs2.Unity.Gs2Mission.ScriptableObject.MissionGroupModel.New(
                parent,
                this._item.Name
            );
            instance.name = this._item.Name + "MissionGroupModel";
            return instance;
        }

        public override void OnGUI() {
            MissionGroupModelEditorExt.OnGUI(this._item);
            
            if (GUILayout.Button("Create Reference Object")) {
                var directory = "Assets/Gs2/Resources/Mission";
                directory += "/Namespace" + "/" + NamespaceName;
                directory += "/MissionGroupModel" + "/" + MissionGroupName;

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