using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Artnix.MapperFramework.Builders.Interfaces;
using System.Linq.Expressions;
using Artnix.MapperFramework.Extensions;

namespace Artnix.MapperFramework.Builders
{
    internal class DataReaderBuilder<TModel> : IDataReaderBuilder<TModel>
        where TModel : class, new()
    {
        private readonly ModelTypeBuilder _modelTypeBuilder;
        private bool _useCodingStandardNames;

        public DataReaderBuilder(ModelTypeBuilder modelTypeBuilder)
        {
            _modelTypeBuilder = modelTypeBuilder;
            _memberBindings = new Dictionary<string, string>();
        }

        private readonly IDictionary<string, string> _memberBindings;
        public IDataReaderBuilder<TModel> UseCodingStandardNames()
        {
            _useCodingStandardNames = true;
            return this;
        }

        public IDataReaderBuilder<TModel> Property<TProperty>(Expression<Func<TModel, TProperty>> propertyExp, string columnName)
        {
            MemberExpression memberExp1 = propertyExp.Body as MemberExpression;
            if (memberExp1 == null)
                return this;

            _memberBindings[memberExp1.Member.Name] = columnName;
            return this;
        }

        public void Finish()
        {
            _modelTypeBuilder.FinishDataReaderMap<TModel>(_memberBindings.IsNullOrEmpty()
                ? null
                : new ReadOnlyDictionary<string, string>(_memberBindings), _useCodingStandardNames);
        }
    }
}