using System;
using System.Linq.Expressions;

namespace Artnix.MapperFramework.Builders.Interfaces
{
    public interface IDataReaderBuilder<TModel>
        where TModel : class, new()
    {
        IDataReaderBuilder<TModel> UseStandardCodeStyleForMembers();
        IDataReaderBuilder<TModel> Property<TProperty>(Expression<Func<TModel, TProperty>> propertyExp, string columnName);
    }
}