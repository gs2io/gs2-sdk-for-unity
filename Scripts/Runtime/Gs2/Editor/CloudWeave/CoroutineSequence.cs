/*
 * CoroutineSequence.cs
 * 
 * Copyright (c) 2016 Kazunori Tamura
 * This software is released under the MIT License.
 * http://opensource.org/licenses/mit-license.php
 */

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.EditorCoroutines.Editor;

namespace Gs2.CloudWeave
{
    /// <summary>
    /// コルーチンを組み合わせて実行するためのSequenceクラス
    /// </summary>
    public class CoroutineSequence
    {
        /// <summary>
        /// Insertで追加されたEnumeratorを管理するクラス
        /// </summary>
        private class InsertedEnumerator
        {
            /// <summary>
            /// 位置
            /// </summary>
            private float _atPosition;

            /// <summary>
            /// 内部のIEnumerator
            /// </summary>
            public IEnumerator InternalEnumerator { get; private set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public InsertedEnumerator(float atPosition, IEnumerator enumerator)
            {
                _atPosition = atPosition;
                InternalEnumerator = enumerator;
            }

            /// <summary>
            /// Enumeratorの取得
            /// </summary>
            public IEnumerator GetEnumerator(Action callback)
            {
                if (_atPosition > 0f)
                {
                    yield return new WaitForSeconds(_atPosition);
                }
                yield return InternalEnumerator;
                if (callback != null)
                {
                    callback();
                }
            }
        }

        /// <summary>
        /// Insertされたenumerator
        /// </summary>
        private List<InsertedEnumerator> _insertedEnumerators;

        /// <summary>
        /// Appendされたenumerator
        /// </summary>
        private List<IEnumerator> _appendedEnumerators;

        /// <summary>
        /// 終了時に実行するAction
        /// </summary>
        private Action _onCompleted;

        /// <summary>
        /// 内部で実行されたコルーチンのリスト
        /// </summary>
        private List<EditorCoroutine> _coroutines;

        /// <summary>
        /// 追加されたCoroutineSequenceのリスト
        /// </summary>
        private List<CoroutineSequence> _sequences;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CoroutineSequence()
        {
            _insertedEnumerators = new List<InsertedEnumerator>();
            _appendedEnumerators = new List<IEnumerator>();
            _coroutines = new List<EditorCoroutine>();
            _sequences = new List<CoroutineSequence>();
        }

        /// <summary>
        /// enumeratorをatPositionにInsertする
        /// atPosition秒後にenumeratorが実行される
        /// </summary>
        public CoroutineSequence Insert(float atPosition, IEnumerator enumerator)
        {
            _insertedEnumerators.Add(new InsertedEnumerator(atPosition, enumerator));
            return this;
        }

        /// <summary>
        /// CoroutineSequenceをatPositionにInsertする
        /// </summary>
        public CoroutineSequence Insert(float atPosition, CoroutineSequence sequence)
        {
            _insertedEnumerators.Add(new InsertedEnumerator(atPosition, sequence.GetEnumerator()));
            _sequences.Add(sequence);
            return this;
        }

        /// <summary>
        /// callbackをatPositionにInsertする
        /// </summary>
        public CoroutineSequence InsertCallback(float atPosition, Action callback)
        {
            _insertedEnumerators.Add(new InsertedEnumerator(atPosition, GetCallbackEnumerator(callback)));
            return this;
        }

        /// <summary>
        /// enumeratorをAppendする
        /// Appendされたenumeratorは、Insertされたenumeratorが全て実行された後に順番に実行される
        /// </summary>
        public CoroutineSequence Append(IEnumerator enumerator)
        {
            _appendedEnumerators.Add(enumerator);
            return this;
        }

        /// <summary>
        /// CoroutineSequenceをAppendする
        /// </summary>
        public CoroutineSequence Append(CoroutineSequence sequence)
        {
            _appendedEnumerators.Add(sequence.GetEnumerator());
            _sequences.Add(sequence);
            return this;
        }

        /// <summary>
        /// callbackをAppendする
        /// </summary>
        public CoroutineSequence AppendCallback(Action callback)
        {
            _appendedEnumerators.Add(GetCallbackEnumerator(callback));
            return this;
        }

        /// <summary>
        /// 待機をAppendする
        /// </summary>
        public CoroutineSequence AppendInterval(float seconds)
        {
            _appendedEnumerators.Add(GetWaitForSecondsEnumerator(seconds));
            return this;
        }

        /// <summary>
        /// 終了時の処理を追加する
        /// </summary>
        public CoroutineSequence OnCompleted(Action action)
        {
            _onCompleted += action;
            return this;
        }

        /// <summary>
        /// シーケンスを実行する
        /// </summary>
        public EditorCoroutine Play()
        {
            var coroutine = EditorCoroutineUtility.StartCoroutineOwnerless(GetEnumerator());
            _coroutines.Add(coroutine);
            return coroutine;
        }

        /// <summary>
        /// シーケンスを止める
        /// </summary>
        public void Stop()
        {
            foreach (EditorCoroutine coroutine in _coroutines)
            {
                EditorCoroutineUtility.StopCoroutine(coroutine);
            }
            foreach (CoroutineSequence sequence in _sequences)
            {
                sequence.Stop();
            }
            _coroutines.Clear();
            _insertedEnumerators.Clear();
            _appendedEnumerators.Clear();
            _sequences.Clear();
        }

        /// <summary>
        /// callbackを実行するIEnumeratorを取得する
        /// </summary>
        private IEnumerator GetCallbackEnumerator(Action callback)
        {
            callback();
            yield break;
        }

        /// <summary>
        /// seconds秒待機するIEnumeratorを取得する
        /// </summary>
        private IEnumerator GetWaitForSecondsEnumerator(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }

        /// <summary>
        /// シーケンスのIEnumeratorを取得する
        /// </summary>
        private IEnumerator GetEnumerator()
        {
            // InsertされたIEnumeratorの実行
            int counter = _insertedEnumerators.Count;
            foreach (InsertedEnumerator insertedEnumerator in _insertedEnumerators)
            {
                var coroutine = EditorCoroutineUtility.StartCoroutineOwnerless(insertedEnumerator.GetEnumerator(() =>
                {
                    counter--;
                }));
                _coroutines.Add(coroutine);
            }
            // InsertされたIEnumeratorが全て実行されるのを待つ
            while (counter > 0)
            {
                yield return null;
            }
            // AppendされたIEnumeratorの実行
            foreach (IEnumerator appendedEnumerator in _appendedEnumerators)
            {
                yield return appendedEnumerator;
            }
            // 終了時の処理
            if (_onCompleted != null)
            {
                _onCompleted();
            }
        }
    }
}