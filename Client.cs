using Gs2.Unity.Util;

namespace Gs2.Unity
{
    public class Client
    {
        public readonly Gs2Account.Client Account;
        public readonly Gs2AdReward.Client AdReward;
        public readonly Gs2Auth.Client Auth;
        public readonly Gs2Chat.Client Chat;
        public readonly Gs2Datastore.Client Datastore;
        public readonly Gs2Dictionary.Client Dictionary;
        public readonly Gs2Distributor.Client Distributor;
        public readonly Gs2Enchant.Client Enchant;
        public readonly Gs2Enhance.Client Enhance;
        public readonly Gs2Exchange.Client Exchange;
        public readonly Gs2Experience.Client Experience;
        public readonly Gs2Formation.Client Formation;
        public readonly Gs2Friend.Client Friend;
        public readonly Gs2Gateway.Client Gateway;
        public readonly Gs2Grade.Client Grade;
        public readonly Gs2Guild.Client Guild;
        public readonly Gs2Idle.Client Idle;
        public readonly Gs2Inbox.Client Inbox;
        public readonly Gs2Inventory.Client Inventory;
        public readonly Gs2JobQueue.Client JobQueue;
        public readonly Gs2Limit.Client Limit;
        public readonly Gs2LoginReward.Client LoginReward;
        public readonly Gs2Lottery.Client Lottery;
        public readonly Gs2Matchmaking.Client Matchmaking;
        public readonly Gs2MegaField.Client MegaField;
        public readonly Gs2Mission.Client Mission;
        public readonly Gs2Money.Client Money;
        public readonly Gs2Money2.Client Money2;
        public readonly Gs2News.Client News;
        public readonly Gs2Quest.Client Quest;
        public readonly Gs2Ranking.Client Ranking;
        public readonly Gs2Ranking2.Client Ranking2;
        public readonly Gs2Realtime.Client Realtime;
        public readonly Gs2Schedule.Client Schedule;
        public readonly Gs2SeasonRating.Client SeasonRating;
        public readonly Gs2SerialKey.Client SerialKey;
        public readonly Gs2Showcase.Client Showcase;
        public readonly Gs2SkillTree.Client SkillTree;
        public readonly Gs2Stamina.Client Stamina;
        public readonly Gs2StateMachine.Client StateMachine;
        public readonly Gs2Version.Client Version;
        public readonly Gs2Connection Connection;

        public Client(
            Gs2Connection connection
        )
        {
            Connection = connection;
            
            Account = new Gs2Account.Client(connection);
            AdReward = new Gs2AdReward.Client(connection);
            Auth = new Gs2Auth.Client(connection);
            Chat = new Gs2Chat.Client(connection);
            Datastore = new Gs2Datastore.Client(connection);
            Dictionary = new Gs2Dictionary.Client(connection);
            Distributor = new Gs2Distributor.Client(connection);
            Enchant = new Gs2Enchant.Client(connection);
            Enhance = new Gs2Enhance.Client(connection);
            Exchange = new Gs2Exchange.Client(connection);
            Experience = new Gs2Experience.Client(connection);
            Formation = new Gs2Formation.Client(connection);
            Friend = new Gs2Friend.Client(connection);
            Gateway = new Gs2Gateway.Client(connection);
            Grade = new Gs2Grade.Client(connection);
            Guild = new Gs2Guild.Client(connection);
            Idle = new Gs2Idle.Client(connection);
            Inbox = new Gs2Inbox.Client(connection);
            Inventory = new Gs2Inventory.Client(connection);
            JobQueue = new Gs2JobQueue.Client(connection);
            Limit = new Gs2Limit.Client(connection);
            LoginReward = new Gs2LoginReward.Client(connection);
            Lottery = new Gs2Lottery.Client(connection);
            Matchmaking = new Gs2Matchmaking.Client(connection);
            MegaField = new Gs2MegaField.Client(connection);
            Mission = new Gs2Mission.Client(connection);
            Money = new Gs2Money.Client(connection);
            Money2 = new Gs2Money2.Client(connection);
            News = new Gs2News.Client(connection);
            Quest = new Gs2Quest.Client(connection);
            Ranking = new Gs2Ranking.Client(connection);
            Ranking2 = new Gs2Ranking2.Client(connection);
            Realtime = new Gs2Realtime.Client(connection);
            Schedule = new Gs2Schedule.Client(connection);
            SeasonRating = new Gs2SeasonRating.Client(connection);
            SerialKey = new Gs2SerialKey.Client(connection);
            Showcase = new Gs2Showcase.Client(connection);
            SkillTree = new Gs2SkillTree.Client(connection);
            Stamina = new Gs2Stamina.Client(connection);
            StateMachine = new Gs2StateMachine.Client(connection);
            Version = new Gs2Version.Client(connection);
        }
    }
}