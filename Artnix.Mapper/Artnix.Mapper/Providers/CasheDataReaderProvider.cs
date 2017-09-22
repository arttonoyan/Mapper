using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Artnix.MapperFramework.Extensions;

namespace Artnix.MapperFramework.Providers
{
    internal static class CasheDataReaderProvider<TModel>
        where TModel : class, new()
    {
        private static Func<IDataRecord, TModel> _funcIDataRecordMapper;
        private static IReadOnlyDictionary<string, string> _bindings;
        private static bool _useStandardCodeStyleForMembers;

        public static void SetBindings(IReadOnlyDictionary<string, string> bindings)
        {
            _bindings = bindings;
        }

        public static Func<IDataRecord, TModel> GetOrCreateDataReaderToModelMapper(IDataRecord reader)
        {
            if (_funcIDataRecordMapper != null)
                return _funcIDataRecordMapper;

            _funcIDataRecordMapper = reader.Map<TModel>(_bindings, _useStandardCodeStyleForMembers).Compile();
            return _funcIDataRecordMapper;
        }

        internal static void UseStandardCodeStyleForMembers(bool useMode)
        {
            _useStandardCodeStyleForMembers = useMode;
        }
    }
}