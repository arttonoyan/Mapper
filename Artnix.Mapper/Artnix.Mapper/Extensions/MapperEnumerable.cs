using System;
using System.Collections.Generic;
using System.Linq;

namespace Artnix.Mapper.Extensions
{
    public static class MapperEnumerable
    {
        public static IEnumerable<TModel2> MapConvert<TModel1, TModel2>(this IEnumerable<TModel1> source)
            where TModel1 : class
            where TModel2 : class, new()
        {
            var Converter = Mapper.Converter<TModel1, TModel2>();
            return source.Select(Converter);
        }

        public static IDictionary<TKey, TModel2> MapConvert<TModel1, TModel2, TKey>(this IDictionary<TKey, TModel1> source)
            where TModel1 : class
            where TModel2 : class, new()
        {
            var Converter = Mapper.Converter<TModel1, TModel2>();
            return source.ToDictionary(p => p.Key, p => Converter(p.Value));
        }

        public static TProperty IfNotNull<TModel, TProperty>(this TModel model, Func<TModel, TProperty> predicate) where TModel : class
        {
            return model == null ? default(TProperty) : predicate(model);
        }
    }
}