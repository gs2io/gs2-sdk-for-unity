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
using System.Collections.Generic;
using Gs2.Core.Exception;
using Gs2.Core.Model;
using Gs2.Core.Result;
using Gs2.Core.Util;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Core.Net
{
    using OpenCallbackType = UnityAction<AsyncResult<Result.OpenResult>>;
    using CloseCallbackType = UnityAction;

    public abstract class Gs2Session
    {
        private enum State
        {
            Idle,               // セッションが未オープン
            Opening,            // セッションのオープン処理中
            CancellingOpen,     // セッションのオープン処理中断中
            Available,          // セッションがオープン済み
            CancellingTasks,    // オープン済みセッションをクローズしようとして、タスクのキャンセル完了待ち
            Closing,            // セッションのクローズ処理中
            Closed,             // セッションがクローズされて、タスクのキャンセル完了待ち
        }

        private class OpenTask
        {
            private readonly OpenCallbackType _callback;
            private AsyncResult<OpenResult> _asyncResult = null;
            public bool IsCompleted => _asyncResult != null;

            public OpenTask(OpenCallbackType callback)
            {
                _callback = callback;
            }

            public void Complete(AsyncResult<OpenResult> asyncResult)
            {
                _asyncResult = asyncResult;
            }

            public void InvokeCallback()
            {
                _callback.Invoke(_asyncResult);
            }
        }

        private class CloseTask
        {
            private readonly CloseCallbackType _callback;
            public bool IsCompleted { get; private set; } = false;

            public CloseTask(CloseCallbackType callback)
            {
                _callback = callback;
            }

            public void Complete()
            {
                IsCompleted = true;
            }

            public void InvokeCallback()
            {
                _callback.Invoke();
            }
        }
        
        private readonly NonreentrantLock _lock = new NonreentrantLock();
        
        private State _state = State.Idle;

        public string ProjectToken = null;

        private readonly List<OpenTask> _openTaskList = new List<OpenTask>();
        private readonly List<CloseTask> _closeTaskList = new List<CloseTask>();
        private List<Gs2SessionTask> _gs2SessionTaskList = new List<Gs2SessionTask>();

        private readonly Gs2SessionTaskId.Generator _gs2SessionTaskIdGenerator = new Gs2SessionTaskId.Generator();

        /// <summary>
        ///  セッションがクローズされた際のコールバックを指定します。<br />
        ///  <br />
        ///  このコールバックは、オープン状態だったセッションが、クローズ操作ないしは外部要因によってクローズド状態へ移行した場合に呼び出されます。<br />
        ///  ただし、オープン状態のセッションに対して同時に複数のクローズ操作をおこなった場合、1回しか呼び出されません。<br />
        ///  また、すでにクローズド状態のセッションに対してクローズ操作をおこなった場合や、オープン操作の完了前にクローズ操作を開始してオープン操作がキャンセルされた場合には呼び出されません。<br />
        /// </summary>
        public CloseCallbackType OnClose { private get; set; }
        
        public BasicGs2Credential Credential { get; }
        public Region Region { get; }
        
        public Gs2Session(BasicGs2Credential basicGs2Credential)
        {
            Credential = basicGs2Credential;
            Region = Region.ApNortheast1;
        }

        public Gs2Session(BasicGs2Credential basicGs2Credential, Region region) : this(basicGs2Credential)
        {
            Region = region;
        }

        public Gs2Session(BasicGs2Credential basicGs2Credential, string region) : this(basicGs2Credential)
        {
            Region = RegionExt.ValueOf(region);
        }

        private void CompleteOpenTasks(AsyncResult<OpenResult> asyncResult)
        {
            foreach (var openTask in _openTaskList)
            {
                openTask.Complete(asyncResult);
            }
            _openTaskList.Clear();
        }

        private void CompleteCloseTasks()
        {
            foreach (var closeTask in _closeTaskList)
            {
                closeTask.Complete();
            }
            _closeTaskList.Clear();
        }

        private void CompleteGs2SessionTasks(Gs2Response gs2Response)
        {
            foreach (var gs2SessionTask in _gs2SessionTaskList)
            {
                gs2SessionTask.Complete(gs2Response);
            }
        }

        private List<Gs2SessionTask> MoveGs2SessionTaskList()
        {
            var gs2SessionTaskList = _gs2SessionTaskList;
            _gs2SessionTaskList = new List<Gs2SessionTask>();
            return gs2SessionTaskList;
        }
        
        /// <summary>
        ///  セッションのオープン操作をおこなうコルーチンを返却します。<br />
        ///  <br />
        ///  コルーチンの完了時にはコールバックが返っていることが保証されます。<br />
        ///  ただし、オープン状態のセッションは外部要因によって予期せずクローズされることがあるため、オープン操作成功のコールバックが返った時点でも、セッションが確実にオープン状態であることは保証されません。<br />
        ///  <br />
        ///  セッションのオープン操作は、オープン途中やすでにオープン状態のセッションに対しても重ねておこなうことができます。<br />
        /// </summary>
        ///
        /// <returns>IEnumerator</returns>
        /// <param name="callback">コールバックハンドラ</param>
        public IEnumerator Open(OpenCallbackType callback)
        {
            var openTask = new OpenTask(callback);

            using (var scopedLock = new NonreentrantLock.ScopedLock(_lock))
            {
                if (_state == State.Available)
                {
                    openTask.Complete(new AsyncResult<OpenResult>(new OpenResult(), null));
                }
                else
                {
                    _openTaskList.Add(openTask);

                    while (true)
                    {
                        if (openTask.IsCompleted)
                        {
                            break;
                        }

                        IEnumerator current = null;

                        if (_state == State.Idle)
                        {
                            _state = State.Opening;
                            current = OpenImpl();
                        }

                        using (var scopedUnlock = new NonreentrantLock.ScopedUnlock(_lock))
                        {
                            yield return current;
                        }
                    }
                }
            }
                        
            openTask.InvokeCallback();
        }

        protected void OpenCallback(string projectToken, Gs2Exception e)
        {
            if (projectToken == null && e == null)
            {
                // 応答からプロジェクトトークンが取得できなかった場合
                // そのまま Idle 状態に遷移するので、 Open 失敗の後始末が必要な場合は派生クラスで対応が必要

                e = new UnknownException("No project token returned.");
            }

            using (var scopedLock = new NonreentrantLock.ScopedLock(_lock))
            {
                if (e == null)
                {
                    ProjectToken = projectToken;
                    _state = State.Available;

                    CompleteOpenTasks(new AsyncResult<OpenResult>(new OpenResult(), null));
                }
                else
                {
                    // キャンセルがかけられていれば、実際の失敗の内容が何であれ、キャンセルによる失敗として扱う
                    if (_state == State.CancellingOpen)
                    {
                        e = new SessionNotOpenException("Cancelled.");
                    }

                    _state = State.Idle;

                    // TODO: Open と Close のコールバックの順番の保証
                    CompleteOpenTasks(new AsyncResult<OpenResult>(null, e));
                    CompleteCloseTasks();
                }
            }
        }

        /// <summary>
        ///  セッションのクローズ操作をおこなうコルーチンを返却します。<br />
        ///  <br />
        ///  コルーチンの完了時にはコールバックが返っていることが保証されます。<br />
        ///  また、セッションのクローズ操作は必ず成功し、コールバックが返った時点でセッションがクローズド状態であり、そのセッションを利用していたすべての API の実行が完了していることが保証されます。<br />
        ///  ただし、複数のクローズ操作を同時に実行し、そのコールバックでオープン操作を開始した場合はその限りではありません。<br />
        ///  <br />
        ///  セッションに対して複数回のオープン操作をおこなっていた場合であっても、1回のクローズ操作でクローズド状態へ移行します。<br />
        /// </summary>
        ///
        /// <returns>IEnumerator</returns>
        /// <param name="callback">コールバックハンドラ</param>
        public IEnumerator Close(CloseCallbackType callback)
        {
            var closeTask = new CloseTask(callback);

            var isNowClosed = false;

            using (var scopedLock = new NonreentrantLock.ScopedLock(_lock))
            {
                if (_state == State.Idle)
                {
                    closeTask.Complete();
                }
                else
                {
                    _closeTaskList.Add(closeTask);
    
                    while (true)
                    {
                        if (closeTask.IsCompleted)
                        {
                            break;
                        }
    
                        IEnumerator current = null;
    
                        switch (_state)
                        {
                            case State.Opening:
                                _state = State.CancellingOpen;
                                current = CancelOpenImpl();
                                break;
                            
                            case State.Available:
                                ProjectToken = null;
                                if (_gs2SessionTaskList.Count == 0)
                                {
                                    _state = State.Closing;
                                    current = CloseImpl();
                                }
                                else
                                {
                                    _state = State.CancellingTasks;
                                    foreach (var gs2SessionTask in _gs2SessionTaskList)
                                    {
                                        gs2SessionTask.Cancel();
                                    }
                                }
                                break;
    
                            case State.CancellingTasks:
                                if (_gs2SessionTaskList.Count == 0)
                                {
                                    _state = State.Closing;
                                    current = CloseImpl();
                                }
                                break;
    
                            case State.Closed:
                                if (_gs2SessionTaskList.Count == 0)
                                {
                                    _state = State.Idle;

                                    isNowClosed = true;
                                    
                                    CompleteCloseTasks();

                                    continue;
                                }
                                break;
    
                            case State.Idle:
                            case State.CancellingOpen:
                            case State.Closing:
                                break;
                        }
    
                        using (var unlockScope = new NonreentrantLock.ScopedUnlock(_lock))
                        {
                            yield return current;
                        }
                    }
                }
            }

            if (isNowClosed)
            {
                OnClose?.Invoke();
            }
            
            closeTask.InvokeCallback();
        }

        protected void CloseCallback()
        {
            var isNowClosed = false;

            using (var scopedLock = new NonreentrantLock.ScopedLock(_lock))
            {
                ProjectToken = null;

                if (_gs2SessionTaskList.Count > 0)
                {
                    _state = State.Closed;

                    CompleteGs2SessionTasks(new Gs2Response(new SessionNotOpenException("Session no longer open.")));
                }
                else
                {
                    _state = State.Idle;

                    isNowClosed = true;
                
                    CompleteCloseTasks();
                }
            }

            if (isNowClosed)
            {
                OnClose?.Invoke();
            }
        }

        // 実行するセッションとタスクは型の整合が取れている必要があるので、派生クラスで型を限定したインターフェースを公開する
        protected IEnumerator Execute(Gs2SessionTask gs2SessionTask)
        {
            var isNowClosed = false;

            using (var scopedLock = new NonreentrantLock.ScopedLock(_lock))
            {
                if (_state == State.Available)
                {
                    gs2SessionTask.Gs2SessionTaskId = _gs2SessionTaskIdGenerator.Issue();
                    _gs2SessionTaskList.Add(gs2SessionTask);

                    var current = gs2SessionTask.Execute(this);

                    using (var unlockScope = new NonreentrantLock.ScopedUnlock(_lock))
                    {
                        yield return current;
                    }

                    while (!gs2SessionTask.IsCompleted)
                    {
                        using (var unlockScope = new NonreentrantLock.ScopedUnlock(_lock))
                        {
                            yield return null;
                        }
                    }

                    _gs2SessionTaskList.Remove(gs2SessionTask);

                    if (_gs2SessionTaskList.Count == 0 && _state == State.Closed)
                    {
                        _state = State.Idle;

                        isNowClosed = true;

                        CompleteCloseTasks();
                    };
                }
                else
                {
                    gs2SessionTask.Complete(new Gs2Response(new SessionNotOpenException("The session is not opened.")));
                }
            }

            gs2SessionTask.InvokeCallback();

            if (isNowClosed)
            {
                OnClose?.Invoke();
            }
        }

        public void OnMessage(Gs2SessionTaskId gs2SessionTaskId, Gs2Response gs2Response)
        {
            using (var scopedLock = new NonreentrantLock.ScopedLock(_lock))
            {
                _gs2SessionTaskList.Find(v => v.Gs2SessionTaskId == gs2SessionTaskId)?.Complete(gs2Response);
            }
        }
        
        protected abstract IEnumerator OpenImpl();
        protected abstract IEnumerator CancelOpenImpl();
        protected abstract IEnumerator CloseImpl();
    }
}
