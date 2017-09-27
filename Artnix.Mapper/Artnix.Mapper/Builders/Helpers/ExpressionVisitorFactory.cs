using System.Linq.Expressions;

namespace Artnix.MapperFramework.Builders.Helpers
{
    public static class ExpressionVisitorFactory
    {
        public static ExpressionVisitor AllParametersReplacer(params ParameterExpression[] parameters)
        {
            return new ParameterReplacer(parameters);
        }
    }
}