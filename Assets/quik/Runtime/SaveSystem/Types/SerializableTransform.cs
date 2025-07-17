using System;
using UnityEngine;

namespace quik.Runtime.SaveSystem.Types
{
    [Serializable]
    public class SerializableTransform
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;

        public SerializableTransform(Transform transform)
        {
            position = transform.position;
            rotation = transform.rotation;
            scale = transform.localScale;
        }
        
        public void ApplyTo(Transform transform)
        {
            transform.position = position;
            transform.rotation = rotation;
            transform.localScale = scale;
        }
    }
}
