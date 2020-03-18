﻿/*
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

using System.Collections.Generic;
using Gs2.Editor.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Core.Util;
using Gs2.Gs2Project;
using Gs2.Gs2Project.Request;
using Gs2.Tutorial;
using UnityEditor;
using UnityEngine;

namespace Gs2.Editor.Project
{
    public class SelectProjectWindow : EditorWindow
    {
        private List<global::Gs2.Gs2Project.Model.Project> _projects;

        void OnGUI()
        {
            if (Context.AccountToken == null)
            {
                GetWindow(typeof(SigninWindow), true, "Sign-in to GS2").Show();
                Close();
                return;
            }
            if (_projects == null)
            {
                RunCoroutineUtil.Run(new Gs2ProjectRestClient(
                    Context.Session
                ).DescribeProjects(
                    new DescribeProjectsRequest()
                        .WithAccountToken(Context.AccountToken),
                    r =>
                    {
                        if (r.Error != null)
                        {
                            EditorUtility.DisplayDialog("Error", r.Error.Message, "OK");
                            _projects = new List<global::Gs2.Gs2Project.Model.Project>();
                        }
                        else
                        {
                            _projects = r.Result.items;
                        }
                    }
                ));
            }

            if (GUILayout.Button("Reload"))
            {
                _projects = null;
            }

            if (_projects != null)
            {
                foreach (var project in _projects)
                {
                    using (new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label(project.name);
                        if (GUILayout.Button("選択"))
                        {
                            RunCoroutineUtil.Run(new Gs2ProjectRestClient(
                                Context.Session
                            ).GetProjectToken(
                                new GetProjectTokenRequest()
                                    .WithAccountToken(Context.AccountToken)
                                    .WithProjectName(project.name),
                                r =>
                                {
                                    if (r.Error != null)
                                    {
                                        EditorUtility.DisplayDialog("Error", r.Error.Message, "OK");
                                    }
                                    else
                                    {
                                        Context.ProjectToken = r.Result.projectToken;
                                        Context.OwnerId = r.Result.ownerId;

                                        if (bool.Parse(PlayerPrefs.GetString("io.gs2.tutorial", false.ToString())))
                                        {
                                            TutorialWindow.Open();
                                        }

                                        Close();
                                    }
                                }
                            ));
                        }
                    }
                }
            }
            
            GUILayout.Label("");
            if (GUILayout.Button("プロジェクトを新規作成"))
            {
                Application.OpenURL("https://app.gs2.io/project/create");
            }
        } 
    }
}