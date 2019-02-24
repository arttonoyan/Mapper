using Artnix.MapperFramework.Builders.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Artnix.MapperFramework.Providers
{
    internal static class BindingsCasheProvider
    {
        static BindingsCasheProvider()
        {
            bindings = new Dictionary<string, Dictionary<string, MemberAssignment>>();
        }

        private static Dictionary<string, Dictionary<string, MemberAssignment>> bindings;

        public static void Cashe<TEntity, TDto>(Dictionary<string, MemberBinding> bindingsDic)
            where TEntity : class
            where TDto : class, new()
        {
            string key = Key<TDto, TEntity>();
            bindings[key] = bindingsDic.ToDictionary(p => p.Key, p => (MemberAssignment)p.Value);
        }

        private static string Key<To, From>()
        {
            return $"{typeof(To).Name}_{typeof(From).Name}";
        }

        public static Expression<Func<TEntity, bool>> ConvertPredicate<TDto, TEntity>(Expression<Func<TDto, bool>> predicate)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e`");
            var mbindings = bindings[Key<TDto, TEntity>()];
            var bodyExp = new ReplaceExpressionVisitor(parameter, mbindings).Visit(predicate.Body);
            return Expression.Lambda<Func<TEntity, bool>>(bodyExp, parameter);
        }
        private class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _newParamExp;
            private readonly Dictionary<string, MemberAssignment> _bindings;

            public ReplaceExpressionVisitor(ParameterExpression newValue, Dictionary<string, MemberAssignment> bindings)
            {
                _newParamExp = newValue;
                _bindings = bindings;
            }

            public override Expression Visit(Expression node)
            {
                if (node.NodeType == ExpressionType.MemberAccess)
                {
                    var memberExp = (MemberExpression)node;

                    MemberAssignment member;
                    if (_bindings.TryGetValue(memberExp.Member.Name, out member))
                    {
                        var newMemberExp = ExpressionVisitorFactory.AllParametersReplacer(_newParamExp).Visit(member.Expression);
                        return base.Visit(newMemberExp);
                    }
                }
                return base.Visit(node);
            }
        }
    }
}
