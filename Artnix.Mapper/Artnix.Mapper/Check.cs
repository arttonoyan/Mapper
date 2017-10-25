using System;
using System.Diagnostics;

namespace Artnix.MapperFramework
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

        #region ToType

        public static byte ToByte(byte? obj)
        {
            return obj ?? 0;
        }

        public static short ToInt16(short? obj)
        {
            return obj ?? 0;
        }

        public static int ToInt32(int? obj)
        {
            return obj ?? 0;
        }

        public static long ToInt64(long? obj)
        {
            return obj ?? 0;
        }

        public static sbyte ToSByte(sbyte? obj)
        {
            return obj ?? 0;
        }

        public static ushort ToUInt16(ushort? obj)
        {
            return obj ?? 0;
        }

        public static uint ToUInt32(uint? obj)
        {
            return obj ?? 0;
        }

        public static ulong ToUInt64(ulong? obj)
        {
            return obj ?? 0;
        }

        public static short ToSingle(short? obj)
        {
            return obj ?? 0;
        }

        public static double ToDouble(double? obj)
        {
            return obj ?? 0;
        }

        public static decimal ToDecimal(decimal? obj)
        {
            return obj ?? 0;
        }

        public static DateTime ToDateTime(DateTime? obj)
        {
            return obj ?? default(DateTime);
        }

        #endregion

        #region AsType

        public static bool ToBoolean(bool? obj)
        {
            return obj ?? false;
        }

        public static bool AsBoolean(object obj)
        {
            if (obj == null)
                return false;

            return obj.ToString() == "1";
        }

        public static byte AsByte(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return 0;

            return byte.Parse(obj.ToString());
        }

        public static sbyte AsSByte(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return 0;
            
            return sbyte.Parse(obj.ToString());
        }

        public static short AsInt16(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return 0;

            return short.Parse(obj.ToString());
        }

        public static ushort AsUInt16(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return 0;
            
            return ushort.Parse(obj.ToString());
        }

        public static int AsInt32(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return 0;

            return int.Parse(obj.ToString());
        }

        public static uint AsUInt32(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return 0;

            return uint.Parse(obj.ToString());
        }

        public static long AsInt64(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return 0;

            return long.Parse(obj.ToString());
        }

        public static ulong AsUInt64(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return 0;

            return ulong.Parse(obj.ToString());
        }

        public static decimal AsDecimal(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return 0;
            
            return decimal.Parse(obj.ToString());
        }

        public static double AsDouble(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return 0;

            return double.Parse(obj.ToString());
        }

        public static float AsSingle(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return 0;

            return float.Parse(obj.ToString());
        }

        public static DateTime AsDateTime(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(DateTime);

            return DateTime.Parse(obj.ToString());
        }

        #endregion

        #region As Nullable Type

        public static bool? AsNullableBoolean(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;
            
            return obj.ToString() == "1";
        }

        public static byte? AsNullableByte(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;

            return byte.Parse(obj.ToString());
        }

        public static short? AsNullableInt16(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;

            return short.Parse(obj.ToString());
        }

        public static int? AsNullableInt32(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;

            return int.Parse(obj.ToString());
        }

        public static long? AsNullableInt64(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;

            return long.Parse(obj.ToString());
        }

        public static sbyte? AsNullableSByte(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;

            return sbyte.Parse(obj.ToString());
        }

        public static ushort? AsNullableUInt16(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;
            
            return ushort.Parse(obj.ToString());
        }

        public static uint? AsNullableUInt32(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;

            return uint.Parse(obj.ToString());
        }

        public static ulong? AsNullableUInt64(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;

            return ulong.Parse(obj.ToString());
        }

        public static decimal? AsNullableDecimal(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;

            return decimal.Parse(obj.ToString());
        }

        public static double? AsNullableDouble(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;

            return double.Parse(obj.ToString());
        }

        public static float? AsNullableSingle(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;

            return float.Parse(obj.ToString());
        }

        public static DateTime? AsNullableDateTime(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;
            
            return DateTime.Parse(obj.ToString());
        }

        #endregion
    }
}