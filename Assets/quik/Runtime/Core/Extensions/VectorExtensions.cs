using UnityEngine;

namespace quik.Runtime.Core.Extensions
{
    public static class VectorExtensions
    {
        #region Vector2 Extensions

        /// <summary>
        /// Converts a Vector2 to a Vector3 with Z = 0.
        /// </summary>
        public static Vector3 ToVector3(this Vector2 v) => new(v.x, v.y, 0f);

        /// <summary>
        /// Converts a Vector2 to a Vector2Int by flooring its components.
        /// </summary>
        public static Vector2Int ToInt(this Vector2 v) => new((int)v.x, (int)v.y);

        /// <summary>
        /// Returns a new Vector2 with a modified X value.
        /// </summary>
        public static Vector2 WithX(this Vector2 v, float newX) => new(newX, v.y);

        /// <summary>
        /// Returns a new Vector2 with a modified Y value.
        /// </summary>
        public static Vector2 WithY(this Vector2 v, float newY) => new(v.x, newY);

        /// <summary>
        /// Adds a value to the X component of a Vector2.
        /// </summary>
        public static Vector2 AddX(this Vector2 v, float deltaX) => new(v.x + deltaX, v.y);

        /// <summary>
        /// Adds a value to the Y component of a Vector2.
        /// </summary>
        public static Vector2 AddY(this Vector2 v, float deltaY) => new(v.x, v.y + deltaY);

        /// <summary>
        /// Returns a Vector2 with the absolute values of each component.
        /// </summary>
        public static Vector2 Abs(this Vector2 v) => new(Mathf.Abs(v.x), Mathf.Abs(v.y));

        /// <summary>
        /// Checks if two Vector2 values are approximately equal within a small epsilon.
        /// </summary>
        public static bool IsCloseTo(this Vector2 v, Vector2 other, float epsilon = 0.0001f)
            => Vector2.Distance(v, other) < epsilon;

        #endregion

        #region Vector3 Extensions

        /// <summary>
        /// Converts a Vector3 to a Vector3Int by flooring its components.
        /// </summary>
        public static Vector3Int ToInt(this Vector3 v) => new((int)v.x, (int)v.y, (int)v.z);

        /// <summary>
        /// Returns a new Vector3 with a modified X value.
        /// </summary>
        public static Vector3 WithX(this Vector3 v, float newX) => new(newX, v.y, v.z);

        /// <summary>
        /// Returns a new Vector3 with a modified Y value.
        /// </summary>
        public static Vector3 WithY(this Vector3 v, float newY) => new(v.x, newY, v.z);

        /// <summary>
        /// Returns a new Vector3 with a modified Z value.
        /// </summary>
        public static Vector3 WithZ(this Vector3 v, float newZ) => new(v.x, v.y, newZ);

        /// <summary>
        /// Adds a value to the X component of a Vector3.
        /// </summary>
        public static Vector3 AddX(this Vector3 v, float deltaX) => new(v.x + deltaX, v.y, v.z);

        /// <summary>
        /// Adds a value to the Y component of a Vector3.
        /// </summary>
        public static Vector3 AddY(this Vector3 v, float deltaY) => new(v.x, v.y + deltaY, v.z);

        /// <summary>
        /// Adds a value to the Z component of a Vector3.
        /// </summary>
        public static Vector3 AddZ(this Vector3 v, float deltaZ) => new(v.x, v.y, v.z + deltaZ);

        /// <summary>
        /// Returns a Vector3 with the absolute values of each component.
        /// </summary>
        public static Vector3 Abs(this Vector3 v) => new(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));

        /// <summary>
        /// Checks if two Vector3 values are approximately equal within a small epsilon.
        /// </summary>
        public static bool IsCloseTo(this Vector3 v, Vector3 other, float epsilon = 0.0001f)
            => Vector3.Distance(v, other) < epsilon;

        #endregion
    }
}
