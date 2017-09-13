using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Artnix.Mapper.Extensions
{
    public static class MapperExtensions
    {
        public static IEnumerable<TModel2> MapConvert<TModel1, TModel2>(this IEnumerable<TModel1> source)
            where TModel1 : class
            where TModel2 : class, new()
        {
            var Convert = Mapper.Converter<TModel1, TModel2>();
            return source.Select(Convert);
        }

        public static IDictionary<TKey, TModel2> MapConvert<TModel1, TModel2, TKey>(this IDictionary<TKey, TModel1> source)
            where TModel1 : class
            where TModel2 : class, new()
        {
            var Convert = Mapper.Converter<TModel1, TModel2>();
            return source.ToDictionary(p => p.Key, p => Convert(p.Value));
        }

        public static IEnumerable<TModel> MapConvert<TModel>(this IDataReader reader)
            where TModel : class, new()
        {
            var ConvertToModel = Mapper.Converter<TModel>(reader);
            while (reader.Read())
                yield return ConvertToModel(reader);
        }
    }
}