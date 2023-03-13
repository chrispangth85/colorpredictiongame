using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using FreshMVC.Component;

namespace FreshMVC.ThirdParties
{
    public class SmsClient : ISms
    {
        #region public
        public void SendMessage(string receiverPhoneNo, string message, string countryCode, int type)
        {
            string stringUrl = string.Format(urlSMS123, this.apiKey, message, receiverPhoneNo);
            //All send out also require BrandName
            //For sending sms using new sms client, milkysms, we need to append RM0.00 for Malaysia
            //For sending sms using new sms client, milkysms, we need to append ADB for singapore
            if (countryCode.ToUpper() == "MY")
            {
                stringUrl = string.Format(url, this.vendor, this.key, message, receiverPhoneNo, type);
            }
            //Do not use bulksms for china
            //else if (countryCode.ToUpper() == "CN")
            //{
            //    message = string.Format("{0}", message);
            //    stringUrl = string.Format(url, this.vendor, this.key, message, receiverPhoneNo, type);
            //}
            else
            {
                message = string.Format("{0}", message);
            }

            MerchantGeneralDB.InsertSMSFullLog(stringUrl, this.vendor, this.key, receiverPhoneNo, countryCode);

            using (var client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                client.UploadString(stringUrl, string.Empty);
            }
        }

        public void SendMessage(string url)
        {
          using (var client = new WebClient())
          {
            client.Encoding = System.Text.Encoding.UTF8;
            client.UploadString(url, string.Empty);
          }
        }

        #endregion

        #region private

        #region Bulksms2u
        //private readonly string vendor = "ThinkValue2018";
        //private readonly string key = "TV2018";
        private readonly string vendor = "chrispangth99";
        private readonly string key = "triton99";
        #endregion

        #region SMS123
        private readonly string apiKey = "94b345592abc130259d1ae7c74f27c19";
        #endregion

        private string url = "http://www.bulksms2u.com/websmsapi/ISendSMS.aspx?username={0}&password={1}&message={2}&mobile={3}&Sender=68886&type={4}";//bulksms2u - Only MY and CN
        private string urlSMS123 = "https://www.sms123.net/api/send.php?apiKey={0}&messageContent={1}&recipients={2}";//sms123

        #endregion
    }
}
