﻿using System;
using System.Data;
using Artnix.MapperFramework.Builders;
using Artnix.MapperFramework.Providers;

namespace Artnix.MapperFramework
{
    public static class Mapper
    {
        public static void MapConfiguration(Action<ModelTypeBuilder> buildAction)
        {
            var entityBuilder =  new ModelTypeBuilder();
            buildAction(entityBuilder);
            entityBuilder.ConfigurationFinish();
        }

        public static TModel2 Convert<TModel1, TModel2>(TModel1 model)
            where TModel1 : class
            where TModel2 : class, new()
        {
            Func<TModel1, TModel2> convert = Converter<TModel1, TModel2>();
            return convert(model);
        }

        public static Func<TModel1, TModel2> Converter<TModel1, TModel2>()
            where TModel1 : class
            where TModel2 : class, new()
        {
            Func<TModel1, TModel2> mapper = CasheProvider<TModel1, TModel2>.GetModelToModelMapper();
            if (mapper != null)
                return mapper;

            MapConfiguration(cfg => cfg.CreateMap<TModel1, TModel2>());
            return CasheProvider<TModel1, TModel2>.GetModelToModelMapper();
        }

        public static Func<TModel1, TModel2, TModel2> Mergeer<TModel1, TModel2>()
            where TModel1 : class
            where TModel2 : class, new()
        {
            Func<TModel1, TModel2, TModel2> mergeer = CasheProvider<TModel1, TModel2>.GetModelToModelMergeMapper();
            if (mergeer != null)
                return mergeer;

            MapConfiguration(cfg => cfg.CreateMap<TModel1, TModel2>());
            return CasheProvider<TModel1, TModel2>.GetModelToModelMergeMapper();
        }

        public static Func<IDataRecord, TModel> Converter<TModel>(IDataRecord reader)
            where TModel : class, new()
        {
            return CasheDataReaderProvider<TModel>.GetOrCreateDataReaderToModelMapper(reader);
        }
    }
}