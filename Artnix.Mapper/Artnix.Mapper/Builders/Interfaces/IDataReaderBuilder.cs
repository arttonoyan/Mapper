using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace Artnix.MapperFramework.Builders.Interfaces
{
    public interface IDataReaderBuilder<TModel>
        where TModel : class, new()
    {
        IDataReaderBuilder<TModel> UseStandardCodeStyleForMembers();
        IDataReaderBuilder<TModel> Property<TProperty>(Expression<Func<TModel, TProperty>> propertyExp, string columnName);

        IPropertyConfigurationBuilder<TPropertyModel, TModel> IfIsNotNull<TPropertyModel>(Expression<Func<TModel, TPropertyModel>> modelProperty) 
            where TPropertyModel : class;
        IDataReaderBuilder<TModel> Ignore(IEnumerable<string> members);
        IDataReaderBuilder<TModel> Ignore(params string[] members);
        IDataReaderBuilder<TModel> Ignore(Expression<Func<TModel, object>> predicate);
    }
}