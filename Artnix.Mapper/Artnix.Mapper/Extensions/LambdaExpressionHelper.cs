using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Artnix.MapperFramework.Extensions
{
    internal static class LambdaExpressionHelper
    {
        public static MemberInfo GetMember(this LambdaExpression expression)
        {
            if (expression.Body is MemberExpression memberExpression)
                return memberExpression.Member;

            throw new InvalidCastException();
        }

        public static string GetMemberName(this LambdaExpression expression)
        {
            return GetMember(expression).Name;
        }
    }
}
