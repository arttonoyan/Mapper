using System.Collections.Generic;
using System.Linq;

namespace Artnix.Mapper.Extensions
{
    public static class MapperEnumerable
    {
        public static IEnumerable<TModel2> MapConvert<TModel1, TModel2>(this IEnumerable<TModel1> source)
            where TModel1 : class, new()
            where TModel2 : class, new()
        {
            return source.Select(Mapper.Convert<TModel1, TModel2>);
        }

        public static IDictionary<TKey, TModel2> MapConvert<TModel1, TModel2, TKey>(this IDictionary<TKey, TModel1> source)
            where TModel1 : class, new()
            where TModel2 : class, new()
        {
            return source.ToDictionary(p => p.Key, p => Mapper.Convert<TModel1, TModel2>(p.Value));
        }
    }
}