using System;
using System.Data;
using System.Linq.Expressions;

namespace Artnix.MapperFramework.Providers
{
    internal static class CasheProvider<TModel1, TModel2>
        where TModel1 : class
        where TModel2 : class, new()
    {
        private static Func<TModel1, TModel2> _funcModelsMapper;
        //private static Func<IDataRecord, TModel1> _funcIDataRecordMapper;

        internal static void SetMapper(Expression<Func<TModel1, TModel2>> mapperExp)
        {
            _funcModelsMapper = mapperExp.Compile();
        }

        //internal static void SetMapper(Expression<Func<IDataRecord, TModel1>> mapperExp)
        //{
        //    _funcIDataRecordMapper = mapperExp.Compile();
        //}

        public static Func<TModel1, TModel2> GetModelToModelMapper()
        {
            return _funcModelsMapper;
        }

        //public static Func<IDataRecord, TModel1> GetIDataRecordToModelMapper()
        //{
        //    return _funcIDataRecordMapper;
        //}
    }
}