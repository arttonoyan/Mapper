using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Artnix.Mapper.Builders
{
    internal class ModelTypeConfigurationBuilder<TModel1, TModel2> : IModelTypeConfigurationBuilder<TModel1, TModel2>
        where TModel1 : class
        where TModel2 : class, new()
    {
        private readonly ModelTypeBuilder _modelTypeBuilder;

        public ModelTypeConfigurationBuilder(ModelTypeBuilder modelTypeBuilder)
        {
            _modelTypeBuilder = modelTypeBuilder;
            _memberBindings = new Dictionary<string, MemberBinding>();
        }

        // The key is model2 member name.
        private readonly Dictionary<string, MemberBinding> _memberBindings;

        public IModelTypeConfigurationBuilder<TModel1, TModel2> Property<TProperty>(Expression<Func<TModel2, TProperty>> model2Property, Expression<Func<TModel1, TProperty>> model1Property)
        {
            MemberExpression memberExp1 = model2Property.Body as MemberExpression;
            if (memberExp1 == null)
                return this;
            _memberBindings.Add(memberExp1.Member.Name, Expression.Bind(memberExp1.Member, model1Property.Body));

            return this;
        }

        internal void Finish()
        {
            _modelTypeBuilder.Finish<TModel1, TModel2>(_memberBindings);
        }
    }
}
