using System;
using System.Collections.Generic;

namespace KSS.Patterns.Extensions
{
    public static class ICollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection,  IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException("items");

            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static bool IsEmpty<T>( this ICollection<T> collection)
        {
            if (collection == null) throw new ArgumentNullException("collection");

            return collection.Count <= 0;
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return collection == null || collection.Count <= 0;
        }
    }
}