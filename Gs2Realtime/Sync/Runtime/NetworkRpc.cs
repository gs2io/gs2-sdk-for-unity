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
    /// <summary>
    /// Marks a method on a component under a NetworkEntity as a remote procedure. It must
    /// return void or IEnumerator (started as a coroutine) and take no ref/out parameters; an
    /// optional last NetworkRpcInfo parameter receives the sender.
    /// </summary>
    /// <remarks>
    /// [NetworkRpc] derives the ID from the method name (like PUN's name-based RPCs);
    /// [NetworkRpc(id)] fixes it so the method can be renamed. When the room has an object
    /// catalog, the ID must be listed in the prefab's "methods" (owner-only) or, with
    /// AnyCaller, in "publicMethods" (any member, on any player's object); the room rejects
    /// other calls. Rooms without a catalog accept any call.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class NetworkRpcAttribute : PreserveAttribute
    {
        /// <summary>The explicit method ID, or 0 to derive it from the method name.</summary>
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
        /// <summary>
        /// The ID of a [NetworkRpc] without an explicit ID: FNV-1a (32-bit) of the UTF-8 method
        /// name, never 0. The catalog exporter writes the same value.
        /// </summary>
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
        /// <summary>
        /// Lets any member call it on other players' objects: the catalog export lists it in
        /// "publicMethods". The room, not the client, enforces the catalog.
        /// </summary>
        public bool AnyCaller
        {
            get; set;
        }
    }

    /// <summary>
    /// Marks a room event handler, like PUN's RaiseEvent/OnEvent: register the component with
    /// NetworkWorld.AddEventTarget. [NetworkEvent] derives the code from the method name;
    /// [NetworkEvent(code)] fixes it. Parameters follow the [NetworkRpc] rules.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class NetworkEventAttribute : PreserveAttribute
    {
        /// <summary>The explicit event code, or 0 to derive it from the method name.</summary>
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

    /// <summary>A [NetworkRpc] method as declared, for tools (inspector, catalog export).</summary>
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

    /// <summary>
    /// Details of a received RPC or room event; declare it as the last parameter of a
    /// [NetworkRpc] or [NetworkEvent] method to receive it.
    /// </summary>
    public readonly struct NetworkRpcInfo
    {
        /// <summary>The entity the RPC was called on (null for room events).</summary>
        public NetworkEntity Entity
        {
            get;
        }
        /// <summary>The RPC method ID or event code.</summary>
        public uint Method
        {
            get;
        }
        /// <summary>The sender's connection ID, set by the server; clients cannot forge it.</summary>
        public uint Sender
        {
            get;
        }
        /// <summary>True when this client sent it.</summary>
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
        // How each parameter travels, resolved once here.
        public readonly NetworkRpcCodec.ArgumentCodec[] Codecs;
        public readonly bool HasInfo, IsCoroutine, AnyCaller;
        public string Name => Method.Name;
        NetworkRpcMethod(uint id, MethodInfo method, NetworkRpcCodec.ArgumentCodec[] codecs, bool hasInfo, bool isCoroutine, bool anyCaller)
        {
            Codecs = codecs;
            var parameters = new Type[codecs.Length];
            for (int i = 0; i < codecs.Length; i++)
                parameters[i] = codecs[i].Type;
            Id = id;
            AnyCaller = anyCaller;
            Method = method;
            Parameters = parameters;
            HasInfo = hasInfo;
            IsCoroutine = isCoroutine;
        }

        static readonly Dictionary<Type, NetworkRpcMethod[]> cache = new Dictionary<Type, NetworkRpcMethod[]>();
        static readonly Dictionary<Type, NetworkRpcMethod[]> eventCache = new Dictionary<Type, NetworkRpcMethod[]>();

        // Cached per component type. events selects [NetworkEvent] handlers instead of
        // [NetworkRpc] methods.
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
                    // An override inherits the attribute; register only the most derived one.
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
            var codecs = new NetworkRpcCodec.ArgumentCodec[hasInfo ? parameters.Length - 1 : parameters.Length];
            for (int i = 0; i < codecs.Length; i++)
            {
                var parameter = parameters[i];
                if (parameter.ParameterType.IsByRef || parameter.IsOut)
                    throw new InvalidOperationException("RPC " + type.Name + "." + method.Name + " must not use ref/out parameters");
                codecs[i] = NetworkRpcCodec.For(parameter.ParameterType) ??
                    throw new InvalidOperationException("RPC " + type.Name + "." + method.Name + " has unsupported parameter type " + parameter.ParameterType.Name);
            }
            uint id = explicitId != 0 ? explicitId : NetworkRpcAttribute.IdOf(method.Name);
            return new NetworkRpcMethod(id, method, codecs, hasInfo, coroutine, anyCaller);
        }
    }

    // Tag-free argument encoding: both sides know the parameter types from the method.
    // Integers are (zigzag) varints, floating point values are little-endian IEEE 754,
    // strings are UTF-8, and strings/byte arrays carry length+1 so that 0 means null.
    static class NetworkRpcCodec
    {
        static readonly UTF8Encoding utf8 = new UTF8Encoding(false, true);

        internal delegate void WriteValue(ref Writer writer, object value);
        internal delegate object ReadValue(ref Reader reader);
        // How one argument type travels. One per supported type (and per enum type),
        // chosen once when a method registers, so encoding and decoding never test types.
        internal sealed class ArgumentCodec
        {
            public readonly Type Type;
            // Written before custom property values; 0 when the type cannot be one (enums).
            public readonly byte Tag;
            readonly WriteValue write;
            readonly ReadValue read;
            public ArgumentCodec(Type type, byte tag, WriteValue write, ReadValue read)
            {
                Type = type;
                Tag = tag;
                this.write = write;
                this.read = read;
            }
            public void Write(ref Writer writer, object value) => write(ref writer, value);
            public object Read(ref Reader reader) => read(ref reader);
        }

        // Their order gives the property tags (index + 1), which are the wire format: append
        // new types at the end.
        static readonly ArgumentCodec[] Tagged = Build(
            (typeof(bool), (ref Writer w, object v) => w.Byte((bool)v ? (byte)1 : (byte)0), (ref Reader r) => ReadBool(ref r)),
            (typeof(byte), (ref Writer w, object v) => w.Byte((byte)v), (ref Reader r) => r.Byte()),
            (typeof(sbyte), (ref Writer w, object v) => w.Byte(unchecked((byte)(sbyte)v)), (ref Reader r) => unchecked((sbyte)r.Byte())),
            (typeof(short), (ref Writer w, object v) => w.Signed((short)v), (ref Reader r) => checked((short)r.Signed())),
            (typeof(ushort), (ref Writer w, object v) => w.Unsigned((ushort)v), (ref Reader r) => checked((ushort)r.Unsigned())),
            (typeof(int), (ref Writer w, object v) => w.Signed((int)v), (ref Reader r) => checked((int)r.Signed())),
            (typeof(uint), (ref Writer w, object v) => w.Unsigned((uint)v), (ref Reader r) => checked((uint)r.Unsigned())),
            (typeof(long), (ref Writer w, object v) => w.Signed((long)v), (ref Reader r) => r.Signed()),
            (typeof(ulong), (ref Writer w, object v) => w.Unsigned((ulong)v), (ref Reader r) => r.Unsigned()),
            (typeof(char), (ref Writer w, object v) => w.Unsigned((char)v), (ref Reader r) => checked((char)r.Unsigned())),
            (typeof(float), (ref Writer w, object v) => w.Single((float)v), (ref Reader r) => r.Single()),
            (typeof(double), (ref Writer w, object v) => w.Fixed64(BitConverter.DoubleToInt64Bits((double)v)), (ref Reader r) => BitConverter.Int64BitsToDouble(r.Fixed64())),
            (typeof(string), (ref Writer w, object v) => WriteString(ref w, (string)v), (ref Reader r) => ReadString(ref r)),
            (typeof(byte[]), (ref Writer w, object v) => WriteBytes(ref w, (byte[])v), (ref Reader r) => ReadBytes(ref r)),
            (typeof(Vector2), (ref Writer w, object v) => { var x = (Vector2)v; w.Single(x.x); w.Single(x.y); }, (ref Reader r) => new Vector2(r.Single(), r.Single())),
            (typeof(Vector3), (ref Writer w, object v) => { var x = (Vector3)v; w.Single(x.x); w.Single(x.y); w.Single(x.z); }, (ref Reader r) => new Vector3(r.Single(), r.Single(), r.Single())),
            (typeof(Vector4), (ref Writer w, object v) => { var x = (Vector4)v; w.Single(x.x); w.Single(x.y); w.Single(x.z); w.Single(x.w); }, (ref Reader r) => new Vector4(r.Single(), r.Single(), r.Single(), r.Single())),
            (typeof(Quaternion), (ref Writer w, object v) => { var x = (Quaternion)v; w.Single(x.x); w.Single(x.y); w.Single(x.z); w.Single(x.w); }, (ref Reader r) => new Quaternion(r.Single(), r.Single(), r.Single(), r.Single())),
            (typeof(Color), (ref Writer w, object v) => { var x = (Color)v; w.Single(x.r); w.Single(x.g); w.Single(x.b); w.Single(x.a); }, (ref Reader r) => new Color(r.Single(), r.Single(), r.Single(), r.Single())),
            (typeof(Vector2Int), (ref Writer w, object v) => { var x = (Vector2Int)v; w.Signed(x.x); w.Signed(x.y); }, (ref Reader r) => new Vector2Int(checked((int)r.Signed()), checked((int)r.Signed()))),
            (typeof(Vector3Int), (ref Writer w, object v) => { var x = (Vector3Int)v; w.Signed(x.x); w.Signed(x.y); w.Signed(x.z); }, (ref Reader r) => new Vector3Int(checked((int)r.Signed()), checked((int)r.Signed()), checked((int)r.Signed())))
        );
        static readonly Dictionary<Type, ArgumentCodec> byType = new Dictionary<Type, ArgumentCodec>();
        static ArgumentCodec[] Build(params (Type type, WriteValue write, ReadValue read)[] entries)
        {
            var codecs = new ArgumentCodec[entries.Length];
            for (int i = 0; i < entries.Length; i++)
                codecs[i] = new ArgumentCodec(entries[i].type, (byte)(i + 1), entries[i].write, entries[i].read);
            return codecs;
        }
        static NetworkRpcCodec()
        {
            foreach (var codec in Tagged)
                byType.Add(codec.Type, codec);
        }

        // The codec of a parameter type (an enum travels as its underlying integer); null
        // when the type cannot be an RPC argument.
        public static ArgumentCodec For(Type type)
        {
            if (byType.TryGetValue(type, out var codec))
                return codec;
            if (!type.IsEnum)
                return null;
            codec = EnumCodec(type);
            byType.Add(type, codec);
            return codec;
        }
        static ArgumentCodec EnumCodec(Type type)
        {
            var underlying = Enum.GetUnderlyingType(type);
            var culture = System.Globalization.CultureInfo.InvariantCulture;
            if (IsUnsigned(underlying))
                return new ArgumentCodec(type, 0, (ref Writer w, object v) => w.Unsigned(Convert.ToUInt64(v, culture)), (ref Reader r) => EnumValue(type, underlying, r.Unsigned()));
            return new ArgumentCodec(type, 0, (ref Writer w, object v) => w.Signed(Convert.ToInt64(v, culture)), (ref Reader r) => EnumValue(type, underlying, r.Signed()));
        }
        static object EnumValue(Type type, Type underlying, object raw)
        {
            try
            {
                return Enum.ToObject(type, Convert.ChangeType(raw, underlying, System.Globalization.CultureInfo.InvariantCulture));
            }
            catch (OverflowException e)
            {
                throw new FormatException("RPC enum value out of range", e);
            }
        }

        public static int Encode(byte[] buffer, NetworkRpcMethod method, object[] args)
        {
            var codecs = method.Codecs;
            // Rpc(id, target, null) passes a null params array; it means one null argument.
            if (args == null)
                args = codecs.Length == 1 ? new object[] { null } : Array.Empty<object>();
            if (args.Length != codecs.Length)
                throw new ArgumentException("RPC expects " + codecs.Length + " argument(s) but got " + args.Length);
            var writer = new Writer(buffer);
            for (int i = 0; i < codecs.Length; i++)
                codecs[i].Write(ref writer, Coerce(codecs[i].Type, args[i], i));
            return writer.Position;
        }

        // Accepts the conversions C# applies implicitly at a direct call (e.g. 1 for a float).
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
            catch (Exception e) when (IsConversionFailure(e))
            {
                throw new ArgumentException("RPC argument " + index + " cannot be converted to " + type.Name, e);
            }
            throw new ArgumentException("RPC argument " + index + " must be " + type.Name + " but was " + value.GetType().Name);
        }

        // What Enum.ToObject and Convert.ChangeType throw for a value they cannot convert.
        static bool IsConversionFailure(Exception e) =>
            e is InvalidCastException || e is OverflowException || e is FormatException || e is ArgumentException;

        static bool IsUnsigned(Type integer)
        {
            return integer == typeof(ulong) || integer == typeof(uint) || integer == typeof(ushort) || integer == typeof(byte);
        }
        static bool IsNumber(Type type)
        {
            return type.IsPrimitive && type != typeof(bool) && type != typeof(char) && type != typeof(IntPtr) && type != typeof(UIntPtr);
        }

        // Custom property values carry their type's tag; no value (empty) deletes the
        // property. SyncClient frames them with their key.
        public static int EncodePropertyValue(byte[] buffer, object value)
        {
            var writer = new Writer(buffer);
            if (value == null)
                return 0;
            var type = value.GetType();
            if (type.IsEnum)
                throw new ArgumentException("Store enums as their integer value");
            if (!byType.TryGetValue(type, out var codec) || codec.Tag == 0)
                throw new ArgumentException("Unsupported property type " + type.Name);
            writer.Byte(codec.Tag);
            codec.Write(ref writer, value);
            return writer.Position;
        }
        // null for an empty (deleted) value.
        public static object DecodePropertyValue(byte[] buffer, int offset, int count)
        {
            if (count == 0)
                return null;
            var reader = new Reader(buffer, offset, count);
            int tag = reader.Byte() - 1;
            if (tag < 0 || tag >= Tagged.Length)
                throw new FormatException("Unknown property type");
            var value = Tagged[tag].Read(ref reader);
            if (!reader.AtEnd)
                throw new FormatException("Trailing property data");
            return value;
        }
        public static object[] Decode(byte[] buffer, int offset, int count, NetworkRpcMethod method, NetworkRpcInfo info)
        {
            var codecs = method.Codecs;
            var result = new object[method.HasInfo ? codecs.Length + 1 : codecs.Length];
            var reader = new Reader(buffer, offset, count);
            for (int i = 0; i < codecs.Length; i++)
                result[i] = codecs[i].Read(ref reader);
            if (!reader.AtEnd)
                throw new FormatException("Trailing RPC arguments");
            if (method.HasInfo)
                result[codecs.Length] = info;
            return result;
        }

        static object ReadBool(ref Reader r)
        {
            byte b = r.Byte();
            if (b > 1)
                throw new FormatException("Invalid RPC bool");
            return b == 1;
        }
        static void WriteString(ref Writer w, string text)
        {
            if (text == null)
            {
                w.Unsigned(0);
                return;
            }
            int length = utf8.GetByteCount(text);
            w.Unsigned((ulong)length + 1);
            w.Reserve(length);
            w.Position += utf8.GetBytes(text, 0, text.Length, w.Buffer, w.Position);
        }
        static object ReadString(ref Reader r)
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
        static void WriteBytes(ref Writer w, byte[] bytes)
        {
            if (bytes == null)
            {
                w.Unsigned(0);
                return;
            }
            w.Unsigned((ulong)bytes.Length + 1);
            w.Reserve(bytes.Length);
            Buffer.BlockCopy(bytes, 0, w.Buffer, w.Position, bytes.Length);
            w.Position += bytes.Length;
        }
        static object ReadBytes(ref Reader r)
        {
            int length = r.Length();
            if (length < 0)
                return null;
            var bytes = new byte[length];
            Buffer.BlockCopy(r.Buffer, r.Position, bytes, 0, length);
            r.Position += length;
            return bytes;
        }

        internal struct Writer
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

        internal struct Reader
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
