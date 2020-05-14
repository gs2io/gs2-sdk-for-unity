using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Gs2.Core.Util
{
	public class RunCoroutineUtil
	{
		private List<Runner> _runners;

		public RunCoroutineUtil()
		{
			_runners = new List<Runner>();
		}

		public RunCoroutineUtil(IEnumerator e) : this()
		{
			Register(e);
		}

		public void Register(IEnumerator e)
		{
			_runners.Add(new Runner(e));
		}

		public bool Step()
		{
			var isComplete = true;

			var allRunners = new List<Runner>();

			// 実行中のコールバックからでも Register() できるように
			var runners = _runners;
			_runners = new List<Runner>();

			foreach (var runner in runners)
			{
				isComplete = isComplete && runner.Step();
			}

			// 追加分は追加時に1ステップ実行されるので、結果だけ反映する
			foreach (var runner in _runners)
			{
				isComplete = isComplete && runner.IsComplete;
			}

			// 一応、追加順を維持
			runners.AddRange(_runners);
			_runners = runners;

			return isComplete;
		}

		public bool Run(int seconds = 60)
		{
			var left = TimeSpan.FromSeconds(seconds);

			do
			{
				if (left < TimeSpan.FromSeconds(0))
				{
					return false;
				}

				Thread.Sleep(TimeSpan.FromMilliseconds(16));
				left -= TimeSpan.FromMilliseconds(16);
			} while (!Step());

			return true;
		}
		
		private class Runner
		{
			private readonly Stack<object> _stack;
			public bool IsComplete { get; private set; }

			public Runner(IEnumerator e)
			{
				_stack = new Stack<object>();
				_stack.Push(e);

				// 一瞬で終わる場合の確認のために、1ステップ実行しておく
				Step();
			}

			public bool Step()
			{
				while (_stack.Count > 0)
				{
					var enumerator = _stack.Peek() as IEnumerator;
					if (enumerator != null)
					{
						if (enumerator.MoveNext())
						{
							if (enumerator.Current is IEnumerator || enumerator.Current is AsyncOperation)
							{
								_stack.Push(enumerator.Current);
								continue;
							}
						}
						else
						{
							_stack.Pop();
							continue;
						}

						break;
					}

					var asyncOperation = _stack.Peek() as AsyncOperation;
					if (asyncOperation != null)
					{
						if (asyncOperation.isDone)
						{
							_stack.Pop();
							continue;
						}

						break;
					}

					throw new InvalidCastException();
				}

				IsComplete = _stack.Count == 0;
				return IsComplete;
			}
		}

		public static bool Run(IEnumerator e, int seconds = 60)
		{
			return new RunCoroutineUtil(e).Run(seconds);
		}
	}
}

