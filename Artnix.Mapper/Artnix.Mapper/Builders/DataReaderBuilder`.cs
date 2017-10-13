using System;
using System.Collections.Generic;
using Artnix.MapperFramework.Builders.Interfaces;
using System.Linq.Expressions;
using Artnix.MapperFramework.Extensions;

namespace Artnix.MapperFramework.Builders
{
    internal class DataReaderBuilder<TModel> : ConfigurationBuilder, IDataReaderBuilder<TModel>
        where TModel : class, new()
    {
        private readonly ModelTypeBuilder _modelTypeBuilder;
        private bool _useStandardCodeStyleForMembers;

        public DataReaderBuilder(ModelTypeBuilder modelTypeBuilder)
        {
            _modelTypeBuilder = modelTypeBuilder;
        }

        public IDataReaderBuilder<TModel> UseStandardCodeStyleForMembers()
        {
            _useStandardCodeStyleForMembers = true;
            return this;
        }

        public IDataReaderBuilder<TModel> Property<TProperty>(Expression<Func<TModel, TProperty>> propertyExp, string columnName)
        {
            OnProperty(propertyExp, columnName);
            return this;
        }

        public void Finish()
        {
            _modelTypeBuilder.FinishDataReaderMap<TModel>(_memberNameBindings.IsNullOrEmpty()
                ? null
                : _memberNameBindings, _ignoreMembers, _useStandardCodeStyleForMembers);

            Dispose();
        }

        public IPropertyConfigurationBuilder<TPropertyModel, TModel> IfIsNotNull<TPropertyModel>(Expression<Func<TModel, TPropertyModel>> modelProperty) 
            where TPropertyModel : class
        {
            return new PropertyConfigurationBuilder<TPropertyModel, TModel>(_modelTypeBuilder);
        }

        public IDataReaderBuilder<TModel> Ignore(IEnumerable<string> members)
        {
            OnIgnore(members);
            return this;
        }

        public IDataReaderBuilder<TModel> Ignore(params string[] members)
        {
            return Ignore((IEnumerable<string>)members);
        }

        public IDataReaderBuilder<TModel> Ignore(Expression<Func<TModel, object>> predicate)
        {
            OnIgnore(predicate);
            return this;
        }
    }
}