using System;
using Newtonsoft.Json;
using UnityEngine;

namespace quik.Runtime.SaveSystem.Converters
{
    public class Vector3Converter : JsonConverter<Vector3>
    {
        private const string X = "x";
        private const string Y = "y";
        private const string Z = "z";
        
        public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            WriteFloat(writer, X, value.x);
            WriteFloat(writer, Y, value.y);
            WriteFloat(writer, Z, value.z);
            writer.WriteEndObject();
        }

        public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var x = 0f;
            var y = 0f;
            var z = 0f;

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject)
                {
                    break;
                }

                var propertyName = reader.Value?.ToString();
                if (propertyName == null)
                {
                    continue;
                }

                reader.Read();
                switch (propertyName)
                {
                    case X: x = Convert.ToSingle(reader.Value); break;
                    case Y: y = Convert.ToSingle(reader.Value); break;
                    case Z: z = Convert.ToSingle(reader.Value); break;
                }
            }

            return new Vector3(x, y, z);
        }

        private static void WriteFloat(JsonWriter writer, string propertyName, float value)
        {
            writer.WritePropertyName(propertyName);
            writer.WriteValue(value);
        }
    }
}
