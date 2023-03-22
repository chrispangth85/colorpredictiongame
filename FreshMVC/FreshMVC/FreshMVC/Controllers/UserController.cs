using Entity.Context;
using FreshMVC.Component;
using FreshMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Newtonsoft.Json;
using Telegram.Bot;
using Stripe.Checkout;
using Stripe;
using Entity.Context.Models;
using QRCoder;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
using RestSharp;
using System.Text;
using System.Web;

namespace FreshMVC.Controllers
{
    public class UserController : Controller
    {
        private DbContextOptionsBuilder<DbContext> optionBuilder;
        public UserController()
        {
            optionBuilder = new DbContextOptionsBuilder<DbContext>();
            optionBuilder.UseSqlServer(DBConn.connString);
        }

        #region HeaderMenu
        public IActionResult HeaderMenu()
        {
            return PartialView("HeaderMenu");
        }
        #endregion

        #region FooterMenu
        public IActionResult FooterMenu()
        {
            return PartialView("FooterMenu");
        }
        #endregion

        #region Home
        public ActionResult Home(string search)
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new MemberHomeModel();
            am.Username = usernameCookie;

            if (am.Username != "" && am.Username != null)
            {
                am.IsUserLoggedIn = true;

                var dsHomeData = AdminGeneralDB.GetHomeData(usernameCookie, out _, out _);

                foreach (DataRow dr in dsHomeData.Tables[0].Rows)
                {
                    am.ThisYearReceiptCount = dr["CurrentYearCount"].ToString();
                }
            }

            //Get Product Listing
            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                if (!string.IsNullOrEmpty(search))
                {
                    var productListModel = getProductListSearch(search);
                    return View("ProductListing", productListModel);
                }
                else
                {


                    var productList = dbContext.CvdProduct.Where(p => p.CproDeletionstate == false).ToList();
                    int index = 0;
                    int maxTopProduct = 5;
                    int maxActiveProduct = 6;
                    foreach (var product in productList)
                    {
                        if (index < maxTopProduct)
                        {
                            ProductModel productModel = new ProductModel();
                            productModel.id = product.CproId;
                            productModel.Title = product.CproTitle;
                            productModel.Desc = product.CproDesc;
                            productModel.AdditionalDesc = product.CproDescAdd;
                            productModel.Price = product.CproPrice;
                            productModel.ImagePath = "Uploads/Products/" + product.CproTitle.Replace(" ", "") + "/" + product.CproImages;
                            am.TopProductList.Add(productModel);
                        }
                        else
                        {
                            if (index < (maxTopProduct + maxActiveProduct))
                            {
                                ProductModel productModel = new ProductModel();
                                productModel.id = product.CproId;
                                productModel.Title = product.CproTitle;
                                productModel.Desc = product.CproDesc;
                                productModel.AdditionalDesc = product.CproDescAdd;
                                productModel.Price = product.CproPrice;
                                productModel.ImagePath = "Uploads/Products/" + product.CproTitle.Replace(" ", "") + "/" + product.CproImages;
                                productModel.Other = getRandomProductStatus();
                                am.ActiveProductList.Add(productModel);
                            }
                        }
                        index++;
                    }

                    var bannerList = dbContext.CvdBanner.Where(p => p.CbannerDeletionstate == false).ToList();
                    foreach (var banner in bannerList)
                    {
                        BannerModel bannerModel = new BannerModel();
                        bannerModel.ProtraitPhotoPath = "Uploads/Banners/" + banner.CbannerImages;
                        am.BannerList.Add(bannerModel);
                    }
                }
            }

            ModelState.Clear();
            ViewBag.HeaderPage = "Home";

            return View("Home", am);
        }
        #endregion

        #region DoNothing
        public ActionResult DoNothing()
        {
            return View("DoNothing");
        }
        #endregion

        #region AboutUs
        public ActionResult AboutUs()
        {
            try
            {
                return View("AboutUs");
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

        #region PrivacyPolicy
        public ActionResult PrivacyPolicy()
        {
            try
            {
                return View("PrivacyPolicy");
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

        #region Cookies
        public void SetCookie(string key, string value, int? expireInMinute)
        {
            CookieOptions option = new CookieOptions();

            if (expireInMinute.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireInMinute.Value);
            else
                option.Expires = DateTime.Now.AddDays(1);

            option.HttpOnly = false;
            option.IsEssential = true;

            Response.Cookies.Append(key, value, option);
        }
        #endregion

        #region RedPacketListing [Agent to distribute]
        public ActionResult RedPacketListing(int selectedPage = 1)
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new PaginationRedPacketModel();

            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                var foundAgent = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == usernameCookie && c.CroleId == 3);
                if (foundAgent == null)
                {
                    return RedirectToAction("ClientLogin", "UserLogin", new
                    {
                        reloadPage = true
                    });
                }
                else
                {
                    am.RedPacketBalance = (decimal)foundAgent.CusrRedpacketwlt;
                }
            }

            if (usernameCookie == "" || usernameCookie == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var dsAdmin = MerchantGeneralDB.GetRedPacketListingByUsername(usernameCookie);

            //Not completed task
            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                RedPacketModel temp = new RedPacketModel();
                temp.ID = int.Parse(dr["CREDP_ID"].ToString());
                temp.IDEncrypted = Authentication.MD5Encrypt(dr["CREDP_ID"].ToString());
                temp.ReferenceID = dr["CREDP_REFERENCE_ID"].ToString();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.Amount = decimal.Parse(dr["CREDP_AMOUNT"].ToString());
                temp.CreatedOn = DateTime.Parse(dr["CREDP_CREATEDON"].ToString()).ToString("dd/MM/yyyy");
                temp.ClaimedOn = dr["CREDP_CLAIMEDON"] == DBNull.Value ? "" : DateTime.Parse(dr["CREDP_CLAIMEDON"].ToString()).ToString("dd/MM/yyyy");
                temp.Status = dr["CREDP_STATUS"].ToString() == "0" ? Resources.PackBuddyShared.lblNotUsed : Resources.PackBuddyShared.lblUsed;

                am.List.Add(temp);
            }

            return View("RedPacketListing", am);
        }

        public ActionResult ModelEditRedPacket(int idz = 0)
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new RedPacketModel();

            if (usernameCookie == "" || usernameCookie == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                if (idz != 0)
                {
                    var found = dbContext.CvdRedPacket.FirstOrDefault(c => c.CredpAgent == usernameCookie && c.CredpId == idz);
                    if (found != null)
                    {
                        am.ID = found.CredpId;
                        am.IDEncrypted = Authentication.MD5Encrypt(am.ID.ToString());
                        am.ReferenceID = found.CredpReferenceId;
                        am.Username = found.CusrUsername;
                        am.Amount = found.CredpAmount;
                        am.CreatedOn = found.CredpCreatedon.ToString("dd/MM/yyyy");
                        am.ClaimedOn = found.CredpClaimedon == null ? "" : found.CredpClaimedon?.ToString("dd/MM/yyyy");
                        am.Status = found.CredpStatus == 0 ? Resources.PackBuddyShared.lblNotUsed : Resources.PackBuddyShared.lblUsed;
                    }
                }

                var foundAgent = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == usernameCookie && c.CroleId == 3);
                if (foundAgent != null)
                {
                    am.RedPacketBalance = (decimal)foundAgent.CusrRedpacketwlt;
                }
            }

            return View("ModelEditRedPacket", am);
        }

        //[HttpPost]
        //public IActionResult ModelEditRedPacketMethod(RedPacketModel am)
        //{
        //    try
        //    {
        //        string usernameCookie = "";
        //        try
        //        {
        //            string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
        //            usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
        //        }
        //        catch (Exception e)
        //        {
        //            return RedirectToAction("ClientLogin", "UserLogin", new
        //            {
        //                reloadPage = true
        //            });
        //        }

        //        if (usernameCookie == "")
        //        {
        //            Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //            return Json(new
        //            {
        //                status = false,
        //                message = Resources.PackBuddyShared.lblInvalidUsername
        //            });
        //        }

        //        if (Helper.NVLDecimal(am.Amount) == 0)
        //        {
        //            Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //            return Json(new
        //            {
        //                status = false,
        //                message = Resources.PackBuddyShared.msgInputAmountRedPacket
        //            });
        //        }

        //        if(Helper.NVL(am.Username) == "")
        //        {
        //            Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //            return Json(new
        //            {
        //                status = false,
        //                message = Resources.PackBuddyShared.lblInvalidUsername
        //            });
        //        }

        //        using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
        //        {
        //            var found = dbContext.CvdRedPacket.FirstOrDefault(c => c.CredpAgent == usernameCookie && c.CredpId == am.ID);
        //            var foundUser = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == am.Username && c.CroleId == 2);
        //            var foundAgent = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == usernameCookie && c.CroleId == 3);
        //            if (foundAgent != null)
        //            {
        //                decimal redPacketBalance = (decimal)foundAgent.CusrRedpacketwlt;

        //                if (am.Amount > redPacketBalance)
        //                {
        //                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //                    return Json(new
        //                    {
        //                        status = false,
        //                        message = Resources.PackBuddyShared.msgInsufficientWalletBalance
        //                    });
        //                }
        //            }

        //            if (foundUser == null)
        //            {
        //                Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //                return Json(new
        //                {
        //                    status = false,
        //                    message = Resources.PackBuddyShared.lblInvalidUsername
        //                });
        //            }

        //            if (found != null)
        //            {
        //                found.CusrUsername = am.Username;
        //            }
        //            else
        //            {
        //                CvdRedPacket newz = new CvdRedPacket();
        //                newz.CredpAgent = usernameCookie;
        //                newz.CredpReferenceId = string.Format("{0}_{1}", DateTime.Now.ToString("yyyyMMdd_HH_mm"), Misc.GenerateRandomDigit(5));
        //                newz.CusrUsername = am.Username;
        //                newz.CredpAmount = am.Amount;
        //                newz.CredpCreatedon = DateTime.Now;
        //                newz.CredpDeletionstate = false;
        //                newz.CredpStatus = 0;//0 Not Used 1 Used
        //                dbContext.CvdRedPacket.Add(newz);

        //                MerchantGeneralDB.RedPacketWalletOperation(usernameCookie, "GENERATE_RED_PKG", 0 - am.Amount, am.Username);
        //            }

        //            var result = dbContext.SaveChanges();
        //        }

        //        return RedPacketListing();
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

        public ActionResult RedPacketSponsorTreeListing(string upIDEnc)
        {
            string usernameCookie = "";
            string upID = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
                upID = Authentication.MD5Decrypt(upIDEnc);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new SponsorChartModel();

            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                var foundAgent = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == usernameCookie && c.CroleId == 3);
                var foundUpID = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == upID);

                if (foundAgent == null)
                {
                    return RedirectToAction("ClientLogin", "UserLogin", new
                    {
                        reloadPage = true
                    });
                }
                else
                {
                    am.RedPacketBalance = (decimal)foundAgent.CusrRedpacketwlt;
                }

                if (foundUpID == null)
                {
                    upID = "";
                }
            }

            if (usernameCookie == "" || usernameCookie == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            //if upID is not empty string, means we want to view upID sponsor chart. Else we view the logged in account sponsor chart
            if (upID == "")
            {
                upID = usernameCookie;
            }

            am.Upline = upID;
            am.UpIDEnc = upIDEnc;

            var dsAdmin = MerchantGeneralDB.GetSponsorTreeByUsername(upID);

            //First level
            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                SponsorModel temp = new SponsorModel();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.EncryptedUsername = Authentication.MD5Encrypt(temp.Username);

                am.FirstLevel.Add(temp);
            }

            foreach (DataRow dr in dsAdmin.Tables[1].Rows)
            {
                SponsorModel temp = new SponsorModel();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.EncryptedUsername = Authentication.MD5Encrypt(temp.Username);

                am.SecondLevel.Add(temp);
            }

            foreach (DataRow dr in dsAdmin.Tables[2].Rows)
            {
                SponsorModel temp = new SponsorModel();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.EncryptedUsername = Authentication.MD5Encrypt(temp.Username);

                am.ThirdLevel.Add(temp);
            }

            foreach (DataRow dr in dsAdmin.Tables[3].Rows)
            {
                SponsorModel temp = new SponsorModel();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.EncryptedUsername = Authentication.MD5Encrypt(temp.Username);

                am.FourthLevel.Add(temp);
            }

            foreach (DataRow dr in dsAdmin.Tables[4].Rows)
            {
                SponsorModel temp = new SponsorModel();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.EncryptedUsername = Authentication.MD5Encrypt(temp.Username);

                am.FifthLevel.Add(temp);
            }

            return View("RedPacketSponsorTreeListing", am);
        }

        [HttpPost]
        public IActionResult SendRedPacket(string IDEnc, decimal amount)
        {
            try
            {
                string usernameCookie = "";
                string username = "";
                try
                {
                    string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                    usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);

                    username = Authentication.MD5Decrypt(IDEnc);
                }
                catch (Exception e)
                {
                    return RedirectToAction("ClientLogin", "UserLogin", new
                    {
                        reloadPage = true
                    });
                }

                if (usernameCookie == "")
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblInvalidUsername
                    });
                }

                if (Helper.NVLDecimal(amount) == 0)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.msgInputAmountRedPacket
                    });
                }

                if (Helper.NVL(username) == "")
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblInvalidUsername
                    });
                }

                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var foundUser = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == username);
                    var foundAgent = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == usernameCookie && c.CroleId == 3);
                    if (foundAgent != null)
                    {
                        decimal redPacketBalance = (decimal)foundAgent.CusrRedpacketwlt;

                        if (amount > redPacketBalance)
                        {
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            return Json(new
                            {
                                status = false,
                                message = Resources.PackBuddyShared.msgInsufficientWalletBalance
                            });
                        }
                    }

                    if (foundUser == null)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(new
                        {
                            status = false,
                            message = Resources.PackBuddyShared.lblInvalidUsername
                        });
                    }
                    CvdRedPacket newz = new CvdRedPacket();
                    newz.CredpAgent = usernameCookie;
                    newz.CredpReferenceId = string.Format("{0}_{1}", DateTime.Now.ToString("yyyyMMdd_HH_mm"), Misc.GenerateRandomDigit(5));
                    newz.CusrUsername = username;
                    newz.CredpAmount = amount;
                    newz.CredpCreatedon = DateTime.Now;
                    newz.CredpDeletionstate = false;
                    newz.CredpStatus = 0;//0 Not Used 1 Used
                    dbContext.CvdRedPacket.Add(newz);

                    MerchantGeneralDB.RedPacketWalletOperation(usernameCookie, "GENERATE_RED_PKG", 0 - amount, username);

                    var result = dbContext.SaveChanges();
                }

                return RedPacketListing();
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

        [HttpPost]
        public IActionResult SendRedPacketByLevel(string UpIDEnc, int level, decimal amount)
        {
            try
            {
                string usernameCookie = "";
                string upID = "";
                try
                {
                    string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                    usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);

                    upID = Authentication.MD5Decrypt(UpIDEnc);
                }
                catch (Exception e)
                {
                    return RedirectToAction("ClientLogin", "UserLogin", new
                    {
                        reloadPage = true
                    });
                }

                if (upID == "")
                {
                    upID = usernameCookie;
                }

                if (usernameCookie == "")
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblInvalidUsername
                    });
                }

                if (Helper.NVLDecimal(amount) == 0)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.msgInputAmountRedPacket
                    });
                }

                if (Helper.NVL(upID) == "")
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblInvalidUsername
                    });
                }

                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var foundUser = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == upID);
                    var foundAgent = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == usernameCookie && c.CroleId == 3);
                    List<CvdUser> allDirectDownline = new List<CvdUser>();

                    if (level == 1)
                    {
                        allDirectDownline = dbContext.CvdUser.Where(c => c.CusrReferralid == upID).ToList();
                    }
                    else if (level == 2)
                    {
                        allDirectDownline = dbContext.CvdUser.Where(c => c.MemberLevel2Intro == upID).ToList();
                    }
                    else if (level == 3)
                    {
                        allDirectDownline = dbContext.CvdUser.Where(c => c.MemberLevel3Intro == upID).ToList();
                    }
                    else if (level == 4)
                    {
                        allDirectDownline = dbContext.CvdUser.Where(c => c.MemberLevel4Intro == upID).ToList();
                    }
                    else if (level == 5)
                    {
                        allDirectDownline = dbContext.CvdUser.Where(c => c.MemberLevel5Intro == upID).ToList();
                    }

                    if (foundAgent != null)
                    {
                        decimal redPacketBalance = (decimal)foundAgent.CusrRedpacketwlt;

                        if ((amount * allDirectDownline.Count) > redPacketBalance)
                        {
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            return Json(new
                            {
                                status = false,
                                message = Resources.PackBuddyShared.msgInsufficientWalletBalance
                            });
                        }
                    }

                    if (foundUser == null)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(new
                        {
                            status = false,
                            message = Resources.PackBuddyShared.lblInvalidUsername
                        });
                    }

                    foreach (var m in allDirectDownline)
                    {
                        CvdRedPacket newz = new CvdRedPacket();
                        newz.CredpAgent = usernameCookie;
                        newz.CredpReferenceId = string.Format("{0}_{1}", DateTime.Now.ToString("yyyyMMdd_HH_mm"), Misc.GenerateRandomDigit(5));
                        newz.CusrUsername = m.CusrUsername;
                        newz.CredpAmount = amount;
                        newz.CredpCreatedon = DateTime.Now;
                        newz.CredpDeletionstate = false;
                        newz.CredpStatus = 0;//0 Not Used 1 Used
                        dbContext.CvdRedPacket.Add(newz);

                        MerchantGeneralDB.RedPacketWalletOperation(usernameCookie, "GENERATE_RED_PKG", 0 - amount, upID);
                    }



                    var result = dbContext.SaveChanges();
                }

                return RedPacketListing();
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

        #region RedPacketClaimListing [Member to claim]
        public ActionResult RedPacketClaimListing(int selectedPage = 1)
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new PaginationRedPacketModel();

            if (usernameCookie == "" || usernameCookie == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var dsAdmin = MerchantGeneralDB.GetRedPacketClaimListingByUsername(usernameCookie);

            //Not completed task
            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                RedPacketModel temp = new RedPacketModel();
                temp.ID = int.Parse(dr["CREDP_ID"].ToString());
                temp.IDEncrypted = Authentication.MD5Encrypt(dr["CREDP_ID"].ToString());
                temp.ReferenceID = dr["CREDP_REFERENCE_ID"].ToString();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.Amount = decimal.Parse(dr["CREDP_AMOUNT"].ToString());
                temp.CreatedOn = DateTime.Parse(dr["CREDP_CREATEDON"].ToString()).ToString("dd/MM/yyyy");
                temp.ClaimedOn = dr["CREDP_CLAIMEDON"] == DBNull.Value ? "" : DateTime.Parse(dr["CREDP_CLAIMEDON"].ToString()).ToString("dd/MM/yyyy");
                temp.Status = dr["CREDP_STATUS"].ToString() == "0" ? Resources.PackBuddyShared.lblNotUsed : Resources.PackBuddyShared.lblUsed;
                temp.StatusInt = int.Parse(dr["CREDP_STATUS"].ToString());

                am.List.Add(temp);
            }

            return View("RedPacketClaimListing", am);
        }

        [HttpPost]
        public IActionResult ClaimRedPacket(string idz)
        {
            try
            {
                string usernameCookie = "";
                string id = "";
                try
                {
                    string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                    usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);

                    id = Authentication.MD5Decrypt(idz);
                }
                catch (Exception e)
                {
                    return RedirectToAction("ClientLogin", "UserLogin", new
                    {
                        reloadPage = true
                    });
                }

                if (usernameCookie == "")
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblInvalidUsername
                    });
                }

                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var found = dbContext.CvdRedPacket.FirstOrDefault(c => c.CusrUsername == usernameCookie && c.CredpId == int.Parse(id));

                    if (found != null)
                    {
                        found.CredpStatus = 1;
                        found.CredpClaimedon = DateTime.Now;

                        var result = dbContext.SaveChanges();

                        MerchantGeneralDB.CashWalletOperation(usernameCookie, "RED_PACKET", found.CredpAmount, found.CredpAgent, 0, 0, found.CredpReferenceId);
                    }
                    else
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(new
                        {
                            status = false,
                            message = Resources.PackBuddyShared.msgInvalidRedPacket
                        });
                    }
                }

                return MyPacketClaimList();
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

        #region PlayGame

        public ActionResult PlayGame(int gameid = 0)
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            ViewBag.GameIDTab = gameid;

            var am = new MemberGamePageModel();

            if (usernameCookie == "" || usernameCookie == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var dsAdmin = AdminGeneralDB.GetAllGameSessionActive();

            //Not completed task
            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                MemberGameModel tempo = new MemberGameModel();

                //only active game
                if (dr["CGAME_STATUS"].ToString() == "1")
                {
                    tempo.GameID = int.Parse(dr["CGAME_ID"].ToString());
                    tempo.Name = dr["CGAME_NAME"].ToString();
                    tempo.CurrentPeriod = dr["CGAME_PERIOD"].ToString();
                    tempo.StartDateTime = DateTime.Parse(dr["CGAME_START"].ToString());
                    tempo.EndDateTime = DateTime.Parse(dr["CGAME_END"].ToString());
                    am.ServerCurrentTime = DateTime.Now;
                    var ts = (tempo.EndDateTime - am.ServerCurrentTime);
                    am.TimeLeftBasedEndDateTotalSeconds = (int)ts.TotalSeconds < 0 ? 0 : (int)ts.TotalSeconds;
                    //just for display purpose so that it wont show 0 > Not accurate lets not show
                    //am.MinuteLeft = (int)ts.Minutes < 0 ? 0 : (int)ts.Minutes;
                    //am.SecondLeft = (int)ts.Seconds < 0 ? 0 : (int)ts.Seconds;

                    #region Get the history from [CVD_GAME_SESSION_DAILY_HISTORY]
                    var dsHistory = AdminGeneralDB.GetGameSessionHistory(tempo.GameID);

                    foreach (DataRow drr in dsHistory.Tables[0].Rows)
                    {
                        string resultz = drr["CGAME_RESULT"].ToString();

                        string[] tempResult = resultz.Split(";");

                        //this is to sort but doesn't seems like need as the table displayed alr got sort [data table]
                        //Array.Sort(tempResult, new ReverseStringComparer());

                        foreach (var t in tempResult)
                        {
                            string[] childResult = t.Split("_");
                            GameHistoryRecord tmpResult = new GameHistoryRecord();
                            int zzz = 0;

                            if (childResult.Length == 4)
                            {
                                foreach (var tt in childResult)
                                {
                                    if (zzz == 0)
                                    {
                                        tmpResult.Period = tt;
                                    }
                                    else if (zzz == 1)
                                    {
                                        tmpResult.TotalBet = decimal.Parse(tt);
                                    }
                                    else if (zzz == 2)
                                    {
                                        tmpResult.Price = decimal.Parse(tt);//this is total win
                                    }
                                    else if (zzz == 3)
                                    {
                                        tmpResult.ResultNumber = int.Parse(tt);
                                    }

                                    zzz++;
                                }

                                tempo.HistoryList.Add(tmpResult);
                            }

                        }
                    }

                    #endregion

                    #region Get own bet history
                    var dsBetHistory = AdminGeneralDB.GetBetHistory(tempo.GameID, usernameCookie);
                    foreach (DataRow drBetHistory in dsBetHistory.Tables[0].Rows)
                    {
                        GameHistoryRecord tmpResult = new GameHistoryRecord();
                        tmpResult.Period = drBetHistory["CGAME_PERIOD"].ToString();
                        tmpResult.Price = decimal.Parse(drBetHistory["CGAME_AMOUNT"].ToString());
                        tmpResult.ResultNumber = int.Parse(drBetHistory["CGAME_NUMBER"].ToString());

                        if (tmpResult.ResultNumber == 55)
                        {
                            tmpResult.ResultNumberString = Resources.PackBuddyShared.lblJoinGreen;
                        }
                        else if (tmpResult.ResultNumber == 66)
                        {
                            tmpResult.ResultNumberString = Resources.PackBuddyShared.lblJoinViolet;
                        }
                        else if (tmpResult.ResultNumber == 77)
                        {
                            tmpResult.ResultNumberString = Resources.PackBuddyShared.lblJoinRed;
                        }
                        else
                        {
                            tmpResult.ResultNumberString = tmpResult.ResultNumber.ToString();
                        }

                        tmpResult.Won = decimal.Parse(drBetHistory["CGAME_WIN_AMOUNT"].ToString());

                        if (tmpResult.Price > tmpResult.Won)
                        {
                            tmpResult.Won = 0 - tmpResult.Price;
                        }

                        tempo.MyRecordHistoryList.Add(tmpResult);
                    }
                    #endregion

                    am.GameList.Add(tempo);
                }
            }

            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                var found = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == usernameCookie);
                if (found != null)
                {
                    am.Balance = (Decimal)found.CusrCashwlt;
                }
            }

            ViewBag.HeaderPage = "Win";

            return View("PlayGame", am);
        }

        [HttpPost]
        public IActionResult PlaceNumberGame(int num, int amount, int gameid, string period)
        {
            try
            {
                string usernameCookie = "";
                string id = "";
                try
                {
                    string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                    usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
                }
                catch (Exception e)
                {
                    return RedirectToAction("ClientLogin", "UserLogin", new
                    {
                        reloadPage = true
                    });
                }

                if (usernameCookie == "")
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblInvalidUsername
                    });
                }

                if (amount <= 0)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.msgInvalidAmount
                    });
                }

                int ok = 0;
                string msg = "";
                AdminGeneralDB.PlaceBet(usernameCookie, num, amount, gameid, period, out ok, out msg);

                if (ok == 1)
                {

                }
                else
                {
                    if (ok == -1)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(new
                        {
                            status = false,
                            message = Resources.PackBuddyShared.msgGameSessionNotFound
                        });
                    }
                    else if (ok == -2)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(new
                        {
                            status = false,
                            message = Resources.PackBuddyShared.msgGameTooLateToBet
                        });
                    }
                    else if (ok == -3)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(new
                        {
                            status = false,
                            message = Resources.PackBuddyShared.msgGameInsufficientFund
                        });
                    }
                }

                return PlayGame();
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

        #region Profile
        public ActionResult Profile()
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            if (usernameCookie == "")
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new MemberHomeModel();
            ViewBag.HeaderPage = "Profile";
            ViewBag.IsAgent = false;
            ViewBag.CashWallet = 0;
            ViewBag.TotalCommission = 0;

            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                var foundAgent = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == usernameCookie && c.CroleId == 3);
                if (foundAgent != null)
                {
                    ViewBag.IsAgent = true;
                }

                var found = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == usernameCookie && (c.CroleId == 3 || c.CroleId == 2));

                if (found != null)
                {
                    ViewBag.CashWallet = found.CusrCashwlt;
                    ViewBag.TotalCommission = found.MemberDownlineTotalCommission;
                }

            }

            return View("Profile", am);
        }
        #endregion

        #region MyReferral
        public ActionResult MyReferral()
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            if (usernameCookie == "")
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new MemberHomeModel();

            ViewBag.Referral = string.Format("https://www.hot-mall.club/UserLogin/SignUp?referral={0}", Authentication.Encrypt(usernameCookie));

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeDataAndroid = qrGenerator.CreateQrCode(ViewBag.Referral, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCodeAndroid = new QRCode(qrCodeDataAndroid);
            Bitmap qrCodeImageAndroid = qrCodeAndroid.GetGraphic(20);

            // Create a rectangle for the entire bitmap
            RectangleF rectf = new RectangleF(0, 0, qrCodeImageAndroid.Width, qrCodeImageAndroid.Height - 30);

            // Create graphic object that will draw onto the bitmap
            Graphics g = Graphics.FromImage(qrCodeImageAndroid);

            // ------------------------------------------
            // Ensure the best possible quality rendering
            // ------------------------------------------
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // The interpolation mode determines how intermediate values between two endpoints are calculated.
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Use this property to specify either higher quality, slower rendering, or lower quality, faster rendering of the contents of this Graphics object.
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // This one is important
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            // Create string formatting options (used for alignment)
            StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Near
            };

            // Create string formatting options (used for alignment)
            StringFormat bottomFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Far
            };

            // Flush all graphics changes to the bitmap
            g.Flush();

            System.IO.MemoryStream msAndroid = new MemoryStream();
            qrCodeImageAndroid.Save(msAndroid, ImageFormat.Jpeg);
            byte[] byteImageAndroid = msAndroid.ToArray();

            ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(byteImageAndroid);

            ViewBag.HeaderPage = "MyReferral";

            return View("MyReferral", am);
        }
        #endregion

        #region ProductListing
        public ActionResult ProductListing(string search)
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new ProductListModel();

            if (usernameCookie == "" || usernameCookie == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                if (!string.IsNullOrEmpty(search))
                {
                    var productListModel = getProductListSearch(search);
                    return View("ProductListing", productListModel);
                }

                var productList = dbContext.CvdProduct.Where(p => p.CproDeletionstate == false).ToList();
                foreach (var product in productList)
                {
                    ProductModel productModel = new ProductModel();
                    productModel.id = product.CproId;
                    productModel.Title = product.CproTitle;
                    productModel.Desc = product.CproDesc;
                    productModel.AdditionalDesc = product.CproDescAdd;
                    productModel.Price = product.CproPrice;
                    productModel.ImagePath = "Uploads/Products/" + product.CproTitle.Replace(" ", "") + "/" + product.CproImages;
                    productModel.Other = getRandomProductStatus();
                    am.ProductList.Add(productModel);
                }
            }

            return View("ProductListing", am);
        }
        #endregion

        #region ProductDescription
        public ActionResult ProductDescription(int ID, string search)
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new ProductListModel();

            if (usernameCookie == "" || usernameCookie == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            ProductModel productModel = new ProductModel();
            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                if (!string.IsNullOrEmpty(search))
                {
                    var productListModel = getProductListSearch(search);
                    return View("ProductListing", productListModel);
                }

                var product = dbContext.CvdProduct.FirstOrDefault(c => c.CproId == ID);
                productModel.id = product.CproId;
                productModel.Title = product.CproTitle;
                productModel.Desc = product.CproDesc;
                productModel.AdditionalDesc = product.CproDescAdd;
                productModel.Price = product.CproPrice;
                productModel.ImagePath = "Uploads/Products/" + product.CproTitle.Replace(" ", "") + "/" + product.CproImages;
                productModel.Other = getRandomProductStatus();

            }

            return View("ProductDescription", productModel);
        }
        #endregion

        #region ProductStatus + Search
        private string getRandomProductStatus()
        {
            List<string> firstNames = new List<string>();
            firstNames.Add("SOLD");
            firstNames.Add("DEAL");
            firstNames.Add("NEW");

            Random randNum = new Random();
            int aRandomPos = randNum.Next(firstNames.Count);//Returns a nonnegative random number less than the specified maximum (firstNames.Count).

            return firstNames[aRandomPos];
        }

        private ProductListModel getProductListSearch(string search)
        {
            ProductListModel productListModel = new ProductListModel();
            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                var productList = (from c in dbContext.CvdProduct where c.CproDesc.Contains(search) select c).ToList();

                foreach (var product in productList)
                {

                    ProductModel productModel = new ProductModel();
                    productModel.id = product.CproId;
                    productModel.Title = product.CproTitle;
                    productModel.Desc = product.CproDesc;
                    productModel.AdditionalDesc = product.CproDescAdd;
                    productModel.Price = product.CproPrice;
                    productModel.ImagePath = "Uploads/Products/" + product.CproTitle.Replace(" ", "") + "/" + product.CproImages;
                    productModel.Other = getRandomProductStatus();
                    productListModel.ProductList.Add(productModel);
                }
            }
            return productListModel;
        }
        #endregion

        #region MyPacketList
        public ActionResult MyPacketList()
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new PaginationRedPacketModel();

            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                var foundAgent = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == usernameCookie && c.CroleId == 3);
                if (foundAgent == null)
                {
                    return RedirectToAction("ClientLogin", "UserLogin", new
                    {
                        reloadPage = true
                    });
                }
                else
                {
                    am.RedPacketBalance = (decimal)foundAgent.CusrRedpacketwlt;
                }
            }

            if (usernameCookie == "" || usernameCookie == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var dsAdmin = MerchantGeneralDB.GetRedPacketListingByUsername(usernameCookie);

            //Not completed task
            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                RedPacketModel temp = new RedPacketModel();
                temp.ID = int.Parse(dr["CREDP_ID"].ToString());
                temp.IDEncrypted = Authentication.MD5Encrypt(dr["CREDP_ID"].ToString());
                temp.ReferenceID = dr["CREDP_REFERENCE_ID"].ToString();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.Amount = decimal.Parse(dr["CREDP_AMOUNT"].ToString());
                temp.CreatedOn = DateTime.Parse(dr["CREDP_CREATEDON"].ToString()).ToString("dd/MM/yyyy");
                temp.ClaimedOn = dr["CREDP_CLAIMEDON"] == DBNull.Value ? "" : DateTime.Parse(dr["CREDP_CLAIMEDON"].ToString()).ToString("dd/MM/yyyy");
                temp.Status = dr["CREDP_STATUS"].ToString() == "0" ? Resources.PackBuddyShared.lblNotUsed : Resources.PackBuddyShared.lblUsed;

                am.List.Add(temp);
            }

            return PartialView("MyPacketList", am);
        }

        public ActionResult MyPacketSponsorTreeListing(string upIDEnc)
        {
            string usernameCookie = "";
            string upID = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
                upID = Authentication.MD5Decrypt(upIDEnc);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new SponsorChartModel();

            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                var foundAgent = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == usernameCookie && c.CroleId == 3);
                var foundUpID = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == upID);

                if (foundAgent == null)
                {
                    return RedirectToAction("ClientLogin", "UserLogin", new
                    {
                        reloadPage = true
                    });
                }
                else
                {
                    am.RedPacketBalance = (decimal)foundAgent.CusrRedpacketwlt;
                }

                if (foundUpID == null)
                {
                    upID = "";
                }
            }

            if (usernameCookie == "" || usernameCookie == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            //if upID is not empty string, means we want to view upID sponsor chart. Else we view the logged in account sponsor chart
            if (upID == "")
            {
                upID = usernameCookie;
            }

            am.Upline = upID;
            am.UpIDEnc = upIDEnc;

            var dsAdmin = MerchantGeneralDB.GetSponsorTreeByUsername(upID);

            //First level
            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                SponsorModel temp = new SponsorModel();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.EncryptedUsername = Authentication.MD5Encrypt(temp.Username);

                am.FirstLevel.Add(temp);
            }

            foreach (DataRow dr in dsAdmin.Tables[1].Rows)
            {
                SponsorModel temp = new SponsorModel();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.EncryptedUsername = Authentication.MD5Encrypt(temp.Username);

                am.SecondLevel.Add(temp);
            }

            foreach (DataRow dr in dsAdmin.Tables[2].Rows)
            {
                SponsorModel temp = new SponsorModel();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.EncryptedUsername = Authentication.MD5Encrypt(temp.Username);

                am.ThirdLevel.Add(temp);
            }

            foreach (DataRow dr in dsAdmin.Tables[3].Rows)
            {
                SponsorModel temp = new SponsorModel();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.EncryptedUsername = Authentication.MD5Encrypt(temp.Username);

                am.FourthLevel.Add(temp);
            }

            foreach (DataRow dr in dsAdmin.Tables[4].Rows)
            {
                SponsorModel temp = new SponsorModel();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.EncryptedUsername = Authentication.MD5Encrypt(temp.Username);

                am.FifthLevel.Add(temp);
            }

            return View("MyPacketSponsorTreeListing", am);
        }
        #endregion

        #region MyPacketClaimList
        public ActionResult MyPacketClaimList(int selectedPage = 1)
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new PaginationRedPacketModel();

            if (usernameCookie == "" || usernameCookie == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var dsAdmin = MerchantGeneralDB.GetRedPacketClaimListingByUsername(usernameCookie);

            //Not completed task
            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                RedPacketModel temp = new RedPacketModel();
                temp.ID = int.Parse(dr["CREDP_ID"].ToString());
                temp.IDEncrypted = Authentication.MD5Encrypt(dr["CREDP_ID"].ToString());
                temp.ReferenceID = dr["CREDP_REFERENCE_ID"].ToString();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.Amount = decimal.Parse(dr["CREDP_AMOUNT"].ToString());
                temp.CreatedOn = DateTime.Parse(dr["CREDP_CREATEDON"].ToString()).ToString("dd/MM/yyyy");
                temp.ClaimedOn = dr["CREDP_CLAIMEDON"] == DBNull.Value ? "" : DateTime.Parse(dr["CREDP_CLAIMEDON"].ToString()).ToString("dd/MM/yyyy");
                temp.Status = dr["CREDP_STATUS"].ToString() == "0" ? Resources.PackBuddyShared.lblNotUsed : Resources.PackBuddyShared.lblUsed;
                temp.StatusInt = int.Parse(dr["CREDP_STATUS"].ToString());

                am.List.Add(temp);
            }

            return View("MyPacketClaimList", am);
        }

        #endregion

        #region BankCard

        public ActionResult BankCard()
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new PaginationBankModelModel();

            if (usernameCookie == "" || usernameCookie == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            // for record
            am.List = new List<BankModel>();
            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                foreach( var item in dbContext.CvdMemberBank.Where(c => c.CusrUsername.ToLower() == usernameCookie.ToLower()))
                {
                    var bankModel = new BankModel();
                    bankModel.Id = item.CbankId;
                    bankModel.ActualName = item.CbankName;
                    bankModel.BankName = item.CbankBankaccountname;
                    bankModel.Email = item.CbankEmail;
                    am.List.Add(bankModel);
                }                
            }

            return View("BankCard", am);
        }

        public ActionResult AddBankCard(int id)
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new BankModel();

            if (usernameCookie == "" || usernameCookie == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            // for record
            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                var bankFound = dbContext.CvdMemberBank.FirstOrDefault(c => c.CbankId == id && c.CusrUsername.ToLower() == usernameCookie.ToLower());
                if (bankFound != null)
                {
                    am.Id = id;
                    am.ActualName = bankFound.CbankName;
                    am.IFSCCode = bankFound.CbankIfsccode;
                    am.BankName = bankFound.CbankBankaccountname;
                    am.BankAccount = bankFound.CbankBankaccount;
                    am.State = bankFound.CbankState;
                    am.City = bankFound.CbankCity;
                    am.Address = bankFound.CbankAddress;
                    am.MobileNumber = bankFound.CbankMobile;
                    am.Email = bankFound.CbankEmail;
                }
            }

            return View("BankCardInfo", am);
        }

        [HttpPost]
        public IActionResult UpdateBankInfo(int id, string actualname, string ifsccode, string bankname, string state, string bankaccount, string city, string address, string mobile, string email)
        {
            try
            {
                string usernameCookie = "";
                try
                {
                    string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                    usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
                }
                catch (Exception e)
                {
                    return RedirectToAction("ClientLogin", "UserLogin", new
                    {
                        reloadPage = true
                    });
                }

                if (usernameCookie == "")
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblInvalidUsername
                    });
                }

                if (id == 0)
                {
                    // for record
                    using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                    {                        
                        var memberBank = new CvdMemberBank();
                        memberBank.CusrUsername = usernameCookie;
                        memberBank.CbankIfsccode = ifsccode;
                        memberBank.CbankName = actualname;
                        memberBank.CbankState = state;
                        memberBank.CbankBankaccountname = bankname;
                        memberBank.CbankBankaccount = bankaccount;
                        memberBank.CbankCity = city;
                        memberBank.CbankAddress = address;
                        memberBank.CbankMobile = mobile;
                        memberBank.CbankEmail = email;                        
                        memberBank.CbankCreatedby = usernameCookie;
                        memberBank.CbankCreatedon = DateTime.Now;

                        dbContext.CvdMemberBank.Add(memberBank);
                        dbContext.SaveChanges();
                    }
                }
                else
                {
                    // for record
                    using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                    {
                        var memberBank = dbContext.CvdMemberBank.FirstOrDefault(c => c.CusrUsername.ToLower() == usernameCookie.ToLower() && c.CbankId == id);                        
                        memberBank.CbankIfsccode = ifsccode;
                        memberBank.CbankName = actualname;
                        memberBank.CbankState = state;
                        memberBank.CbankBankaccountname = bankname;
                        memberBank.CbankBankaccount = bankaccount;
                        memberBank.CbankCity = city;
                        memberBank.CbankAddress = address;
                        memberBank.CbankMobile = mobile;
                        memberBank.CbankEmail = email;
                        memberBank.CbankCreatedby = usernameCookie;

                        dbContext.CvdMemberBank.Update(memberBank);
                        dbContext.SaveChanges();
                    }
                }    
            }
            catch (Exception ex)
            {

            }

            return BankCard();
        }

        #endregion

        #region Recharge
        public ActionResult Recharge(string search)
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new PaymentModel();

            if (usernameCookie == "" || usernameCookie == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            // for record
            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                var userFound = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == usernameCookie);
                if (userFound != null)
                {
                    am.Amount = userFound.CusrCashwlt.Value.ToString("0.00");
                }
            }

            return View("Recharge", am);
        }

        #endregion

        #region Withdrawal
        public ActionResult Withdrawal()
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new PaymentModel();

            if (usernameCookie == "" || usernameCookie == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }
            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                var userFound = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == usernameCookie);
                if (userFound != null)
                {
                    am.Amount = userFound.CusrCashwlt.Value.ToString("0.00");
                }
                var serviceFeeFound = dbContext.CvdParameter.FirstOrDefault(c => c.CparaName == "WithdrawalServiceFee");
                if (serviceFeeFound != null)
                {
                    am.ServiceFee = serviceFeeFound.CparaDecimalvalue.ToString("0.00");
                }

                var bankListFound = dbContext.CvdMemberBank.Where(c => c.CusrUsername == usernameCookie).ToList();
                if (bankListFound != null)
                {
                    List<SelectListItem> bankList = new List<SelectListItem>();
                    bankList.Add(new SelectListItem() { Text = Resources.PackBuddyShared.lblSelectBankCard, Value = "" });

                    foreach (var bank in bankListFound)
                    {
                        bankList.Add(new SelectListItem() { Text = bank.CbankBankaccount, Value = bank.CbankId.ToString() });
                    }
                    am.BankList = bankList;
                }
            }


            return View("Withdrawal", am);
        }

        public IActionResult WithdrawalMethod(string password, decimal withdrawalAmount, decimal serviceFee, int bankId)
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            if (usernameCookie == "" || usernameCookie == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }
            
            if (withdrawalAmount <= 0)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = Resources.PackBuddyShared.msgInvalidAmount
                });
            }
            
            if (bankId <= 0)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = Resources.PackBuddyShared.msgInvalidBankId
                });
            }
            
            if (string.IsNullOrEmpty(password))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = Resources.PackBuddyShared.msgInvalidPassword
                }); ;
            }

            decimal balanceAmount = 0;
            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                var found = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == usernameCookie && (c.CroleId == 3 || c.CroleId == 2));
                if (found != null)
                {
                    balanceAmount = found.CusrCashwlt.Value;                    
                }
            }

            if (withdrawalAmount > balanceAmount)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = Resources.PackBuddyShared.msgInsufficientWalletBalance
                });
            }
            string message = ValidatePassword(usernameCookie, password);
            if (string.IsNullOrEmpty(message))
            {
                withdrawalAmount = 0 - withdrawalAmount;
                AdminDB.CashWalletOperation(usernameCookie, withdrawalAmount, "WDR", 0, "", "", "0", serviceFee, bankId);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { status = false, message = message });

            }

            return StatusCode((int)HttpStatusCode.OK);
        }


        private string ValidatePassword(string userName, string password)
        {
            string message = "";
            try
            {
                string pass = "";
                var ds = AdminDB.GetUserByUsername(userName, out int ok, out string msg);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    message = Resources.PackBuddyShared.msgInvalidLogin;
                    return message;
                }

                userName = ds.Tables[0].Rows[0]["CUSR_USERNAME"].ToString();
                pass = ds.Tables[0].Rows[0]["CUSR_PASSWORD"].ToString();

                if (Authentication.Encrypt(password) != pass)
                {
                    message = Resources.PackBuddyShared.msgInvalidPassword;
                    return message;
                }

                var encryptedUsername = Authentication.Encrypt(userName);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    message = Resources.PackBuddyShared.msgInvalidLogin;
                    return message;
                }

                return message;

            }
            catch (Exception ex)
            {
                message = ex.ToString();
                return message;
            }
        }

        #endregion

        #region MyOrder
        public ActionResult MyOrder()
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new MemberHomeModel();

            if (usernameCookie == "")
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            return View("MyOrder", am);
        }
        #endregion

        #region RechargeMethod
        public IActionResult RechargeMethod(string rechargeAmount)
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            if (usernameCookie == "" || usernameCookie == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            if (!decimal.TryParse(rechargeAmount, out decimal recharge) || recharge < 100)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    status = false,
                    message = Resources.PackBuddyShared.lblInvalidAmount
                });
            }

            return StatusCode((int)HttpStatusCode.OK);
        }
        #endregion

        #region CreatePaymentTwo + ReturnUrl
        public IActionResult CreatePaymentTwo(decimal amount, string bankName, string accountName, string cardNo, string branch, string province, string city)
        {
            string username = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                username = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            if (username == "" || username == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var payment = new PaymentModel();
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var paymentId = string.Format("{0}{1}{2}", DateTime.Today.ToString("yyyyMMdd"), "MY", DateTime.Now.ToString("HHmmss"));

            var url = this.Request.Host.Host.ToLower();

            try
            {
                var postUrl = Misc.depositUrl;

                if (url == "localhost" || url.Contains("h2init.com"))
                {
                    postUrl = Misc.depositUrl;
                }

                var dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var dataSign = string.Format("pay_amount={0}&pay_applydate={1}&pay_bankcode={2}&pay_callbackurl={3}&pay_memberid={4}&pay_notifyurl={5}&pay_orderid={6}&key={7}",
                    amount.ToString("#0.00"), dateTime, Misc.bankCode, Misc._baseUrl + "/Payment/CallBackUrl", Misc.merchantCode, Misc._baseUrl + "/User/ReturnUrl", paymentId, Misc.merchantKey);

                var dataMd5 = Misc.MD5(dataSign).ToUpper();

                payment.MerchantCode = Misc.merchantCode;
                payment.RefNo = paymentId;
                payment.Amount = amount.ToString("#0.00");
                payment.BankName = bankName;
                payment.Created = dateTime;
                payment.AccountName = accountName;
                payment.CardNo = cardNo;
                payment.Bankcode = Misc.bankCode;
                payment.SubBranch = branch;
                payment.NotifyUrl = Misc._baseUrl + "/User/ReturnUrl";
                payment.CallbackUrl = Misc._baseUrl + "/Payment/CallBackUrl";
                payment.Province = province;
                payment.City = city;
                payment.PayMd5 = dataMd5;
                payment.PaymentUrl = postUrl;
                payment.ProductDesc = "Reload";

                // for record
                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var paymentInfo = new CvdPaymentinfo();
                    paymentInfo.CpaymentTranspaymentid = paymentId;
                    paymentInfo.CusrUsername = username ?? "";
                    paymentInfo.CpaymentTransid = string.Empty;
                    paymentInfo.CpaymentAmount = decimal.Parse(payment.Amount);
                    paymentInfo.CpaymentCreatedon = DateTime.Now;

                    dbContext.CvdPaymentinfo.Add(paymentInfo);
                    dbContext.SaveChanges();

                    AdminDB.CashWalletOperationTemp(username, amount, "Recharge", paymentId, 0);
                }
            }
            catch (Exception ex)
            {
                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var err = new CvdSystemerror();
                    err.CerrorInfo = ex.ToString();
                    err.CerrorAppother = "CreatePayment";
                    err.CerrorAppother2 = paymentId;
                    err.CerrorAppother3 = string.Empty;
                    err.CerrorCreatedon = DateTime.Now;

                    dbContext.CvdSystemerror.Add(err);
                    dbContext.SaveChanges();
                }
            }

            return View("CreatePaymentTwo", payment);
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
            var attach = HttpUtility.UrlDecode(Request.Form["attach"]);
            var sign = HttpUtility.UrlDecode(Request.Form["sign"]);
            var merchantKey = Misc.merchantKey;

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

            return View("ReturnUrl");
        }
        #endregion

        #region MyTeam
        public ActionResult MyTeam(string upIDEnc)
        {
            string usernameCookie = "";
            string upID = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
                upID = Authentication.MD5Decrypt(upIDEnc);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new SponsorChartModel();

            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                var foundAgentOrUser = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == usernameCookie && (c.CroleId == 3 || c.CroleId == 2));
                var foundUpID = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == upID);

                if (foundAgentOrUser == null)
                {
                    return RedirectToAction("ClientLogin", "UserLogin", new
                    {
                        reloadPage = true
                    });
                }
                else
                {
                    //am.RedPacketBalance = (decimal)foundAgent.CusrRedpacketwlt;
                }

                if (foundUpID == null)
                {
                    upID = "";
                }
            }

            if (usernameCookie == "" || usernameCookie == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            //if upID is not empty string, means we want to view upID sponsor chart. Else we view the logged in account sponsor chart
            if (upID == "")
            {
                upID = usernameCookie;
            }

            am.Upline = upID;
            am.UpIDEnc = upIDEnc;

            var dsAdmin = MerchantGeneralDB.GetSponsorTreeByUsername(upID);

            //First level
            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                SponsorModel temp = new SponsorModel();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.EncryptedUsername = Authentication.MD5Encrypt(temp.Username);

                am.FirstLevel.Add(temp);
            }

            foreach (DataRow dr in dsAdmin.Tables[1].Rows)
            {
                SponsorModel temp = new SponsorModel();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.EncryptedUsername = Authentication.MD5Encrypt(temp.Username);

                am.SecondLevel.Add(temp);
            }

            foreach (DataRow dr in dsAdmin.Tables[2].Rows)
            {
                SponsorModel temp = new SponsorModel();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.EncryptedUsername = Authentication.MD5Encrypt(temp.Username);

                am.ThirdLevel.Add(temp);
            }

            foreach (DataRow dr in dsAdmin.Tables[3].Rows)
            {
                SponsorModel temp = new SponsorModel();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.EncryptedUsername = Authentication.MD5Encrypt(temp.Username);

                am.FourthLevel.Add(temp);
            }

            foreach (DataRow dr in dsAdmin.Tables[4].Rows)
            {
                SponsorModel temp = new SponsorModel();
                temp.Username = dr["CUSR_USERNAME"].ToString();
                temp.EncryptedUsername = Authentication.MD5Encrypt(temp.Username);

                am.FifthLevel.Add(temp);
            }

            return View("MyTeam", am);
        }
        #endregion

        #region Transaction
        public ActionResult Transaction()
        {
            string usernameCookie = "";
            try
            {
                string encryptedUsernameCookie = HttpContext.Request.Cookies["UserIDCookie"];
                usernameCookie = Authentication.Decrypt(encryptedUsernameCookie);
            }
            catch (Exception e)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }

            var am = new PaginationPaymentModel();

            if (usernameCookie == "" || usernameCookie == null)
            {
                return RedirectToAction("ClientLogin", "UserLogin", new
                {
                    reloadPage = true
                });
            }
            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                var transactionFound = dbContext.CvdCashwalletlog.OrderByDescending(t => t.CcashCreatedon).Take(100);
                foreach (var transaction in transactionFound)
                {
                    PaymentModel paymentModel = new PaymentModel();
                    paymentModel.Username = transaction.CusrUsername;
                    paymentModel.RefNo = transaction.CcashAppother;
                    paymentModel.Remark = transaction.CcashCashname;
                    paymentModel.FinalAmount = Convert.ToString(Convert.ToDecimal(transaction.CcashCashin) == 0 ? Convert.ToDecimal(transaction.CcashCashout) : Convert.ToDecimal(transaction.CcashCashin));
                    paymentModel.Created = transaction.CcashCreatedon.ToString("dd/MM/yyyy HH:mm:ss");
                    paymentModel.Status = transaction.CcashStatus == 0 ? Resources.PackBuddyShared.lblInProgress : transaction.CcashStatus == 1 ? Resources.PackBuddyShared.lblSuccess : Resources.PackBuddyShared.lblFailed;
                    am.List.Add(paymentModel);
                }
            }
         
            return View("Transaction", am);
        }
        #endregion
    }
}