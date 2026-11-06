#if UNITY_2021_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Gs2.Realtime.Sync
{
    public enum NetworkValueType
    {
        Int, Bool, Float
    }
    // Generated from the room's C# RoomSchema. This is a client asset, never sent on the wire.
    public sealed class NetworkSchema : ScriptableObject
    {
        [Serializable]
        public sealed class Field
        {
            [SerializeField] string name;
            [SerializeField] int slot;
            [SerializeField] NetworkValueType type;
            [SerializeField] float precision = 1;
            public string Name => name;
            public int Slot => slot;
            public NetworkValueType Type => type;
            public float Precision => precision;
            public Field(string name, int slot, NetworkValueType type, float precision = 1)
            {
                this.name = name;
                this.slot = slot;
                this.type = type;
                this.precision = precision;
            }
        }
        [SerializeField] int prefab;
        [SerializeField] Field[] fields = Array.Empty<Field>();
        public int Prefab => prefab;
        public IReadOnlyList<Field> Fields => fields;
        public Field Find(string name)
        {
            foreach (var field in fields)
                if (field.Name == name)
                    return field;
            return null;
        }
        // Without a generated schema, objects use the built-in position and rotation fields.
        public static IReadOnlyList<Field> FieldsOf(NetworkSchema schema)
        {
            return schema == null ? NetworkStandardFields.Fields : schema.Fields;
        }
        public static Field Resolve(NetworkSchema schema, string name)
        {
            return schema == null ? NetworkStandardFields.Find(name) : schema.Find(name);
        }
        // True when this schema was generated for the object's prefab and field permissions.
        public bool Describes(NetworkObject value)
        {
            return (uint)prefab == value.Prefab && Validate() == value.Fields;
        }
        public void Initialize(int prefab, Field[] fields)
        {
            this.prefab = prefab;
            this.fields = (Field[])fields.Clone();
            Validate();
        }
        public uint Validate()
        {
            if (prefab <= 0 || fields == null || fields.Length == 0 || fields.Length > 32)
                throw new InvalidOperationException("Invalid network schema");
            uint mask = 0;
            var names = new HashSet<string>(StringComparer.Ordinal);
            foreach (var field in fields)
            {
                if (field == null || string.IsNullOrEmpty(field.Name) || !names.Add(field.Name) || field.Slot < 0 || field.Slot > 31 || (mask & (1u << field.Slot)) != 0 || !Enum.IsDefined(typeof(NetworkValueType), field.Type))
                    throw new InvalidOperationException("Invalid network field");
                if (float.IsNaN(field.Precision) || float.IsInfinity(field.Precision) || field.Precision <= 0 || float.IsInfinity(field.Precision * (float)int.MinValue) || (field.Type != NetworkValueType.Float && field.Precision != 1))
                    throw new InvalidOperationException("Invalid field precision");
                mask |= 1u << field.Slot;
            }
            return mask;
        }
    }
    [Serializable]
    public sealed class NetworkPropertyBinding
    {
        [SerializeField] string field;
        [SerializeField] NetworkIntEvent intChanged = new NetworkIntEvent();
        [SerializeField] NetworkBoolEvent boolChanged = new NetworkBoolEvent();
        [SerializeField] NetworkFloatEvent floatChanged = new NetworkFloatEvent();
        public string Field => field;
        public NetworkIntEvent IntChanged => intChanged;
        public NetworkBoolEvent BoolChanged => boolChanged;
        public NetworkFloatEvent FloatChanged => floatChanged;
        public NetworkPropertyBinding(string field)
        {
            this.field = field;
        }
        internal void Publish(NetworkSchema.Field definition, int value)
        {
            switch (definition.Type)
            {
                case NetworkValueType.Int:
                    intChanged.Invoke(value);
                    break;
                case NetworkValueType.Bool:
                    boolChanged.Invoke(value != 0);
                    break;
                case NetworkValueType.Float:
                    floatChanged.Invoke(value * definition.Precision);
                    break;
            }
        }
    }
}
#endif
