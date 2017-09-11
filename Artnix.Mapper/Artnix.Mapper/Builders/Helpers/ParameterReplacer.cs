using System.Linq.Expressions;

namespace Artnix.Mapper.Builders.Helpers
{
    internal class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        protected override Expression VisitParameter(ParameterExpression node)
        {
            //if (node.Name == _parameter.Name)
            //    return base.VisitParameter(_parameter);
            return base.VisitParameter(_parameter);
        }

        internal ParameterReplacer(ParameterExpression parameter)
        {
            _parameter = parameter;
        }
    }
}