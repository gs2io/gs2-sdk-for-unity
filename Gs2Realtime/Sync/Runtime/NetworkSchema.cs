#if UNITY_2021_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Gs2.Realtime.Sync
{
    /// <summary>The value type of a network field; Float values are ints scaled by Precision.</summary>
    public enum NetworkValueType
    {
        Int, Bool, Float
    }
    /// <summary>
    /// The fields of one prefab, generated from the room's C# RoomSchema. A client asset,
    /// never sent on the wire; assign it to NetworkEntity.Schema.
    /// </summary>
    public sealed class NetworkSchema : ScriptableObject
    {
        /// <summary>One synchronized field: name, slot (0-31), type and precision.</summary>
        [Serializable]
        public sealed class Field
        {
            [SerializeField] string name;
            [SerializeField] int slot;
            [SerializeField] NetworkValueType type;
            [SerializeField] float precision = 1;
            [SerializeField] bool hasDefault;
            [SerializeField] int defaultValue;
            public string Name => name;
            public int Slot => slot;
            public NetworkValueType Type => type;
            /// <summary>
            /// Step of a Float field (wire value * Precision = value); 1 for other types.
            /// </summary>
            public float Precision => precision;
            /// <summary>
            /// True when objects of a rule-defined prefab start with DefaultValue (the wire
            /// value from [RoomField(Default = ...)]); otherwise they start with 0.
            /// </summary>
            public bool HasDefault => hasDefault;
            public int DefaultValue => defaultValue;
            public Field(string name, int slot, NetworkValueType type, float precision = 1)
            {
                this.name = name;
                this.slot = slot;
                this.type = type;
                this.precision = precision;
            }
            public Field(string name, int slot, NetworkValueType type, float precision, int defaultValue) : this(name, slot, type, precision)
            {
                hasDefault = true;
                this.defaultValue = defaultValue;
            }
        }
        [SerializeField] int prefab;
        [SerializeField] bool ruleDefined;
        [SerializeField] uint rules;
        [SerializeField] Field[] fields = Array.Empty<Field>();
        public int Prefab => prefab;
        /// <summary>
        /// True when the room rules define the prefab (Room.DefineServerPrefab,
        /// DefineLeasedPrefab, DefineSimulatedPrefab...), not the object catalog.
        /// </summary>
        public bool RuleDefined => ruleDefined;
        /// <summary>
        /// The rules this schema was generated from (NetworkWorld compares it with the
        /// room's); 0 when unknown.
        /// </summary>
        public uint Rules => rules;
        /// <summary>
        /// Writes the default wire value of each field that has one into values (indexed by
        /// slot), as the room does on spawn.
        /// </summary>
        public void ApplyDefaults(int[] values)
        {
            foreach (var field in fields)
                if (field.HasDefault)
                    values[field.Slot] = field.DefaultValue;
        }
        public IReadOnlyList<Field> Fields => fields;
        /// <summary>The field with this name; null when none.</summary>
        public Field Find(string name)
        {
            foreach (var field in fields)
                if (field.Name == name)
                    return field;
            return null;
        }
        /// <summary>
        /// The schema's fields, or the standard position and rotation fields when schema is null.
        /// </summary>
        public static IReadOnlyList<Field> FieldsOf(NetworkSchema schema)
        {
            return schema == null ? NetworkStandardFields.Fields : schema.Fields;
        }
        /// <summary>Find in the schema, or in the standard fields when schema is null.</summary>
        public static Field Resolve(NetworkSchema schema, string name)
        {
            return schema == null ? NetworkStandardFields.Find(name) : schema.Find(name);
        }
        /// <summary>
        /// True when this schema matches the object's prefab and the fields the room enabled.
        /// </summary>
        public bool Describes(NetworkObject value)
        {
            return (uint)prefab == value.Prefab && Validate() == value.Fields;
        }
        /// <summary>Fills a schema (the schema importer uses it); throws when invalid (see Validate).</summary>
        public void Initialize(int prefab, Field[] fields, bool ruleDefined = false, uint rules = 0)
        {
            this.prefab = prefab;
            this.ruleDefined = ruleDefined;
            this.rules = rules;
            this.fields = (Field[])fields.Clone();
            Validate();
        }
        // Floats: a positive step whose int range stays finite. Other types: 1.
        static bool ValidPrecision(Field field)
        {
            if (field.Type != NetworkValueType.Float)
                return field.Precision == 1;
            float p = field.Precision;
            return p > 0 && !float.IsInfinity(p) && !float.IsInfinity(p * (float)int.MinValue);
        }
        /// <summary>
        /// Checks the prefab ID, names, slots and precisions; returns the slot bit mask of the
        /// fields. Throws InvalidOperationException when invalid.
        /// </summary>
        public uint Validate()
        {
            if (prefab <= 0 || fields == null || fields.Length == 0 || fields.Length > 32)
                throw new InvalidOperationException("Invalid network schema");
            uint mask = 0;
            var names = new HashSet<string>(StringComparer.Ordinal);
            foreach (var field in fields)
            {
                if (field == null || string.IsNullOrEmpty(field.Name) || !names.Add(field.Name))
                    throw new InvalidOperationException("Invalid network field");
                if (field.Slot < 0 || field.Slot > 31 || FieldMask.Contains(mask, field.Slot))
                    throw new InvalidOperationException("Invalid network field");
                if (!Enum.IsDefined(typeof(NetworkValueType), field.Type))
                    throw new InvalidOperationException("Invalid network field");
                if (!ValidPrecision(field))
                    throw new InvalidOperationException("Invalid field precision");
                mask |= 1u << field.Slot;
            }
            return mask;
        }
    }
    /// <summary>
    /// Raises the UnityEvent matching a field's type when the field changes (Inspector
    /// bindings of NetworkEntity).
    /// </summary>
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
