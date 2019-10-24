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
using Gs2.Core;
using Gs2.Core.Exception;
using Gs2.Unity.Gs2Distributor.Result;
using UnityEngine.Events;

namespace Gs2.Unity.Util
{
	public class StampSheetStateMachine
	{
		public delegate void ErrorHandler(Gs2Exception e);
		public delegate void DoneStampTaskHandler(EzStampTask task, EzRunStampTaskResult result);
		public delegate void CompleteStampSheetHandler(EzStampSheet sheet, EzRunStampSheetResult result);
		
		public event ErrorHandler OnError;
		public event DoneStampTaskHandler OnDoneStampTask;
		public event CompleteStampSheetHandler OnCompleteStampSheet;

		private readonly EzStampSheet _stampSheet;
		private readonly Gs2.Unity.Client _client;
		private readonly string _distributorNamespaceName;
		private readonly string _stampSheetEncryptKeyId;

		private bool _running;
		
		public StampSheetStateMachine(
			string stampSheet,
			Gs2.Unity.Client client,
			string distributorNamespaceName,
			string stampSheetEncryptKeyId
		)
		{
			_stampSheet = new EzStampSheet(stampSheet);
			_client = client;
			_distributorNamespaceName = distributorNamespaceName;
			_stampSheetEncryptKeyId = stampSheetEncryptKeyId;
			_running = false;
		}

		public StampSheetStateMachine(
			EzStampSheet stampSheet,
			Gs2.Unity.Client client,
			string distributorNamespaceName,
			string stampSheetEncryptKeyId
		)
		{
			_stampSheet = stampSheet;
			_client = client;
			_distributorNamespaceName = distributorNamespaceName;
			_stampSheetEncryptKeyId = stampSheetEncryptKeyId;
			_running = false;
		}

		public IEnumerator Execute()
		{
			if (_running)
			{
				yield break;
			}

			_running = true;
			
			string stackContext = null;
			bool error = false;
			foreach (var task in _stampSheet.Tasks)
			{
				if (error)
				{
					yield break;
				}
				yield return task.Execute(
					r =>
					{
						if (r.Error != null)
						{
							if (OnError != null)
							{
								OnError.Invoke(r.Error);
							}

							error = true;
						}
						else
						{
							if (OnDoneStampTask != null)
							{
								OnDoneStampTask.Invoke(task, r.Result);
							}

							stackContext = r.Result.ContextStack;
						}
					},
					stackContext,
					_client,
					_distributorNamespaceName,
					_stampSheetEncryptKeyId
				);
			}
			if (error)
			{
				yield break;
			}
			
			yield return _stampSheet.Execute(
				r =>
				{
					if (r.Error != null)
					{
						if (OnError != null)
						{
							OnError.Invoke(r.Error);
						}
					}
					else
					{
						if (OnCompleteStampSheet != null)
						{
							OnCompleteStampSheet.Invoke(_stampSheet, r.Result);
						}
					}
				},
				stackContext,
				_client,
				_distributorNamespaceName,
				_stampSheetEncryptKeyId
			);
		}
	}
}