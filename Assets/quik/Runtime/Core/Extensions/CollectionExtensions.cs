using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace quik.Runtime.Core.Extensions
{
    public static class CollectionExtensions
    {
        #region Query Helpers
        
        /// <summary>
        /// Returns true if the collection is null or contains no elements.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }

        /// <summary>
        /// Returns true if no elements match the given predicate.
        /// </summary>
        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return !source.Any(predicate);
        }
        
        /// <summary>
        /// Safely gets the item at the specified index. Returns default if out of bounds.
        /// </summary>
        public static T SafeGet<T>(this IList<T> list, int index)
        {
            if (list == null || index < 0 || index >= list.Count)
            {
                return default;
            }
            return list[index];
        }
        
        /// <summary>
        /// Returns the index of the minimum element based on the selector.
        /// </summary>
        public static int IndexOfMin<T>(this IList<T> list, Func<T, float> selector)
        {
            if (list == null || list.Count == 0)
            {
                return -1;
            }

            int index = 0;
            float min = selector(list[0]);

            for (int i = 1; i < list.Count; i++)
            {
                float value = selector(list[i]);
                if (value < min)
                {
                    min = value;
                    index = i;
                }
            }

            return index;
        }
        
        /// <summary>
        /// Returns the index of the maximum element based on the selector.
        /// </summary>
        public static int IndexOfMax<T>(this IList<T> list, Func<T, float> selector)
        {
            if (list == null || list.Count == 0)
            {
                return -1;
            }

            int index = 0;
            float max = selector(list[0]);

            for (int i = 1; i < list.Count; i++)
            {
                float value = selector(list[i]);
                if (value > max)
                {
                    max = value;
                    index = i;
                }
            }

            return index;
        }
        
        #endregion

        #region Randomization

        /// <summary>
        /// Returns a random element from the list.
        /// </summary>
        public static T GetRandom<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new InvalidOperationException("List is null or empty.");
            }
            return list[Random.Range(0, list.Count)];
        }
        
        /// <summary>
        /// Returns a new list with a random subset of elements.
        /// </summary>
        public static List<T> TakeRandom<T>(this IEnumerable<T> source, int count)
        {
            var list = source.ToList();
            list.Shuffle();
            return list.Take(Math.Min(count, list.Count)).ToList();
        }
        
        /// <summary>
        /// Shuffles the elements in the list in-place using Fisher-Yates algorithm.
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
        
        #endregion
        
        #region Modification Helpers
        
        /// <summary>
        /// Removes all null entries from the list.
        /// </summary>
        public static void RemoveNulls<T>(this IList<T> list) where T : class
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] == null)
                {
                    list.RemoveAt(i);
                }
            }
        }
        
        /// <summary>
        /// Swaps the elements at the specified indices.
        /// </summary>
        public static void Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            if (list == null || indexA < 0 || indexB < 0 || indexA >= list.Count || indexB >= list.Count)
            {
                return;
            }
            
            (list[indexA], list[indexB]) = (list[indexB], list[indexA]);
        }
        
        /// <summary>
        /// Adds the item to the collection if it is not already present.
        /// </summary>
        public static void AddIfNotContains<T>(this ICollection<T> collection, T item)
        {
            if (!collection.Contains(item))
            {
                collection.Add(item);
            }
        }
        
        #endregion
        
        #region Iteration Helpers
        
        /// <summary>
        /// Performs the specified action for each item in the collection.
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }
        
        #endregion
        
        #region Conversion & Transformation
        
        /// <summary>
        /// Converts the source collection to a HashSet.
        /// </summary>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }

        /// <summary>
        /// Returns distinct elements from a collection based on a key selector.
        /// </summary>
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        
        #endregion
    }
}
