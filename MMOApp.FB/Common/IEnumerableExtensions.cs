using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace KSS.Patterns.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();

            return source.Where(item => seenKeys.Add(keySelector(item)));
        }

        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        public static T PickRandomOne<T>(this IEnumerable<T> source)
        {
            return PickRandom(source, 1).FirstOrDefault();
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            return new ObservableCollection<T>(source);
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T> source)
        {
            return new Queue<T>(source);
        }

        public static ConcurrentQueue<T> ToConcurrentQueue<T>(this IEnumerable<T> source)
        {
            return new ConcurrentQueue<T>(source);
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        public static List<string> RemoveDuplicated(this IEnumerable<string> collection)
        {
            return collection.Distinct().ToList();
        }

        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        /// <summary>
        /// Convert collection dictionary with safety check key already exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="enumeration"></param>
        /// <param name="getKey"></param>
        /// <param name="getValue"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> ToDictionarySafety<T, TKey, TValue>(this IEnumerable<T> enumeration, Func<T, TKey> getKey, Func<T, TValue> getValue)
        {
            var dic = new Dictionary<TKey, TValue>();
            foreach (var item in enumeration)
            {
                var key = getKey(item);
                var val = getValue(item);

                if (!dic.ContainsKey(key))
                    dic.Add(key, val);
            }

            return dic;
        }
    }
}