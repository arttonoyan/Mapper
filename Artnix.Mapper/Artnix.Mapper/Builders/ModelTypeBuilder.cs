using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Artnix.MapperFramework.Extensions;
using Artnix.MapperFramework.Builders.Helpers;
using Artnix.MapperFramework.Providers;

namespace Artnix.MapperFramework.Builders
{
    public class ModelTypeBuilder
    {
        internal Action ConfigurationFinish;

        public IModelTypeConfigurationBuilder<TModel1, TModel2> CreateMap<TModel1, TModel2>()
            where TModel1 : class
            where TModel2 : class, new()
        {
            var builder = new ModelTypeConfigurationBuilder<TModel1, TModel2>(this);
            ConfigurationFinish += builder.Finish;
            return builder;
        }

        internal void Finish<TModel1, TModel2>(Dictionary<string, MemberBinding> bindings, HashSet<string> ignoreMembers)
            where TModel1 : class
            where TModel2 : class, new()
        {
            Type model2Type = typeof(TModel2);
            Type model1Type = typeof(TModel1);
            
            Dictionary<string, PropertyInfo> model1PiDic = model1Type.GetProperties().ToDictionary(pi => pi.Name, pi => pi);
            IEnumerable<PropertyInfo> model2Pis = model2Type.GetProperties().Where(pi => !bindings.ContainsKey(pi.Name) && !ignoreMembers.Contains(pi.Name) && model1PiDic.ContainsKey(pi.Name));
            bindings.RemoveAllIfContains(ignoreMembers);

            var parameter = Expression.Parameter(model1Type, "model");
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

            var exprBody = ExpressionVisitorFactory.AllParametersReplacer(parameter).Visit(memberInitExpression);
            var lambda = Expression.Lambda<Func<TModel1, TModel2>>(exprBody, parameter);
            CasheProvider<TModel1, TModel2>.SetMapper(lambda);
        }
    }
}