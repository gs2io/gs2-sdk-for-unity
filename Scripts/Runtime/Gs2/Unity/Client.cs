using Gs2.Unity.Util;

namespace Gs2.Unity
{
    public class Client
    {
        public readonly Gs2Account.Client Account;
        public readonly Gs2Auth.Client Auth;
        public readonly Gs2Chat.Client Chat;
        public readonly Gs2Datastore.Client Datastore;
        public readonly Gs2Distributor.Client Distributor;
        public readonly Gs2Exchange.Client Exchange;
        public readonly Gs2Experience.Client Experience;
        public readonly Gs2Formation.Client Formation;
        public readonly Gs2Friend.Client Friend;
        public readonly Gs2Gateway.Client Gateway;
        public readonly Gs2Inbox.Client Inbox;
        public readonly Gs2Inventory.Client Inventory;
        public readonly Gs2JobQueue.Client JobQueue;
        public readonly Gs2Limit.Client Limit;
        public readonly Gs2Lock.Client Lock;
        public readonly Gs2Lottery.Client Lottery;
        public readonly Gs2Matchmaking.Client Matchmaking;
        public readonly Gs2Mission.Client Mission;
        public readonly Gs2Money.Client Money;
        public readonly Gs2Quest.Client Quest;
        public readonly Gs2Ranking.Client Ranking;
        public readonly Gs2Realtime.Client Realtime;
        public readonly Gs2Schedule.Client Schedule;
        public readonly Gs2Showcase.Client Showcase;
        public readonly Gs2Stamina.Client Stamina;
        public readonly Gs2Version.Client Version;

        public Client(
            Profile profile
        )
        {
            Account = new Gs2Account.Client(profile);
            Auth = new Gs2Auth.Client(profile);
            Chat = new Gs2Chat.Client(profile);
            Datastore = new Gs2Datastore.Client(profile);
            Distributor = new Gs2Distributor.Client(profile);
            Exchange = new Gs2Exchange.Client(profile);
            Experience = new Gs2Experience.Client(profile);
            Formation = new Gs2Formation.Client(profile);
            Friend = new Gs2Friend.Client(profile);
            Gateway = new Gs2Gateway.Client(profile);
            Inbox = new Gs2Inbox.Client(profile);
            Inventory = new Gs2Inventory.Client(profile);
            JobQueue = new Gs2JobQueue.Client(profile);
            Limit = new Gs2Limit.Client(profile);
            Lock = new Gs2Lock.Client(profile);
            Lottery = new Gs2Lottery.Client(profile);
            Matchmaking = new Gs2Matchmaking.Client(profile);
            Mission = new Gs2Mission.Client(profile);
            Money = new Gs2Money.Client(profile);
            Quest = new Gs2Quest.Client(profile);
            Ranking = new Gs2Ranking.Client(profile);
            Realtime = new Gs2Realtime.Client(profile);
            Schedule = new Gs2Schedule.Client(profile);
            Showcase = new Gs2Showcase.Client(profile);
            Stamina = new Gs2Stamina.Client(profile);
            Version = new Gs2Version.Client(profile);
        }
    }
}