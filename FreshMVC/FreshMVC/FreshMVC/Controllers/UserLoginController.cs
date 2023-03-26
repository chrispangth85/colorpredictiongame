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
using Entity.Context;
using Entity.Context.Models;
using Facebook;
using FreshMVC.Component;
using FreshMVC.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreshMVC.Controllers
{

    public class UserLoginController : Controller
    {
        private DbContextOptionsBuilder<DbContext> optionBuilder;
        public UserLoginController()
        {  
            optionBuilder = new DbContextOptionsBuilder<DbContext>();
            optionBuilder.UseSqlServer(DBConn.connString);
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        #region ClientLogin
        public ActionResult ClientLogin()
        {
            try
            {
                ClientModel pm = new ClientModel();
                ModelState.Clear();

                return View("ClientLogin", pm);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = e.ToString()
                });
            }
        }
        #endregion

        #region ValidateLogin
        [HttpPost]
        public ActionResult ValidateLogin(string userName, string countrycode, string password)
        {
            try
            {
                string pass = "";
                string userId = "";
                string firstname = "";
                string email = "";

                //help user to remove the initial 0
                userName = Misc.MassagePhoneNumber(userName, countrycode);
                userName = string.Format("{0}{1}", countrycode, userName);

                var ds = AdminDB.GetUserByUsername(userName, out int ok, out string msg);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { status = false, message = Resources.PackBuddyShared.msgInvalidLogin });
                }

                userName = ds.Tables[0].Rows[0]["CUSR_USERNAME"].ToString();
                pass = ds.Tables[0].Rows[0]["CUSR_PASSWORD"].ToString();

                if (Authentication.Encrypt(password) != pass)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { status = false, message = Resources.PackBuddyShared.msgInvalidPassword });
                }

                userId = ds.Tables[0].Rows[0]["CUSR_ID"].ToString();
                firstname = ds.Tables[0].Rows[0]["CUSR_FIRSTNAME"].ToString();
                email = ds.Tables[0].Rows[0]["CUSR_EMAIL"].ToString();

                var encryptedUsername = Authentication.Encrypt(userName);

                Response.Cookies.Append("UserIDCookie", encryptedUsername, new CookieOptions() { Expires = DateTime.Now.AddYears(1), HttpOnly = false, IsEssential = true });


                if (ds.Tables[0].Rows.Count == 0)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { status = false, message = Resources.PackBuddyShared.msgInvalidLogin });
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

        #region SignUp
        public ActionResult SignUp(string referral = "")
        {
            try
            {
                ClientModel pm = new ClientModel();
                ModelState.Clear();

                pm.IntroCode = referral;
                return View("SignUp", pm);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = e.ToString()
                });
            }
        }

        public ActionResult GetOTPMethod(string phone, string countrycode)
        {
            try
            {
                //help user to remove the initial 0
                phone = Misc.MassagePhoneNumber(phone, countrycode);

                phone = string.Format("{0}{1}", countrycode, phone);

                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var foundUser = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == phone);
                    if (foundUser != null)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(new
                        {
                            status = false,
                            message = Resources.PackBuddyShared.msgUserExisted
                        });
                    }
                }

                ClientModel pm = new ClientModel();
                pm.Username = phone;
                pm.CountryCode = countrycode;
                pm.OTP = Misc.GenerateRandomDigit(6);

                ModelState.Clear();

                //malaysia phone is max 10 digit min 9 digit for mobile : 16-937-2328 vs 11-5566-7878
                if (phone.Length != 9 && phone.Length != 10)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.msgInvalidPhoneNumber
                    });
                }

                string phoneNo = string.Format("{0}{1}", countrycode, phone);

                string msg = string.Format("[H2INIT] ReceiptTracker verification code is {0} and valid for 3 mins.", pm.OTP);

                Misc.SendSMS("MY", phoneNo, msg, "en-US");

                HttpContext.Session.SetObject("RegistrationUserObject", pm);

                return Json(new { status = true, message = "Success" });
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = e.ToString()
                });
            }
        }

        public ActionResult VerifyOTP()
        {
            try
            {
                ClientModel pm = HttpContext.Session.GetObjectClientCookie("RegistrationUserObject");

                ModelState.Clear();

                return View("VerifyOTP", pm);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = e.ToString()
                });
            }
        }

        public ActionResult SignUpMethod(string phone, string countrycode, string password, string confirmpassword, string introcode)
        {
            try
            {
                phone = Misc.MassagePhoneNumber(phone, countrycode);

                if(countrycode == "91" && phone.Length != 10)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.msgPhoneNumberIndiaMustBe10Digit
                    });
                }

                phone = string.Format("{0}{1}", countrycode, phone);

                string intro = "";

                try
                {
                    intro = introcode;
                }
                catch(Exception e)
                {

                }

                if(intro == "")
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.msgInvalidReferral
                    });
                }
                
                ClientModel pm = HttpContext.Session.GetObjectClientCookie("RegistrationUserObject");

                if (password != confirmpassword)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.msgPasswordNotMatch
                    });
                }

                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var foundUser = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == phone);
                    if (foundUser != null)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(new
                        {
                            status = false,
                            message = Resources.PackBuddyShared.msgUserExisted
                        });
                    }

                    var encryptedPassword = Authentication.Encrypt(password);

                    #region countryID
                    int countryID = 1;//Malaysia default
                    if (countrycode == "91")
                    {
                        countryID = 15;
                    }
                    #endregion

                    CvdUser usr = new CvdUser();
                    usr.CusrUsername = phone;
                    usr.CusrPassword = encryptedPassword;
                    usr.CusrPin = encryptedPassword;
                    usr.CcountryId = countryID;
                    usr.CusrReferralid = intro;
                    usr.CroleId = 2;
                    usr.CusrFirstname = "";
                    usr.CusrCreatedby = "SYS";
                    usr.CusrUpdatedby = "SYS";
                    usr.CusrCreatedon = DateTime.Now;
                    usr.CusrUpdatedon = DateTime.Now;
                    usr.CusrDeletionstate = false;

                    dbContext.CvdUser.Add(usr);
                    dbContext.SaveChanges();

                    //update the level 2-5
                    AdminDB.UpdateUserLevel2To5Data(phone);
                    AdminDB.InsertPendingJob(usr.CusrUsername, "UpdateTotalSponsor", 0, "", "");

                    Response.Cookies.Append("UserIDCookie", usr.CusrUsername, new CookieOptions() { Expires = DateTime.Now.AddYears(1), HttpOnly = false, IsEssential = true });
                }

                return Json(new { status = true, message = "Success" });
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = e.ToString()
                });
            }
        }

        //ori code with OTP page and call this method
        //public ActionResult SignUpMethod(string phone, string password, string verificationCode)
        //{
        //    try
        //    {
        //        phone = Misc.MassagePhoneNumber(phone);
        //        ClientModel pm = HttpContext.Session.GetObjectClientCookie("RegistrationUserObject");

        //        if (verificationCode != pm.OTP)
        //        {
        //            Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //            return Json(new
        //            {
        //                status = false,
        //                message = Resources.PackBuddyShared.msgOTPNotMatch
        //            });
        //        }

        //        using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
        //        {
        //            var foundUser = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == phone);
        //            if (foundUser != null)
        //            {
        //                Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //                return Json(new
        //                {
        //                    status = false,
        //                    message = Resources.PackBuddyShared.msgUserExisted
        //                });
        //            }

        //            var encryptedPassword = Authentication.Encrypt(password);

        //            CvdUser usr = new CvdUser();
        //            usr.CusrUsername = phone;
        //            usr.CusrPassword = encryptedPassword;
        //            usr.CusrPin = encryptedPassword;
        //            usr.CcountryId = 1;
        //            usr.CroleId = 2;
        //            usr.CusrFirstname = "";
        //            usr.CusrCreatedby = "SYS";
        //            usr.CusrUpdatedby = "SYS";
        //            usr.CusrCreatedon = DateTime.Now;
        //            usr.CusrUpdatedon = DateTime.Now;
        //            usr.CusrDeletionstate = false;

        //            dbContext.CvdUser.Add(usr);
        //            dbContext.SaveChanges();

        //            Response.Cookies.Append("UserIDCookie", usr.CusrUsername, new CookieOptions() { Expires = DateTime.Now.AddYears(1), HttpOnly = false, IsEssential = true });
        //        }

        //        return Json(new { status = true, message = "Success" });
        //    }
        //    catch (Exception e)
        //    {
        //        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //        return Json(new
        //        {
        //            status = false,
        //            message = e.ToString()
        //        });
        //    }
        //}

        public ActionResult ResendOTP()
        {
            try
            {
                ClientModel pm = HttpContext.Session.GetObjectClientCookie("RegistrationUserObject");

                return GetOTPMethod(pm.Username, pm.CountryCode);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = e.ToString()
                });
            }
        }

        public ActionResult Logout()
        {
            try
            {
                Response.Cookies.Delete("UserIDCookie");

                return Json(new { status = true, message = "Success" });
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = e.ToString()
                });
            }
        }
        #endregion

        #region ForgetPassword
        public ActionResult ForgetPassword()
        {
            try
            {
                ClientModel pm = new ClientModel();
                ModelState.Clear();

                return View("ForgetPassword", pm);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = e.ToString()
                });
            }
        }

        public ActionResult GetOTPForgetPasswordMethod(string phone, string countrycode)
        {
            try
            {
                phone = Misc.MassagePhoneNumber(phone, countrycode);

                phone = string.Format("{0}{1}", countrycode, phone);
                //help user to remove the initial 0
                //phone = phone.TrimStart('0');

                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var foundUser = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == phone);
                    if (foundUser == null)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(new
                        {
                            status = false,
                            message = Resources.PackBuddyShared.msgPhoneIsNotRegistered
                        });
                    }
                }

                ClientModel pm = new ClientModel();
                pm.Username = phone;
                pm.CountryCode = countrycode;
                pm.OTP = Misc.GenerateRandomDigit(6);

                ModelState.Clear();

                string phoneNo = string.Format("{0}{1}", countrycode, phone);

                string msg = string.Format("[H2INIT] ReceiptTracker verification code is {0} and valid for 3 mins.", pm.OTP);

                Misc.SendSMS("MY", phoneNo, msg, "en-US");

                HttpContext.Session.SetObject("ForgetPasswordObject", pm);

                return Json(new { status = true, message = "Success" });
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = e.ToString()
                });
            }
        }

        public ActionResult VerifyForgetPasswordOTP()
        {
            try
            {
                ClientModel pm = HttpContext.Session.GetObjectClientCookie("ForgetPasswordObject");

                ModelState.Clear();

                return View("VerifyForgetPasswordOTP", pm);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = e.ToString()
                });
            }
        }

        public ActionResult UpdatePasswordMethod(string phone, string password, string verificationCode)
        {
            try
            {
                ClientModel pm = HttpContext.Session.GetObjectClientCookie("ForgetPasswordObject");

                if (verificationCode != pm.OTP)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = "OTP Not Match"
                    });
                }

                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var foundUser = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == pm.Username);
                    if (foundUser != null)
                    {
                        var encryptedPassword = Authentication.Encrypt(password);
                        foundUser.CusrPassword = encryptedPassword;
                        dbContext.SaveChanges();
                    }
                }

                return Json(new { status = true, message = "Success" });
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = e.ToString()
                });
            }
        }

        public ActionResult ResendForgetPasswordOTP()
        {
            try
            {
                ClientModel pm = HttpContext.Session.GetObjectClientCookie("ForgetPasswordObject");

                return GetOTPForgetPasswordMethod(pm.Username, pm.CountryCode);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = e.ToString()
                });
            }
        }
        #endregion

        #region EditProfile
        public ActionResult EditProfile()
        {
            try
            {
                string usernameCookie = HttpContext.Request.Cookies["UserIDCookie"];

                ClientModel pm = new ClientModel();

                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var foundUser = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == usernameCookie);
                    if (foundUser != null)
                    {
                        pm.Email = foundUser.CusrEmail;
                        pm.Name = foundUser.CusrFirstname;
                    }
                }

                ModelState.Clear();

                return View("EditProfile", pm);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = e.ToString()
                });
            }
        }

        public ActionResult EditProfileMethod(string name, string email)
        {
            try
            {
                string usernameCookie = HttpContext.Request.Cookies["UserIDCookie"];

                ClientModel pm = new ClientModel();

                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var foundUser = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == usernameCookie);
                    if (foundUser != null)
                    {
                        foundUser.CusrFirstname = name;
                        foundUser.CusrEmail = email;

                        HttpContext.Session.SetString("FirstName", name);
                        HttpContext.Session.SetString("Email", email);

                        dbContext.SaveChanges();
                    }
                }

                ModelState.Clear();

                return Json(new { status = true, message = "Success" });
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = e.ToString()
                });
            }
        }
        #endregion

    }
}
