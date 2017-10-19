using System;
using System.Collections.Generic;
using System.Data;
using Artnix.MapperFramework.Extensions;

namespace Artnix.MapperFramework.Providers
{
    internal static class CasheDataReaderProvider<TModel>
        where TModel : class, new()
    {
        static CasheDataReaderProvider()
        {
            _casheConfig = new CasheConfig();
        }

        private static Func<IDataRecord, TModel> _funcIDataRecordMapper;
        private static CasheConfig _casheConfig;

        public static void SetBindingsConfiguration(IDictionary<string, string> bindings)
        {
            _casheConfig.bindings = bindings;
        }

        public static void SetBindingsConfiguration(HashSet<string> ignoreMembers)
        {
            _casheConfig.ignoreMembers = ignoreMembers;
        }

        public static Func<IDataRecord, TModel> GetOrCreateDataReaderToModelMapper(IDataRecord reader)
        {
            if (_funcIDataRecordMapper != null)
                return _funcIDataRecordMapper;

            _casheConfig.BindingsConfigurations();
            _funcIDataRecordMapper = reader.Map<TModel>(_casheConfig).Compile();

            _casheConfig.Dispose();

            return _funcIDataRecordMapper;
        }

        internal static void UseStandardCodeStyleForMembers(bool useMode)
        {
            _casheConfig.useStandardCodeStyleForMembers = useMode;
        }
    }
}