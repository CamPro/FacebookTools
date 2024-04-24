using System;
using System.Collections.Generic;

namespace KSS.Patterns.Extensions
{
    public static class IDictionaryExtensions
    {
        public static TKey GetKeyByValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
                if (value.Equals(pair.Value)) 
                    return pair.Key;

            throw new Exception("the value is not found in the dictionary");
        }

        public static bool TryGetKeyByValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value, out TKey key)
        {
            try
            {
                key = GetKeyByValue(dictionary, value);
                return true;
            }
            catch (Exception)
            {
                key = default(TKey);
            }

            return false;
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> createValueDelegate)
        {
            TValue tValue;
            if (!dictionary.TryGetValue(key, out tValue))
            {
                TValue tValue1 = createValueDelegate();
                tValue = tValue1;
                dictionary[key] = tValue1;
            }
            return tValue;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue tValue;
            dictionary.TryGetValue(key, out tValue);
            return tValue;
        }

        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, value);
            else
                dictionary[key] = value;
        }

        public static void AddIfNotContains<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, value);
        }

        public static void RemoveIfContains<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary.ContainsKey(key))
                dictionary.Remove(key);
        }

        public static void RemoveIfContains<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey[] keys)
        {
            foreach (var key in keys)
            {
                RemoveIfContains(dictionary, key);
            }
        }
    }
}