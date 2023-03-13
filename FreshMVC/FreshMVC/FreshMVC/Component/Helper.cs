using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshMVC.Component
{
    public class Helper
    {
        public static string NVL(object value)
        {
            if (IsNull(value))
            {
                return "";
            }
            else
            {
                return value.ToString();
            }
        }

        public static int NVLInteger(object value)
        {
            if (IsNull(value))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(value);
            }
        }

        public static decimal NVLDecimal(object value)
        {
            if (IsNull(value))
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(value);
            }
        }

        public static bool IsNull(object value)
        {
            return value == null || value.ToString().Length == 0;
        }

    }
}