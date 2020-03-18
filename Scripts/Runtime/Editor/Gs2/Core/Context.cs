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

using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Core.Util;
using UnityEditor;

namespace Gs2.Editor.Core
{
    public class Context : EditorWindow
    {
        private static bool _sessionOpened;
        private static Gs2RestSession _session;
        public static Gs2RestSession Session
        {
            get
            {
                if (_sessionOpened)
                {
                    return _session;
                }

                if (ProjectToken == null)
                {
                    _session = new Gs2RestSession(
                        new ProjectTokenGs2Credential("", "")
                    );
                }
                else
                {
                    _session = new Gs2RestSession(
                        new ProjectTokenGs2Credential(OwnerId, ProjectToken)
                    );
                }

                RunCoroutineUtil.Run(_session.Open(
                    r =>
                    {
                        if (r.Error != null)
                        {
                            EditorUtility.DisplayDialog("Error", r.Error.Message, "OK");
                        }
                        else
                        {
                            _sessionOpened = true;
                        }
                    }
                ));

                return _session;
            }
            private set => _session = value;
        }

        public static string AccountToken;

        private static string projectToken;
        
        public static string ProjectToken
        {
            set
            {
                projectToken = value;
                
                if (_sessionOpened)
                {
                    RunCoroutineUtil.Run(_session.Close(() => {}));
                }

                _sessionOpened = false;
                _session = null;
            }
            get => projectToken;
        }

        public static string OwnerId;
        
    }
}