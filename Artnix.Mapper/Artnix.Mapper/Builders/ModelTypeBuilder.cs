using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Artnix.Mapper.Builders.Helpers;

namespace Artnix.Mapper.Builders
{
    public class ModelTypeBuilder<TModel1, TModel2>
        where TModel1 : class, new()
        where TModel2 : class, new()
    {
        private ModelTypeConfigurationBuilder _configBuilder;

        public IModelTypeConfigurationBuilder<TModel1, TModel2> CreateMap()
        {
            _configBuilder = new ModelTypeConfigurationBuilder();
            return _configBuilder;
        }

        internal Expression<Func<TModel1, TModel2>> Finish()
        {
            Dictionary<string, MemberBinding> bindings = _configBuilder.Finish();

            Type model2Type = typeof(TModel2);
            Type model1Type = typeof(TModel1);

            Dictionary<string, PropertyInfo> model1PiDic = model1Type.GetProperties().ToDictionary(pi => pi.Name, pi => pi);
            IEnumerable<PropertyInfo> model2Pis = model2Type.GetProperties().Where(pi => !bindings.ContainsKey(pi.Name) && model1PiDic.ContainsKey(pi.Name));

            var parameter = Expression.Parameter(typeof(TModel1), "model");
            foreach (PropertyInfo member in model2Pis)
            {
                var memberExp = Expression.MakeMemberAccess(parameter, model1PiDic[member.Name]);
                Type model1MemberType = model1PiDic[member.Name].PropertyType;
                if (member.PropertyType == model1MemberType)
                {
                    bindings.Add(member.Name, Expression.Bind(member, memberExp));
                }
                else
                {
                    //For Nullable Types
                    if (Check.TryGetUnderlyingType(model1MemberType, out Type underlyingType))
                    {
                        MethodInfo mi = typeof(Check).GetMethods().FirstOrDefault(p => p.Name == $"To{underlyingType.Name}");
                        if (mi != null)
                        {
                            var changeTypeExp = Expression.Call(mi, memberExp);
                            bindings.Add(member.Name, Expression.Bind(member, changeTypeExp));
                        }
                        else
                        {
                            throw new InvalidCastException($"Can't find the To{underlyingType} method from {typeof(Check).FullName} static class.");
                        }
                    }
                    else
                    {
                        var valueExp = Expression.Convert(memberExp, member.PropertyType);
                        bindings.Add(member.Name, Expression.Bind(member, valueExp));
                    }
                }
            }

            NewExpression model = Expression.New(model2Type);
            MemberInitExpression memberInitExpression = Expression.MemberInit(model, bindings.Values);

            var exprBody = (MemberInitExpression) ExpressionVisitorFactory.AllParametersReplacer(parameter).Visit(memberInitExpression);
            return Expression.Lambda<Func<TModel1, TModel2>>(exprBody, parameter);
        }

        private class ModelTypeConfigurationBuilder : IModelTypeConfigurationBuilder<TModel1, TModel2>
        {
            // The key is model2 member name.
            private readonly Dictionary<string, MemberBinding> _memberBindings;

            public ModelTypeConfigurationBuilder()
            {
                _memberBindings = new Dictionary<string, MemberBinding>();
            }

            public IModelTypeConfigurationBuilder<TModel1, TModel2> Property<TProperty>(Expression<Func<TModel2, TProperty>> model2Property, Expression<Func<TModel1, TProperty>> model1Property)
            {
                MemberExpression memberExp1 = model2Property.Body as MemberExpression;
                if (memberExp1 == null)
                    return this;
                _memberBindings.Add(memberExp1.Member.Name, Expression.Bind(memberExp1.Member, model1Property.Body));

                return this;
            }

            internal Dictionary<string, MemberBinding> Finish()
            {
                return _memberBindings;
            }
        }
    }
}