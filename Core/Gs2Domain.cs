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

using Cysharp.Threading.Tasks;
using Gs2.Core.Net;
using Gs2.Unity.Util;

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
        public Gs2Mission.Domain.Gs2Mission Mission;
        public Gs2Money.Domain.Gs2Money Money;
        public Gs2Quest.Domain.Gs2Quest Quest;
        public Gs2Ranking.Domain.Gs2Ranking Ranking;
        public Gs2Realtime.Domain.Gs2Realtime Realtime;
        public Gs2Schedule.Domain.Gs2Schedule Schedule;
        public Gs2Showcase.Domain.Gs2Showcase Showcase;
        public Gs2Stamina.Domain.Gs2Stamina Stamina;
        public Gs2Version.Domain.Gs2Version Version;
        
        public Gs2Domain(
            Profile profile
        )
        {
            _gs2 = new Gs2.Core.Domain.Gs2(
                profile.Gs2RestSession,
                profile.Gs2Session
            );
            
            Account = new Gs2Account.Domain.Gs2Account(_gs2.Account);
            Auth = new Gs2Auth.Domain.Gs2Auth(_gs2.Auth);
            Chat = new Gs2Chat.Domain.Gs2Chat(_gs2.Chat);
            Datastore = new Gs2Datastore.Domain.Gs2Datastore(_gs2.Datastore);
            Dictionary = new Gs2Dictionary.Domain.Gs2Dictionary(_gs2.Dictionary);
            Distributor = new Gs2Distributor.Domain.Gs2Distributor(_gs2.Distributor);
            Exchange = new Gs2Exchange.Domain.Gs2Exchange(_gs2.Exchange);
            Experience = new Gs2Experience.Domain.Gs2Experience(_gs2.Experience);
            Formation = new Gs2Formation.Domain.Gs2Formation(_gs2.Formation);
            Friend = new Gs2Friend.Domain.Gs2Friend(_gs2.Friend);
            Gateway = new Gs2Gateway.Domain.Gs2Gateway(_gs2.Gateway);
            Inbox = new Gs2Inbox.Domain.Gs2Inbox(_gs2.Inbox);
            Inventory = new Gs2Inventory.Domain.Gs2Inventory(_gs2.Inventory);
            JobQueue = new Gs2JobQueue.Domain.Gs2JobQueue(_gs2.JobQueue);
            Limit = new Gs2Limit.Domain.Gs2Limit(_gs2.Limit);
            Lock = new Gs2Lock.Domain.Gs2Lock(_gs2.Lock);
            Lottery = new Gs2Lottery.Domain.Gs2Lottery(_gs2.Lottery);
            Matchmaking = new Gs2Matchmaking.Domain.Gs2Matchmaking(_gs2.Matchmaking);
            Mission = new Gs2Mission.Domain.Gs2Mission(_gs2.Mission);
            Money = new Gs2Money.Domain.Gs2Money(_gs2.Money);
            Quest = new Gs2Quest.Domain.Gs2Quest(_gs2.Quest);
            Ranking = new Gs2Ranking.Domain.Gs2Ranking(_gs2.Ranking);
            Realtime = new Gs2Realtime.Domain.Gs2Realtime(_gs2.Realtime);
            Schedule = new Gs2Schedule.Domain.Gs2Schedule(_gs2.Schedule);
            Showcase = new Gs2Showcase.Domain.Gs2Showcase(_gs2.Showcase);
            Stamina = new Gs2Stamina.Domain.Gs2Stamina(_gs2.Stamina);
            Version = new Gs2Version.Domain.Gs2Version(_gs2.Version);
        }

        public void ClearCache()
        {
            _gs2.ClearCache();
        }
        
        public async UniTask DispatchAsync(
            GameSession gameSession
        )
        {
            await _gs2.DispatchAsync(
                gameSession.AccessToken
            );
        }
    }
}