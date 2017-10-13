using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Artnix.MapperFramework.Extensions;

namespace Artnix.MapperFramework.Builders
{
    internal class ConfigurationBuilder : IDisposable
    {
        public ConfigurationBuilder()
        {
            _memberBindings = new Dictionary<string, MemberBinding>();
            _memberNameBindings = new Dictionary<string, string>();
            _ignoreMembers = new HashSet<string>();
        }

        // The key is model2 member name.
        protected Dictionary<string, MemberBinding> _memberBindings;
        protected Dictionary<string, string> _memberNameBindings;
        protected HashSet<string> _ignoreMembers;

        public void OnProperty(LambdaExpression model2Property, LambdaExpression model1Property)
        {
            MemberExpression memberExp1 = model2Property.Body as MemberExpression;
            if (memberExp1 == null)
                return;

            _memberBindings.Add(memberExp1.Member.Name, Expression.Bind(memberExp1.Member, model1Property.Body));
        }

        protected void OnProperty(LambdaExpression propertyExp, string columnName)
        {
            MemberExpression memberExp1 = propertyExp.Body as MemberExpression;
            if (memberExp1 == null)
                return;

            _memberNameBindings[memberExp1.Member.Name] = columnName;
        }

        protected void OnIgnore(IEnumerable<string> members)
        {
            _ignoreMembers.AddRange(members);
        }

        protected void OnIgnore(LambdaExpression expression)
        {
            string name = expression.GetMemberName();
            if (!string.IsNullOrEmpty(name))
                _ignoreMembers.Add(name);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            _ignoreMembers = null;
            _memberBindings = null;
            _memberNameBindings = null;
        }
    }
}
