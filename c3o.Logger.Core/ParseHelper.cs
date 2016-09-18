using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace c3o.Core
{
	public static class ParseHelper
	{
		public static DateTime? ParseDate(this string value)
		{
			DateTime date;
			return DateTime.TryParse(value, out date) ? (DateTime?)date : null;
		}

		public static DateTime ParseDateMin(this string value)
		{
			DateTime date;
			return DateTime.TryParse(value, out date) ? (DateTime)date : DateTime.MinValue;
		}
		
		public static int? ParseInt(this string value)
		{
			int i;
			return int.TryParse(value, out i) ? (int?)i : null;
		}

        public static int ParseInt(this string value, int def)
        {
            int i;
            return int.TryParse(value, out i) ? i : def;
        }


		public static decimal? ParseDecimal(this string value)
		{
			decimal i;
			return decimal.TryParse(value, out i) ? (decimal?)i : null;
		}

		public static decimal ParseDecimal(this string value, decimal def)
		{
			decimal i;
			return decimal.TryParse(value, out i) ? (decimal)i : def;
		}


		public static decimal? ParseMoney(this string value)
		{
			decimal i;
			if (!string.IsNullOrWhiteSpace(value))
			{
				value = value.Replace("$", "");
			}
			return decimal.TryParse(value, out i) ? (decimal?)i : null;
		}

		public static decimal ParseMoney(this string value, decimal def)
		{
			decimal i;
			if (!string.IsNullOrWhiteSpace(value))
			{
				value = value.Replace("$", "");
			}
			return decimal.TryParse(value, out i) ? (decimal)i : def;
		}
		
		public static int ParseInt0(this string value)
		{
			int i;
			return int.TryParse(value, out i) ? i : 0;
		}

		public static long? ParseLong(this string value)
		{
			long i;
            return long.TryParse(value, out i) ? (long?)i : null;
		}

        public static long ParseLong(this string value, long def)
        {
            long i;
            return long.TryParse(value, out i) ? i : def;
        }

		public static long ParseLong0(this string value)
		{
			long i;
			return long.TryParse(value, out i) ? i : 0;
		}

		public static bool? ParseBool(this string value)
		{
			bool i;
			return bool.TryParse(value, out i) ? (bool?)i : null;
		}

		public static bool ParseBoolFalse(this string value)
		{
			bool i;
			return bool.TryParse(value, out i) ? i : false;
		}

        public static TEnum? ParseEnum<TEnum>(this string value) where TEnum : struct
        {
            TEnum result;
            if (Enum.TryParse<TEnum>(value, true, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static TEnum ParseEnum<TEnum>(this string value, TEnum def) where TEnum : struct
        {
            TEnum result;
            if (Enum.TryParse<TEnum>(value, true, out result))
            {
                return result;
            }
            else
            {
                return def;
            }
        }

        public static DateTime? ParseDateTime(string val)
        {
            DateTime i;
            return DateTime.TryParse(val, out i) ? (DateTime?)i : null;
        }


        public static decimal ParseDecimal0(string val)
        {
            decimal i;
            return decimal.TryParse(val, out i) ? i : 0;
        }


        public static List<T> ParseList<T>(this string value)
        {
            return value.ParseList<T>(',');
        }

        public static List<T> ParseList<T>(this string value, params char[] separator)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    List<T> list = value.Split(separator).Select(i => (T)converter.ConvertFromString(i)).ToList();
                    return list;
                }
            }
            return new List<T>();
        }
    }
}