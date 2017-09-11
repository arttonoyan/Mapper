using System;
using System.Data;
using System.Linq.Expressions;
using Artnix.Mapper.Builders;
using Artnix.Mapper.Extensions;

namespace Artnix.Mapper
{
    public static class Mapper
    {
        private static class Cashe<TModel1, TModel2>
            where TModel1 : class, new()
            where TModel2 : class, new()
        {
            private static Func<TModel1, TModel2> _funcModelsMapper;
            private static Func<IDataRecord, TModel1> _funcIDataRecordMapper;

            internal static void SetMapper(Expression<Func<TModel1, TModel2>> mapperExp)
            {
                _funcModelsMapper = mapperExp.Compile();
            }

            internal static void SetMapper(Expression<Func<IDataRecord, TModel1>> mapperExp)
            {
                _funcIDataRecordMapper = mapperExp.Compile();
            }

            public static Func<TModel1, TModel2> GetModelToModelMapper()
            {
                return _funcModelsMapper;
            }

            public static Func<IDataRecord, TModel1> GetIDataRecordToModelMapper()
            {
                return _funcIDataRecordMapper;
            }
        }

        public static void MapConfiguration<TModel1, TModel2>(Action<ModelTypeBuilder<TModel1, TModel2>> buildAction)
            where TModel1 : class, new()
            where TModel2 : class, new()
        {
            var entityBuilder = new ModelTypeBuilder<TModel1, TModel2>();
            buildAction(entityBuilder);
            var mapperExp = entityBuilder.Finish();
            Cashe<TModel1, TModel2>.SetMapper(mapperExp);
        }

        public static TModel2 Convert<TModel1, TModel2>(TModel1 model)
            where TModel1 : class, new()
            where TModel2 : class, new()
        {
            Func<TModel1, TModel2> convert = Converter<TModel1, TModel2>();
            return convert(model);
        }

        public static Func<TModel1, TModel2> Converter<TModel1, TModel2>()
            where TModel1 : class, new()
            where TModel2 : class, new()
        {
            Func<TModel1, TModel2> mapper = Cashe<TModel1, TModel2>.GetModelToModelMapper();
            if (mapper != null)
                return mapper;

            MapConfiguration<TModel1, TModel2>(cfg => cfg.CreateMap());
            return Cashe<TModel1, TModel2>.GetModelToModelMapper();
        }

        public static Func<IDataRecord, TModel> Converter<TModel>(IDataRecord reader)
            where TModel : class, new()
        {
            var converter = Cashe<TModel, TModel>.GetIDataRecordToModelMapper();
            if (converter != null)
                return converter;

            Cashe<TModel, TModel>.SetMapper(reader.Map<TModel>());
            converter = Cashe<TModel, TModel>.GetIDataRecordToModelMapper();
            return converter;
        }
    }
}