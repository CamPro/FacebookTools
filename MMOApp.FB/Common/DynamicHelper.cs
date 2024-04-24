using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSS.Patterns.Extensions
{
    public static class DynamicHelper
    {
        public static bool HasProperty(dynamic obj, string name)
        {
            Type objType = obj.GetType();

            if (objType == typeof(ExpandoObject))
            {
                return ((IDictionary<string, object>)obj).ContainsKey(name);
            }

            return objType.GetProperty(name) != null;
        }

        public static T TryGet<T>(Func<T> func, T defaultValue)
        {
            try
            {
                return func();
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static string TryGetString<T>(Func<T> func, string defaultVal)
        {
            try
            {
                return func().ToString();
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }


        public static int TryConvertInt32(dynamic obj, int defaultVal)
        {
            try
            {
                return int.Parse(obj.ToString());
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }

        public static DateTime TryConvertDateTime(dynamic obj, DateTime defaultVal)
        {
            try
            {
                return DateTime.Parse(obj.ToString());
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }

        public static bool TryConvertBoolean(dynamic obj, bool defaultVal)
        {
            try
            {
                return bool.Parse(obj.ToString());
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }




        public static int TryConvertInt32<T>(Func<T> func, int defaultVal)
        {
            try
            {
                return int.Parse(func().ToString());
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }

        public static float TryConvertFloat<T>(Func<T> func, int defaultVal)
        {
            try
            {
                return float.Parse(func().ToString());
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }

        public static DateTime TryConvertDateTime<T>(Func<T> func, DateTime defaultVal)
        {
            try
            {
                return DateTime.Parse(func().ToString());
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }

        public static DateTime? TryConvertDateTime<T>(Func<T> func)
        {
            try
            {
                return DateTime.Parse(func().ToString());
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime TryConvertFromUnixDateTime<T>(Func<T> func, DateTime defaultVal)
        {
            try
            {
                return DateTimeExtensions.KFromUnixTime(long.Parse(func().ToString()));
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }

        public static bool TryConvertBoolean<T>(Func<T> func, bool defaultVal)
        {
            try
            {
                return bool.Parse(func().ToString());
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }





        public static bool Has(Action checking)
        {
            try
            {
                checking();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Has<T>(Func<T> checking)
        {
            try
            {
                checking();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }





        public static bool TryGetAndPerform<T>(Func<T> checking, Action<string> action)
        {
            if (Has(checking) && action != null)
            {
                try
                {
                    var value = checking().ToString();
                    action(value);
                    return true;
                }
                catch (Exception) { }
            }

            return false;
        }

        public static bool TryGetInt32AndPerform<T>(Func<T> checking, Action<int> action)
        {
            if (Has(checking) && action != null)
            {
                try
                {
                    var value = int.Parse(checking().ToString());
                    action(value);
                    return true;
                }
                catch (Exception) { }
            }

            return false;
        }

        public static bool TryGetBooleanAndPerform<T>(Func<T> checking, Action<bool> action)
        {
            if (Has(checking) && action != null)
            {
                try
                {
                    var value = bool.Parse(checking().ToString());
                    action(value);
                    return true;
                }
                catch (Exception) { }
            }

            return false;
        }        
    }
}