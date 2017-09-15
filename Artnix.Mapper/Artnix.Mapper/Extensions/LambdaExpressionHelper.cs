using System;
using System.Linq.Expressions;

namespace Artnix.MapperFramework.Extensions
{
    internal static class LambdaExpressionHelper
    {
        public static string GetParameterName<TEntity, TRelationModel>(this Expression<Func<TEntity, TRelationModel>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
                return memberExpression.Member.Name;

            throw new InvalidCastException();
        }
    }
}
