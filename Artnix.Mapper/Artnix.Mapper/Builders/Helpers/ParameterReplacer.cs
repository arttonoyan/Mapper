using System.Collections.Generic;
using System.Linq.Expressions;

namespace Artnix.MapperFramework.Builders.Helpers
{
    internal class ParameterReplacer : ExpressionVisitor
    {
        internal ParameterReplacer(ParameterExpression[] parameters)
        {
            _parametersDic = new Dictionary<string, ParameterExpression>();
            foreach (var parameter in parameters)
                _parametersDic[parameter.Type.Name] = parameter;
        }
        
        private readonly Dictionary<string, ParameterExpression> _parametersDic;

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_parametersDic.TryGetValue(node.Type.Name, out ParameterExpression parameter))
                return base.VisitParameter(parameter);
            return base.VisitParameter(node);

            //return base.VisitParameter(node.Type == _parameter.Type ? _parameter : node);
        }
    }
}