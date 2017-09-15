using System.Linq.Expressions;

namespace Artnix.MapperFramework.Builders.Helpers
{
    internal class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node.Type == _parameter.Type)
                return base.VisitParameter(_parameter);
            return base.VisitParameter(node);
        }

        internal ParameterReplacer(ParameterExpression parameter)
        {
            _parameter = parameter;
        }
    }
}