using System;
using System.Collections.Generic;
using System.Text;

namespace Artnix.MapperFramework.Builders
{
    public interface IPropertyConfigurationBuilder<TModel1, TModel2> : IModelTypeConfigurationBuilder<TModel1, TModel2>
        where TModel1 : class
        where TModel2 : class, new()
    {
    }
}