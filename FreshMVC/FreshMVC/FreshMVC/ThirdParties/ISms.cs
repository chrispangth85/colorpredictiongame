using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreshMVC.ThirdParties
{
    public interface ISms
    {
        void SendMessage(string receiver, string message, string countryCode, int type);
        void SendMessage(string url);
    }
}
