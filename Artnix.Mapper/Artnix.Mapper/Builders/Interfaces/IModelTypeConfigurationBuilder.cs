using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Artnix.MapperFramework.Builders
{
    public interface IModelTypeConfigurationBuilder<TModel1, TModel2>
        where TModel1 : class
        where TModel2 : class, new()
    {
        IModelTypeConfigurationBuilder<TModel1, TModel2> Property<TProperty>(Expression<Func<TModel2, TProperty>> model2Property, Expression<Func<TModel1, TProperty>> model1Property);
        IModelTypeConfigurationBuilder<TModel1, TModel2> Ignore(IEnumerable<string> members);
        IModelTypeConfigurationBuilder<TModel1, TModel2> Ignore(params string[] members);
        IModelTypeConfigurationBuilder<TModel1, TModel2> Ignore(Expression<Func<TModel2, object>> predicate);
    }
}