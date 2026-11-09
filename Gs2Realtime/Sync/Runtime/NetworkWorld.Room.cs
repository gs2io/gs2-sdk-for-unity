#if UNITY_2021_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Gs2.Realtime.Sync
{
    // Room-level state: master client, room events, custom properties, interest groups.
    public sealed partial class NetworkWorld
    {
        // ---- Master client ----
        /// <summary>
        /// Connection ID of the master client, like PhotonNetwork.MasterClient (0 when unknown
        /// or detached). The room names the earliest remaining member; the master may name another.
        /// </summary>
        public uint MasterClientId => Client?.MasterId ?? 0;
        /// <summary>True when this connection is the master client.</summary>
        public bool IsMasterClient => Client != null && Client.MasterId != 0 && Client.MasterId == Client.ConnectionId;
        /// <summary>Raised with the new master's connection ID.</summary>
        public event Action<uint> MasterClientChanged;
        /// <summary>Names another member as master; only the current master may call it.</summary>
        /// <returns>False when the request could not be sent.</returns>
        public bool SetMasterClient(uint connectionId)
        {
            RequireRoom();
            if (!IsMasterClient)
                throw new InvalidOperationException("Only the master client can name another master");
            return Client.SetMaster(connectionId);
        }

        // ---- Room events ----
        // Raising an event encodes the arguments by a registered handler's signature for
        // that code, so a code needs a registered handler on the sender too.
        sealed class EventHandler
        {
            public MonoBehaviour Target;
            public NetworkRpcMethod Method;
        }
        readonly List<EventHandler> eventHandlers = new List<EventHandler>();
        static readonly byte[] eventScratch = new byte[SyncCodec.MaxArgs];
        /// <summary>
        /// Every room event as received: (code, sender connection ID, a copy of the encoded
        /// arguments). Use it with RaiseRawEvent, or to observe all events.
        /// </summary>
        public event Action<uint, uint, byte[]> EventReceived;

        /// <summary>
        /// Registers a component's [NetworkEvent] methods as room event handlers, like
        /// Photon's OnEvent. Registering again replaces the earlier registration.
        /// </summary>
        public void AddEventTarget(MonoBehaviour target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            RemoveEventTarget(target);
            foreach (var method in NetworkRpcMethod.Of(target.GetType(), true))
                eventHandlers.Add(new EventHandler { Target = target, Method = method });
        }
        /// <summary>Unregisters a component added with AddEventTarget.</summary>
        public void RemoveEventTarget(MonoBehaviour target)
        {
            eventHandlers.RemoveAll(h => h.Target == target || h.Target == null);
        }
        uint EventCode(string name)
        {
            foreach (var handler in eventHandlers)
                if (handler.Target != null && handler.Method.Name == name)
                    return handler.Method.Id;
            return NetworkRpcAttribute.IdOf(name);
        }
        NetworkRpcMethod EventSignature(uint code)
        {
            foreach (var handler in eventHandlers)
                if (handler.Target != null && handler.Method.Id == code)
                    return handler.Method;
            throw new ArgumentException("No [NetworkEvent] handler for code " + code + " is registered; register one with AddEventTarget or use RaiseRawEvent");
        }
        /// <summary>
        /// Raises a room event to All, Others or their buffered forms, like
        /// PhotonNetwork.RaiseEvent. name is a registered [NetworkEvent] method's name.
        /// </summary>
        /// <returns>False when the packet could not be sent.</returns>
        public bool RaiseEvent(string name, RpcTarget target, params object[] args)
        {
            return RaiseEvent(EventCode(name), target, args);
        }
        /// <summary>Raises a room event by code; see the overload taking a name.</summary>
        public bool RaiseEvent(uint code, RpcTarget target, params object[] args)
        {
            byte value = (byte)target;
            bool roomEventTarget = RpcTargetByte.IsUnaddressed(value) && target != RpcTarget.Owner; // events have no owner
            if (!roomEventTarget)
                throw new ArgumentOutOfRangeException(nameof(target), "Room events go to All, Others or their buffered forms; use RaiseEventTo for one player");
            return SendEvent(code, value, 0, args);
        }
        /// <summary>Raises a room event to one connection ID; false when not sent.</summary>
        public bool RaiseEventTo(string name, uint connectionId, params object[] args)
        {
            return RaiseEventTo(EventCode(name), connectionId, args);
        }
        /// <summary>Raises a room event by code to one player (a connection ID).</summary>
        public bool RaiseEventTo(uint code, uint connectionId, params object[] args)
        {
            if (connectionId == 0)
                throw new ArgumentOutOfRangeException(nameof(connectionId));
            return SendEvent(code, RpcTargetByte.Player, connectionId, args);
        }
        /// <summary>
        /// Raises an event with a raw payload, for handlers outside [NetworkEvent] (received
        /// through EventReceived). False when not sent.
        /// </summary>
        public bool RaiseRawEvent(uint code, RpcTarget target, byte[] data)
        {
            RequireRoom();
            return Client.RaiseEvent(code, target, data, data?.Length ?? 0);
        }
        /// <summary>Raises a raw-payload event to one player (a connection ID).</summary>
        public bool RaiseRawEventTo(uint code, uint connectionId, byte[] data)
        {
            RequireRoom();
            return Client.RaiseEventTo(code, connectionId, data, data?.Length ?? 0);
        }
        bool SendEvent(uint code, byte target, uint recipient, object[] args)
        {
            RequireRoom();
            int count = NetworkRpcCodec.Encode(eventScratch, EventSignature(code), args);
            return Client.SendEvent(code, target, recipient, eventScratch, count);
        }
        void ISyncPresenter.EventReceived(uint code, uint sender, byte[] args, int offset, int count)
        {
            var copy = new byte[count];
            Buffer.BlockCopy(args, offset, copy, 0, count);
            if (EventReceived != null)
                deferred.Add(() => EventReceived?.Invoke(code, sender, copy));
            var info = new NetworkRpcInfo(null, code, sender, Client != null && sender == Client.ConnectionId);
            foreach (var handler in eventHandlers)
            {
                if (handler.Target == null || handler.Method.Id != code)
                    continue;
                object[] values;
                try
                {
                    values = NetworkRpcCodec.Decode(copy, 0, count, handler.Method, info);
                }
                catch (Exception e) when (e is FormatException || e is OverflowException)
                {
                    Debug.LogWarning("Dropped event " + code + " for " + handler.Target.name + ": " + e.Message, handler.Target);
                    continue;
                }
                var target = handler.Target;
                var method = handler.Method;
                deferred.Add(() => Invoke(target, method, values));
            }
        }
        static void Invoke(MonoBehaviour target, NetworkRpcMethod method, object[] values)
        {
            if (target == null)
                return;
            object result;
            try
            {
                result = method.Method.Invoke(target, values);
            }
            catch (System.Reflection.TargetInvocationException e)
            {
                Debug.LogException(e.InnerException ?? e, target);
                return;
            }
            if (method.IsCoroutine && result is System.Collections.IEnumerator routine && target.isActiveAndEnabled)
                target.StartCoroutine(routine);
        }

        // ---- Custom properties ----
        readonly Dictionary<string, object> roomProperties = new Dictionary<string, object>();
        readonly Dictionary<uint, Dictionary<string, object>> playerProperties = new Dictionary<uint, Dictionary<string, object>>();
        static readonly byte[] propertyScratch = new byte[SyncCodec.MaxArgs];
        /// <summary>The room's custom properties, like Photon's Room.CustomProperties.</summary>
        public IReadOnlyDictionary<string, object> RoomProperties => roomProperties;
        /// <summary>Raised with the key of a room property that changed or was deleted.</summary>
        public event Action<string> RoomPropertyChanged;
        /// <summary>Raised with (connection ID, key) when a player property changes.</summary>
        public event Action<uint, string> PlayerPropertyChanged;

        /// <summary>
        /// Writes a room property; null deletes it. Values: bool, integers, char, float, double,
        /// string, byte[], Vector2/3/4, Vector2Int/3Int, Quaternion, Color (store enums as
        /// integers). Keys are 1..64 UTF-8 bytes. The write applies when the room echoes it, so
        /// every member (including the writer) sees the same order. False when not sent.
        /// </summary>
        public bool SetRoomProperty(string key, object value)
        {
            return SetProperty(0, key, value);
        }
        /// <summary>
        /// Writes this player's property, with the rules of SetRoomProperty. Other players'
        /// properties are read-only.
        /// </summary>
        public bool SetPlayerProperty(string key, object value)
        {
            RequireRoom();
            return SetProperty(Client.ConnectionId, key, value);
        }
        bool SetProperty(uint scope, string key, object value)
        {
            RequireRoom();
            int length = string.IsNullOrEmpty(key) ? 0 : System.Text.Encoding.UTF8.GetByteCount(key);
            if (length == 0 || length > SyncClient.MaxPropertyKey)
                throw new ArgumentException("Property keys are 1.." + SyncClient.MaxPropertyKey + " UTF-8 bytes");
            int count = NetworkRpcCodec.EncodePropertyValue(propertyScratch, value);
            return Client.SetProperty(scope, key, new ReadOnlySpan<byte>(propertyScratch, 0, count));
        }
        /// <summary>Reads a room property; false when it is missing or not a T.</summary>
        public bool TryGetRoomProperty<T>(string key, out T value)
        {
            return TryGet(roomProperties, key, out value);
        }
        /// <summary>Reads a player's property; false when it is missing or not a T.</summary>
        public bool TryGetPlayerProperty<T>(uint connectionId, string key, out T value)
        {
            playerProperties.TryGetValue(connectionId, out var properties);
            return TryGet(properties, key, out value);
        }
        /// <summary>A player's custom properties (empty when it has none).</summary>
        public IReadOnlyDictionary<string, object> PlayerProperties(uint connectionId)
        {
            return playerProperties.TryGetValue(connectionId, out var properties) ? properties : (IReadOnlyDictionary<string, object>)new Dictionary<string, object>();
        }
        static bool TryGet<T>(Dictionary<string, object> properties, string key, out T value)
        {
            if (properties != null && properties.TryGetValue(key, out var stored) && stored is T typed)
            {
                value = typed;
                return true;
            }
            value = default;
            return false;
        }
        void ISyncPresenter.PropertyChanged(uint scope, string key, byte[] args, int offset, int count)
        {
            var value = NetworkRpcCodec.DecodePropertyValue(args, offset, count);
            Dictionary<string, object> properties;
            if (scope == 0)
                properties = roomProperties;
            else if (!playerProperties.TryGetValue(scope, out properties))
            {
                if (value == null)
                    return;
                properties = playerProperties[scope] = new Dictionary<string, object>();
            }
            if (value == null)
                properties.Remove(key);
            else
                properties[key] = value;
            if (scope == 0)
                deferred.Add(() => RoomPropertyChanged?.Invoke(key));
            else
            {
                if (properties.Count == 0)
                    playerProperties.Remove(scope);
                deferred.Add(() => PlayerPropertyChanged?.Invoke(scope, key));
            }
        }

        // ---- Refused commands ----
        /// <summary>
        /// The room refused one of this client's commands (which, and why); also logged as a
        /// warning while LogRejections is on. Spawn requests report theirs in SpawnRequest and
        /// SpawnFailed.
        /// </summary>
        public event Action<Rejection> CommandRejected;
        [SerializeField, Tooltip("Log a warning for every command the room refuses.")]
        bool logRejections = true;
        /// <summary>Logs a warning for every command the room refuses (default on).</summary>
        public bool LogRejections
        {
            get => logRejections;
            set => logRejections = value;
        }
        void OnRejected(Rejection refused)
        {
            if (logRejections)
                Debug.LogWarning("The room refused " + refused, this);
            deferred.Add(() => CommandRejected?.Invoke(refused));
        }

        // ---- Interest groups ----
        readonly HashSet<uint> interestGroups = new HashSet<uint>();
        /// <summary>The interest groups this client subscribes to (group 0 is implicit).</summary>
        public IReadOnlyCollection<uint> InterestGroups => interestGroups;
        /// <summary>Subscribes to one interest group; see SetInterestGroups.</summary>
        public bool SubscribeGroup(uint group)
        {
            return SetInterestGroups(null, new[] { group });
        }
        /// <summary>Unsubscribes from one interest group; see SetInterestGroups.</summary>
        public bool UnsubscribeGroup(uint group)
        {
            return SetInterestGroups(new[] { group }, null);
        }
        /// <summary>
        /// Changes interest group subscriptions, like PhotonNetwork.SetInterestGroups. Group 0
        /// is always received. Subscriptions persist across reconnects and are sent again to a
        /// new room; subscribing delivers the group's current values. Works while detached.
        /// </summary>
        /// <returns>
        /// False when the outbox refused a change; that change is not recorded, so call again.
        /// </returns>
        public bool SetInterestGroups(uint[] disable, uint[] enable)
        {
            bool ok = true;
            foreach (var group in disable ?? Array.Empty<uint>())
            {
                if (group == 0 || !interestGroups.Contains(group))
                    continue;
                if (Client == null || Client.Subscribe(group, false))
                    interestGroups.Remove(group);
                else
                    ok = false;
            }
            foreach (var group in enable ?? Array.Empty<uint>())
            {
                if (group == 0 || interestGroups.Contains(group))
                    continue;
                if (Client == null || Client.Subscribe(group, true))
                    interestGroups.Add(group);
                else
                    ok = false;
            }
            return ok;
        }

        void RequireRoom()
        {
            if (Client == null)
                throw new InvalidOperationException("Attach first");
        }
        // ---- Rules consistency ----
        // A room running other rules than the generated code is logged once per pair,
        // since fields, precision or predictions may no longer match.
        uint predictionRules;
        readonly HashSet<string> rulesWarnings = new HashSet<string>();
        /// <summary>
        /// Records the rules hash the registered prediction was generated from, so that a
        /// room running other rules logs a warning. Called by generated code.
        /// </summary>
        public void ExpectRules(uint rules)
        {
            predictionRules = rules;
        }
        void CheckRules(uint room)
        {
            if (room == 0)
                return;
            if (GeneratedFromOtherRules(predictionRules, room))
                WarnRules("The registered prediction", predictionRules, room);
            foreach (var prefab in catalog.Values)
            {
                var schema = prefab.GetComponent<NetworkEntity>().Schema;
                if (schema != null && GeneratedFromOtherRules(schema.Rules, room))
                    WarnRules("Schema " + schema.name + " (" + prefab.name + ")", schema.Rules, room);
            }
        }
        // generated is 0 when the code names no rules.
        static bool GeneratedFromOtherRules(uint generated, uint room) => generated != 0 && generated != room;
        void WarnRules(string what, uint generated, uint room)
        {
            if (rulesWarnings.Add(what + generated + ":" + room))
                Debug.LogWarning(what + " was generated from other rules (" + generated.ToString("x8") + ") than the room runs (" + room.ToString("x8") +
                    "): rebuild the room rules and copy their unity/ outputs into the project", this);
        }

        void OnSnapshotStarted()
        {
            CheckRules(Client.RulesHash);
            ResetClock();
            roomProperties.Clear();
            playerProperties.Clear();
        }
        void OnMasterChanged(uint master)
        {
            deferred.Add(() => MasterClientChanged?.Invoke(master));
        }
        void AttachRoom()
        {
            Client.MasterChanged += OnMasterChanged;
            Client.SnapshotStarted += OnSnapshotStarted;
            Client.Rejected += OnRejected;
            // Sent once the snapshot completes (the outbox holds them until then).
            foreach (var group in interestGroups)
                Client.Subscribe(group, true);
        }
        void DetachRoom()
        {
            if (Client != null)
            {
                Client.MasterChanged -= OnMasterChanged;
                Client.SnapshotStarted -= OnSnapshotStarted;
                Client.Rejected -= OnRejected;
            }
            roomProperties.Clear();
            playerProperties.Clear();
        }
    }
}
#endif
