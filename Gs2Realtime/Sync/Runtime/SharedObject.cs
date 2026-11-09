#if UNITY_2021_3_OR_NEWER
using System;
using UnityEngine;

namespace Gs2.Realtime.Sync
{
    /// <summary>
    /// Attach to a room-owned (leased or server) prefab, next to its NetworkEntity. The
    /// server catalog decides the kind; a client cannot grant authority or create room-owned
    /// objects. Interactions and lease requests return a CommandRequest.
    /// </summary>
    public sealed class SharedObject : MonoBehaviour, INetworkBinding
    {
        SyncClient client;
        NetworkWorld world;
        /// <summary>The bound room object; null while unbound.</summary>
        public NetworkObject Object
        {
            get; private set;
        }
        /// <summary>True while this client may write the object's fields.</summary>
        public bool CanWrite
        {
            get
            {
                return client != null && Object != null && client.CanWrite(Object.Id);
            }
        }
        /// <summary>
        /// Called by the NetworkWorld that creates the entity; throws for an owned (player or
        /// simulated) object.
        /// </summary>
        public void Bind(SyncClient session, NetworkObject value)
        {
            if (value.Kind.Owned)
                throw new ArgumentException("Expected shared object");
            var entity = GetComponentInParent<NetworkEntity>(true);
            world = entity != null ? entity.World : null;
            if (world == null)
                throw new InvalidOperationException("A SharedObject is bound by the NetworkWorld that creates its entity");
            client = session;
            Object = value;
        }
        /// <summary>
        /// Detaches from the room object. Requests already sent still complete: the world
        /// keeps them, since the room may answer after the object is gone.
        /// </summary>
        public void Unbind()
        {
            client = null;
            world = null;
            Object = null;
        }
        /// <summary>
        /// Requests the lease manually. It does not enable the entity's controlled behaviours
        /// and is not renewed; to take a leased object and wait until it is controlled, use
        /// NetworkEntity.AcquireControl. Throws while unbound.
        /// </summary>
        public CommandRequest Acquire()
        {
            return Command(completed => Require().Lease(Object.Id, LeaseAction.Acquire, completed));
        }
        /// <summary>Renews the held lease manually; see Acquire.</summary>
        public CommandRequest Renew()
        {
            return Command(completed => Require().Lease(Object.Id, LeaseAction.Renew, completed));
        }
        /// <summary>Gives the lease back manually; see Acquire.</summary>
        public CommandRequest Release()
        {
            return Command(completed => Require().Lease(Object.Id, LeaseAction.Release, completed));
        }
        /// <summary>
        /// Runs the rules' Interact(state, objectId, method) in the room. Throws while unbound.
        /// </summary>
        public CommandRequest Interact(uint method)
        {
            return Command(completed => Require().Interact(Object.Id, method, completed));
        }
        /// <summary>
        /// Interact with argument bytes (copied). Rule-based rooms take no arguments;
        /// host-defined actions may.
        /// </summary>
        public CommandRequest Interact(uint method, ReadOnlySpan<byte> args)
        {
            var client = Require();
            var id = Object.Id;
            var copy = args.ToArray();
            return Command(completed => client.Interact(id, method, copy, completed));
        }
        CommandRequest Command(Func<CommandCallback, uint> send)
        {
            Require();
            return world.Command(send);
        }
        SyncClient Require()
        {
            if (client == null || Object == null)
                throw new InvalidOperationException("The shared object is not bound to a room object (not spawned yet, or despawned)");
            return client;
        }
    }

    /// <summary>
    /// The outcome of one interaction or lease request on a shared object. It completes even
    /// when the request removes its object, and with Abandoned when no answer will come (the
    /// world detached, or the room was rejoined). Wait for it in a coroutine (yield return
    /// request) or with OnCompleted; callbacks run outside network callbacks, so they may send.
    /// </summary>
    public sealed class CommandRequest : NetworkRequest<CommandRequest, RequestStatus>
    {
        internal CommandRequest(uint id) : base(id)
        {
        }
        /// <summary>
        /// The room's result (when Succeeded or Refused); codes from 100 are the rules' own.
        /// </summary>
        public CommandResult Result { get; private set; }
        internal void Complete(RequestStatus status, CommandResult result)
        {
            Finish(status, () => Result = result);
        }
    }
}
#endif
