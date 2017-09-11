using System.Linq.Expressions;

namespace Artnix.Mapper.Builders.Helpers
{
    public static class ExpressionVisitorFactory
    {
        public static ExpressionVisitor AllParametersReplacer(ParameterExpression parameter)
        {
            return new ParameterReplacer(parameter);
        }
    }
}