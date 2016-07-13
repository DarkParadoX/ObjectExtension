using System;
using System.Diagnostics;
using System.Linq;

namespace ObjectExtension
{
    public static class ObjectExtension
    {
        //============================================================
        //============================================================
        /// <summary> Проверяет, равен ли объект null или DBNull.Value </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        static public bool IsNull(this object obj)
        {
            return obj == null || obj == DBNull.Value;
        }

        /// <summary> Проверяет, равен ли объект null или DBNull.Value или пустой строке </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        static public bool IsNullOrEmpty(this object obj)
        {
            return obj == null || obj == DBNull.Value || obj.ToString() == "";
        }

        //============================================================
        //============================================================
        /// <summary> Ничего не делает. Возвращает сам объект. Не используйте.</summary>
        /// <param name="obj"></param>
        /// <returns>obj</returns>
        [Obsolete("Объект уже является int типом")]
        public static int ToInteger(this int obj) { return obj; }
        /// <summary> Пытается привести объект к типу Int32.</summary>
        /// <param name="obj"></param>
        /// <returns>Convert.ToInt32(obj)</returns>
        /// <exception cref="ConvertException">Объект не приводится к типу Int32.</exception>
        static public int ToInteger(this object obj)
        {
            try
            {
                return Int32.Parse(obj.ToString());
            }
            catch (Exception e)
            {
                throw new ConvertException(obj, typeof(Int32), e);
            }
        }
        /// <summary> Пытается привести объект к типу Nullable[Int32].</summary>
        /// <param name="obj"></param>
        /// <returns>if (IsNull(obj)) nullValue else Convert.ToInt32(obj)</returns>
        /// <exception cref="ConvertException">Объект не приводится к типу Nullable[Int32].</exception>
        static public int? ToIntegerNullable(this object obj, int? nullValue = null)
        {
            if (IsNull(obj)) return nullValue;
            return ToInteger(obj);
        }


        //============================================================
        //============================================================
        /// <summary> Пытается привести объект к типу DateTime.</summary>
        /// <param name="obj"></param>
        /// <returns>Convert.ToDateTime(obj)</returns>
        /// <exception cref="ConvertException">Объект не приводится к типу DateTime.</exception>
        static public DateTime ToDateTime(this object obj)
        {
            try
            {
                return DateTime.Parse(obj.ToString());
            }
            catch (Exception e)
            {
                throw new ConvertException(obj, typeof(DateTime), e);
            }
        }
        /// <summary> Пытается привести объект к типу Nullable[DateTime].</summary>
        /// <param name="obj"></param>
        /// <returns>if (IsNull(obj) || obj as string == "") nullValue else Convert.ToDateTime(obj)</returns>
        /// <exception cref="ConvertException">Объект не приводится к типу Nullable[DateTime].</exception>
        static public DateTime? ToeDateTimeNullabl(this object obj, DateTime? nullValue = null)
        {
            if (IsNull(obj) || obj as string == "") return nullValue;
            return ToDateTime(obj);
        }
        //============================================================
        //============================================================
        /// <summary> Пытается привести объект к типу Boolean.</summary>
        /// <param name="obj"></param>
        /// <returns>Convert.ToBoolean(obj)</returns>
        /// <exception cref="ConvertException">Объект не приводится к типу Boolean.</exception>
        static public bool ToBoolean(this object obj)
        {
            try
            {
                return Boolean.Parse(obj.ToString());
            }
            catch (Exception e)
            {
                throw new ConvertException(obj, typeof(Boolean), e); 
            }
        }
        
        //============================================================
        //============================================================
        /// <summary> Пытается привести объект к типу Decimal.</summary>
        /// <param name="obj"></param>
        /// <returns>Convert.ToDecimal(obj)</returns>
        /// <exception cref="ConvertException">Объект не приводится к типу Decimal.</exception>
        static public decimal ToDecimal(this object obj)
        {
            try
            {
                return Decimal.Parse(obj.ToString());
            }
            catch (Exception e)
            {
                throw new ConvertException(obj, typeof(Decimal), e); 
            }
        }
        /// <summary> Пытается привести объект к типу Nullable[Decimal].</summary>
        /// <param name="obj"></param>
        /// <returns>if (IsNull(obj)) nullValue else Convert.ToDecimal(obj)</returns>
        /// <exception cref="ConvertException">Объект не приводится к типу Nullable[Decimal].</exception>
        public static decimal? ToDecimalNullable(this object obj, decimal? nullValue = null)
        {
            if (IsNull(obj)) return nullValue;
            return ToDecimal(obj);
        }
        /// <summary> Пытается привести объект к типу Decimal.</summary>
        /// <param name="obj"></param>
        /// <returns>Convert.ToDecimal(obj)</returns>
        /// <exception cref="ConvertException">Объект не приводится к типу Decimal.</exception>
        static public float ToFloat(this object obj)
        {
            try
            {
                return float.Parse(obj.ToString());
            }
            catch (Exception e)
            {
                throw new ConvertException(obj, typeof(float), e); 
            }
        }
        
        //============================================================
        //============================================================
        /// <summary> Пытается привести объект к заданному типу.</summary>
        /// <param name="obj"></param>
        /// <returns>(T)obj</returns>
        /// <exception cref="ConvertException">Объект не приводится к заданному типу.</exception>
        static public T CastTo<T>(this object obj)
        {
            try
            {
                return (T)obj;
            }
            catch (Exception e)
            {
                throw new ConvertException(obj, typeof(T), e);
            }
        }

        /// <summary>
        /// Проверяет, равен ли данный объект хотя бы одному из списка аргументов
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static bool In<T>(this T t, params T[] arguments)
        {
            return arguments.Any(e => e.Equals(t));
        }
    }

    public class ConvertException : Exception
    {
        public object Object;
        public Type Type;
        public new StackTrace StackTrace;

        public ConvertException(object obj, Type type, Exception innerException)
            : base("Не удалось преобразовать объект(" + (obj.IsNull() ? "<null>" : String.IsNullOrWhiteSpace(obj.ToString()) ? "<empty>" : obj) + ") к типу " + type.Name, innerException)
        {
            Object = obj;
            Type = type;
            StackTrace = new StackTrace();
        }
    }
}
