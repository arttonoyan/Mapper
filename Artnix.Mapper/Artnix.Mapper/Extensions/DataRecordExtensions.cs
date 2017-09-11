using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Artnix.Mapper.Extensions
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

        public static Expression<Func<IDataRecord, TModel>> Map<TModel>(this IDataRecord reader)
            where TModel : class, new()
        {
            HashSet<string> columnNames = GetUpperCaseColumnNames(reader);
            return Map<TModel>(columnNames);
        }

        private static Expression<Func<IDataRecord, TModel>> Map<TModel>(HashSet<string> columnNames)
            where TModel : class, new()
        {
            Type modelType = typeof(TModel);
            IEnumerable<PropertyInfo> properties = modelType
                .GetProperties()
                .Where(pi => columnNames.Contains(pi.Name.ToUpper()) && !(pi.PropertyType.IsClass && pi.PropertyType.Name != typeof(string).Name));

            var parameter = Expression.Parameter(typeof(IDataRecord), "ireader");

            var mi = typeof(IDataRecord).GetProperties()
                .FirstOrDefault(p => p.Name == "Item" && p.GetIndexParameters()[0].ParameterType == typeof(string))
                ?.GetMethod;

            MethodInfo dBNullValueMethodInfo = typeof(Check).GetMethods().Single(p => p.Name == nameof(Check.DBNullValue));
            var memberBindings = new List<MemberBinding>();
            foreach (PropertyInfo member in properties)
            {
                var indexatorExp = Expression.Call(parameter, mi, Expression.Constant(member.Name, typeof(string)));

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
    }
}