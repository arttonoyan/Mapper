namespace Artnix.MapperFramework.Builders
{
    internal class PropertyConfigurationBuilder<TModel1, TModel2> : ModelTypeConfigurationBuilder<TModel1, TModel2>, IPropertyConfigurationBuilder<TModel1, TModel2>
        where TModel1 : class
        where TModel2 : class, new()
    {
        public PropertyConfigurationBuilder(ModelTypeBuilder modelTypeBuilder)
            : base(modelTypeBuilder)
        { }
    }
}