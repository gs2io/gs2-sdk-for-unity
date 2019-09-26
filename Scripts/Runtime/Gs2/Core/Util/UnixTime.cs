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

namespace Gs2.Core.Util
{
	public class UnixTime
	{
		// unix epochをDateTimeで表した定数
		public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		/// <summary>
		/// DateTimeをUNIX時間に変換するメソッド
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		public static long ToUnixTime(DateTime dateTime)
		{
			// 時刻をUTCに変換
			dateTime = dateTime.ToUniversalTime();

			// unix epochからの経過秒数を求める
			return (long)dateTime.Subtract(UnixEpoch).TotalMilliseconds;
		}

		/// <summary>
		/// UNIX時間からDateTimeに変換するメソッド
		/// </summary>
		/// <param name="unixTime"></param>
		/// <returns></returns>
		public static DateTime FromUnixTime(long unixTime)
		{
			// unix epochからunixTime秒だけ経過した時刻を求める
			return UnixEpoch.AddMilliseconds(unixTime);
		}
	}
}