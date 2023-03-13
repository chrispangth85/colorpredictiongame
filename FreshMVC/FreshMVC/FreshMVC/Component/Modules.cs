using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshMVC.Component
{
    public class Modules
    {
        /// <summary>
        /// Get module name
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        public static string GetModuleName(string moduleCode)
        {
            switch (moduleCode)
            {
                case "GENERAL":
                    return "GENERAL";
                default:
                    return moduleCode;
            }
        }

        /// <summary>
        /// Get sub module name
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        public static string GetSubModuleName(string moduleCode)
        {
            switch (moduleCode)
            {
                // SubModules
                case "ADMINUSER":
                    return "Admin User";
                
                default:
                    return moduleCode;
            }
        }

        /// <summary>
        /// Get submodule method
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        public static string GetSubModuleMethod(string moduleCode)
        {
            if (moduleCode.ToLower().Contains("view"))
            {
                return "View";
            }
            else if (moduleCode.ToLower().Contains("mainmenu"))
            {
                return "Menu";
            }
            else
            {
                return "";
            }
        }
    }
}