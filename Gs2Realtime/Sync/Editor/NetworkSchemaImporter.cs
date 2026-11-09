#if UNITY_2021_3_OR_NEWER
using System;
using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;
namespace Gs2.Realtime.Sync.Editor
{
    // Bump the version whenever the import changes, so Unity re-imports existing schemas.
    [ScriptedImporter(5, "realtimeschema")]
    public sealed class NetworkSchemaImporter : ScriptedImporter
    {
        [Serializable]
        sealed class Document
        {
            public int formatVersion;
            public string name;
            public int prefab;
            public string mode; // "server", "lease" or "simulated" when the rules define the prefab
            public string rules; // hex hash of the GSL the schema was generated with
            public Field[] fields;
            public Default[] defaults; // wire values of fields with [RoomField(Default)]
        }
        [Serializable]
        sealed class Field
        {
            public string name;
            public int slot;
            public string type;
            public int precisionBits;
        }
        [Serializable]
        sealed class Default
        {
            public int slot;
            public int value;
        }
        // How the room rules define a prefab (the compiler's RoomSchemas.DefinedMode).
        static bool IsRuleDefinedMode(string mode) => mode is "server" or "lease" or "simulated";
        public override void OnImportAsset(AssetImportContext context)
        {
            var document = JsonUtility.FromJson<Document>(File.ReadAllText(context.assetPath));
            if (document == null || document.formatVersion != 1 || document.fields == null)
                throw new InvalidDataException("Invalid realtime schema");
            bool ruleDefined = !string.IsNullOrEmpty(document.mode);
            if (ruleDefined && !IsRuleDefinedMode(document.mode))
                throw new InvalidDataException("Invalid prefab mode: " + document.mode);
            var fields = new NetworkSchema.Field[document.fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                var value = document.fields[i];
                if (value == null || !Enum.TryParse(value.type, out NetworkValueType type) || !Enum.IsDefined(typeof(NetworkValueType), type))
                    throw new InvalidDataException("Invalid field type");
                float precision = BitConverter.Int32BitsToSingle(value.precisionBits);
                var initial = Array.Find(document.defaults ?? Array.Empty<Default>(), d => d != null && d.slot == value.slot);
                fields[i] = initial == null ? new NetworkSchema.Field(value.name, value.slot, type, precision)
                    : new NetworkSchema.Field(value.name, value.slot, type, precision, initial.value);
            }
            var schema = ScriptableObject.CreateInstance<NetworkSchema>();
            try
            {
                uint rules = 0;
                if (!string.IsNullOrEmpty(document.rules) && !uint.TryParse(document.rules, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out rules))
                    throw new InvalidDataException("Invalid rules hash");
                schema.Initialize(document.prefab, fields, ruleDefined, rules);
                schema.name = document.name;
                context.AddObjectToAsset("schema", schema);
                context.SetMainObject(schema);
            }
            catch
            {
                DestroyImmediate(schema);
                throw;
            }
        }
    }
}
#endif
