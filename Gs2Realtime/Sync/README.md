# Unity realtime synchronization

## First setup, end to end

1. **A network prefab.** Create a prefab with **NetworkEntity** (and **NetworkTransform** to
   synchronize its position/rotation) on its root. Custom fields need a schema generated with
   the room rules (below).
2. **A prefab catalog.** *Create > GS2 > Realtime > Prefab Catalog*, add an entry, give it the
   prefab ID the room uses for that prefab and drag the prefab in. The Inspector warns about
   repeated IDs, missing prefabs or NetworkEntity components, and schemas generated for
   another prefab ID.
3. **A world.** Add **NetworkWorld** and **NetworkSession** to one GameObject in the scene and
   assign the catalog to the world's **Prefab Catalog**.
4. **Connect and spawn.** With the room's address from GS2-Realtime (access token, IP address,
   port, encryption key):

```csharp
public sealed class Game : MonoBehaviour
{
    [SerializeField] NetworkWorld world;
    [SerializeField] NetworkSession session;
    const uint PlayerPrefab = 1; // the catalog entry's ID

    // Subscribe once per component, not per connection: each reconnect reuses them.
    void OnEnable()
    {
        session.Ready += OnReady;
        world.Spawned += OnSpawned;
        session.Disconnected += OnDisconnected;
    }
    void OnDisable()
    {
        session.Ready -= OnReady;
        world.Spawned -= OnSpawned;
        session.Disconnected -= OnDisconnected;
    }
    // Only starts a connection; call it again to reconnect.
    public void Join(string accessToken, string ipAddress, int port, string encryptionKey)
    {
        session.Connect(accessToken, ipAddress, port, encryptionKey);
    }
    void OnReady()
    {
        // Usable at once (moves, RPCs); its fields exist once the room confirms it.
        world.Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
    }
    void OnSpawned(NetworkEntity entity)
    {
        if (entity.IsMine && entity.IsPrefab(PlayerPrefab))
            Debug.Log("My player is in the room: " + entity.name);
        // With a schema (Custom fields, below), set the player's fields here, once the room
        // confirms the object: entity.SetInt("Hp", 100) for a schema with an int field Hp.
        // This prefab synchronizes only its position/rotation and has no other fields.
    }
    void OnDisconnected(string reason) => Debug.Log("Disconnected: " + reason);
}
```

## Connecting

NetworkSession opens the RealtimeSession, attaches the world, sends its packets in order,
delivers received messages with the server clock, uses UDP for state updates where available
(not WebGL) and detaches the world when the session ends. `State` is Disconnected, Connecting,
Synchronizing (the snapshot is being applied) or Ready. Subscribe to `world.Client` events in
`Attached`. The sections below describe the parts it wires, for custom transports.

`NetworkWorld` receives object snapshots/deltas and creates registered prefabs.
`NetworkEntity` owns identity, control/lease handling and named property bindings.
`NetworkTransform` is optional and handles quantization/interpolation of the selected axes.

For position/rotation only, add **NetworkEntity** and **NetworkTransform**, then select the
axes. Leave Schema empty. The SDK uses slots 0..2 with .01 units/step for position, and slots
3..5 with 360/4096 degrees/step for rotation. The room must permit these fields (e.g.
`Room.DefineLeasedPrefab(5, RoomSync.Position, 6000)`); Unity cannot grant itself permission.

For custom properties or precision:

1. Declare `[RoomSchema(7, StandardFields = RoomSync.Position)]` in the C# room sources.
   List only custom fields such as BlueScore/HP; the compiler adds standard fields automatically.
   `RoomSync.Transform` includes position and rotation. Optional `PositionPrecision` and
   `RotationPrecision` override the defaults once in the room definition.
2. Generate with LocalStateMachineKit (Unity Editor or C# CLI, RealtimeRoomV1), import the
   generated `unity/*.realtimeschema` files and assign the corresponding asset to Entity.Schema.
3. Add a **Property binding**, choose a field name, and choose the target's dynamic property
   setter/method under **Apply to**. Its int/bool/float argument follows the C# declaration.
4. **NetworkTransform** reads the standard fields and any overridden precision from the schema.
   Unselected axes stay local. Field numbers, scale and offset do not need Inspector settings.
5. Put gameplay input behaviours in **Controlled Behaviours**. They are enabled while this
   client drives the object (`HasControl`): it owns a player or simulated object, or holds a
   leased object's lease (Entity's RequestControl/ReleaseControl, or AcquireControl to wait
   until control is gained; SharedObject.Acquire is the manual lease, which neither enables
   these behaviours nor renews). On a simulated object the
   input behaviours send inputs (`NetworkWorld.SetInput`); the rules write its fields.

Property bindings apply server values to Unity. Ordinary target-property edits are not
implicitly sent. Scripts can use Entity.GetInt/GetBool/GetFloat and SetInt/SetBool/SetFloat
by name; writes retain SyncClient's authority checks. The writer's Get* returns its own latest
Set* of the field (sent or not), so `SetInt("Hp", GetInt("Hp") + 1)` twice adds 2; other
clients, and fields it has not written, read the room's state. GetConfirmedInt/Bool/Float
always read the room's latest state. NetworkTransform sends owned motion
and interpolates remote motion. Server-owned game state changes through room interactions.

Every command (RPCs, spawns, events, properties, groups, leases, interactions) leaves through
the client's outbox, in call order across all entities and the world. Commands made before
the snapshot completes, inside callbacks (Spawned, RpcReceived, ...) or while the transport
cannot take more wait there and leave as soon as they may (`CanSend` says whether the outbox
has room). The outbox belongs to the room it was filled for: `Reset` and a new snapshot drop
what is left. `Interact(id, method, args)` sends the array whole (a `ReadOnlySpan<byte>` or a
`count` from 0 to its length sends part of it). `SpawnConfirmed(request, object)` reports this
client's spawns; another member's objects carry no request ID (every member counts from 1).
Results from 100 are the rules' own codes (`CommandResults.IsRulesCode`); compare them with
the error enum generated with the rules.

Generated schemas and predictions name the rules they come from; when the room runs other
rules, NetworkWorld logs a warning (copy the regenerated files). Refused commands raise
`CommandRejected` with the command, its target and the reason, and are logged (LogRejections).

Bindings resolve schema entries once when binding and dispatch only changed slots. UnityEvent
persistent callbacks support player stripping/IL2CPP without per-frame reflection. Snapshots,
reconnect and inactive prefabs receive initial values. Nested entities retain their own bindings.
The compact numeric int32/delta wire protocol is unchanged. Matching schema/GSL versions must
be distributed together; the wire validates field masks, not presentation names or precision.

## Spawning and ownership

`NetworkWorld.Instantiate(prefabId, position, rotation)` works like `PhotonNetwork.Instantiate`:
it returns the instance at once, `IsMine`, so it can move and call RPCs immediately (calls wait
until the room confirms the object; `IsSpawned` turns true then). Movement made before the
confirmation is kept. `entity.SpawnRequest` gives the room's answer (below). If the room
refuses the request (e.g. capacity), `SpawnFailed` is raised and the instance is destroyed.

`NetworkWorld.Spawn(prefabId, position, rotation)` instead waits for the room and creates the
instance on confirmation. Position and rotation fill the fields that the prefab's
NetworkTransform synchronizes, using its schema's precision. It returns a `SpawnRequest` that
completes once: `Spawned` (with `Entity`), `Rejected` (with the room's `Rejection`, e.g.
capacity), `NotSent` (the outbox was full) or `Abandoned` (no answer will come: the world
detached, or the room was rejoined after a reconnection, before the room answered).
Wait for it with `yield return request` or `request.OnCompleted(r => ...)`; callbacks run
outside network callbacks, so they may send. `Spawn(prefabId, values)` sends raw wire values
(`new SlotValues().Set(slot, value)`).

An `Instantiate`d entity may move and send RPCs at once, but its fields (`SetInt`, `GetFloat`,
...) exist only once the room confirms the object (`IsSpawned`): set them in
`NetworkWorld.Spawned` or `SpawnRequest.OnCompleted`. Before that the field accessors say so.

`NetworkEntity.IsMine` is true for the owner of a player object and the current controller of a
leased object. `OwnerId` returns that connection (0 for server objects or a free lease).
`NetworkEntity.Destroy()` despawns a player object owned by this connection. Player objects are
removed by the server when their owner leaves.

## Remote procedure calls

Mark methods of components under a NetworkEntity with `[NetworkRpc]`:

```csharp
public sealed class Weapon : MonoBehaviour
{
    // Public, so that other classes can name it with nameof(Weapon.Fire).
    [NetworkRpc]
    public void Fire(Vector3 direction, int damage, NetworkRpcInfo info)
    {
        // info.Sender is the caller's connection ID, set by the server.
    }
}

entity.Rpc(nameof(Weapon.Fire), RpcTarget.Others, direction, 10);
```

- `[NetworkRpc]` derives the ID from the method name (`NetworkRpcAttribute.IdOf`, FNV-1a), so
  renaming the method changes it, as in PUN. `[NetworkRpc(id)]` fixes the ID instead.
- Set an object catalog (namespace `syncSetting.objectCatalog`) when the room should check
  spawns, fields, RPCs and ownership requests. Without one the room checks none of these: any
  prefab spawns as a player object with the fields declared at spawn, and any member may call
  any RPC and take ownership.
- The room's object catalog lists the player prefabs, their fields and RPC IDs. Generate it
  with the prefab catalog asset's context menu **Copy Object Catalog JSON** and paste it into
  the namespace's `syncSetting.objectCatalog`. Prefabs the room rules define are skipped:
  SharedObject prefabs, and prefabs whose generated schema says the rules define them
  (simulated prefabs). The room refuses a catalog that lists a prefab the rules define.
- List each ID in the prefab's object catalog entry. `methods` may be called only by the
  object's owner; `publicMethods` by any room member on any player's object (mark those
  `[NetworkRpc(id, AnyCaller = true)]`), e.g.
  `"1":{"fields":63,"mode":"player","methods":[1],"publicMethods":[2]}`. The server checks
  the caller and rejects other IDs. The NetworkEntity inspector lists the IDs on the prefab.
- RPCs target player objects. Shared (leased/server) objects change through
  `SharedObject.Interact` and room rules instead.
- `SharedObject.Interact`, `Acquire`, `Renew` and `Release` return a `CommandRequest` that
  completes once: Succeeded, Refused (`Result`: the room's reason or the rules' code from
  100), NotSent or Abandoned (no answer will come: the world detached, or the room was
  rejoined after a reconnection). It outlives its object: an interaction
  that removes the object (picking up an item) completes with the room's answer, which
  arrives after the despawn. Wait for it with `yield return request` or
  `request.OnCompleted(r => ...)`.
- `RpcTarget.All` delivers to everyone including the caller, `Others` to everyone else and
  `Owner` to the object's owner. `entity.RpcTo(method, connectionId, args)` reaches one member.
- `AllBuffered` / `OthersBuffered` are also replayed, in order, to players who join later
  (inside their snapshot, after the objects exist). The room forgets them when the object
  is removed or the caller leaves, and keeps at most 1024 per room.
- Parameters: bool, integer types, char, float, double, string, byte[], enums, Vector2/3/4,
  Vector2Int/3Int, Quaternion and Color. Arguments are encoded by the method's declared types
  (numeric arguments convert as in C#) and must fit in 4096 bytes. An optional last
  `NetworkRpcInfo` parameter receives the entity, method ID and sender. Methods may return
  `IEnumerator`; the coroutine starts on the component.
- Received calls run after the packet is applied, outside `SyncClient.Receive`, so they may
  send RPCs or set properties. Calls made inside network callbacks (e.g. `NetworkWorld.Spawned`)
  leave right after the packet, in call order with every other command. Malformed or unknown calls are logged
  and dropped; an exception in one method does not stop later calls.
- Method IDs are the wire contract. `[NetworkRpc]` derives the ID from the method name, so
  renaming the method changes the ID (clients and catalogs of the old name no longer match);
  fix it with `[NetworkRpc(id)]` to keep it across renames. Each ID may appear once per
  entity; a method name shared by several components is called by ID.
- `[NetworkRpc]` derives from `[Preserve]` so that managed code stripping keeps the methods.

## Ownership

`entity.TransferOwnership(connectionId)` hands a player object to another member (like PUN's
TransferOwnership); `entity.RequestOwnership()` takes it over when the catalog entry has
`"ownership":"takeover"`. `IsMine`, controlled behaviours and `OwnershipChanged` follow once the
room confirms. With `"keepOnLeave": true` an object passes to the master client when its owner
leaves instead of being removed.

## Master client

`world.MasterClientId` / `IsMasterClient` / `MasterClientChanged` follow the room: the earliest
remaining member is the master, and the master may name another with `SetMasterClient(id)`.

## Interest groups

Like PhotonView.Group and `PhotonNetwork.SetInterestGroups`: the owner moves an object to a group
with `entity.SetGroup(group)`; its updates and All/Others calls then reach only members that
`world.SubscribeGroup(group)` (or `SetInterestGroups(disable, enable)`). Group 0 reaches everyone.
Spawns, despawns and ownership/group changes still reach everyone, and subscribing delivers the
group's current values at once. Subscriptions are kept across reconnects. A change returns false
when the outbox is full; it is then not taken (`InterestGroups` stays as it was), so call again.

## Room events

Like `PhotonNetwork.RaiseEvent`: mark handlers `[NetworkEvent]` (code from the method name) or
`[NetworkEvent(code)]`, register the component with `world.AddEventTarget(component)`, and call
`world.RaiseEvent(name, RpcTarget.Others, args)` or `RaiseEventTo(name, connectionId, args)`.
Any member may raise any event; `AllBuffered` / `OthersBuffered` events are replayed to late
joiners until the sender leaves. `EventReceived` reports every event with its raw arguments and
`RaiseRawEvent` sends bytes without a handler.

## Custom properties

`world.SetRoomProperty(key, value)` and `SetPlayerProperty(key, value)` (this player's own) work
like PUN's custom properties: values are bool, integers, char, float, double, string, byte[],
Vector2/3/4, Vector2Int/3Int, Quaternion or Color (enums as integers), and null deletes. Writes
apply when the room echoes them, so every member sees the same order. Late joiners receive all
properties in their snapshot; a leaving player's properties are deleted. Read them with
`TryGetRoomProperty<T>` / `TryGetPlayerProperty<T>`, observe `RoomPropertyChanged` /
`PlayerPropertyChanged`.

## Custom synchronized values

Components implementing `INetworkObservable` work like `OnPhotonSerializeView`:
`OnNetworkWrite(entity)` runs for the owner (or lease holder) right before each batched send
(`NetworkWorld.SendRate` per second) and writes fields with `entity.SetFloat/SetInt/SetBool`;
`OnNetworkRead(entity, changedFields)` runs on other clients when updates arrive
(`entity.HasChanged(name, changedFields)`, `GetFloat/GetInt/GetBool`). Only changed values
are sent. Fields come from the schema (32 integer slots per object).

## UDP for state updates

Synchronized rooms also accept UDP on their port, like PUN's unreliable channel. Reliable traffic
(commands, spawns, RPCs, events, properties) stays on WebSocket; only the periodic state updates
use UDP. Outside WebGL:

NetworkSession does this; with a custom transport:

```csharp
var udp = new RealtimeUdpChannel(session);   // after Connect; session.UdpSecret != 0
// every frame
udp.Poll((bytes, milliseconds) => world.Receive(bytes, milliseconds));
world.Unreliable = udp.Active ? udp.TrySend : (TrySend)null;
```

`Poll` registers with the room and keeps the registration alive; `Active` turns true once the room
answers and false after 10 s of silence (the room then sends over WebSocket again). Datagrams that
arrive late are dropped. Values sent over UDP are sent again once per second in both directions, so
a lost packet leaves a value stale for at most about a second. Flushes that precede a command stay
on WebSocket, so updates still arrive before the command. WebGL and networks that block UDP keep
using WebSocket only.

## Server time and remote motion

Every message carries the room server's clock (`MessageMetadata.LifeTimeMilliSeconds`).
Subscribe `RealtimeSession.OnMessageWithMetadata` and call
`world.Receive(message.Data.ToByteArray(), metadata.LifeTimeMilliSeconds)`. Then
`NetworkWorld.ServerTime` is a room-wide clock in seconds (like `PhotonNetwork.Time`), and
remote NetworkTransforms show the pose from `InterpolationDelay` seconds ago (default 0.1),
interpolated between the two received updates around it. When updates stop they hold the
newest pose; they do not extrapolate.

Without timestamps (`Receive(payload)`), remote objects approach the latest pose at
`Interpolation` per second. In both modes the orientation is interpolated as a quaternion
when all rotation axes are synchronized. Set `TeleportDistance` to jump instead of sliding
when an object moves farther than that (e.g. a respawn); `SnapToNetwork()` jumps on demand.
