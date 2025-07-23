using UnityEngine;

namespace quik.Runtime.Core.Extensions
{
    public static class MathExtensions
    {
        #region Comparison

        /// <summary>
        /// Checks if two floats are approximately equal within a small tolerance.
        /// </summary>
        public static bool ApproximatelyEquals(this float a, float b, float tolerance = 0.0001f)
        {
            return Mathf.Abs(a - b) < tolerance;
        }

        /// <summary>
        /// Checks if a float is approximately zero.
        /// </summary>
        public static bool IsZero(this float value, float tolerance = 0.0001f)
        {
            return Mathf.Abs(value) < tolerance;
        }

        /// <summary>
        /// Checks if a float is NaN (Not a Number).
        /// </summary>
        public static bool IsNaN(this float value)
        {
            return float.IsNaN(value);
        }

        #endregion

        #region Range & Clamping

        /// <summary>
        /// Remaps a value from one range to another.
        /// </summary>
        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        /// <summary>
        /// Clamps a float to a minimum value.
        /// </summary>
        public static float ClampMin(this float value, float min)
        {
            return Mathf.Max(value, min);
        }

        /// <summary>
        /// Clamps a float to a maximum value.
        /// </summary>
        public static float ClampMax(this float value, float max)
        {
            return Mathf.Min(value, max);
        }

        /// <summary>
        /// Checks if a value is between a minimum and maximum value (inclusive).
        /// </summary>
        public static bool IsBetween(this float value, float min, float max)
        {
            return value >= min && value <= max;
        }

        #endregion

        #region Sign & Snapping

        /// <summary>
        /// Returns -1 if value is less than 0, 1 if greater than 0, and 0 if value is 0.
        /// </summary>
        public static int Sign(this float value)
        {
            return value == 0f ? 0 : Mathf.Sign(value) > 0 ? 1 : -1;
        }

        /// <summary>
        /// Snaps a float value to the nearest multiple of the given increment.
        /// </summary>
        public static float Snap(this float value, float snapIncrement)
        {
            if (snapIncrement == 0)
            {
                return value;
            }
            return Mathf.Round(value / snapIncrement) * snapIncrement;
        }

        /// <summary>
        /// Snaps a Vector2 value to the nearest multiple of the given increment.
        /// </summary>
        public static Vector2 Snap(this Vector2 value, float snapIncrement)
        {
            return new Vector2(
                value.x.Snap(snapIncrement),
                value.y.Snap(snapIncrement)
            );
        }

        #endregion
    }
}
