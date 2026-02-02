// using System;
// using System.Linq;
using System.Reflection;
using App.Modules.Sys.Shared.Attributes;
using App.Modules.Sys.Shared.Models.Configuration;



// Extensions are always put in root namespace
// for maximum usability from elsewhere:

namespace App
{
    /// <summary>
    /// Extensions to Types.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Has the given attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool HasAttribute<T>(this Type type) where T : Attribute
        {
            return type.GetTypeInfo().GetCustomAttributes<T>().Any();
        }


        /// <summary>
        /// TODO: Document better
        /// </summary>
        /// <param name="potentialDescendant"></param>
        /// <param name="potentialBase"></param>
        /// <returns></returns>
        public static bool IsSameOrSubclassOf(this Type potentialDescendant, Type potentialBase)
        {
            return potentialDescendant.IsSubclassOf(potentialBase)
                   || potentialDescendant == potentialBase;
        }

        /// <summary>
        /// Get default Value of T.
        /// <para>
        /// Null for objects, 0 for ints, etc.
        /// </para>
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object? GetDefaultValue(this Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }

        /// <summary>
        /// Gets Key associated to 
        /// from <see cref="KeyAttribute"/>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static string? GetKey(this Type type, bool inherit = false)
        {
            // Use aliases first, as they can be richer, if there are any:
            KeyAttribute? aliasAttribute =
                type.GetCustomAttribute<KeyAttribute>(inherit);

            return aliasAttribute?.Key;
        }


        /// <summary>
        /// Gets the Configuration Section Name 
        /// from the Type provided (expecting
        /// a Type that implements 
        /// <see cref="IConfigurationObject"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string? GetConfigurationObjectName(this Type type)
        {
            string? result = null;
            FieldInfo? fi =
                type.GetField("Name", System.Reflection.BindingFlags.Static | BindingFlags.Public);
            if (fi != null)
            {
                result = fi.GetValue(null) as string;
            }
            else
            {
                string? alias = type.GetAlias();
                if (alias == null)
                {
                    result = type.Name;
                }
            }
            return result;
        }

        /// <summary>
        /// Get the first matching custom attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static T? GetCustomAttribute<T>(this Type type, bool inherit = true) where T : Attribute
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            T result = (T)type.GetCustomAttributes(
                typeof(T),
                inherit)
                .FirstOrDefault();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            return result;
        }

        /// <summary>
        /// Gets the Alias of the given Type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string? GetAlias(this Type type)
        {
            return type.GetCustomAttribute<AliasAttribute>()?.DisplayName;
        }


        /// <summary>
        /// Get Descendent/Sub Types 
        /// of this type within given 
        /// Assemblies.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetInstantiableDescendentTypes(
            this Type type,
            IEnumerable<Assembly> assemblies)
        {
            List<Type> results = [];
            foreach (Assembly assembly in assemblies)
            {
                IEnumerable<Type>? resultSet = assembly.GetInstantiableTypesImplementing(type);
                if (resultSet == null)
                {
                    continue;
                }
                results.AddRange(resultSet);
            }
            return results;
        }
    }
}