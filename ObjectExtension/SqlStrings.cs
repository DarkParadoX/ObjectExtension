using System;

using ObjectExtension;

namespace Sql.StringConvert
{

    public class SqlStrings
    {
        /// <summary> String </summary>
        /// <param name="obj"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        static public string AsSqlString(object obj, string nullValue = "null")
        {
            if (obj.IsNull()) return nullValue;
            try
            {
                return "'" + obj + "'";
            }
            catch(Exception e)
            {
                throw new ConvertException(obj, typeof(String), e); 
            }
        }

        /// <summary> Integer </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        static public string AsSqlInteger(int obj) { return obj.ToString(); }
        static public string AsSqlInteger(object obj, string nullValue = "null")
        {
            if (obj.IsNullOrEmpty()) return nullValue;
            return obj.ToInteger().ToString();
        }
        [Obsolete("Параметр не может быть Decimal", true)]
        static public void AsSqlInteger(decimal obj) { }

        /// <summary> DateTime ('dd.MM.yyyy' или null) </summary>
        /// <param name="obj"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static string AsSqlDate(object obj, string nullValue = "null")
        {
            if (obj.IsNullOrEmpty()) return nullValue;
            return "'" + (obj.ToDateTime()).ToString("dd.MM.yyyy") + "'";
        }

        /// <summary> DateTime ('yyyy-MM-dd hh:mm:ss или null') </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string AsSqlDateTimeYearToSsecond(DateTime? obj, bool time = true)
        {
            if (obj.IsNull() || obj == DateTime.MinValue) return "null";
            return "'" + (obj.ToDateTime()).ToString("yyyy-MM-dd " + (time ? "HH:mm:ss" : "00:00:00")) + "'";
        }
        /// <summary> date('dd.MM.yyyy' или null) </summary>
        /// <param name="obj"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static string AsSqlMdy(object obj)
        {
            if (obj.IsNull()) return "null";
            return "mdy(" + (obj.ToDateTime()).ToString("M,d,yyyy") + ")";
        }
        /// <summary> date('dd.MM.yyyy' или null) </summary>
        /// <param name="obj"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static string AsSqlMdy(DateTime obj)
        {
            if (obj == DateTime.MinValue) return "null";
            return "mdy(" + (obj.ToDateTime()).ToString("M,d,yyyy") + ")";
        }

        /// <summary> Float </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string AsSqlFloat(float obj) { return obj.ToString(); }
        public static string AsSqlFloat(object obj)
        {
            if (obj.IsNullOrEmpty()) return "null";
            return obj.ToFloat().ToString();
        }

        /// <summary> Decimal </summary>
        /// <param name="obj"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public static string AsSqlDecimal(object obj, string nullValue = "null")
        {
            if (obj.IsNullOrEmpty()) return nullValue;
            return obj.ToDecimal().ToString();
        }
        public static string AsSqlDecimal(decimal obj) { return obj.ToString(); }
        public static string Current { get { return "current"; } }

        public static string AsSqlText(object obj)
        {
            if (obj.IsNull()) return "null";
            return "CAST(" + AsSqlString(obj) + " AS text)";
        }
    }

}