#if UNITY_2021_3_OR_NEWER
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Scripting;

namespace Gs2.Realtime.Sync
{
    // Receivers of an RPC. Owner is the object's owner (the caller itself for owner-only
    // methods). Buffered targets are also replayed to players who join later, until the
    // object is removed or the caller leaves. To reach one player use NetworkEntity.RpcTo.
    public enum RpcTarget : byte
    {
        All = RpcTargetByte.All,
        Others = RpcTargetByte.Others,
        Owner = RpcTargetByte.Owner,
        AllBuffered = RpcTargetByte.All | RpcTargetByte.Buffered,
        OthersBuffered = RpcTargetByte.Others | RpcTargetByte.Buffered
    }

    // Marks a method on a component under a NetworkEntity as a remote procedure.
    // [NetworkRpc] derives the ID from the method name (like PUN's name-based RPCs);
    // [NetworkRpc(id)] fixes it so the method can be renamed. When the room has an object
    // catalog, the ID must be listed in the prefab's "methods" (owner-only) or, with
    // AnyCaller, in "publicMethods" (any member, on any player's object); the room rejects
    // other calls. Rooms without a catalog accept any call. Parameters are encoded in
    // declaration order. An optional last NetworkRpcInfo parameter receives the sender.
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class NetworkRpcAttribute : PreserveAttribute
    {
        public uint Id
        {
            get;
        }
        public NetworkRpcAttribute()
        {
        }
        public NetworkRpcAttribute(uint id)
        {
            if (id == 0)
                throw new ArgumentOutOfRangeException(nameof(id), "RPC method ID must be positive");
            Id = id;
        }
        // Stable ID for [NetworkRpc] without an explicit ID: FNV-1a (32-bit) of the UTF-8
        // method name, never 0. The catalog exporter writes the same value.
        public static uint IdOf(string methodName)
        {
            uint hash = 2166136261;
            foreach (var b in Encoding.UTF8.GetBytes(methodName))
            {
                hash ^= b;
                hash = unchecked(hash * 16777619);
            }
            return hash == 0 ? 1 : hash;
        }
        // Meant to be called by any member on other players' objects: the catalog export
        // lists it in "publicMethods". The room, not the client, enforces the catalog.
        public bool AnyCaller
        {
            get; set;
        }
    }

    // Marks a room event handler, like PUN's RaiseEvent/OnEvent: register the component
    // with NetworkWorld.AddEventTarget. [NetworkEvent] derives the code from the method
    // name; [NetworkEvent(code)] fixes it. Parameters follow the [NetworkRpc] rules.
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class NetworkEventAttribute : PreserveAttribute
    {
        public uint Code
        {
            get;
        }
        public NetworkEventAttribute()
        {
        }
        public NetworkEventAttribute(uint code)
        {
            if (code == 0)
                throw new ArgumentOutOfRangeException(nameof(code), "Event code must be positive");
            Code = code;
        }
    }

    // A [NetworkRpc] method as declared, for tools (inspector, catalog export).
    public readonly struct NetworkRpcDescription
    {
        public uint Id
        {
            get;
        }
        public string Name
        {
            get;
        }
        public Type Component
        {
            get;
        }
        public bool AnyCaller
        {
            get;
        }
        public Type[] Parameters
        {
            get;
        }
        public NetworkRpcDescription(uint id, string name, Type component, bool anyCaller, Type[] parameters)
        {
            Id = id;
            Name = name;
            Component = component;
            AnyCaller = anyCaller;
            Parameters = parameters;
        }
    }

    public readonly struct NetworkRpcInfo
    {
        public NetworkEntity Entity
        {
            get;
        }
        public uint Method
        {
            get;
        }
        // Connection ID assigned by the server; clients cannot forge it.
        public uint Sender
        {
            get;
        }
        public bool IsLocal
        {
            get;
        }
        public NetworkRpcInfo(NetworkEntity entity, uint method, uint sender, bool isLocal)
        {
            Entity = entity;
            Method = method;
            Sender = sender;
            IsLocal = isLocal;
        }
    }

    // One [NetworkRpc] method of a component type.
    sealed class NetworkRpcMethod
    {
        public readonly uint Id;
        public readonly MethodInfo Method;
        public readonly Type[] Parameters;
        public readonly bool HasInfo, IsCoroutine, AnyCaller;
        public string Name => Method.Name;
        NetworkRpcMethod(uint id, MethodInfo method, Type[] parameters, bool hasInfo, bool isCoroutine, bool anyCaller)
        {
            Id = id;
            AnyCaller = anyCaller;
            Method = method;
            Parameters = parameters;
            HasInfo = hasInfo;
            IsCoroutine = isCoroutine;
        }

        static readonly Dictionary<Type, NetworkRpcMethod[]> cache = new Dictionary<Type, NetworkRpcMethod[]>();
        static readonly Dictionary<Type, NetworkRpcMethod[]> eventCache = new Dictionary<Type, NetworkRpcMethod[]>();

        // Reflection runs once per component type; later lookups reuse the table.
        // events selects [NetworkEvent] handlers instead of [NetworkRpc] methods.
        public static NetworkRpcMethod[] Of(Type type, bool events = false)
        {
            var table = events ? eventCache : cache;
            if (table.TryGetValue(type, out var methods))
                return methods;
            var result = new List<NetworkRpcMethod>();
            var ids = new HashSet<uint>();
            for (var current = type; current != null && current != typeof(MonoBehaviour); current = current.BaseType)
            {
                const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
                foreach (var method in current.GetMethods(flags))
                {
                    uint id;
                    bool anyCaller = false;
                    if (events)
                    {
                        var handler = method.GetCustomAttribute<NetworkEventAttribute>(true);
                        if (handler == null)
                            continue;
                        id = handler.Code;
                    }
                    else
                    {
                        var attribute = method.GetCustomAttribute<NetworkRpcAttribute>(true);
                        if (attribute == null)
                            continue;
                        id = attribute.Id;
                        anyCaller = attribute.AnyCaller;
                    }
                    // An override inherits the attribute; register the most derived declaration once.
                    if (method.GetBaseDefinition() != method && result.Exists(m => m.Method.GetBaseDefinition() == method.GetBaseDefinition()))
                        continue;
                    var created = Create(type, method, id, anyCaller);
                    if (!ids.Add(created.Id))
                        throw new InvalidOperationException("Duplicate " + (events ? "event code " : "RPC method ID ") + created.Id + " in " + type.Name);
                    result.Add(created);
                }
            }
            methods = result.ToArray();
            table[type] = methods;
            return methods;
        }

        static NetworkRpcMethod Create(Type type, MethodInfo method, uint explicitId, bool anyCaller)
        {
            bool coroutine = method.ReturnType == typeof(IEnumerator);
            if (method.ReturnType != typeof(void) && !coroutine)
                throw new InvalidOperationException("RPC " + type.Name + "." + method.Name + " must return void or IEnumerator");
            if (method.IsGenericMethodDefinition)
                throw new InvalidOperationException("RPC " + type.Name + "." + method.Name + " must not be generic");
            var parameters = method.GetParameters();
            bool hasInfo = parameters.Length > 0 && parameters[parameters.Length - 1].ParameterType == typeof(NetworkRpcInfo);
            var types = new Type[hasInfo ? parameters.Length - 1 : parameters.Length];
            for (int i = 0; i < types.Length; i++)
            {
                var parameter = parameters[i];
                if (parameter.ParameterType.IsByRef || parameter.IsOut)
                    throw new InvalidOperationException("RPC " + type.Name + "." + method.Name + " must not use ref/out parameters");
                if (!NetworkRpcCodec.Supports(parameter.ParameterType))
                    throw new InvalidOperationException("RPC " + type.Name + "." + method.Name + " has unsupported parameter type " + parameter.ParameterType.Name);
                types[i] = parameter.ParameterType;
            }
            uint id = explicitId != 0 ? explicitId : NetworkRpcAttribute.IdOf(method.Name);
            return new NetworkRpcMethod(id, method, types, hasInfo, coroutine, anyCaller);
        }
    }

    // Tag-free argument encoding: both sides know the parameter types from the method.
    // Integers are (zigzag) varints, floating point values are little-endian IEEE 754,
    // strings are UTF-8, and strings/byte arrays carry length+1 so that 0 means null.
    static class NetworkRpcCodec
    {
        static readonly UTF8Encoding utf8 = new UTF8Encoding(false, true);

        public static bool Supports(Type type)
        {
            if (type.IsEnum)
                return true;
            return type == typeof(bool) || type == typeof(byte) || type == typeof(sbyte) || type == typeof(short) || type == typeof(ushort) ||
                type == typeof(int) || type == typeof(uint) || type == typeof(long) || type == typeof(ulong) || type == typeof(char) ||
                type == typeof(float) || type == typeof(double) || type == typeof(string) || type == typeof(byte[]) ||
                type == typeof(Vector2) || type == typeof(Vector3) || type == typeof(Vector4) || type == typeof(Quaternion) || type == typeof(Color) ||
                type == typeof(Vector2Int) || type == typeof(Vector3Int);
        }

        public static int Encode(byte[] buffer, Type[] types, object[] args)
        {
            // Rpc(id, target, null) passes a null params array; it means one null argument.
            if (args == null)
                args = types.Length == 1 ? new object[] { null } : Array.Empty<object>();
            if (args.Length != types.Length)
                throw new ArgumentException("RPC expects " + types.Length + " argument(s) but got " + args.Length);
            var writer = new Writer(buffer);
            for (int i = 0; i < types.Length; i++)
                Write(ref writer, types[i], Coerce(types[i], args[i], i));
            return writer.Position;
        }

        // Accept the conversions C# would apply implicitly at a direct call site (e.g. 1 for a float).
        static object Coerce(Type type, object value, int index)
        {
            if (value == null)
            {
                if (type.IsValueType)
                    throw new ArgumentException("RPC argument " + index + " must not be null (" + type.Name + ")");
                return null;
            }
            if (type.IsInstanceOfType(value))
                return value;
            try
            {
                if (type.IsEnum)
                    return Enum.ToObject(type, value);
                // Numeric widening/narrowing only; bool and string never convert implicitly.
                if (IsNumber(type) && IsNumber(value.GetType()))
                    return Convert.ChangeType(value, type, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e) when (e is InvalidCastException || e is OverflowException || e is FormatException || e is ArgumentException)
            {
                throw new ArgumentException("RPC argument " + index + " cannot be converted to " + type.Name, e);
            }
            throw new ArgumentException("RPC argument " + index + " must be " + type.Name + " but was " + value.GetType().Name);
        }

        static bool IsNumber(Type type)
        {
            return type.IsPrimitive && type != typeof(bool) && type != typeof(char) && type != typeof(IntPtr) && type != typeof(UIntPtr);
        }

        // Custom property values carry a type tag (index + 1 in Tagged); keys are a
        // varint length and UTF-8. A key without a value deletes the property.
        static readonly Type[] Tagged =
        {
            typeof(bool), typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong),
            typeof(char), typeof(float), typeof(double), typeof(string), typeof(byte[]), typeof(Vector2), typeof(Vector3), typeof(Vector4),
            typeof(Quaternion), typeof(Color), typeof(Vector2Int), typeof(Vector3Int)
        };
        public const int MaxPropertyKey = 64;
        public static int EncodeProperty(byte[] buffer, string key, object value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Property key must not be empty");
            var writer = new Writer(buffer);
            int length = utf8.GetByteCount(key);
            if (length > MaxPropertyKey)
                throw new ArgumentException("Property key exceeds " + MaxPropertyKey + " bytes");
            writer.Unsigned((ulong)length);
            writer.Reserve(length);
            writer.Position += utf8.GetBytes(key, 0, key.Length, writer.Buffer, writer.Position);
            if (value == null)
                return writer.Position;
            var type = value.GetType();
            if (type.IsEnum)
                throw new ArgumentException("Store enums as their integer value");
            int tag = Array.IndexOf(Tagged, type);
            if (tag < 0)
                throw new ArgumentException("Unsupported property type " + type.Name);
            writer.Byte((byte)(tag + 1));
            Write(ref writer, type, value);
            return writer.Position;
        }
        public static void DecodeProperty(byte[] buffer, int offset, int count, out string key, out object value)
        {
            var reader = new Reader(buffer, offset, count);
            ulong length = reader.Unsigned();
            if (length == 0 || length > MaxPropertyKey || length > (ulong)(count - (reader.Position - offset)))
                throw new FormatException("Invalid property key");
            try
            {
                key = utf8.GetString(buffer, reader.Position, (int)length);
            }
            catch (DecoderFallbackException e)
            {
                throw new FormatException("Invalid UTF-8 in property key", e);
            }
            reader.Position += (int)length;
            value = null;
            if (reader.AtEnd)
                return;
            int tag = reader.Byte() - 1;
            if (tag < 0 || tag >= Tagged.Length)
                throw new FormatException("Unknown property type");
            value = Read(ref reader, Tagged[tag]);
            if (!reader.AtEnd)
                throw new FormatException("Trailing property data");
        }

        public static object[] Decode(byte[] buffer, int offset, int count, NetworkRpcMethod method, NetworkRpcInfo info)
        {
            var types = method.Parameters;
            var result = new object[method.HasInfo ? types.Length + 1 : types.Length];
            var reader = new Reader(buffer, offset, count);
            for (int i = 0; i < types.Length; i++)
                result[i] = Read(ref reader, types[i]);
            if (!reader.AtEnd)
                throw new FormatException("Trailing RPC arguments");
            if (method.HasInfo)
                result[types.Length] = info;
            return result;
        }

        static void Write(ref Writer w, Type type, object value)
        {
            if (type.IsEnum)
            {
                var underlying = Enum.GetUnderlyingType(type);
                if (underlying == typeof(ulong) || underlying == typeof(uint) || underlying == typeof(ushort) || underlying == typeof(byte))
                    w.Unsigned(Convert.ToUInt64(value, System.Globalization.CultureInfo.InvariantCulture));
                else
                    w.Signed(Convert.ToInt64(value, System.Globalization.CultureInfo.InvariantCulture));
                return;
            }
            switch (value)
            {
                case bool v:
                    w.Byte(v ? (byte)1 : (byte)0);
                    return;
                case byte v:
                    w.Byte(v);
                    return;
                case sbyte v:
                    w.Byte(unchecked((byte)v));
                    return;
                case short v:
                    w.Signed(v);
                    return;
                case int v:
                    w.Signed(v);
                    return;
                case long v:
                    w.Signed(v);
                    return;
                case ushort v:
                    w.Unsigned(v);
                    return;
                case uint v:
                    w.Unsigned(v);
                    return;
                case ulong v:
                    w.Unsigned(v);
                    return;
                case char v:
                    w.Unsigned(v);
                    return;
                case float v:
                    w.Single(v);
                    return;
                case double v:
                    w.Fixed64(BitConverter.DoubleToInt64Bits(v));
                    return;
                case Vector2 v:
                    w.Single(v.x);
                    w.Single(v.y);
                    return;
                case Vector3 v:
                    w.Single(v.x);
                    w.Single(v.y);
                    w.Single(v.z);
                    return;
                case Vector4 v:
                    w.Single(v.x);
                    w.Single(v.y);
                    w.Single(v.z);
                    w.Single(v.w);
                    return;
                case Quaternion v:
                    w.Single(v.x);
                    w.Single(v.y);
                    w.Single(v.z);
                    w.Single(v.w);
                    return;
                case Color v:
                    w.Single(v.r);
                    w.Single(v.g);
                    w.Single(v.b);
                    w.Single(v.a);
                    return;
                case Vector2Int v:
                    w.Signed(v.x);
                    w.Signed(v.y);
                    return;
                case Vector3Int v:
                    w.Signed(v.x);
                    w.Signed(v.y);
                    w.Signed(v.z);
                    return;
            }
            if (type == typeof(string))
            {
                var text = (string)value;
                if (text == null)
                {
                    w.Unsigned(0);
                    return;
                }
                int length = utf8.GetByteCount(text);
                w.Unsigned((ulong)length + 1);
                w.Reserve(length);
                w.Position += utf8.GetBytes(text, 0, text.Length, w.Buffer, w.Position);
                return;
            }
            if (type == typeof(byte[]))
            {
                var bytes = (byte[])value;
                if (bytes == null)
                {
                    w.Unsigned(0);
                    return;
                }
                w.Unsigned((ulong)bytes.Length + 1);
                w.Reserve(bytes.Length);
                Buffer.BlockCopy(bytes, 0, w.Buffer, w.Position, bytes.Length);
                w.Position += bytes.Length;
                return;
            }
            throw new ArgumentException("Unsupported RPC argument type " + type.Name);
        }

        static object Read(ref Reader r, Type type)
        {
            if (type.IsEnum)
            {
                var underlying = Enum.GetUnderlyingType(type);
                bool unsigned = underlying == typeof(ulong) || underlying == typeof(uint) || underlying == typeof(ushort) || underlying == typeof(byte);
                object raw = unsigned ? (object)r.Unsigned() : r.Signed();
                try
                {
                    return Enum.ToObject(type, Convert.ChangeType(raw, underlying, System.Globalization.CultureInfo.InvariantCulture));
                }
                catch (OverflowException e)
                {
                    throw new FormatException("RPC enum value out of range", e);
                }
            }
            if (type == typeof(bool))
            {
                byte b = r.Byte();
                if (b > 1)
                    throw new FormatException("Invalid RPC bool");
                return b == 1;
            }
            if (type == typeof(byte))
                return r.Byte();
            if (type == typeof(sbyte))
                return unchecked((sbyte)r.Byte());
            if (type == typeof(short))
                return checked((short)r.Signed());
            if (type == typeof(int))
                return checked((int)r.Signed());
            if (type == typeof(long))
                return r.Signed();
            if (type == typeof(ushort))
                return checked((ushort)r.Unsigned());
            if (type == typeof(uint))
                return checked((uint)r.Unsigned());
            if (type == typeof(ulong))
                return r.Unsigned();
            if (type == typeof(char))
                return checked((char)r.Unsigned());
            if (type == typeof(float))
                return r.Single();
            if (type == typeof(double))
                return BitConverter.Int64BitsToDouble(r.Fixed64());
            if (type == typeof(Vector2))
                return new Vector2(r.Single(), r.Single());
            if (type == typeof(Vector3))
                return new Vector3(r.Single(), r.Single(), r.Single());
            if (type == typeof(Vector4))
                return new Vector4(r.Single(), r.Single(), r.Single(), r.Single());
            if (type == typeof(Quaternion))
                return new Quaternion(r.Single(), r.Single(), r.Single(), r.Single());
            if (type == typeof(Color))
                return new Color(r.Single(), r.Single(), r.Single(), r.Single());
            if (type == typeof(Vector2Int))
                return new Vector2Int(checked((int)r.Signed()), checked((int)r.Signed()));
            if (type == typeof(Vector3Int))
                return new Vector3Int(checked((int)r.Signed()), checked((int)r.Signed()), checked((int)r.Signed()));
            if (type == typeof(string))
            {
                int length = r.Length();
                if (length < 0)
                    return null;
                try
                {
                    var text = utf8.GetString(r.Buffer, r.Position, length);
                    r.Position += length;
                    return text;
                }
                catch (DecoderFallbackException e)
                {
                    throw new FormatException("Invalid UTF-8 in RPC string", e);
                }
            }
            if (type == typeof(byte[]))
            {
                int length = r.Length();
                if (length < 0)
                    return null;
                var bytes = new byte[length];
                Buffer.BlockCopy(r.Buffer, r.Position, bytes, 0, length);
                r.Position += length;
                return bytes;
            }
            throw new FormatException("Unsupported RPC argument type " + type.Name);
        }

        struct Writer
        {
            public readonly byte[] Buffer;
            public int Position;
            public Writer(byte[] buffer)
            {
                Buffer = buffer;
                Position = 0;
            }
            public void Reserve(int count)
            {
                if (count > Buffer.Length - Position)
                    throw new ArgumentException("RPC arguments exceed " + SyncCodec.MaxArgs + " bytes");
            }
            public void Byte(byte value)
            {
                Reserve(1);
                Buffer[Position++] = value;
            }
            public void Unsigned(ulong value)
            {
                while (value >= 128)
                {
                    Byte((byte)(value | 128));
                    value >>= 7;
                }
                Byte((byte)value);
            }
            public void Signed(long value)
            {
                Unsigned(unchecked((ulong)(value << 1) ^ (ulong)(value >> 63)));
            }
            public void Single(float value)
            {
                int bits = BitConverter.SingleToInt32Bits(value);
                Reserve(4);
                for (int i = 0; i < 4; i++)
                    Buffer[Position++] = (byte)(bits >> (8 * i));
            }
            public void Fixed64(long bits)
            {
                Reserve(8);
                for (int i = 0; i < 8; i++)
                    Buffer[Position++] = (byte)(bits >> (8 * i));
            }
        }

        struct Reader
        {
            public readonly byte[] Buffer;
            public int Position;
            readonly int end;
            public Reader(byte[] buffer, int offset, int count)
            {
                Buffer = buffer;
                Position = offset;
                end = offset + count;
            }
            public bool AtEnd => Position == end;
            void Need(int count)
            {
                if (count > end - Position)
                    throw new FormatException("Truncated RPC arguments");
            }
            public byte Byte()
            {
                Need(1);
                return Buffer[Position++];
            }
            public ulong Unsigned()
            {
                ulong value = 0;
                for (int shift = 0; shift < 70; shift += 7)
                {
                    byte b = Byte();
                    if (shift == 63 && b > 1)
                        throw new FormatException("RPC varint overflow");
                    value |= (ulong)(b & 127) << shift;
                    if (b < 128)
                        return value;
                }
                throw new FormatException("Invalid RPC varint");
            }
            public long Signed()
            {
                ulong v = Unsigned();
                return unchecked((long)(v >> 1) ^ -(long)(v & 1));
            }
            public float Single()
            {
                Need(4);
                int bits = 0;
                for (int i = 0; i < 4; i++)
                    bits |= Buffer[Position++] << (8 * i);
                return BitConverter.Int32BitsToSingle(bits);
            }
            public long Fixed64()
            {
                Need(8);
                long bits = 0;
                for (int i = 0; i < 8; i++)
                    bits |= (long)Buffer[Position++] << (8 * i);
                return bits;
            }
            // -1 for null; otherwise a byte count that fits in the remaining arguments.
            public int Length()
            {
                ulong value = Unsigned();
                if (value == 0)
                    return -1;
                if (value - 1 > (ulong)(end - Position))
                    throw new FormatException("Truncated RPC arguments");
                return (int)(value - 1);
            }
        }
    }
}
#endif
