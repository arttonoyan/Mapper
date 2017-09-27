using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Artnix.MapperFramework.Extensions;

namespace Artnix.MapperFramework.Builders
{
    internal class ModelTypeConfigurationBuilder<TModel1, TModel2> : IModelTypeConfigurationBuilder<TModel1, TModel2>
        where TModel1 : class
        where TModel2 : class, new()
    {
        public ModelTypeConfigurationBuilder(ModelTypeBuilder modelTypeBuilder)
        {
            _modelTypeBuilder = modelTypeBuilder;
            _memberBindings = new Dictionary<string, MemberBinding>();
            _ignoreMembers = new HashSet<string>();
        }

        // The key is model2 member name.
        private readonly Dictionary<string, MemberBinding> _memberBindings;
        private readonly ModelTypeBuilder _modelTypeBuilder;
        private readonly HashSet<string> _ignoreMembers;

        public IModelTypeConfigurationBuilder<TModel1, TModel2> Property<TProperty>(Expression<Func<TModel2, TProperty>> model2Property, Expression<Func<TModel1, TProperty>> model1Property)
        {
            MemberExpression memberExp1 = model2Property.Body as MemberExpression;
            if (memberExp1 == null)
                return this;
            _memberBindings.Add(memberExp1.Member.Name, Expression.Bind(memberExp1.Member, model1Property.Body));

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
            _ignoreMembers.AddRange(members);
            return this;
        }

        public IModelTypeConfigurationBuilder<TModel1, TModel2> Ignore(Expression<Func<TModel2, object>> expression)
        {
            string name = expression.GetMemberName();
            if (!string.IsNullOrEmpty(name))
                _ignoreMembers.Add(name);
            return this;
        }
    }
}
