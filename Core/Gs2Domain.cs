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
using Gs2.Core.Net;
using Gs2.Core.Domain;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Gs2.Unity.Core
{
    public class Gs2Domain
    {
        private Gs2.Core.Domain.Gs2 _gs2;
        
        public Gs2Account.Domain.Gs2Account Account;
        public Gs2Auth.Domain.Gs2Auth Auth;
        public Gs2Chat.Domain.Gs2Chat Chat;
        public Gs2Datastore.Domain.Gs2Datastore Datastore;
        public Gs2Dictionary.Domain.Gs2Dictionary Dictionary;
        public Gs2Distributor.Domain.Gs2Distributor Distributor;
        public Gs2Enhance.Domain.Gs2Enhance Enhance;
        public Gs2Exchange.Domain.Gs2Exchange Exchange;
        public Gs2Experience.Domain.Gs2Experience Experience;
        public Gs2Formation.Domain.Gs2Formation Formation;
        public Gs2Friend.Domain.Gs2Friend Friend;
        public Gs2Gateway.Domain.Gs2Gateway Gateway;
        public Gs2Inbox.Domain.Gs2Inbox Inbox;
        public Gs2Inventory.Domain.Gs2Inventory Inventory;
        public Gs2JobQueue.Domain.Gs2JobQueue JobQueue;
        public Gs2Limit.Domain.Gs2Limit Limit;
        public Gs2Lock.Domain.Gs2Lock Lock;
        public Gs2Lottery.Domain.Gs2Lottery Lottery;
        public Gs2Matchmaking.Domain.Gs2Matchmaking Matchmaking;
        public Gs2MegaField.Domain.Gs2MegaField MegaField;
        public Gs2Mission.Domain.Gs2Mission Mission;
        public Gs2Money.Domain.Gs2Money Money;
        public Gs2News.Domain.Gs2News News;
        public Gs2Quest.Domain.Gs2Quest Quest;
        public Gs2Ranking.Domain.Gs2Ranking Ranking;
        public Gs2Realtime.Domain.Gs2Realtime Realtime;
        public Gs2Schedule.Domain.Gs2Schedule Schedule;
        public Gs2Showcase.Domain.Gs2Showcase Showcase;
        public Gs2Stamina.Domain.Gs2Stamina Stamina;
        public Gs2Version.Domain.Gs2Version Version;

        public Gs2.Core.Domain.Gs2 Super => _gs2;

        public Gs2Domain(
            Profile profile,
            string distributorNamespaceName = null
        )
        {
            _gs2 = new Gs2.Core.Domain.Gs2(
                profile.Gs2RestSession,
                profile.Gs2Session,
                distributorNamespaceName
            );
            
            Account = new Gs2Account.Domain.Gs2Account(_gs2.Account, profile);
            Auth = new Gs2Auth.Domain.Gs2Auth(_gs2.Auth, profile);
            Chat = new Gs2Chat.Domain.Gs2Chat(_gs2.Chat, profile);
            Datastore = new Gs2Datastore.Domain.Gs2Datastore(_gs2.Datastore, profile);
            Dictionary = new Gs2Dictionary.Domain.Gs2Dictionary(_gs2.Dictionary, profile);
            Distributor = new Gs2Distributor.Domain.Gs2Distributor(_gs2.Distributor, profile);
            Enhance = new Gs2Enhance.Domain.Gs2Enhance(_gs2.Enhance, profile);
            Exchange = new Gs2Exchange.Domain.Gs2Exchange(_gs2.Exchange, profile);
            Experience = new Gs2Experience.Domain.Gs2Experience(_gs2.Experience, profile);
            Formation = new Gs2Formation.Domain.Gs2Formation(_gs2.Formation, profile);
            Friend = new Gs2Friend.Domain.Gs2Friend(_gs2.Friend, profile);
            Gateway = new Gs2Gateway.Domain.Gs2Gateway(_gs2.Gateway, profile);
            Inbox = new Gs2Inbox.Domain.Gs2Inbox(_gs2.Inbox, profile);
            Inventory = new Gs2Inventory.Domain.Gs2Inventory(_gs2.Inventory, profile);
            JobQueue = new Gs2JobQueue.Domain.Gs2JobQueue(_gs2.JobQueue, profile);
            Limit = new Gs2Limit.Domain.Gs2Limit(_gs2.Limit, profile);
            Lock = new Gs2Lock.Domain.Gs2Lock(_gs2.Lock, profile);
            Lottery = new Gs2Lottery.Domain.Gs2Lottery(_gs2.Lottery, profile);
            Matchmaking = new Gs2Matchmaking.Domain.Gs2Matchmaking(_gs2.Matchmaking, profile);
            MegaField = new Gs2MegaField.Domain.Gs2MegaField(_gs2.MegaField, profile);
            Mission = new Gs2Mission.Domain.Gs2Mission(_gs2.Mission, profile);
            Money = new Gs2Money.Domain.Gs2Money(_gs2.Money, profile);
            News = new Gs2News.Domain.Gs2News(_gs2.News, profile);
            Quest = new Gs2Quest.Domain.Gs2Quest(_gs2.Quest, profile);
            Ranking = new Gs2Ranking.Domain.Gs2Ranking(_gs2.Ranking, profile);
            Realtime = new Gs2Realtime.Domain.Gs2Realtime(_gs2.Realtime, profile);
            Schedule = new Gs2Schedule.Domain.Gs2Schedule(_gs2.Schedule, profile);
            Showcase = new Gs2Showcase.Domain.Gs2Showcase(_gs2.Showcase, profile);
            Stamina = new Gs2Stamina.Domain.Gs2Stamina(_gs2.Stamina, profile);
            Version = new Gs2Version.Domain.Gs2Version(_gs2.Version, profile);
        }

        public void ClearCache()
        {
            _gs2.ClearCache();
        }
        
#if GS2_ENABLE_UNITASK
        public async UniTask DispatchAsync(
            GameSession gameSession
        )
        {
            await _gs2.DispatchAsync(
                gameSession.AccessToken
            );
        }
#else
        public Gs2Future<bool> Dispatch(
            GameSession gameSession
        )
        {
            return _gs2.Dispatch(
                gameSession.AccessToken
            );
        }
#endif

#if GS2_ENABLE_UNITASK
        public async UniTask Disconnect()
        {
            await _gs2.Disconnect();
        }
#else
        public IEnumerator Disconnect()
        {
            yield return _gs2.Disconnect();
        }
#endif
    }
}