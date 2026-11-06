# Unity realtime synchronization

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
5. Put gameplay input behaviours in **Controlled Behaviours**. Leased objects use Entity's
   RequestControl/ReleaseControl; input enables only while control is held.

Property bindings apply server values to Unity. Ordinary target-property edits are not
implicitly sent. Scripts can use Entity.GetInt/GetBool/GetFloat and SetInt/SetBool/SetFloat
by name; writes retain SyncClient's authority checks. NetworkTransform sends owned motion
and interpolates remote motion. Server-owned game state changes through room interactions.

Bindings resolve schema entries once when binding and dispatch only changed slots. UnityEvent
persistent callbacks support player stripping/IL2CPP without per-frame reflection. Snapshots,
reconnect and inactive prefabs receive initial values. Nested entities retain their own bindings.
The compact numeric int32/delta wire protocol is unchanged. Matching schema/GSL versions must
be distributed together; the wire validates field masks, not presentation names or precision.
