using UnityEngine;

namespace quik.Runtime.Core.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Resets the local position, rotation, and scale of the Transform to zero.
        /// </summary>
        public static void ResetLocalTransform(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        /// <summary>
        /// Sets the X component of the Transform's position.
        /// </summary>
        public static void SetX(this Transform transform, float x) => transform.position = transform.position.WithX(x);

        /// <summary>
        /// Sets the Y component of the Transform's position.
        /// </summary>
        public static void SetY(this Transform transform, float y) => transform.position = transform.position.WithY(y);

        /// <summary>
        /// Sets the Z component of the Transform's position.
        /// </summary>
        public static void SetZ(this Transform transform, float z) => transform.position = transform.position.WithZ(z);

        /// <summary>
        /// Moves the Transform towards a target position at a specified speed.
        /// </summary>
        public static void MoveTowardsTarget(this Transform transform, Vector3 target, float speed)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        /// <summary>
        /// Makes the Transform look at a target position, but only on the 2D plane.
        /// </summary>
        public static void LookAt2D(this Transform transform, Vector2 position)
        {
            Vector3 target = new Vector3(position.x, position.y, transform.position.z);
            transform.LookAt(target);
        }

        /// <summary>
        /// Finds a deep child by its name, including all nested children.
        /// </summary>
        public static Transform FindDeepChild(this Transform parent, string name)
        {
            Transform foundChild = parent.Find(name);
            if (foundChild != null)
            {
                return foundChild;
            }

            foreach (Transform child in parent)
            {
                foundChild = child.FindDeepChild(name);
                if (foundChild != null)
                {
                    return foundChild;
                }
            }

            return null;
        }
    }
}
