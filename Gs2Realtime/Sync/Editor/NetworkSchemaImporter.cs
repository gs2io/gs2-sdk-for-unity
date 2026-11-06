#if UNITY_2021_3_OR_NEWER
using System;
using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;
namespace Gs2.Realtime.Sync.Editor
{
    // Version 2 reads precisionBits; invalidate cached imports from the numeric-precision format.
    [ScriptedImporter(2, "realtimeschema")]
    public sealed class NetworkSchemaImporter : ScriptedImporter
    {
        [Serializable]
        sealed class Document
        {
            public int formatVersion;
            public string name;
            public int prefab;
            public Field[] fields;
        }
        [Serializable]
        sealed class Field
        {
            public string name;
            public int slot;
            public string type;
            public int precisionBits;
        }
        public override void OnImportAsset(AssetImportContext context)
        {
            var document = JsonUtility.FromJson<Document>(File.ReadAllText(context.assetPath));
            if (document == null || document.formatVersion != 1 || document.fields == null)
                throw new InvalidDataException("Invalid realtime schema");
            var fields = new NetworkSchema.Field[document.fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                var value = document.fields[i];
                if (value == null || !Enum.TryParse(value.type, out NetworkValueType type) || !Enum.IsDefined(typeof(NetworkValueType), type))
                    throw new InvalidDataException("Invalid field type");
                fields[i] = new NetworkSchema.Field(value.name, value.slot, type, BitConverter.Int32BitsToSingle(value.precisionBits));
            }
            var schema = ScriptableObject.CreateInstance<NetworkSchema>();
            try
            {
                schema.Initialize(document.prefab, fields);
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
