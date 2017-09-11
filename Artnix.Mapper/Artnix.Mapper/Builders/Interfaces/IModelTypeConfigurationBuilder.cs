using System;
using System.Linq.Expressions;

namespace Artnix.Mapper.Builders
{
    public interface IModelTypeConfigurationBuilder<TModel1, TModel2>
        where TModel1 : class, new()
        where TModel2 : class, new()
    {
        IModelTypeConfigurationBuilder<TModel1, TModel2> Property<TProperty>(Expression<Func<TModel2, TProperty>> model2Property, Expression<Func<TModel1, TProperty>> model1Property);
    }
}