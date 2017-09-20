using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Artnix.MapperFramework.Extensions
{
    public static class DataRecordExtensions
    {
        public static HashSet<string> GetUpperCaseColumnNames(this IDataRecord reader)
        {
            return GetColumnNames(reader, colName => colName.ToUpper());
        }

        public static HashSet<string> GetColumnNames(this IDataRecord reader, Func<string, string> predicate)
        {
            var items = new HashSet<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string name = reader.GetName(i);
                items.Add(predicate(name));
            }
            return items;
        }

        public static Expression<Func<IDataRecord, TModel>> Map<TModel>(this IDataRecord reader, IReadOnlyDictionary<string, string> bindings, bool useCodingStandartNames)
            where TModel : class, new()
        {
            return Map<TModel>(GetPropertyNames<TModel>(reader, bindings, useCodingStandartNames));
        }

        private static IReadOnlyDictionary<string, string> GetPropertyNames<TModel>(IDataRecord reader, IReadOnlyDictionary<string, string> bindings, bool useCodingStandartNames)
            where TModel : class, new()
        {
            var columnNames = GetUpperCaseColumnNames(reader);
            Dictionary<string, string> columnNamesDic = useCodingStandartNames ?
                columnNames.ToDictionary(p => p.Replace("_", ""), p => p) :
                columnNames.ToDictionary(p => p, p => p);

            var piDic = typeof(TModel).GetProperties().Where(pi => columnNamesDic.ContainsKey(pi.Name.ToUpper())).ToDictionary(pi => pi.Name, pi => columnNamesDic[pi.Name.ToUpper()]);
            if (!bindings.IsNullOrEmpty())
            {
                foreach (var b in bindings)
                    piDic[b.Key] = b.Value;
            }

            return new ReadOnlyDictionary<string, string>(piDic);
        }

        private static Expression<Func<IDataRecord, TModel>> Map<TModel>(IReadOnlyDictionary<string, string> bindings)
            where TModel : class, new()
        {
            Type modelType = typeof(TModel);
            IEnumerable<PropertyInfo> properties = modelType.GetProperties(bindings);

            var parameter = Expression.Parameter(typeof(IDataRecord), "ireader");

            var mi = typeof(IDataRecord).GetProperties()
                .FirstOrDefault(p => p.Name == "Item" && p.GetIndexParameters()[0].ParameterType == typeof(string))
                ?.GetMethod;

            MethodInfo dBNullValueMethodInfo = typeof(Check).GetMethods().Single(p => p.Name == nameof(Check.DBNullValue));
            var memberBindings = new List<MemberBinding>();
            foreach (PropertyInfo member in properties)
            {
                string name = member.Name;
                if (!bindings.IsNullOrEmpty())
                {
                    if (bindings.ContainsKey(member.Name))
                        name = bindings[member.Name];
                }

                var indexatorExp = Expression.Call(parameter, mi, Expression.Constant(name, typeof(string)));
                UnaryExpression valueExp;
                if (member.PropertyType.IsPrimitive)
                {
                    valueExp = Expression.Convert(indexatorExp, member.PropertyType);
                }
                else
                {
                    var nullableExp = Expression.Call(dBNullValueMethodInfo, indexatorExp);
                    valueExp = Expression.Convert(nullableExp, member.PropertyType);
                }
                memberBindings.Add(Expression.Bind(member, valueExp));
            }

            NewExpression model = Expression.New(modelType);
            MemberInitExpression memberInitExpression = Expression.MemberInit(model, memberBindings);
            return Expression.Lambda<Func<IDataRecord, TModel>>(memberInitExpression, parameter);
        }

        private static IEnumerable<PropertyInfo> GetProperties(this Type modelType, IReadOnlyDictionary<string, string> bindings)
        {
            bool IsNotClass(PropertyInfo pi) => !(pi.PropertyType.IsClass && pi.PropertyType.Name != typeof(string).Name);
            return bindings.IsNullOrEmpty() ?
                modelType.GetProperties().Where(IsNotClass) :
                modelType.GetProperties().Where(pi => bindings.ContainsKey(pi.Name) && IsNotClass(pi));
        }
    }
}