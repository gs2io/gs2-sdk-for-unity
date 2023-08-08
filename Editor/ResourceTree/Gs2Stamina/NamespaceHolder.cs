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

using System;
using System.Collections;
using System.Linq;
using Gs2.Core.Net;
using Gs2.Core.Exception;
using Gs2.Editor.ResourceTree.Core;
using Gs2.Gs2Stamina;
using Gs2.Gs2Stamina.Request;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Gs2.Editor.ResourceTree.Gs2Stamina
{
    public sealed class NamespaceHolder : AbstractTreeViewItem
    {

        public NamespaceHolder(
                int id
        ) {
            this.id = id = id * 100;
            this.depth = 1;
            this.displayName = "Namespace";
            this.children = new TreeViewItem[] {
                new Loading(this, ++id, this.depth + 1),
            }.ToList();
        }

        public override void Reload() {
            this.children = new TreeViewItem[] {
                new Loading(this, id + 1, this.depth + 1),
            }.ToList();
        }

        public override IEnumerator Load(TreeView view, Gs2RestSession session, Func<bool> reload) {
            var future = new Gs2StaminaRestClient(session).DescribeNamespacesFuture(
                new DescribeNamespacesRequest()
            );
            yield return future;
            if (future.Error != null) {
                if (future.Error is NotFoundException) {
                    this.children = new TreeViewItem[] {
                    }.ToList();
                    reload.Invoke();
                    yield break;
                }
                Debug.LogError(future.Error.Message);
                yield break;
            }
            var id = this.id;
            this.children = future.Result.Items.Select(
                v => new Namespace(++id, v)
            ).ToList<TreeViewItem>();

            reload.Invoke();
        }

        public override void OnGUI() {

        }
    }
}