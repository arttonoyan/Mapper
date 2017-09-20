using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Artnix.MapperFramework.Extensions
{
    internal static class LambdaExpressionHelper
    {
        public static MemberInfo GetMember<TEntity, TRelationModel>(this Expression<Func<TEntity, TRelationModel>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
                return memberExpression.Member;

            throw new InvalidCastException();
        }

        public static string GetMemberName<TEntity, TRelationModel>(this Expression<Func<TEntity, TRelationModel>> expression)
        {
            return GetMember(expression).Name;
        }
    }
}
