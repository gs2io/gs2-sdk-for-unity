using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gs2.Core.Net;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Gs2.Editor.ResourceTree.Core
{
    public abstract class AbstractTreeViewItem : TreeViewItem
    {
        public virtual void Render(TreeView view, IList<TreeViewItem> rows, Gs2RestSession session, Func<bool> reload) {
            rows.Add(this);
            if (session == null) {
                return;
            }
            if (!view.IsExpanded(id)) {
                return;
            }
            foreach (var item in children.OfType<AbstractTreeViewItem>()) {
                item.Render(view, rows, session, reload);
            }
            foreach (var item in children.OfType<Loading>()) {
            }
        }

        public virtual IEnumerator Load(TreeView view, Gs2RestSession session, Func<bool> reload) {
            yield return null;
        }

        public virtual ScriptableObject ToScriptableObject() {
            return null;
        }

        public virtual void Reload() {
            
        }

        public abstract void OnGUI();
        
        protected static void CreateFolder(string path)
        {
            var target = "";
            var splitChars = new char[]{ Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
            foreach (var dir in path.Split(splitChars)) {
                var parent = target;
                target = Path.Combine(target, dir);
                if (!AssetDatabase.IsValidFolder(target)) {
                    AssetDatabase.CreateFolder(parent, dir);
                }
            }
        }
    }
}
