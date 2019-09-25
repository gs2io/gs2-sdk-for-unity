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
using UnityEngine.Assertions;

namespace Gs2.Core.Util
{
	public class NonreentrantLock
	{
		private readonly object _lockObject;
		private bool _isAlreadyLocked;

		public NonreentrantLock()
		{
			_lockObject = this;
			_isAlreadyLocked = false;
		}

		public NonreentrantLock(object lockObject)
		{
			_lockObject = lockObject;
			_isAlreadyLocked = false;
		}

		public void Enter()
		{
			var wasLockTaken = false;
			System.Threading.Monitor.Enter(_lockObject, ref wasLockTaken);

			if (_isAlreadyLocked)
			{
				Assert.IsTrue(false);
			}
			_isAlreadyLocked = true;
		}

		public void Exit()
		{
			_isAlreadyLocked = false;
			System.Threading.Monitor.Exit(_lockObject);
		}

		public class ScopedLock : IDisposable
		{
			private readonly NonreentrantLock _nonreentrantLock;

			public ScopedLock(NonreentrantLock nonreentrantLock)
			{
				_nonreentrantLock = nonreentrantLock;
				_nonreentrantLock.Enter();
			}

			public void Dispose()
			{
				_nonreentrantLock.Exit();
			}
		}

		public class ScopedUnlock : IDisposable
		{
			private readonly NonreentrantLock _nonreentrantLock;

			public ScopedUnlock(NonreentrantLock nonreentrantLock)
			{
				_nonreentrantLock = nonreentrantLock;
				_nonreentrantLock.Exit();
			}

			public void Dispose()
			{
				_nonreentrantLock.Enter();
			}
		}
	}
}