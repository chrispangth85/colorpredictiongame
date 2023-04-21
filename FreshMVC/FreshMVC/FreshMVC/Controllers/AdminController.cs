using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FreshMVC.Component;
using FreshMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreshMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        #region ChangeLanguageFromLogin
        public ActionResult ChangeLanguageFromLogin(string languageCode)
        {
            HttpContext.Session.SetString("LanguageChosen", languageCode);

            var cultureInfo = new CultureInfo(languageCode);

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            return View("Home", null);
        }
        #endregion

        #region Login
        public IActionResult Login(bool reloadPage = false)
        {
            if (reloadPage)
                ViewBag.ReloadPage = "Y";

            //Remove all session
            HttpContext.Session.Clear();

            ModelState.Clear();

            return View();
        }
        #endregion

        #region ValidateLogin
        [HttpPost]
        public ActionResult ValidateLogin(string userName, string password, bool rememberMeCheck)
        {
            try
            {   
                var ds = AdminDB.GetAdminByUsername(userName, out int ok, out string msg);

                string pass = "";
                string userId = "";
                string firstname = "";
                string userAccessRight = "";

                if (ds.Tables[0].Rows.Count == 0)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { status = false, message = Resources.PackBuddyShared.msgInvalidLogin });
                }
                else
                {
                    userName = ds.Tables[0].Rows[0]["cusr_username"].ToString();
                    pass = ds.Tables[0].Rows[0]["cusr_password"].ToString();

                    if (Authentication.Encrypt(password) != pass)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(new { status = false, message = Resources.PackBuddyShared.msgInvalidPassword });
                    }

                    userId = ds.Tables[0].Rows[0]["cusr_id"].ToString();
                    firstname = ds.Tables[0].Rows[0]["cusr_firstname"].ToString();

                    HttpContext.Session.SetString("UserID", userId);
                    HttpContext.Session.SetString("Admin", userName);
                    HttpContext.Session.SetString("FirstName", firstname);
                    HttpContext.Session.SetString("LanguageChosen", "en-US");
                    HttpContext.Session.SetString("UserType", "admin");

                    var dsUAR = AdminDB.GetUserAccessRight(userName, out ok, out msg);
                    userAccessRight = dsUAR.Tables[0].Rows[0][0].ToString();
                    string[] uar = userAccessRight.Split(new string[] { ", " }, StringSplitOptions.None);
                    foreach (string accessRight in uar)
                    {
                        HttpContext.Session.SetString(accessRight, "true");
                    }
                }

                return Json(new { status = true, message = "Success", adminname = firstname });

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { status = false, message = ex.ToString() });
            }
        }

        #endregion

        #region Home
        public IActionResult Home()
        {
            if (HttpContext.Session.GetString("Admin") == null)
            {
                return RedirectToAction("Login", "Admin", new { reloadPage = true });
            }
            
            var userName = HttpContext.Session.GetString("Admin");
            
            ViewBag.username = userName;
            ViewBag.firstname = HttpContext.Session.GetString("FirstName");

            var am = new BannerModel();
            ViewBag.MyPersonalPage = string.Format("{0}/User/Home?name={1}", Misc._baseUrl, HttpContext.Session.GetString("Admin"));

            TemporaryAddressModel model = new TemporaryAddressModel();

            var dsHomeData = AdminDB.GetAdminHomeData(out _, out _);

            foreach (DataRow dr in dsHomeData.Tables[0].Rows)
            {
                ViewBag.TopUp = decimal.Parse(dr["TotalTopUp"].ToString()).ToString("#,##0.00");
                ViewBag.TotalWithdrawal = decimal.Parse(dr["TotalWDR"].ToString()).ToString("#,##0.00");
                ViewBag.TodayTopUp = decimal.Parse(dr["TodayTopUp"].ToString()).ToString("#,##0.00");
                ViewBag.TodayWithdrawal = decimal.Parse(dr["TodayWDR"].ToString()).ToString("#,##0.00");
                ViewBag.TotalMember = dr["TotalMember"].ToString();
                ViewBag.TotalNewMember = dr["TotalNewMember"].ToString();

                ViewBag.TotalPlatformWalletBalance = decimal.Parse(dr["TotalPlatform"].ToString()).ToString("#,##0.00");
                ViewBag.TotalRedPacket = decimal.Parse(dr["TotalRedPacket"].ToString()).ToString("#,##0.00");
                ViewBag.TotalClaimedWallet = decimal.Parse(dr["TotalClaimed"].ToString()).ToString("#,##0.00");
            }

            return View("Home", model);
        }
        #endregion
    }
}
