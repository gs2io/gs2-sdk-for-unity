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
using Gs2.Core.Model;
using Gs2.Unity.Util;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Gs2.Unity.Core
{
    public static class Gs2Client
    {
        public static Gs2Future<Gs2Domain> CreateFuture(
            IGs2Credential credential,
            Region region = Region.ApNortheast1,
            string distributorNamespaceName = "default"
        ) {
            IEnumerator Impl(Gs2Future<Gs2Domain> self)
            {
                var connection = new Gs2Connection(
                    credential,
                    region
                );
                var future = connection.ConnectFuture();
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                var domain = new Gs2Domain(
                    connection,
                    distributorNamespaceName
                );
                self.OnComplete(domain);
            }

            return new Gs2InlineFuture<Gs2Domain>(Impl);
        }
        
#if GS2_ENABLE_UNITASK
        public static async UniTask<Gs2Domain> CreateAsync(
            IGs2Credential credential,
            Region region = Region.ApNortheast1,
            string distributorNamespaceName = "default"
        ) {
            var connection = new Gs2Connection(
                credential,
                region
            );
            await connection.ConnectAsync();
            return new Gs2Domain(
                connection,
                distributorNamespaceName
            );
        }
#endif
        
        public static Gs2Future<Gs2Domain> CreateChaosFuture(
            IGs2Credential credential,
            float chaos,
            Region region = Region.ApNortheast1,
            string distributorNamespaceName = "default"
        ) {
            IEnumerator Impl(Gs2Future<Gs2Domain> self)
            {
                var connection = new Gs2Connection(
                    credential,
                    region,
                    chaos
                );
                var future = connection.ConnectFuture();
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                var domain = new Gs2Domain(
                    connection,
                    distributorNamespaceName
                );
                self.OnComplete(domain);
            }

            return new Gs2InlineFuture<Gs2Domain>(Impl);
        }
        
#if GS2_ENABLE_UNITASK
        public static async UniTask<Gs2Domain> CreateChaosAsync(
            IGs2Credential credential,
            float chaos,
            Region region = Region.ApNortheast1,
            string distributorNamespaceName = "default"
        ) {
            var connection = new Gs2Connection(
                credential,
                region,
                chaos
            );
            await connection.ConnectAsync();
            return new Gs2Domain(
                connection,
                distributorNamespaceName
            );
        }
#endif
    }
    
    public class Gs2Domain
    {
        private Gs2.Core.Domain.Gs2 _gs2;
        
        public Gs2Account.Domain.Gs2Account Account;
        public Gs2AdReward.Domain.Gs2AdReward AdReward;
        public Gs2Buff.Domain.Gs2Buff Buff;
        public Gs2Chat.Domain.Gs2Chat Chat;
        public Gs2Datastore.Domain.Gs2Datastore Datastore;
        public Gs2Dictionary.Domain.Gs2Dictionary Dictionary;
        public Gs2Distributor.Domain.Gs2Distributor Distributor;
        public Gs2Enchant.Domain.Gs2Enchant Enchant;
        public Gs2Enhance.Domain.Gs2Enhance Enhance;
        public Gs2Exchange.Domain.Gs2Exchange Exchange;
        public Gs2Experience.Domain.Gs2Experience Experience;
        public Gs2Formation.Domain.Gs2Formation Formation;
        public Gs2Friend.Domain.Gs2Friend Friend;
        public Gs2Gateway.Domain.Gs2Gateway Gateway;
        public Gs2Grade.Domain.Gs2Grade Grade;
        public Gs2Guild.Domain.Gs2Guild Guild;
        public Gs2Idle.Domain.Gs2Idle Idle;
        public Gs2Inbox.Domain.Gs2Inbox Inbox;
        public Gs2Inventory.Domain.Gs2Inventory Inventory;
        public Gs2JobQueue.Domain.Gs2JobQueue JobQueue;
        public Gs2Limit.Domain.Gs2Limit Limit;
        public Gs2Log.Domain.Gs2Log Log;
        public Gs2LoginReward.Domain.Gs2LoginReward LoginReward;
        public Gs2Lottery.Domain.Gs2Lottery Lottery;
        public Gs2Matchmaking.Domain.Gs2Matchmaking Matchmaking;
        public Gs2MegaField.Domain.Gs2MegaField MegaField;
        public Gs2Mission.Domain.Gs2Mission Mission;
        public Gs2Money.Domain.Gs2Money Money;
        public Gs2Money2.Domain.Gs2Money2 Money2;
        public Gs2News.Domain.Gs2News News;
        public Gs2Quest.Domain.Gs2Quest Quest;
        public Gs2Ranking.Domain.Gs2Ranking Ranking;
        public Gs2Ranking2.Domain.Gs2Ranking2 Ranking2;
        public Gs2Realtime.Domain.Gs2Realtime Realtime;
        public Gs2Schedule.Domain.Gs2Schedule Schedule;
        public Gs2SeasonRating.Domain.Gs2SeasonRating SeasonRating;
        public Gs2SerialKey.Domain.Gs2SerialKey SerialKey;
        public Gs2Showcase.Domain.Gs2Showcase Showcase;
        public Gs2SkillTree.Domain.Gs2SkillTree SkillTree;
        public Gs2Stamina.Domain.Gs2Stamina Stamina;
        public Gs2StateMachine.Domain.Gs2StateMachine StateMachine;
        public Gs2Version.Domain.Gs2Version Version;

        public Gs2.Core.Domain.Gs2 Super => _gs2;

        public Gs2Connection Connection { get; private set; }

        [Obsolete("The method of initializing the SDK has changed; use Gs2Client.Create().")]
        public Gs2Domain(
            Profile profile,
            string distributorNamespaceName = null
        ): this(
            new Gs2Connection(
                profile.Gs2RestSession.Credential,
                profile.Gs2RestSession.Region
            ) {
                RestSession = profile.Gs2RestSession,
                WebSocketSession = profile.Gs2Session
            },
            distributorNamespaceName
        ) {
            
        }

        private void Initialize(
            Gs2Connection connection
        ) {
            Account = new Gs2Account.Domain.Gs2Account(_gs2.Account, connection);
            AdReward = new Gs2AdReward.Domain.Gs2AdReward(_gs2.AdReward, connection);
            Buff = new Gs2Buff.Domain.Gs2Buff(_gs2.Buff, connection);
            Chat = new Gs2Chat.Domain.Gs2Chat(_gs2.Chat, connection);
            Datastore = new Gs2Datastore.Domain.Gs2Datastore(_gs2.Datastore, connection);
            Dictionary = new Gs2Dictionary.Domain.Gs2Dictionary(_gs2.Dictionary, connection);
            Distributor = new Gs2Distributor.Domain.Gs2Distributor(_gs2.Distributor, connection);
            Enchant = new Gs2Enchant.Domain.Gs2Enchant(_gs2.Enchant, connection);
            Enhance = new Gs2Enhance.Domain.Gs2Enhance(_gs2.Enhance, connection);
            Exchange = new Gs2Exchange.Domain.Gs2Exchange(_gs2.Exchange, connection);
            Experience = new Gs2Experience.Domain.Gs2Experience(_gs2.Experience, connection);
            Formation = new Gs2Formation.Domain.Gs2Formation(_gs2.Formation, connection);
            Friend = new Gs2Friend.Domain.Gs2Friend(_gs2.Friend, connection);
            Gateway = new Gs2Gateway.Domain.Gs2Gateway(_gs2.Gateway, connection);
            Grade = new Gs2Grade.Domain.Gs2Grade(_gs2.Grade, connection);
            Guild = new Gs2Guild.Domain.Gs2Guild(_gs2.Guild, connection);
            Idle = new Gs2Idle.Domain.Gs2Idle(_gs2.Idle, connection);
            Inbox = new Gs2Inbox.Domain.Gs2Inbox(_gs2.Inbox, connection);
            Inventory = new Gs2Inventory.Domain.Gs2Inventory(_gs2.Inventory, connection);
            JobQueue = new Gs2JobQueue.Domain.Gs2JobQueue(_gs2.JobQueue, connection);
            Limit = new Gs2Limit.Domain.Gs2Limit(_gs2.Limit, connection);
            Log = new Gs2Log.Domain.Gs2Log(_gs2.Log, connection);
            LoginReward = new Gs2LoginReward.Domain.Gs2LoginReward(_gs2.LoginReward, connection);
            Lottery = new Gs2Lottery.Domain.Gs2Lottery(_gs2.Lottery, connection);
            Matchmaking = new Gs2Matchmaking.Domain.Gs2Matchmaking(_gs2.Matchmaking, connection);
            MegaField = new Gs2MegaField.Domain.Gs2MegaField(_gs2.MegaField, connection);
            Mission = new Gs2Mission.Domain.Gs2Mission(_gs2.Mission, connection);
            Money = new Gs2Money.Domain.Gs2Money(_gs2.Money, connection);
            Money2 = new Gs2Money2.Domain.Gs2Money2(_gs2.Money2, connection);
            News = new Gs2News.Domain.Gs2News(_gs2.News, connection);
            Quest = new Gs2Quest.Domain.Gs2Quest(_gs2.Quest, connection);
            Ranking = new Gs2Ranking.Domain.Gs2Ranking(_gs2.Ranking, connection);
            Ranking2 = new Gs2Ranking2.Domain.Gs2Ranking2(_gs2.Ranking2, connection);
            Realtime = new Gs2Realtime.Domain.Gs2Realtime(_gs2.Realtime, connection);
            Schedule = new Gs2Schedule.Domain.Gs2Schedule(_gs2.Schedule, connection);
            SeasonRating = new Gs2SeasonRating.Domain.Gs2SeasonRating(_gs2.SeasonRating, connection);
            SerialKey = new Gs2SerialKey.Domain.Gs2SerialKey(_gs2.SerialKey, connection);
            Showcase = new Gs2Showcase.Domain.Gs2Showcase(_gs2.Showcase, connection);
            SkillTree = new Gs2SkillTree.Domain.Gs2SkillTree(_gs2.SkillTree, connection);
            Stamina = new Gs2Stamina.Domain.Gs2Stamina(_gs2.Stamina, connection);
            StateMachine = new Gs2StateMachine.Domain.Gs2StateMachine(_gs2.StateMachine, connection);
            Version = new Gs2Version.Domain.Gs2Version(_gs2.Version, connection);
        }

        public Gs2Domain(
            Gs2.Core.Domain.Gs2 gs2
        ): this(
            new Gs2Connection(
                gs2.RestSession.Credential,
                gs2.RestSession.Region
            ) {
                RestSession = gs2.RestSession,
                WebSocketSession = gs2.WebSocketSession
            },
            gs2.DistributorNamespaceName
        ) {
            this._gs2 = gs2;
            Initialize(Connection);
        }

        internal Gs2Domain(
            Gs2Connection connection,
            string distributorNamespaceName = null
        ) {
            Connection = connection;
            
            _gs2 = new Gs2.Core.Domain.Gs2(
                connection.RestSession,
                connection.WebSocketSession,
                distributorNamespaceName
            );
            
            Initialize(connection);
        }

        public void ClearCache()
        {
            _gs2.ClearCache();
        }

        public void ClearCache<TKind>(
            string parentKey,
            string key
        )
        {
            _gs2.ClearCache<TKind>(parentKey, key);
        }

        public Gs2Future<GameSession> LoginFuture(
            IAuthenticator authenticator,
            string userId,
            string password
        )
        {
            IEnumerator Impl(Gs2Future<GameSession> self)
            {
                var gameSession = new GameSession(
                    authenticator,
                    Connection,
                    userId,
                    password
                );
                var future = gameSession.RefreshFuture();
                yield return future;
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(gameSession);
            }
            return new Gs2InlineFuture<GameSession>(Impl);
        }

#if GS2_ENABLE_UNITASK
        public async UniTask<GameSession> LoginAsync(
            IAuthenticator authenticator,
            string userId,
            string password
        )
        {
            var gameSession = new GameSession(
                authenticator,
                Connection,
                userId,
                password
            );
            await gameSession.RefreshAsync();
            return gameSession;
        }
#endif

        public Gs2Future<bool> DispatchFuture(
            IGameSession gameSession
        )
        {
            return this._gs2.DispatchFuture(
                gameSession.AccessToken
            );
        }

#if GS2_ENABLE_UNITASK
        public async UniTask DispatchAsync(
            IGameSession gameSession
        )
        {
            await this._gs2.DispatchAsync(
                gameSession.AccessToken
            );
        }
#endif
        [Obsolete("The name has been changed to DispatchFuture.")]
        public IFuture<bool> Dispatch(
            IGameSession gameSession
        )
        {
            return DispatchFuture(gameSession);
        }
        
        public Gs2Future DisconnectFuture()
        {
            return _gs2.DisconnectFuture();
        }
        
#if GS2_ENABLE_UNITASK
        public async UniTask DisconnectAsync()
        {
            await _gs2.DisconnectAsync();
        }
#endif
    }
}