using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Entity.Context;
using Entity.Context.Models;
using Facebook;
using FreshMVC.Component;
using FreshMVC.Models;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreshMVC.Controllers
{

    public class PaymentController : Controller
    {
        private DbContextOptionsBuilder<DbContext> optionBuilder;
        public PaymentController()
        {
            optionBuilder = new DbContextOptionsBuilder<DbContext>();
            optionBuilder.UseSqlServer(DBConn.connString);
        }
        // GET: /<controller>/
        public IActionResult CallBackUrl()
        {
            // handle all the transaction successfull or failure at callback better
            var memberid = HttpUtility.UrlDecode(Request.Form["memberid"]);
            var orderid = HttpUtility.UrlDecode(Request.Form["orderid"]);
            var amount = HttpUtility.UrlDecode(Request.Form["amount"]);
            var datetime = HttpUtility.UrlDecode(Request.Form["datetime"]);
            var returncode = HttpUtility.UrlDecode(Request.Form["returncode"]);
            var transaction_id = HttpUtility.UrlDecode(Request.Form["transaction_id"]);
            var attach = HttpUtility.UrlDecode(Request.Form["attach"]);
            var sign = HttpUtility.UrlDecode(Request.Form["sign"]);
            var merchantKey = Misc.merchantKey;

            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                var paymentKey = dbContext.CvdParameter.FirstOrDefault(c => c.CparaName == "GatewayPaymentKey");
                if (paymentKey != null)
                {
                    merchantKey = paymentKey.CparaStringvalue;
                }
            }

            var SignTemp = string.Format("amount={0}&datetime={1}&memberid={2}&orderid={3}&returncode={4}&transaction_id={5}&key={6}",
                amount, datetime, memberid, orderid, returncode, transaction_id, merchantKey);

            String md5sign = Misc.MD5(SignTemp).ToUpper();
            if (sign == md5sign)
            {
                // for record
                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var paymentInfo = dbContext.CvdPaymentinfo.FirstOrDefault(c => c.CpaymentTranspaymentid == orderid);
                    paymentInfo.CpaymentTransid = transaction_id;
                    paymentInfo.CpaymentReturnsign = sign;
                    paymentInfo.CpaymentReturncode = returncode;
                    paymentInfo.CpaymentCreatedon = DateTime.Now;

                    dbContext.Update(paymentInfo);
                    dbContext.SaveChanges();

                    var paymentLog = dbContext.CvdCashwalletlogtemp.FirstOrDefault(c => c.CcashAppother == orderid);
                    if (paymentLog != null && paymentLog.CcashStatus.Value == 0)
                    {
                        // deposit to user account
                        AdminDB.CashWalletOperation(paymentLog.CusrUsername, paymentLog.CcashCashin, "Recharge", 0, "", orderid, "1");

                        // update payment log
                        paymentLog.CcashStatus = 1;
                        dbContext.CvdCashwalletlogtemp.Update(paymentLog);
                        dbContext.SaveChanges();
                    }
                }
            }

            var text = "OK";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(text);
            Response.Body.Write(data, 0, data.Length);

            return StatusCode((int)HttpStatusCode.OK);
        }

        public IActionResult ReturnUrl()
        {
            // handle all the transaction successfull or failure at callback better
            var memberid = HttpUtility.UrlDecode(Request.Form["memberid"]);
            var orderid = HttpUtility.UrlDecode(Request.Form["orderid"]);
            var amount = HttpUtility.UrlDecode(Request.Form["amount"]);
            var datetime = HttpUtility.UrlDecode(Request.Form["datetime"]);
            var returncode = HttpUtility.UrlDecode(Request.Form["returncode"]);
            var transaction_id = HttpUtility.UrlDecode(Request.Form["transaction_id"]);            
            var sign = HttpUtility.UrlDecode(Request.Form["sign"]);
            var merchantKey = Misc.merchantKey;

            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                var paymentKey = dbContext.CvdParameter.FirstOrDefault(c => c.CparaName == "GatewayPaymentKey");
                if (paymentKey != null)
                {
                    merchantKey = paymentKey.CparaStringvalue;
                }
            }

            var SignTemp = string.Format("amount={0}&datetime={1}&memberid={2}&orderid={3}&returncode={4}&transaction_id={5}&key={6}",
                amount, datetime, memberid, orderid, returncode, transaction_id, merchantKey);

            String md5sign = Misc.MD5(SignTemp).ToUpper();
            if (sign == md5sign)
            {
                // for record
                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var withdrawalInfo = dbContext.CvdCashwalletlog.FirstOrDefault(c => c.CcashAppother == orderid);
                    withdrawalInfo.CcashStatus = 1;
                    withdrawalInfo.CcashApprovaldate = DateTime.Now;

                    dbContext.Update(withdrawalInfo);
                    dbContext.SaveChanges();
                }
            }

            var text = "OK";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(text);
            Response.Body.Write(data, 0, data.Length);

            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}
