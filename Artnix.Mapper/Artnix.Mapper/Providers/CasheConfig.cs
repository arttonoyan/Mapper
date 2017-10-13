using System;
using System.Collections.Generic;
using Artnix.MapperFramework.Extensions;

namespace Artnix.MapperFramework.Providers
{
    internal class CasheConfig : IDisposable
    {
        internal IDictionary<string, string> bindings;
        internal HashSet<string> ignoreMembers;
        internal bool useStandardCodeStyleForMembers;

        public void BindingsConfigurations()
        {
            if (!bindings.IsNullOrEmpty() && !ignoreMembers.IsNullOrEmpty())
                bindings.RemoveAllIfContains(ignoreMembers);
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

            bindings = null;
            ignoreMembers = null;
        }
    }
}