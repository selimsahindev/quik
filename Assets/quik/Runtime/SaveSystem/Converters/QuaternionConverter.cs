using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace quik.Runtime.SaveSystem.Converters
{
    public class QuaternionConverter : JsonConverter<Quaternion>
    {
        private const string X = "x";
        private const string Y = "y";
        private const string Z = "z";
        private const string W = "w";
        
        public override void WriteJson(JsonWriter writer, Quaternion value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            WriteFloat(writer, X, value.x);
            WriteFloat(writer, Y, value.y);
            WriteFloat(writer, Z, value.z);
            WriteFloat(writer, W, value.w);
            writer.WriteEndObject();
        }

        public override Quaternion ReadJson(JsonReader reader, Type objectType, Quaternion existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var x = 0f;
            var y = 0f;
            var z = 0f;
            var w = 0f;

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
                    case W: w = Convert.ToSingle(reader.Value); break;
                }
            }

            return new Quaternion(x, y, z, w);
        }

        private static void WriteFloat(JsonWriter writer, string propertyName, float value)
        {
            writer.WritePropertyName(propertyName);
            writer.WriteValue(value);
        }
    }
}