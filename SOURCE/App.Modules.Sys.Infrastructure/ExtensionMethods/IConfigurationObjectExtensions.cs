// using App.Modules.Sys.Substrate.Models.Configuration;
// using App.Modules.Sys.Shared._TOPARSE.Models.Configuration;
// using JasperFx.Core.Reflection;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Reflection;
// using System.Text;
// using System.Threading.Tasks;

//namespace App
//{
//    /// <summary>
//    /// Extension methods of 
//    /// configuration objects that implement
//    /// <see cref="IConfigurationObject"/>
//    /// </summary>
//    public static class IConfigurationObjectExtensions
//    {
//        /// <summary>
//        /// Return the IConfigurationObject's 
//        /// name -- and therefore name under which it
//        /// is in the app.config file.
//        /// </summary>
//        /// <param name="configurationObject"></param>
//        /// <returns></returns>
//        public static string GetConfigurationSectionName(this IConfigurationObject configurationObject)
//        {
//            Type type = configurationObject.GetType();

//            return type.GetName();

//        }
//    }
//}
