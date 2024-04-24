using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace KSS.Patterns.Reflection
{
    public static class ReflectionHelper
    {
        public static Type GetPropertyType(object obj, string propertyName)
        {
            Type type = obj.GetType();
            PropertyInfo property = type.GetProperty(propertyName);

            return property != null ? property.PropertyType : null;
        }

        public static object GetPropertyValue(object obj, string propertyName)
        {
            Type type = obj.GetType();
            PropertyInfo property = type.GetProperty(propertyName);
            if (property != null && property.CanRead)
            {
                return property.GetValue(obj, null);
            }

            return null;
        }

        public static T GetPropertyValue<T>(object obj, string propertyName, T defaultValue)
        {
            object value = GetPropertyValue(obj, propertyName);
            if (value != null)
            {
                return (T)value;
            }

            return defaultValue;
        }

        public static void SetPropertyValue(object obj, string propertyName, object value)
        {
            Type type = obj.GetType();
            PropertyInfo property = type.GetProperty(propertyName);
            if (property != null && property.CanWrite)
            {
                try
                {
                    property.SetValue(obj, value, null);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public static bool HasProperty(object obj, string propertyName)
        {
            Type type = obj.GetType();
            PropertyInfo property = type.GetProperty(propertyName);

            return property != null;
        }

        public static List<PropertyInfo> GetProperties<T>()
        {
            return new List<PropertyInfo>(typeof(T).GetProperties());
        }

        public static List<PropertyInfo> GetProperties<T, TAttribute>()
            where TAttribute : Attribute
        {
            return GetProperties<T>().Where(p => Attribute.GetCustomAttribute(p, typeof(TAttribute)) != null).ToList();
        }

        public static T GetPropertyAttribute<T>(PropertyInfo property) where T : Attribute
        {
            object[] attributes = property.GetCustomAttributes(typeof(T), true);
            return attributes.Length > 0 ? (T)attributes[0] : null;
        }


        public static bool ParseType(string typeName, out string className, out string assemblyName)
        {
            var segments = typeName.Split(',');

            className = segments[0];
            assemblyName = segments.Length > 1 ? segments[1] : string.Empty;

            return true;
        }

        public static Type LoadType(string typeName, string asmRef)
        {
            Type ret = null;
            if (!TryGetType(Assembly.GetCallingAssembly(), typeName, ref ret))
                if (!TryGetType(asmRef, typeName, ref ret))
                    if (!TryGetType(Assembly.GetExecutingAssembly(), typeName, ref ret))
                        TryGetType(Assembly.GetEntryAssembly(), typeName, ref ret);
            return ret;
        }

        private static bool TryGetType(string asmRef, string typeName, ref Type type)
        {
            try
            {
                return TryGetType(Assembly.Load(asmRef), typeName, ref type);
            }
            catch { }
            return false;
        }

        private static bool TryGetType(Assembly asm, string typeName, ref Type type)
        {
            if (asm != null)
            {
                try
                {
                    type = asm.GetType(typeName, false, false);
                    return (type != null);
                }
                catch { }
            }
            return false;
        }

        public static T InvokeMethod<T>(Type type, string methodName, params object[] args)
        {
            object o = Activator.CreateInstance(type);
            return InvokeMethod<T>(o, methodName, args);
        }

        public static T InvokeMethod<T>(Type type, object[] instArgs, string methodName, params object[] args)
        {
            object o = Activator.CreateInstance(type, instArgs);
            return InvokeMethod<T>(o, methodName, args);
        }

        public static T InvokeMethod<T>(object obj, string methodName, params object[] args)
        {
            Type[] argTypes = (args == null || args.Length == 0) ? Type.EmptyTypes : Array.ConvertAll<object, Type>(args, delegate(object o) { return o == null ? typeof(object) : o.GetType(); });
            return InvokeMethod<T>(obj, methodName, argTypes, args);
        }

        public static T InvokeMethod<T>(object obj, string methodName, Type[] argTypes, object[] args)
        {
            if (obj != null)
            {
                MethodInfo mi = obj.GetType().GetMethod(methodName, argTypes);
                if (mi != null)
                    return (T)Convert.ChangeType(mi.Invoke(obj, args), typeof(T));
            }
            return default(T);
        }
    }
}