using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Artnix.MapperFramework.Builders
{
    internal class ModelTypeConfigurationBuilder<TModel1, TModel2> : ConfigurationBuilder, IModelTypeConfigurationBuilder<TModel1, TModel2>
        where TModel1 : class
        where TModel2 : class, new()
    {
        public ModelTypeConfigurationBuilder(ModelTypeBuilder modelTypeBuilder)
        {
            _modelTypeBuilder = modelTypeBuilder;
        }

        // The key is model2 member name.
        private readonly ModelTypeBuilder _modelTypeBuilder;

        public IModelTypeConfigurationBuilder<TModel1, TModel2> Property<TProperty>(Expression<Func<TModel2, TProperty>> model2Property, Expression<Func<TModel1, TProperty>> model1Property)
        {
            OnProperty(model2Property, model1Property);
            return this;
        }

        internal void FinishMap()
        {
            _modelTypeBuilder.FinishMap<TModel1, TModel2>(_memberBindings, _ignoreMembers);
        }

        internal void FinishMergeMap()
        {
            _modelTypeBuilder.FinishMergeMap<TModel1, TModel2>(_memberBindings, _ignoreMembers);
        }

        public IModelTypeConfigurationBuilder<TModel1, TModel2> Ignore(params string[] members)
        {
            return Ignore((IEnumerable<string>)members);
        }

        public IModelTypeConfigurationBuilder<TModel1, TModel2> Ignore(IEnumerable<string> members)
        {
            OnIgnore(members);
            return this;
        }

        public IModelTypeConfigurationBuilder<TModel1, TModel2> Ignore(Expression<Func<TModel2, object>> expression)
        {
            OnIgnore(expression);
            return this;
        }

        public IPropertyConfigurationBuilder<TPropertyModel, TModel2> IfIsNotNull<TPropertyModel>(Expression<Func<TModel1, TPropertyModel>> modelProperty) where TPropertyModel : class
        {
            return new PropertyConfigurationBuilder<TPropertyModel, TModel2>(_modelTypeBuilder);
        }
    }
}
