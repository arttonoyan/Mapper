using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace Artnix.Mapper.Builders
{
    public interface IModelTypeConfigurationBuilder<TModel1, TModel2>
        where TModel1 : class
        where TModel2 : class, new()
    {
        IModelTypeConfigurationBuilder<TModel1, TModel2> Property<TProperty>(Expression<Func<TModel2, TProperty>> model2Property, Expression<Func<TModel1, TProperty>> model1Property);
        //IModelTypeConfigurationBuilder<TModel1, TModel2> Property<TProperty>(Expression<Func<TModel2, TProperty>> model2Property);
        //IModelTypeConfigurationBuilder<TModel1, TModel2> Map<TProperty>(Expression<Func<TModel1, TProperty>> model1Property, Expression<Func<TModel1, bool>> model1Property);
    }
}