using UnityEngine;

namespace quik.Runtime.Core.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Destroys all children of the GameObject.
        /// </summary>
        public static void DestroyChildren(this GameObject gameObject)
        {
            foreach (Transform child in gameObject.transform)
            {
                Object.Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Adds the specified component to the GameObject if it doesn't already exist.
        /// </summary>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }
            return component;
        }

        /// <summary>
        /// Finds a child GameObject by its name recursively.
        /// </summary>
        public static GameObject FindChildRecursive(this GameObject gameObject, string name)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.name == name)
                {
                    return child.gameObject;
                }

                var result = child.gameObject.FindChildRecursive(name);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// Checks if the GameObject has the specified component.
        /// </summary>
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() != null;
        }
    }
}
