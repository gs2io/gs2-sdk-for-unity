#if UNITY_2021_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Gs2.Realtime.Sync
{
    // Room-level state: master client, room events and custom properties.
    public sealed partial class NetworkWorld
    {
        // ---- Master client (like PhotonNetwork.MasterClient) ----
        // The room names the earliest remaining member; the master may name another.
        public uint MasterClientId => Client?.MasterId ?? 0;
        public bool IsMasterClient => Client != null && Client.MasterId != 0 && Client.MasterId == Client.ConnectionId;
        public event Action<uint> MasterClientChanged;
        public bool SetMasterClient(uint connectionId)
        {
            RequireRoom();
            if (!IsMasterClient)
                throw new InvalidOperationException("Only the master client can name another master");
            return SendRoom(() => Client.SetMaster(connectionId));
        }

        // ---- Room events (like PhotonNetwork.RaiseEvent / OnEvent) ----
        // Handlers are [NetworkEvent] methods of components registered here. Raising an
        // event encodes the arguments by a registered handler's signature for that code.
        sealed class EventHandler
        {
            public MonoBehaviour Target;
            public NetworkRpcMethod Method;
        }
        readonly List<EventHandler> eventHandlers = new List<EventHandler>();
        readonly Queue<Func<bool>> roomCommands = new Queue<Func<bool>>();
        static readonly byte[] eventScratch = new byte[SyncCodec.MaxArgs];
        // Every event as received: (code, sender, a copy of the encoded arguments).
        public event Action<uint, uint, byte[]> EventReceived;

        public void AddEventTarget(MonoBehaviour target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            RemoveEventTarget(target);
            foreach (var method in NetworkRpcMethod.Of(target.GetType(), true))
                eventHandlers.Add(new EventHandler { Target = target, Method = method });
        }
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
        public bool RaiseEvent(string name, RpcTarget target, params object[] args)
        {
            return RaiseEvent(EventCode(name), target, args);
        }
        public bool RaiseEvent(uint code, RpcTarget target, params object[] args)
        {
            byte value = (byte)target;
            if (!RpcTargetByte.IsValid(value) || RpcTargetByte.HasRecipient(value) || target == RpcTarget.Owner)
                throw new ArgumentOutOfRangeException(nameof(target), "Room events go to All, Others or their buffered forms; use RaiseEventTo for one player");
            return SendEvent(code, value, 0, args);
        }
        public bool RaiseEventTo(string name, uint connectionId, params object[] args)
        {
            return RaiseEventTo(EventCode(name), connectionId, args);
        }
        public bool RaiseEventTo(uint code, uint connectionId, params object[] args)
        {
            if (connectionId == 0)
                throw new ArgumentOutOfRangeException(nameof(connectionId));
            return SendEvent(code, RpcTargetByte.Player, connectionId, args);
        }
        // Raw payload for handlers outside [NetworkEvent] (received through EventReceived).
        public bool RaiseRawEvent(uint code, RpcTarget target, byte[] data, uint recipient = 0)
        {
            RequireRoom();
            byte value = recipient != 0 ? RpcTargetByte.Player : (byte)target;
            var copy = data == null ? Array.Empty<byte>() : (byte[])data.Clone();
            return SendRoom(() => Client.RaiseEvent(code, value, copy, copy.Length, recipient));
        }
        bool SendEvent(uint code, byte target, uint recipient, object[] args)
        {
            RequireRoom();
            int count = NetworkRpcCodec.Encode(eventScratch, EventSignature(code).Parameters, args);
            var copy = new byte[count];
            Buffer.BlockCopy(eventScratch, 0, copy, 0, count);
            return SendRoom(() => Client.RaiseEvent(code, target, copy, count, recipient));
        }
        void OnEvent(uint code, uint sender, byte[] args, int offset, int count)
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

        // ---- Custom properties (like Room/Player CustomProperties) ----
        // Values: bool, integers, char, float, double, string, byte[], Vector2/3/4,
        // Vector2Int/3Int, Quaternion, Color. null deletes. Writes apply when the room
        // echoes them, so every member (including the writer) sees the same order.
        readonly Dictionary<string, object> roomProperties = new Dictionary<string, object>();
        readonly Dictionary<uint, Dictionary<string, object>> playerProperties = new Dictionary<uint, Dictionary<string, object>>();
        static readonly byte[] propertyScratch = new byte[SyncCodec.MaxArgs];
        public IReadOnlyDictionary<string, object> RoomProperties => roomProperties;
        public event Action<string> RoomPropertyChanged;
        // (player connection ID, key)
        public event Action<uint, string> PlayerPropertyChanged;

        public bool SetRoomProperty(string key, object value)
        {
            return SetProperty(0, key, value);
        }
        // Writes this player's property; other players' properties are read-only.
        public bool SetPlayerProperty(string key, object value)
        {
            RequireRoom();
            return SetProperty(Client.ConnectionId, key, value);
        }
        bool SetProperty(uint scope, string key, object value)
        {
            RequireRoom();
            int count = NetworkRpcCodec.EncodeProperty(propertyScratch, key, value);
            var copy = new byte[count];
            Buffer.BlockCopy(propertyScratch, 0, copy, 0, count);
            return SendRoom(() => Client.SetProperty(scope, copy, count));
        }
        public bool TryGetRoomProperty<T>(string key, out T value)
        {
            return TryGet(roomProperties, key, out value);
        }
        public bool TryGetPlayerProperty<T>(uint connectionId, string key, out T value)
        {
            playerProperties.TryGetValue(connectionId, out var properties);
            return TryGet(properties, key, out value);
        }
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
        void OnProperty(uint scope, uint writer, byte[] args, int offset, int count)
        {
            NetworkRpcCodec.DecodeProperty(args, offset, count, out var key, out var value);
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

        // ---- Interest groups (like PhotonNetwork.SetInterestGroups) ----
        // Group 0 is always received. Subscriptions persist across reconnects and are
        // sent again to a new room; subscribing delivers the group's current values.
        readonly HashSet<uint> interestGroups = new HashSet<uint>();
        public IReadOnlyCollection<uint> InterestGroups => interestGroups;
        public bool SubscribeGroup(uint group)
        {
            return SetInterestGroups(null, new[] { group });
        }
        public bool UnsubscribeGroup(uint group)
        {
            return SetInterestGroups(new[] { group }, null);
        }
        public bool SetInterestGroups(uint[] disable, uint[] enable)
        {
            bool ok = true;
            foreach (var group in disable ?? Array.Empty<uint>())
                if (group != 0 && interestGroups.Remove(group) && Client != null)
                    ok &= SendRoom(() => Client.Subscribe(group, false));
            foreach (var group in enable ?? Array.Empty<uint>())
                if (group != 0 && interestGroups.Add(group) && Client != null)
                    ok &= SendRoom(() => Client.Subscribe(group, true));
            return ok;
        }

        // ---- Plumbing ----
        void RequireRoom()
        {
            if (Client == null)
                throw new InvalidOperationException("Attach first");
        }
        // Sends now when possible; inside network callbacks or before the snapshot
        // completes, the command waits for the next Update (in call order).
        bool SendRoom(Func<bool> command)
        {
            if (Client.CanSend && roomCommands.Count == 0)
                return command();
            if (roomCommands.Count >= 256)
                return false;
            roomCommands.Enqueue(command);
            return true;
        }
        void ServiceRoom()
        {
            while (roomCommands.Count > 0 && Client != null && Client.CanSend)
            {
                if (!roomCommands.Peek()())
                    return;
                roomCommands.Dequeue();
            }
        }
        void OnSnapshotStarted()
        {
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
            Client.EventReceived += OnEvent;
            Client.PropertyChanged += OnProperty;
            Client.MasterChanged += OnMasterChanged;
            Client.SnapshotStarted += OnSnapshotStarted;
            foreach (var group in interestGroups)
            {
                var g = group;
                roomCommands.Enqueue(() => Client.Subscribe(g, true));
            }
        }
        void DetachRoom()
        {
            if (Client != null)
            {
                Client.EventReceived -= OnEvent;
                Client.PropertyChanged -= OnProperty;
                Client.MasterChanged -= OnMasterChanged;
                Client.SnapshotStarted -= OnSnapshotStarted;
            }
            roomCommands.Clear();
            roomProperties.Clear();
            playerProperties.Clear();
        }
    }
}
#endif
