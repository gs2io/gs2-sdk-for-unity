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
using Gs2.Core.Exception;

namespace Gs2.Core.Net
{
    // すべてのインターフェースは、原則として Gs2Session のロックの中から使用のこと
    public abstract class Gs2SessionTask
    {
        public static TimeSpan DefaultTimeout => TimeSpan.FromSeconds(30);
        public TimeSpan Timeout = DefaultTimeout;
        private DateTime _timeLimit = DateTime.MaxValue;

        private bool _isCancelled = false;
        public Gs2SessionTaskId Gs2SessionTaskId = null;    // Execute 時に Gs2Session から付与される

        private Gs2Response _gs2Response = null;

        public bool IsCompleted
        {
            get
            {
                if (_gs2Response != null)
                {
                    return true;
                }

                if (DateTime.Now >= _timeLimit)
                {
                    Complete(null);    // レスポンスは Complete のなかで RequestTimeoutException が当てられる
                    return true;
                }

                return false;
            }
        }

        public void Complete(Gs2Response gs2Response)
        {
            if (_gs2Response != null)
            {
                // タイムアウトやキャンセルと応答が入れ違いになったとしても、最初に決めた結果を採用する
            }
            else if (_isCancelled)
            {
                // キャンセルがかけられていれば、実際の応答が何であれ、キャンセルによる失敗として扱う
                _gs2Response = new Gs2Response(new SessionNotOpenException("Cancelled."));
            }
            else if (DateTime.Now >= _timeLimit)
            {
                // タイムアウトしていれば、実際の応答が何であれ、タイムアウトによる失敗として扱う
                _gs2Response = new Gs2Response(new RequestTimeoutException("The request timed out."));
            }
            else
            {
                _gs2Response = gs2Response;
            }
        }

        public void Cancel()
        {
            _isCancelled = true;    // ExecuteImpl() の処理が本当にキャンセルされるまで Complete しない

            CancelImpl();
        }

        // Gs2Session の実行中タスクの登録からはずれたあとに、 Gs2Session のロックの外から呼ぶ
        public void InvokeCallback(Gs2Response gs2Response = null)
        {
            InvokeCallbackImpl(gs2Response ?? _gs2Response);
        }

        public IEnumerator Execute(Gs2Session gs2Session)
        {
            _timeLimit = DateTime.Now + Timeout;

            return ExecuteImpl(gs2Session);
        }

        // 応答は Gs2Session.OnMessage() に届くようにすること（Gs2Session のロックの中で扱うため）
        // コルーチンの終了は応答が返る前でも構わない
        protected abstract IEnumerator ExecuteImpl(Gs2Session gs2Session);

        protected abstract void CancelImpl();
        protected abstract void InvokeCallbackImpl(Gs2Response gs2Response);

        // ExecuteImpl() が一瞬で終わる場合に返す空のコルーチン
        protected class EmptyCoroutine : IEnumerator
        {
            public object Current => null;
            public bool MoveNext() => false;
            public void Reset() {}
        }
    }
}
