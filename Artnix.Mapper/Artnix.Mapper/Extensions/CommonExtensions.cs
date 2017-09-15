using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Artnix.MapperFramework.Extensions
{
    public static class CommonExtensions
    {
        public static void RemoveAllIfContains<TKey, TValue>(this IDictionary<TKey, TValue> source, IEnumerable<TKey> keys)
        {
            foreach (var key in keys)
            {
                if (source.ContainsKey(key))
                    source.Remove(key);
            }
        }

        public static TProperty IfNotNull<TModel, TProperty>(this TModel model, Func<TModel, TProperty> predicate) where TModel : class
        {
            return model == null ? default(TProperty) : predicate(model);
        }

        public static void AddRange<T>(this HashSet<T> source, IEnumerable<T> items)
        {
            foreach (var item in items)
                source.Add(item);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }
    }
}