using System;
using Newtonsoft.Json;
using UnityEngine;

namespace quik.Runtime.SaveSystem.Converters
{
    public class Vector2Converter : JsonConverter<Vector2>
    {
        private const string X = "x";
        private const string Y = "y";
        
        public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            WriteFloat(writer, X, value.x);
            WriteFloat(writer, Y, value.y);
            writer.WriteEndObject();
        }

        public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var x = 0f;
            var y = 0f;

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
                }
            }

            return new Vector2(x, y);
        }

        private static void WriteFloat(JsonWriter writer, string propertyName, float value)
        {
            writer.WritePropertyName(propertyName);
            writer.WriteValue(value);
        }
    }
}
