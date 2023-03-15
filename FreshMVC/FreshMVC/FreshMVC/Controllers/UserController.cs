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

                    var productList = dbContext.CvdProduct.ToList();
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
                            productModel.ImagePath = product.CproImages;
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
                                productModel.ImagePath = product.CproImages;
                                productModel.Other = getRandomProductStatus();
                                am.ActiveProductList.Add(productModel);
                            }
                        }
                        index++;
                    }
                }
            }

            ModelState.Clear();
            ViewBag.HeaderPage = "Home";

            return View("Home", am);
        }

        public ActionResult DoNothing()
        {
            return View("DoNothing");
        }

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
            catch(Exception e)
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
                if(foundAgent != null)
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

                if(foundUpID == null)
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

                    if(level == 1)
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

                return RedPacketClaimListing();
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
        public ActionResult PlayGame()
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

                    am.GameList.Add(tempo);
                }
            }

            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                var found = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == usernameCookie);
                if (found == null)
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

                int ok = 0;
                string msg = "";
                AdminGeneralDB.PlaceBet(usernameCookie, num, amount, gameid, period, out ok, out msg);

                if(ok == 1)
                {

                }    
                else
                {
                    if(ok == -1)
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

            var am = new MemberHomeModel();
            ViewBag.HeaderPage = "Profile";

            return View("Profile", am);
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
            //Get Product Listing
            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                if (!string.IsNullOrEmpty(search))
                {
                    var productListModel = getProductListSearch(search);
                    return View("ProductListing", productListModel);
                }

                var productList = dbContext.CvdProduct.ToList();
                foreach (var product in productList)
                {
                    ProductModel productModel = new ProductModel();
                    productModel.id = product.CproId;
                    productModel.Title = product.CproTitle;
                    productModel.Desc = product.CproDesc;
                    productModel.AdditionalDesc = product.CproDescAdd;
                    productModel.Price = product.CproPrice;
                    productModel.ImagePath = product.CproImages;
                    productModel.Other = getRandomProductStatus();
                    am.ProductList.Add(productModel);
                }
            }
            return View("ProductListing", am);
        }
        #endregion

        #region ProductDescription
        public ActionResult ProductDescription(int ID,string search)
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
                productModel.ImagePath = product.CproImages;
                productModel.Other = getRandomProductStatus();

            }

            return View("ProductDescription", productModel);
        }
        #endregion

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
                    productModel.ImagePath = product.CproImages;
                    productModel.Other = getRandomProductStatus();
                    productListModel.ProductList.Add(productModel);
                }
            }
            return productListModel;
        }
    }
}