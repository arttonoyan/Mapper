using System;
using System.Diagnostics;

namespace Artnix.Mapper
{
    [DebuggerStepThrough]
    public static class Check
    {
        public static bool TryGetUnderlyingType(Type propType, out Type underlyingType)
        {
            underlyingType = GetUnderlyingType(propType);
            return underlyingType != null;
        }

        public static Type GetUnderlyingType(Type propType)
        {
            return System.Nullable.GetUnderlyingType(propType);
        }

        public static bool Nullable(Type propType)
        {
            return GetUnderlyingType(propType) != null;
        }

        public static object DBNullValue(object obj)
        {
            return obj == DBNull.Value ? null : obj;
        }

        public static long ToInt64(long? obj)
        {
            return obj ?? 0;
        }

        public static int ToInt32(int? obj)
        {
            return obj ?? 0;
        }

        public static short ToInt16(short? obj)
        {
            return obj ?? 0;
        }

        public static byte ToByte(byte? obj)
        {
            return obj ?? 0;
        }

        public static bool ToBoolean(bool? obj)
        {
            return obj ?? false;
        }

        public static DateTime ToDateTime(DateTime? obj)
        {
            return obj ?? default(DateTime);
        }
    }
}