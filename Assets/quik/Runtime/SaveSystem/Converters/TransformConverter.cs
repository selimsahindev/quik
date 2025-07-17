using System;
using Newtonsoft.Json;
using quik.Runtime.SaveSystem.Types;
using UnityEngine;

namespace quik.Runtime.SaveSystem.Converters
{
    public class TransformConverter : JsonConverter<Transform>
    {
        public override void WriteJson(JsonWriter writer, Transform value, JsonSerializer serializer)
        {
            var data = new SerializableTransform(value);
            serializer.Serialize(writer, data);
        }

        public override Transform ReadJson(JsonReader reader, Type objectType, Transform existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var data = serializer.Deserialize<SerializableTransform>(reader);

            if (existingValue != null && data != null)
            {
                data.ApplyTo(existingValue);
                return existingValue;
            }
            
            Debug.LogWarning("[TransformConverter] Cannot deserialize into a new Transform instance.");
            return null;
        }
    }
}