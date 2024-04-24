using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using KSS.Patterns.Reflection;

namespace KSS.Patterns.Cloning
{
    public class Cloner
    {
        public static T Clone<T>(T sourceObject)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "sourceObject");
            }

            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(sourceObject, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, sourceObject);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        public static IEnumerable<T> CloneCollection<T>(IEnumerable<T> sourceObjects)
        {
            List<T> objectColl = new List<T>();

            foreach (T obj in sourceObjects)
            {
                objectColl.Add(Clone(obj));
            }

            return objectColl;
        }

        public static void Copy<TSource, TDestination>(TSource source, TDestination destination, bool primitiveTypeOnly = false)
            where TSource : class
            where TDestination : class
        {
            List<PropertyInfo> sourceProperties = ReflectionHelper.GetProperties<TSource>();

            foreach (PropertyInfo property in sourceProperties)
            {
                if (primitiveTypeOnly)
                {
                    var propertyType = property.PropertyType;

                    var isPrimitive = propertyType.IsPrimitive;
                    var isGuid = propertyType == typeof (Guid);
                    var isString = propertyType == typeof (string);
                    var isGeneric = propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>);

                    if (!(isPrimitive || isGuid || isString || isGeneric))
                        continue;
                }

                string name = property.Name;
                ReflectionHelper.SetPropertyValue(destination, name, ReflectionHelper.GetPropertyValue(source, name));
            }
        }

        public static void Copy<TSource, TDestination, TAttribute>(TSource source, TDestination destination, bool primitiveTypeOnly = false)
            where TSource : class
            where TDestination : class
            where TAttribute : Attribute
        {
            List<PropertyInfo> sourceProperties = ReflectionHelper.GetProperties<TSource, TAttribute>();

            foreach (PropertyInfo property in sourceProperties)
            {
                if (primitiveTypeOnly && !property.PropertyType.IsPrimitive)
                    continue;

                string name = property.Name;
                ReflectionHelper.SetPropertyValue(destination, name, ReflectionHelper.GetPropertyValue(source, name));
            }
        }
    }
}