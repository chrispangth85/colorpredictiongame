using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshMVC.ThirdParties
{
    public static class CellPhoneParser
    {
        // temporary handle for malaysia phone number first
        public static string Parse(string countryCode, string phoneNumber, string mobileCode = "")
        {
            if (countryCode == "my")
            {
                if (phoneNumber.StartsWith("01"))
                {
                    phoneNumber = "6" + phoneNumber;
                }
                else if (phoneNumber.StartsWith("1"))
                {
                    phoneNumber = "60" + phoneNumber;
                }
            }
            else
            {
                phoneNumber = mobileCode + phoneNumber;
            }

            return phoneNumber;
        }
    }
}