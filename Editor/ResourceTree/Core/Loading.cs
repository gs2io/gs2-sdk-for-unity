using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Net;
using Gs2.Core.Util;
using Gs2.Editor.ResourceTree.Core;
using UnityEditor.IMGUI.Controls;

namespace Gs2.Editor.ResourceTree.Core
{
    public sealed class Loading : AbstractTreeViewItem
    {
        private AbstractTreeViewItem _parent;
        
        public Loading(AbstractTreeViewItem parent, int id, int depth) {
            this.id = id;
            this.depth = depth;
            this.displayName = "Loading";

            this._parent = parent;
        }

        public override void Render(TreeView view, IList<TreeViewItem> rows, Gs2RestSession session, Func<bool> reload) {
            rows.Add(this);
            RunCoroutineUtil.Run(Invoke(view, session, reload));
        }

        public IEnumerator Invoke(TreeView view, Gs2RestSession session, Func<bool> reload) {
            yield return _parent.Load(view, session, reload);
        }

        public override void OnGUI() {
            
        }
    }
}
