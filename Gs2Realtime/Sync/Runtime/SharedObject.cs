#if UNITY_2021_3_OR_NEWER
using UnityEngine;

namespace Gs2.Realtime.Sync
{
    // Attach to a room-owned prefab. The server catalog selects Lease or Server;
    // a client component cannot grant authority or create room-owned objects.
    public sealed class SharedObject : MonoBehaviour, INetworkBinding
    {
        SyncClient client;
        public NetworkObject Object
        {
            get; private set;
        }
        public bool CanWrite
        {
            get
            {
                return client != null && Object != null && client.CanWrite(Object.Id);
            }
        }
        public void Bind(SyncClient session, NetworkObject value)
        {
            if (value.Mode == ObjectMode.Player || value.Mode == ObjectMode.Simulated)
                throw new System.ArgumentException("Expected shared object");
            client = session;
            Object = value;
        }
        public void Unbind()
        {
            client = null;
            Object = null;
        }
        public uint Acquire()
        {
            return client.Lease(Object.Id, LeaseAction.Acquire);
        }
        public uint Renew()
        {
            return client.Lease(Object.Id, LeaseAction.Renew);
        }
        public uint Release()
        {
            return client.Lease(Object.Id, LeaseAction.Release);
        }
        public uint Interact(uint method, byte[] args = null, int count = 0)
        {
            return client.Interact(Object.Id, method, args, count);
        }
    }
}
#endif
