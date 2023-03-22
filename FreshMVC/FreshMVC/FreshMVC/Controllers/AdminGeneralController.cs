using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Entity.Context;
using FreshMVC.Component;
using FreshMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entity.Context.Models;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreshMVC.Controllers
{
    public class AdminGeneralController : Controller
    {
        private DbContextOptionsBuilder<DbContext> optionBuilder;
        public AdminGeneralController()
        {
            optionBuilder = new DbContextOptionsBuilder<DbContext>();
            optionBuilder.UseSqlServer(DBConn.connString);
        }

        #region AdminList
        public IActionResult AdminList(int selectedPage = 1)
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
            {
                return RedirectToAction("Login", "Admin", new
                {
                    reloadPage = true
                });
            }

            int ok;
            string msg;
            int pages = 0;
            PaginationAdminModel model = new PaginationAdminModel();
            var dsAdmin = AdminGeneralDB.GetAllAdmin(selectedPage, out pages, out ok, out msg);

            Misc.ConstructPageList(selectedPage, pages, model);

            //if the selected page is -1, then set the last selected page
            if (model.Pages.Count() != 0 && selectedPage == -1)
            {
                model.Pages.Last().Selected = true;
                selectedPage = int.Parse(model.Pages.Last().Value);
            }

            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                var am = new AdminModel();
                am.Number = dr["rownumber"].ToString();
                am.Username = dr["CUSR_USERNAME"].ToString();
                am.Name = dr["CUSR_FIRSTNAME"].ToString();
                model.AdminList.Add(am);
            }

            return PartialView("AdminList", model);
        }

        public ActionResult ModalEditAdminListData(string Username)
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
            {
                return RedirectToAction("Login", "Admin", new
                {
                    reloadPage = true
                });
            }

            var am = new AdminModel();
            if (Username != null && Username != "")
            {
                //View Existing Admin
                DataSet dsAdmin = AdminGeneralDB.GetAdminByUsername(Username, out int ok, out string msg);
                if (dsAdmin.Tables[0].Rows.Count != 0)
                {
                    am.UserID = Convert.ToInt32(dsAdmin.Tables[0].Rows[0]["CUSR_ID"]);
                    am.Username = dsAdmin.Tables[0].Rows[0]["CUSR_USERNAME"].ToString();
                    am.Name = dsAdmin.Tables[0].Rows[0]["CUSR_FIRSTNAME"].ToString();
                }
            }

            return PartialView("ModalEditAdminListData", am);
        }

        [HttpPost]
        public IActionResult EditAdminListFromDialog(AdminModel am)
        {
            try
            {
                if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
                {
                    return RedirectToAction("Login", "Admin", new
                    {
                        reloadPage = true
                    });
                }

                if (string.IsNullOrEmpty(am.Username))
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblInvalidUsername
                    });
                }

                if (string.IsNullOrEmpty(am.Name))
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblNameIsRequired
                    });
                }

                int ok;
                string msg;

                if (am.UserID == 0)
                {
                    if (string.IsNullOrEmpty(am.Password))
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(new
                        {
                            status = false,
                            message = Resources.PackBuddyShared.lblPasswordIsRequired
                        });
                    }

                    DataSet dsAdmin = AdminGeneralDB.GetAdminByUsername(am.Username, out ok, out msg);
                    if (dsAdmin.Tables[0].Rows.Count != 0)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(new
                        {
                            status = false,
                            message = Resources.PackBuddyShared.lblAdminAccExists
                        });
                    }

                    using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                    {
                        var encryptedPassword = Authentication.Encrypt(am.Password);
                        CvdUser newUsr = new CvdUser
                        {
                            CusrUsername = am.Username,
                            CusrPassword = encryptedPassword,
                            CusrPin = "",
                            CroleId = 1,
                            CcountryId = 1,
                            CusrFirstname = am.Name,
                            CusrCreatedby = HttpContext.Session.GetString("Admin"),
                            CusrUpdatedby = HttpContext.Session.GetString("Admin"),
                            CusrEmail = string.Empty
                        };

                        dbContext.CvdUser.Add(newUsr);
                        var result = dbContext.SaveChanges();

                        if (result == 0)
                        {
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            return Json(new
                            {
                                status = false,
                                message = Resources.PackBuddyShared.lblFailed
                            });
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(am.CurrentPassword) && am.IsChangePassword)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(new
                        {
                            status = false,
                            message = Resources.PackBuddyShared.lblCurrentPasswordIsRequired
                        });
                    }

                    if (string.IsNullOrEmpty(am.NewPassword) && am.IsChangePassword)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(new
                        {
                            status = false,
                            message = Resources.PackBuddyShared.lblNewPasswordIsRequired
                        });
                    }

                    if (string.IsNullOrEmpty(am.ConfirmNewPassword) && am.IsChangePassword)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(new
                        {
                            status = false,
                            message = Resources.PackBuddyShared.lblConfirmNewPasswordIsRequired
                        });
                    }

                    if (am.NewPassword != am.ConfirmNewPassword && am.IsChangePassword)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(new
                        {
                            status = false,
                            message = Resources.PackBuddyShared.lblPassNotMatch
                        });
                    }

                    using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                    {
                        var encryptedPassword = Authentication.Encrypt(am.NewPassword);
                        var foundUser = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername.ToLower() == am.Username.ToLower());
                        if (foundUser != null)
                        {
                            if (foundUser.CusrPassword != Authentication.Encrypt(am.CurrentPassword) && am.IsChangePassword)
                            {
                                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                return Json(new
                                {
                                    status = false,
                                    message = Resources.PackBuddyShared.lblCurrentPasswordNotMatch
                                });
                            }

                            foundUser.CusrFirstname = am.Name;

                            if (am.IsChangePassword)
                            {
                                foundUser.CusrPassword = encryptedPassword;
                            }

                            dbContext.SaveChanges();
                        }
                    }
                }

                return AdminList();
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
        public IActionResult DeleteAdminList(string Username = null)
        {
            try
            {
                if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
                {
                    return RedirectToAction("Login", "Admin", new
                    {
                        reloadPage = true
                    });
                }

                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var foundUser = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername.ToLower() == Username.ToLower());
                    if (foundUser != null)
                    {
                        dbContext.CvdUser.Remove(foundUser);
                        dbContext.SaveChanges();
                    }
                }

                return AdminList();
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

        #region Admin Permission
        public ActionResult AdminPermissionList(int selectedPage = 1)
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
            {
                return RedirectToAction("Login", "Admin", new
                {
                    reloadPage = true
                });
            }

            var model = RetrieveAdminList(selectedPage);
            return PartialView("AdminPermissionList", model);
        }

        public ActionResult AdminPermissionData(string username)
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
            {
                return RedirectToAction("Login", "Admin", new
                {
                    reloadPage = true
                });
            }

            //Access Right
            var adminModel = new AdminModel();
            adminModel.Username = username;

            int ok;
            int userRightOk;
            string msg;
            string userAccessRight = "";

            DataSet dsUAR = AdminGeneralDB.GetUserAccessRight(username, out userRightOk, out msg);
            userAccessRight = dsUAR.Tables[0].Rows[0][0].ToString();

            var resultList = new List<AdminAccessRightModel>();
            DataSet dsAccessRight = AdminGeneralDB.GetModulesAccessRight(out ok, out msg);
            foreach (DataRow dr in dsAccessRight.Tables[0].Rows)
            {
                var acrm = new AdminAccessRightModel();
                acrm.MainModule = Modules.GetModuleName(dr["CMDL_MAIN_MODULE"].ToString());
                acrm.Module = Modules.GetSubModuleName(dr["CMDL_NAME"].ToString());
                acrm.FunctionCode = dr["CAR_CODE"].ToString();
                acrm.FunctionName = Modules.GetSubModuleMethod(dr["CAR_CODE"].ToString());
                acrm.Function = userRightOk == -1 ? true : userAccessRight.Contains(acrm.FunctionCode);

                resultList.Add(acrm);
            }

            adminModel.AdminAccessRight = resultList;
            return PartialView("AdminPermissionData", adminModel);
        }

        [HttpPost]
        public ActionResult EditAdminPermissionMethod(string data, string userId)
        {
            try
            {
                if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
                {
                    return RedirectToAction("Login", "Admin", new
                    {
                        reloadPage = true
                    });
                }

                int ok;
                string msg;
                string[] parameters = data.Split(new string[] { "&" }, StringSplitOptions.None);

                string AccessRight = "";
                bool isClear = true;
                for (var i = 0; i < parameters.Count(); i++)
                {
                    string[] value = parameters[i].Split(new string[] { "=" }, StringSplitOptions.None);
                    if (value[0] != "Username" && value[0] != "username" && value[0].Substring(0, 5) != "Admin" && value[0] != "item.Function" && value[0] != "IsSelectAll" && value[0] != "mainModuleCheck.IsSelectAll" && value[0] != "__RequestVerificationToken")
                    {
                        AccessRight = AccessRight + value[0] + ":1;";
                    }

                    if (i > 0 && i % 100 == 0)
                    {
                        AccessRight = AccessRight.Replace("%26", "&");
                        AccessRight = AccessRight.Substring(0, AccessRight.Length - 1);
                        AdminGeneralDB.UpdateAdminAccessRight(userId, AccessRight, isClear, HttpContext.Session.GetString("Admin"), HttpContext.Session.GetString("Admin"), out ok, out msg);
                        isClear = false;
                        AccessRight = "";
                    }
                }

                if (!string.IsNullOrEmpty(AccessRight))
                {
                    AccessRight = AccessRight.Replace("%26", "&");
                    AccessRight = AccessRight.Substring(0, AccessRight.Length - 1);
                    AdminGeneralDB.UpdateAdminAccessRight(userId, AccessRight, isClear, HttpContext.Session.GetString("Admin"), HttpContext.Session.GetString("Admin"), out ok, out msg);
                }

                return AdminPermissionList();
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

        private PaginationAdminModel RetrieveAdminList(int selectedPage = 1)
        {
            int ok;
            string msg;
            int pages = 0;
            var model = new PaginationAdminModel();
            DataSet dsAdmin = AdminGeneralDB.GetAllAdmin(selectedPage, out pages, out ok, out msg);

            List<int> pageList = new List<int>();
            for (int z = 1; z <= pages; z++)
            {
                pageList.Add(z);
            }

            model.Pages = from c in pageList
                          select new SelectListItem
                          {
                              Selected = (c.ToString() == selectedPage.ToString()),
                              Text = c.ToString(),
                              Value = c.ToString()
                          };

            //if the selected page is -1, then set the last selected page
            if (model.Pages.Count() != 0 && selectedPage == -1)
            {
                model.Pages.Last().Selected = true;
                selectedPage = int.Parse(model.Pages.Last().Value);
            }

            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                var am = new AdminModel();
                am.Number = dr["rownumber"].ToString();
                am.Username = dr["CUSR_USERNAME"].ToString();
                am.Name = dr["CUSR_FIRSTNAME"].ToString();
                model.AdminList.Add(am);
            }

            return model;
        }
        #endregion

        #region UserListing
        public IActionResult UserList(int selectedPage = 1, string filterType = "", string filterValue = "")
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
            {
                return RedirectToAction("Login", "Admin", new
                {
                    reloadPage = true
                });
            }

            filterValue = Helper.NVL(filterValue);

            int ok;
            string msg;
            int pages = 0;
            PaginationAdminModel model = new PaginationAdminModel();
            var dsAdmin = AdminGeneralDB.GetAllUser(selectedPage, filterType, filterValue, out pages, out ok, out msg);

            Misc.ConstructPageList(selectedPage, pages, model);

            //if the selected page is -1, then set the last selected page
            if (model.Pages.Count() != 0 && selectedPage == -1)
            {
                model.Pages.Last().Selected = true;
                selectedPage = int.Parse(model.Pages.Last().Value);
            }

            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                var am = new AdminModel();
                am.Number = dr["rownumber"].ToString();
                am.CashWallet = dr["CUSR_CASHWLT"].ToString();
                am.Username = dr["CUSR_USERNAME"].ToString();
                am.Password = Authentication.Decrypt(dr["CUSR_PASSWORD"].ToString());
                am.AccountType = dr["CROLE_ID"].ToString() == "2" ? Resources.PackBuddyShared.lblMember : Resources.PackBuddyShared.lblAgent;
                am.IsAgent = dr["CROLE_ID"].ToString() == "3" ? true : false;

                model.AdminList.Add(am);
            }

            #region filtering
            List<SelectListItem> filterOptionList = new List<SelectListItem>();
            List<String> filterList = new List<string> { "Phone" };


            foreach (var value in filterList)
            {
                filterOptionList.Add(new SelectListItem() { Text = value, Value = value });
            }

            if (filterType == null || filterType == "")
            {
                model.SelectedFilteringCriteria = filterOptionList.First().Value;

            }
            else
            {
                model.SelectedFilteringCriteria = filterType;
            }
            model.FilterValue = Helper.NVL(filterValue);

            model.FilteringCriteria = filterOptionList;
            #endregion

            return PartialView("UserList", model);
        }

        public ActionResult ModalEditUserListData(string Username, bool isAgent = false)
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
            {
                return RedirectToAction("Login", "Admin", new
                {
                    reloadPage = true
                });
            }

            var am = new AdminModel();
            if (Username != null && Username != "")
            {
                //View Existing User
                DataSet dsAdmin = AdminGeneralDB.GetUserByUsername(Username, out int ok, out string msg);
                if (dsAdmin.Tables[0].Rows.Count != 0)
                {
                    am.UserID = Convert.ToInt32(dsAdmin.Tables[0].Rows[0]["CUSR_ID"]);
                    am.CashWallet = dsAdmin.Tables[0].Rows[0]["CUSR_CASHWLT"].ToString();
                    am.Username = dsAdmin.Tables[0].Rows[0]["CUSR_USERNAME"].ToString();
                    am.Password = Authentication.Decrypt(dsAdmin.Tables[0].Rows[0]["CUSR_PASSWORD"].ToString());
                    am.AccountType = dsAdmin.Tables[0].Rows[0]["CROLE_ID"].ToString() == "2" ? Resources.PackBuddyShared.lblMember : Resources.PackBuddyShared.lblAgent;
                    am.Name = dsAdmin.Tables[0].Rows[0]["CUSR_FIRSTNAME"].ToString();
                    am.ReferralID = dsAdmin.Tables[0].Rows[0]["CUSR_REFERRALID"].ToString();
                }
            }

            am.IsAgent = isAgent;

            return PartialView("ModalEditUserListData", am);
        }

        [HttpPost]
        public IActionResult ModalEditUserListDataMethod(AdminModel am)
        {
            try
            {
                if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
                {
                    return RedirectToAction("Login", "Admin", new
                    {
                        reloadPage = true
                    });
                }

                if (am.Username == am.ReferralID)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.msgInvalidReferral
                    });
                }

                if (string.IsNullOrEmpty(am.Username))
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblInvalidUsername
                    });
                }

                if (am.UserID == 0)
                {
                    using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                    {
                        var foundUser = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == am.Username);
                        if (foundUser != null)
                        {
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            return Json(new
                            {
                                status = false,
                                message = Resources.PackBuddyShared.msgUserExisted
                            });
                        }

                        var foundAgent = dbContext.CvdUser.FirstOrDefault(c => c.CusrUsername == am.ReferralID);

                        if (foundAgent == null)
                        {
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            return Json(new
                            {
                                status = false,
                                message = Resources.PackBuddyShared.msgInvalidReferral
                            });
                        }

                        var encryptedPassword = Authentication.Encrypt(am.Password);

                        #region countryID
                        int countryID = 1;//Malaysia default
                        if (am.Username.Substring(0, 2) == "91")
                        {
                            countryID = 15;
                        }
                        #endregion

                        CvdUser usr = new CvdUser();
                        usr.CusrUsername = am.Username;
                        usr.CusrPassword = encryptedPassword;
                        usr.CusrPin = encryptedPassword;
                        usr.CcountryId = countryID;
                        usr.CusrReferralid = am.ReferralID;
                        usr.CroleId = am.IsAgent ? 3 : 2;//2 Member 3 Agent
                        usr.CusrFirstname = am.Name;
                        usr.CusrCreatedby = "SYS";
                        usr.CusrUpdatedby = "SYS";
                        usr.CusrCreatedon = DateTime.Now;
                        usr.CusrUpdatedon = DateTime.Now;
                        usr.CusrDeletionstate = false;
                        usr.CusrRedpacketwlt = am.IsAgent ? 2000 : 0;

                        dbContext.CvdUser.Add(usr);
                        dbContext.SaveChanges();

                        //update the level 2-5
                        AdminDB.UpdateUserLevel2To5Data(am.Username);
                    }
                }
                else
                {
                    AdminGeneralDB.UpdateUser(am.Username, am.Name, am.ReferralID, HttpContext.Session.GetString("Admin"), out int ok, out string msg);
                }

                return UserList();
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

        #region RedPacketList
        public IActionResult RedPacketList(int selectedPage = 1, string filterType = "", string filterValue = "")
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
            {
                return RedirectToAction("Login", "Admin", new
                {
                    reloadPage = true
                });
            }

            filterValue = Helper.NVL(filterValue);

            int ok;
            string msg;
            int pages = 0;
            PaginationRedPacketModel model = new PaginationRedPacketModel();
            var dsAdmin = AdminGeneralDB.GetAllRedPacketListing(selectedPage, filterType, filterValue, out pages, out ok, out msg);

            Misc.ConstructPageList(selectedPage, pages, model);

            //if the selected page is -1, then set the last selected page
            if (model.Pages.Count() != 0 && selectedPage == -1)
            {
                model.Pages.Last().Selected = true;
                selectedPage = int.Parse(model.Pages.Last().Value);
            }

            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                var am = new RedPacketModel();
                am.Number = int.Parse(dr["rownumber"].ToString());
                am.ID = int.Parse(dr["CREDP_ID"].ToString());
                am.ReferenceID = dr["CREDP_REFERENCE_ID"].ToString();
                am.Agent = dr["CREDP_AGENT"].ToString();
                am.Username = dr["CUSR_USERNAME"].ToString();
                am.Amount = decimal.Parse(dr["CREDP_AMOUNT"].ToString());
                am.CreatedOn = DateTime.Parse(dr["CREDP_CREATEDON"].ToString()).ToString("dd/MM/yyyy");
                am.ClaimedOn = dr["CREDP_CLAIMEDON"] == DBNull.Value ? "" : DateTime.Parse(dr["CREDP_CLAIMEDON"].ToString()).ToString("dd/MM/yyyy");
                am.Status = dr["CREDP_STATUS"].ToString() == "0" ? Resources.PackBuddyShared.lblNotUsed : Resources.PackBuddyShared.lblUsed;

                model.List.Add(am);
            }

            #region filtering
            List<SelectListItem> filterOptionList = new List<SelectListItem>();
            List<String> filterList = new List<string> { "Agent", "Username" };

            foreach (var value in filterList)
            {
                filterOptionList.Add(new SelectListItem() { Text = value, Value = value });
            }

            if (filterType == null || filterType == "")
            {
                model.SelectedFilteringCriteria = filterOptionList.First().Value;

            }
            else
            {
                model.SelectedFilteringCriteria = filterType;
            }
            model.FilterValue = Helper.NVL(filterValue);

            model.FilteringCriteria = filterOptionList;
            #endregion

            return PartialView("RedPacketList", model);
        }
        #endregion

        #region GameListing
        public IActionResult GameListing(int selectedPage = 1, string filterType = "", string filterValue = "")
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
            {
                return RedirectToAction("Login", "Admin", new
                {
                    reloadPage = true
                });
            }

            filterValue = Helper.NVL(filterValue);

            int ok;
            string msg;
            int pages = 0;
            PaginationGameModel model = new PaginationGameModel();
            var dsAdmin = AdminGeneralDB.GetAllGame(selectedPage, filterType, filterValue, out pages, out ok, out msg);

            Misc.ConstructPageList(selectedPage, pages, model);

            //if the selected page is -1, then set the last selected page
            if (model.Pages.Count() != 0 && selectedPage == -1)
            {
                model.Pages.Last().Selected = true;
                selectedPage = int.Parse(model.Pages.Last().Value);
            }

            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                var am = new GameModel();
                am.Number = int.Parse(dr["rownumber"].ToString());
                am.ID = int.Parse(dr["CGAME_ID"].ToString());
                am.Name = dr["CGAME_NAME"].ToString();
                am.Status = dr["CGAME_STATUS"].ToString() == "1" ? Resources.PackBuddyShared.lblActive : Resources.PackBuddyShared.lblInActive;

                string basePath = dr["CGAME_IMAGEPATH"] == DBNull.Value ? "" : "/Uploads/Gamez/" + dr["CGAME_IMAGEPATH"].ToString();
                am.ImagePath = basePath;

                am.Type = dr["CGAME_TYPE"].ToString() == "0" ? Resources.PackBuddyShared.lblBeauty : Resources.PackBuddyShared.lblBeast;

                model.List.Add(am);
            }

            #region filtering
            List<SelectListItem> filterOptionList = new List<SelectListItem>();
            List<String> filterList = new List<string> { "Name" };


            foreach (var value in filterList)
            {
                filterOptionList.Add(new SelectListItem() { Text = value, Value = value });
            }

            if (filterType == null || filterType == "")
            {
                model.SelectedFilteringCriteria = filterOptionList.First().Value;

            }
            else
            {
                model.SelectedFilteringCriteria = filterType;
            }
            model.FilterValue = Helper.NVL(filterValue);

            model.FilteringCriteria = filterOptionList;
            #endregion

            return PartialView("GameListing", model);
        }

        public ActionResult ModalEditGameListData(int id = 0)
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
            {
                return RedirectToAction("Login", "Admin", new
                {
                    reloadPage = true
                });
            }

            var am = new GameModel();
            am.Duration = 180;//default 180

            if (id != 0)
            {
                //View Existing User
                DataSet dsAdmin = AdminGeneralDB.GetGameByID(id, out int ok, out string msg);
                if (dsAdmin.Tables[0].Rows.Count != 0)
                {
                    am.ID = Convert.ToInt32(dsAdmin.Tables[0].Rows[0]["CGAME_ID"]);
                    am.Name = dsAdmin.Tables[0].Rows[0]["CGAME_NAME"].ToString();
                    am.Status = dsAdmin.Tables[0].Rows[0]["CGAME_STATUS"].ToString() == "1" ? Resources.PackBuddyShared.lblActive : Resources.PackBuddyShared.lblInActive;
                    am.SelectedStatus = dsAdmin.Tables[0].Rows[0]["CGAME_STATUS"].ToString();
                    string basePath = dsAdmin.Tables[0].Rows[0]["CGAME_IMAGEPATH"] == DBNull.Value ? "" : "/Uploads/Gamez/" + dsAdmin.Tables[0].Rows[0]["CGAME_IMAGEPATH"].ToString();
                    am.ImagePath = basePath;

                    am.Type = dsAdmin.Tables[0].Rows[0]["CGAME_TYPE"].ToString() == "0" ? Resources.PackBuddyShared.lblBeauty : Resources.PackBuddyShared.lblBeast;
                    am.SelectedGameType = dsAdmin.Tables[0].Rows[0]["CGAME_TYPE"].ToString();
                    am.No9Return = decimal.Parse(dsAdmin.Tables[0].Rows[0]["CGAME_NO9_RETURN"].ToString());
                    am.No8Return = decimal.Parse(dsAdmin.Tables[0].Rows[0]["CGAME_NO8_RETURN"].ToString());
                    am.No7Return = decimal.Parse(dsAdmin.Tables[0].Rows[0]["CGAME_NO7_RETURN"].ToString());
                    am.No6Return = decimal.Parse(dsAdmin.Tables[0].Rows[0]["CGAME_NO6_RETURN"].ToString());
                    am.No5Return = decimal.Parse(dsAdmin.Tables[0].Rows[0]["CGAME_NO5_RETURN"].ToString());
                    am.No4Return = decimal.Parse(dsAdmin.Tables[0].Rows[0]["CGAME_NO4_RETURN"].ToString());
                    am.No3Return = decimal.Parse(dsAdmin.Tables[0].Rows[0]["CGAME_NO3_RETURN"].ToString());
                    am.No2Return = decimal.Parse(dsAdmin.Tables[0].Rows[0]["CGAME_NO2_RETURN"].ToString());
                    am.No1Return = decimal.Parse(dsAdmin.Tables[0].Rows[0]["CGAME_NO1_RETURN"].ToString());
                    am.No0Return = decimal.Parse(dsAdmin.Tables[0].Rows[0]["CGAME_NO0_RETURN"].ToString());

                    am.RedReturn = decimal.Parse(dsAdmin.Tables[0].Rows[0]["CGAME_RED_RETURN"].ToString());
                    am.VioletReturn = decimal.Parse(dsAdmin.Tables[0].Rows[0]["CGAME_VIOLET_RETURN"].ToString());
                    am.Red0Return = decimal.Parse(dsAdmin.Tables[0].Rows[0]["CGAME_RED0_RETURN"].ToString());
                    am.GreenReturn = decimal.Parse(dsAdmin.Tables[0].Rows[0]["CGAME_GREEN_RETURN"].ToString());
                    am.Green5Return = decimal.Parse(dsAdmin.Tables[0].Rows[0]["CGAME_GREEN5_RETURN"].ToString());
                    am.Duration = int.Parse(dsAdmin.Tables[0].Rows[0]["CGAME_DURATION_SECONDS"].ToString());
                }
            }

            #region Construct Game Type
            var gametypes = Misc.ConstructGameType();

            am.GameTypeList = from c in gametypes select new SelectListItem { Selected = false, Text = c.Text, Value = c.Value };
            #endregion

            #region Construct Status
            var statusz = Misc.ConstructStatus();

            am.StatusList = from c in statusz select new SelectListItem { Selected = false, Text = c.Text, Value = c.Value };
            #endregion

            return PartialView("ModalEditGameListData", am);
        }

        [HttpPost]
        public IActionResult ModalEditGameListDataMethod(GameModel am)
        {
            try
            {
                if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
                {
                    return RedirectToAction("Login", "Admin", new
                    {
                        reloadPage = true
                    });
                }

                if (Helper.NVL(am.Name) == "")
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.msgInvalidName
                    });
                }

                if(Helper.NVLInteger(am.Duration) < 60)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.msgDurationMustBeLongerThan60Seconds
                    });
                }

                if (am.ID == 0)
                {
                    using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                    {
                        CvdGame temp = new CvdGame();

                        temp.CgameName = am.Name;
                        temp.CgameStatus = int.Parse(am.SelectedStatus);
                        temp.CgameType = int.Parse(am.SelectedGameType);
                        temp.CgameNo9Return = am.No9Return;
                        temp.CgameNo8Return = am.No8Return;
                        temp.CgameNo7Return = am.No7Return;
                        temp.CgameNo6Return = am.No6Return;
                        temp.CgameNo5Return = am.No5Return;
                        temp.CgameNo4Return = am.No4Return;
                        temp.CgameNo3Return = am.No3Return;
                        temp.CgameNo2Return = am.No2Return;
                        temp.CgameNo1Return = am.No1Return;
                        temp.CgameNo0Return = am.No0Return;
                        temp.CgameRedReturn = am.RedReturn;
                        temp.CgameRed0Return = am.Red0Return;
                        temp.CgameVioletReturn = am.VioletReturn;
                        temp.CgameGreenReturn = am.GreenReturn;
                        temp.CgameGreen5Return = am.Green5Return;
                        temp.CgameDurationSeconds = am.Duration;

                        if (am.ProductImage != null && am.ProductImage.FileName != string.Empty)
                        {
                            string directoryBasePath = Path.Combine("Uploads", "Gamez");
                            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directoryBasePath);
                            if (Directory.Exists(basePath) == false)
                            {
                                Directory.CreateDirectory(basePath);
                            }

                            string ranAlphaNum = Misc.GenerateRandomAlphNumeric(5);
                            string filename = string.Format("{1}_{0}", am.ProductImage.FileName, ranAlphaNum);

                            string path = Path.Combine(basePath, filename);

                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                am.ProductImage.CopyTo(stream);
                            }

                            temp.CgameImagepath = filename;
                        }

                        dbContext.CvdGame.Add(temp);
                        dbContext.SaveChanges();

                        AdminGeneralDB.GenerateGameSessionByID(temp.CgameId);
                    }
                }
                else
                {
                    using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                    {
                        var found = dbContext.CvdGame.FirstOrDefault(c => c.CgameId == am.ID);
                        if (found != null)
                        {
                            found.CgameName = am.Name;
                            found.CgameStatus = int.Parse(am.SelectedStatus);
                            found.CgameType = int.Parse(am.SelectedGameType);
                            found.CgameNo9Return = am.No9Return;
                            found.CgameNo8Return = am.No8Return;
                            found.CgameNo7Return = am.No7Return;
                            found.CgameNo6Return = am.No6Return;
                            found.CgameNo5Return = am.No5Return;
                            found.CgameNo4Return = am.No4Return;
                            found.CgameNo3Return = am.No3Return;
                            found.CgameNo2Return = am.No2Return;
                            found.CgameNo1Return = am.No1Return;
                            found.CgameNo0Return = am.No0Return;
                            found.CgameRedReturn = am.RedReturn;
                            found.CgameRed0Return = am.Red0Return;
                            found.CgameVioletReturn = am.VioletReturn;
                            found.CgameGreenReturn = am.GreenReturn;
                            found.CgameGreen5Return = am.Green5Return;
                            //found.CgameDurationSeconds = am.Duration;//we do not allow admin to change duration once game created

                            if (am.ProductImage != null && am.ProductImage.FileName != string.Empty)
                            {
                                string directoryBasePath = Path.Combine("Uploads", "Gamez");
                                string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directoryBasePath);
                                if (Directory.Exists(basePath) == false)
                                {
                                    Directory.CreateDirectory(basePath);
                                }

                                string ranAlphaNum = Misc.GenerateRandomAlphNumeric(5);
                                string filename = string.Format("{1}_{0}", am.ProductImage.FileName, ranAlphaNum);

                                string path = Path.Combine(basePath, filename);

                                using (var stream = new FileStream(path, FileMode.Create))
                                {
                                    am.ProductImage.CopyTo(stream);
                                }

                                found.CgameImagepath = filename;
                            }

                            dbContext.SaveChanges();
                        }
                    }
                }

                return GameListing();
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

        #region ProdutListing
        public IActionResult ProductListing(int selectedPage = 1, string filterType = "", string filterValue = "")
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
            {
                return RedirectToAction("Login", "Admin", new
                {
                    reloadPage = true
                });
            }

            filterValue = Helper.NVL(filterValue);

            int ok;
            string msg;
            int pages = 0;
            var model = new PaginationProductModel();
            var dsAdmin = AdminGeneralDB.GetAllProducts(selectedPage, filterType, filterValue, out pages, out ok, out msg);

            Misc.ConstructPageList(selectedPage, pages, model);

            //if the selected page is -1, then set the last selected page
            if (model.Pages.Count() != 0 && selectedPage == -1)
            {
                model.Pages.Last().Selected = true;
                selectedPage = int.Parse(model.Pages.Last().Value);
            }

            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                var am = new ProductModel();
                am.Number = int.Parse(dr["rownumber"].ToString());
                am.id = int.Parse(dr["CPRO_ID"].ToString());
                am.Title = dr["CPRO_TITLE"].ToString();

                string basePath = dr["CPRO_IMAGES"] == DBNull.Value ? "" : "/Uploads/Products/" + dr["CPRO_IMAGES"].ToString();
                am.ImagePath = basePath;
                model.List.Add(am);
            }

            #region filtering
            List<SelectListItem> filterOptionList = new List<SelectListItem>();
            List<String> filterList = new List<string> { "Name" };


            foreach (var value in filterList)
            {
                filterOptionList.Add(new SelectListItem() { Text = value, Value = value });
            }

            if (filterType == null || filterType == "")
            {
                model.SelectedFilteringCriteria = filterOptionList.First().Value;

            }
            else
            {
                model.SelectedFilteringCriteria = filterType;
            }
            model.FilterValue = Helper.NVL(filterValue);

            model.FilteringCriteria = filterOptionList;
            #endregion

            return PartialView("ProductListing", model);
        }

        public ActionResult ModalEditProductData(int id = 0)
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
            {
                return RedirectToAction("Login", "Admin", new
                {
                    reloadPage = true
                });
            }

            var am = new ProductModel();

            if (id != 0)
            {
                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var productFound = dbContext.CvdProduct.FirstOrDefault(c => c.CproId == id);
                    am.Title = productFound.CproTitle;
                    am.Desc = productFound.CproDesc;
                    am.AdditionalDesc = productFound.CproDescAdd;
                    am.ImagePath = string.Format("{0}/{1}", Misc._baseUrl, productFound.CproImages);
                    am.Price = productFound.CproPrice;
                }
            }

            return PartialView("ModalEditProductData", am);
        }

        [HttpPost]
        public IActionResult ModalEditProductDataMethod(ProductModel am)
        {
            try
            {
                if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
                {
                    return RedirectToAction("Login", "Admin", new
                    {
                        reloadPage = true
                    });
                }

                if (string.IsNullOrEmpty(am.Title))
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblInvalidTitle
                    });
                }

                string directoryBasePath = Path.Combine(Path.Combine(Path.Combine("Uploads", "Products"), am.Title.Replace(" ", "")));
                string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directoryBasePath);

                if (am.ImagePhoto != null && am.ImagePhoto.FileName != string.Empty)
                {
                    if (Directory.Exists(basePath) == false)
                    {
                        Directory.CreateDirectory(basePath);
                    }

                    string path = Path.Combine(basePath, am.ImagePhoto.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        am.ImagePhoto.CopyTo(stream);
                    }

                    am.ImagePath = am.ImagePhoto.FileName;
                }
                else if (am.ImagePhoto != null)
                {
                    am.ImagePath = am.ImagePhoto.FileName;
                }
                else if ((am.ImagePath == "" || am.ImagePath == null) && am.Number == 0)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblInvalidImage
                    });
                }

                if (am.id == 0)
                {
                    using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                    {
                        var product = new CvdProduct();

                        product.CproTitle = am.Title;
                        product.CproDesc = am.Desc;
                        product.CproDescAdd = am.AdditionalDesc;
                        product.CproImages = am.ImagePath;
                        product.CproPrice = am.Price;

                        dbContext.CvdProduct.Add(product);
                        dbContext.SaveChanges();
                    }
                }
                else
                {
                    using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                    {
                        var product = dbContext.CvdProduct.FirstOrDefault(c => c.CproId == am.id);
                        if (product != null)
                        {
                            product.CproTitle = am.Title;
                            product.CproDesc = am.Desc;
                            product.CproDescAdd = am.AdditionalDesc;
                            product.CproImages = am.ImagePath;
                            product.CproPrice = am.Price;
                            dbContext.SaveChanges();
                        }
                    }
                }

                return ProductListing();
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
        public IActionResult DeleteProduct(int idz = 0)
        {
            try
            {
                if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
                {
                    return RedirectToAction("Login", "Admin", new
                    {
                        reloadPage = true
                    });
                }

                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var product = dbContext.CvdProduct.FirstOrDefault(c => c.CproId == idz);
                    if (product != null)
                    {
                        dbContext.CvdProduct.Remove(product);
                        dbContext.SaveChanges();
                    }
                }

                return ProductListing();
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

        #region GameSessionListing
        public IActionResult GameSessionListing(int selectedPage = 1)
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
            {
                return RedirectToAction("Login", "Admin", new
                {
                    reloadPage = true
                });
            }

            int ok;
            string msg;
            int pages = 0;
            AdminGameMainModel model = new AdminGameMainModel();

            var dsAdmin = AdminGeneralDB.GetAllGame(selectedPage, "", "", out pages, out ok, out msg);

            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                AdminGameModel gameModel = new AdminGameModel();
                gameModel.GameID = int.Parse(dr["CGAME_ID"].ToString());
                gameModel.GameName = dr["CGAME_NAME"].ToString();

                var dsGameSession = AdminGeneralDB.GetAllGameSessionListingByID(selectedPage, gameModel.GameID, out pages, out ok, out msg);

                //multiple tab lets not do the fucking pagination ba
                //Misc.ConstructPageList(selectedPage, pages, model);

                //if the selected page is -1, then set the last selected page
                //if (model.Pages.Count() != 0 && selectedPage == -1)
                //{
                //    model.Pages.Last().Selected = true;
                //    selectedPage = int.Parse(model.Pages.Last().Value);
                //}

                foreach (DataRow drSession in dsGameSession.Tables[0].Rows)
                {
                    var sessionModel = new SessionGameModel();
                    sessionModel.Number = int.Parse(drSession["rownumber"].ToString());
                    sessionModel.Period = drSession["CGAME_PERIOD"].ToString();
                    sessionModel.Result = drSession["CGAME_RESULT"].ToString();
                    sessionModel.Start = DateTime.Parse(drSession["CGAME_START"].ToString()).ToString("dd/MM/yyyy HH:mm");
                    sessionModel.End = DateTime.Parse(drSession["CGAME_END"].ToString()).ToString("dd/MM/yyyy HH:mm");
                    sessionModel.GameState = int.Parse(drSession["CGAME_STATUS"].ToString());

                    gameModel.SessionList.Add(sessionModel);
                }

                model.GameList.Add(gameModel);
            }

            return PartialView("GameSessionListing", model);
        }

        [HttpPost]
        public IActionResult ModelEditRedPacketMethod(int gameid, string period, int number)
        {
            try
            {
                if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
                {
                    return RedirectToAction("Login", "Admin", new
                    {
                        reloadPage = true
                    });
                }

                if (number >= 0 && number <= 9)
                {
                    using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                    {
                        //we only allow admin to update the game where it's running now
                        var found = dbContext.CvdGameSession.FirstOrDefault(c => c.CgameId == gameid && c.CgamePeriod == period && c.CgameStatus == 1);
                        if (found != null)
                        {
                            found.CgameResult = number;
                            dbContext.SaveChanges();
                        }
                    }
                }              
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.msgInvalidResultNumber
                    });
                }

                return GameSessionListing();
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

        #region LotteryBetListing
        public IActionResult LotteryBetListing(int selectedPage = 1, string filterType = "", string filterValue = "")
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
            {
                return RedirectToAction("Login", "Admin", new
                {
                    reloadPage = true
                });
            }

            filterValue = Helper.NVL(filterValue);

            int ok;
            string msg;
            int pages = 0;
            PaginationGameModel model = new PaginationGameModel();
            var dsAdmin = AdminGeneralDB.GetLotteryBetListing(selectedPage, filterType, filterValue, out pages, out ok, out msg);

            Misc.ConstructPageList(selectedPage, pages, model);

            //if the selected page is -1, then set the last selected page
            if (model.Pages.Count() != 0 && selectedPage == -1)
            {
                model.Pages.Last().Selected = true;
                selectedPage = int.Parse(model.Pages.Last().Value);
            }

            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                var am = new GameModel();
                am.Number = int.Parse(dr["rownumber"].ToString());
                am.Name = dr["CGAME_NAME"].ToString();
                am.Period = dr["CGAME_PERIOD"].ToString();
                am.Username = dr["CUSR_USERNAME"].ToString();
                am.Number = int.Parse(dr["CGAME_NUMBER"].ToString());

                am.NumberString = am.Number.ToString();

                if (am.Number == 55)
                {
                    am.NumberString = Resources.PackBuddyShared.lblJoinGreen;
                }
                else if (am.Number == 66)
                {
                    am.NumberString = Resources.PackBuddyShared.lblJoinViolet;
                }
                else if (am.Number == 77)
                {
                    am.NumberString = Resources.PackBuddyShared.lblJoinRed;
                }

                am.BetAmount = decimal.Parse(dr["CGAME_AMOUNT"].ToString());
                am.WinAmount = decimal.Parse(dr["CGAME_WIN_AMOUNT"].ToString());

                model.List.Add(am);
            }

            #region filtering
            List<SelectListItem> filterOptionList = new List<SelectListItem>();
            List<String> filterList = new List<string> { "Username" };


            foreach (var value in filterList)
            {
                filterOptionList.Add(new SelectListItem() { Text = value, Value = value });
            }

            if (filterType == null || filterType == "")
            {
                model.SelectedFilteringCriteria = filterOptionList.First().Value;

            }
            else
            {
                model.SelectedFilteringCriteria = filterType;
            }
            model.FilterValue = Helper.NVL(filterValue);

            model.FilteringCriteria = filterOptionList;
            #endregion

            return PartialView("LotteryBetListing", model);
        }
        #endregion

        #region BannerListing
        public IActionResult BannerListing(int selectedPage = 1)
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
            {
                return RedirectToAction("Login", "Admin", new
                {
                    reloadPage = true
                });
            }

            int ok;
            string msg;
            int pages = 0;
            var model = new PaginationBannerModel();
            var dsAdmin = AdminGeneralDB.GetAllBanners(selectedPage, out pages, out ok, out msg);

            Misc.ConstructPageList(selectedPage, pages, model);

            //if the selected page is -1, then set the last selected page
            if (model.Pages.Count() != 0 && selectedPage == -1)
            {
                model.Pages.Last().Selected = true;
                selectedPage = int.Parse(model.Pages.Last().Value);
            }

            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                var am = new BannerModel();
                am.Number = int.Parse(dr["rownumber"].ToString());
                am.id = int.Parse(dr["CBANNER_ID"].ToString());
                am.Title = dr["CBANNER_TITLE"].ToString();

                string basePath = dr["CBANNER_IMAGES"] == DBNull.Value ? "" : "/Uploads/Banners/" + dr["CBANNER_IMAGES"].ToString();
                am.ProtraitPhotoPath = basePath;
                model.BannerList.Add(am);
            }

            return PartialView("BannerListing", model);
        }

        public ActionResult ModalEditBannerData(int id = 0)
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
            {
                return RedirectToAction("Login", "Admin", new
                {
                    reloadPage = true
                });
            }

            var am = new BannerModel();

            if (id != 0)
            {
                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var bannerFound = dbContext.CvdBanner.FirstOrDefault(c => c.CbannerId == id);
                    am.Title = bannerFound.CbannerTitle;
                    am.ProtraitPhotoPath = string.Format("{0}/{1}", Misc._baseUrl, bannerFound.CbannerImages);
                }
            }
            return PartialView("ModalEditBannerData", am);
        }

        [HttpPost]
        public IActionResult ModalEditBannerDataMethod(BannerModel am)
        {
            try
            {
                if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
                {
                    return RedirectToAction("Login", "Admin", new
                    {
                        reloadPage = true
                    });
                }

                if (string.IsNullOrEmpty(am.Title))
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblInvalidTitle
                    });
                }

                string directoryBasePath = Path.Combine(Path.Combine("Uploads", "Banners"));
                string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directoryBasePath);

                if (am.ProtraitPhoto != null && am.ProtraitPhoto.FileName != string.Empty)
                {
                    if (Directory.Exists(basePath) == false)
                    {
                        Directory.CreateDirectory(basePath);
                    }

                    string path = Path.Combine(basePath, am.ProtraitPhoto.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        am.ProtraitPhoto.CopyTo(stream);
                    }

                    am.ProtraitPhotoPath = am.ProtraitPhoto.FileName;
                }
                else if (am.ProtraitPhoto != null)
                {
                    am.ProtraitPhotoPath = am.ProtraitPhoto.FileName;
                }
                else if ((am.ProtraitPhotoPath == "" || am.ProtraitPhotoPath == null) && am.Number == 0)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblInvalidImage
                    });
                }

                if (am.id == 0)
                {
                    using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                    {
                        var banner = new CvdBanner();
                        banner.CbannerTitle = am.Title;
                        banner.CbannerImages = am.ProtraitPhotoPath;

                        dbContext.CvdBanner.Add(banner);
                        dbContext.SaveChanges();
                    }
                }
                else
                {
                    using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                    {
                        var banner = dbContext.CvdBanner.FirstOrDefault(c => c.CbannerId == am.id);
                        if (banner != null)
                        {
                            banner.CbannerTitle = am.Title;
                            banner.CbannerImages = am.ProtraitPhotoPath;
                            dbContext.SaveChanges();
                        }
                    }
                }

                return BannerListing();
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
        public IActionResult DeleteBanner(int idz = 0)
        {
            try
            {
                if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
                {
                    return RedirectToAction("Login", "Admin", new
                    {
                        reloadPage = true
                    });
                }

                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var banner = dbContext.CvdBanner.FirstOrDefault(c => c.CbannerId == idz);
                    if (banner != null)
                    {
                        dbContext.CvdBanner.Remove(banner);
                        dbContext.SaveChanges();
                    }
                }
                return BannerListing();
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

        #region RechargeListing
        public IActionResult RechargeListing(int selectedPage = 1, string filterType = "", string filterValue = "", string fromDate = "", string toDate = "")
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
            {
                return RedirectToAction("Login", "Admin", new
                {
                    reloadPage = true
                });
            }

            filterValue = Helper.NVL(filterValue);
            string fromDateInput = fromDate;
            string toDateInput = toDate;
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime sd = Convert.ToDateTime(DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                DateTime ed = Convert.ToDateTime(DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));

                fromDate = sd.ToString("yyyy-MM-dd 00:00:00.000");
                toDate = ed.ToString("yyyy-MM-dd 23:59:59.999");
            }

            int ok;
            string msg;
            var model = new PaginationPaymentModel();
            int pages = 0;
            var dsAdmin = AdminGeneralDB.GetAllRechargeList(selectedPage, filterType, filterValue, fromDate, toDate, out pages, out ok, out msg);

            Misc.ConstructPageList(selectedPage, pages, model);

            //if the selected page is -1, then set the last selected page
            if (model.Pages.Count() != 0 && selectedPage == -1)
            {
                model.Pages.Last().Selected = true;
                selectedPage = int.Parse(model.Pages.Last().Value);
            }

            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                var am = new PaymentModel();
                am.Number = int.Parse(dr["rownumber"].ToString());
                am.id = dr["CCASH_ID"].ToString();
                am.Username = dr["CUSR_USERNAME"].ToString();
                am.RefNo = dr["CCASH_APPOTHER"].ToString();
                am.Amount = dr["CCASH_CASHIN"].ToString();
                am.Created = DateTime.Parse(dr["CCASH_CREATEDON"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                am.AccountName = dr["CUSR_FIRSTNAME"].ToString();
                am.MerchantCode = dr["CUSR_REFERRALID"].ToString();
                am.Status = dr["CCASH_STATUS"].ToString() == "0" ? Resources.PackBuddyShared.lblInProgress : dr["CCASH_STATUS"].ToString() == "1" ? Resources.PackBuddyShared.lblSuccess : Resources.PackBuddyShared.lblFailed;
                model.List.Add(am);
            }

            #region filtering
            List<SelectListItem> filterOptionList = new List<SelectListItem>();
            List<String> filterList = new List<string> { "Name" };


            foreach (var value in filterList)
            {
                filterOptionList.Add(new SelectListItem() { Text = value, Value = value });
            }

            if (filterType == null || filterType == "")
            {
                model.SelectedFilteringCriteria = filterOptionList.First().Value;

            }
            else
            {
                model.SelectedFilteringCriteria = filterType;
            }
            model.FilterValue = Helper.NVL(filterValue);

            model.FilteringCriteria = filterOptionList;

            model.FromDate = fromDateInput;
            model.ToDate = toDateInput;
            #endregion

            return PartialView("RechargeListing", model);
        }

        [HttpPost]
        public IActionResult ApproveTransaction(int idz = 0)
        {
            try
            {
                if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
                {
                    return RedirectToAction("Login", "Admin", new
                    {
                        reloadPage = true
                    });
                }

                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var product = dbContext.CvdCashwalletlogtemp.FirstOrDefault(c => c.CcashId == idz);
                    if (product != null)
                    {
                        product.CcashStatus = 1;
                        dbContext.CvdCashwalletlogtemp.Update(product);
                        dbContext.SaveChanges();

                        AdminDB.CashWalletOperation(product.CusrUsername, product.CcashCashin, "Recharge", 0, "", product.CcashAppother, "1");

                    }
                }

                return RechargeListing();
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
        public IActionResult RejectTransaction(int idz = 0)
        {
            try
            {
                if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
                {
                    return RedirectToAction("Login", "Admin", new
                    {
                        reloadPage = true
                    });
                }

                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var product = dbContext.CvdCashwalletlogtemp.FirstOrDefault(c => c.CcashId == idz);
                    if (product != null)
                    {
                        product.CcashStatus = -1;
                        dbContext.CvdCashwalletlogtemp.Update(product);
                        dbContext.SaveChanges();
                    }
                }

                return WithdrawalListing();
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

        public IActionResult DownloadRechargeListing(int selectedPage = 1, string filterType = "", string filterValue = "", string fromDate = "", string toDate = "")
        {
            try
            {
                if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
                {
                    return RedirectToAction("Login", "Admin", new
                    {
                        reloadPage = true
                    });
                }

                filterValue = Helper.NVL(filterValue);
                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    DateTime sd = Convert.ToDateTime(DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    DateTime ed = Convert.ToDateTime(DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));

                    fromDate = sd.ToString("yyyy-MM-dd 00:00:00.000");
                    toDate = ed.ToString("yyyy-MM-dd 23:59:59.999");
                }

                int ok;
                string msg;
                var model = new PaginationPaymentModel();
                int pages = 0;
                var dsAdmin = AdminGeneralDB.GetAllRechargeList(selectedPage, filterType, filterValue, fromDate, toDate, out pages, out ok, out msg);

                byte[] fileContents;

                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage excelPackage = new ExcelPackage();
                var workSheet = excelPackage.Workbook.Worksheets.Add("Report");
                workSheet.DefaultRowHeight = 12;

                //Header of table  
                //  
                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cells[1, 1].Value = "#";
                workSheet.Cells[1, 2].Value = FreshMVC.Resources.PackBuddyShared.lblReferenceID;
                workSheet.Cells[1, 3].Value = FreshMVC.Resources.PackBuddyShared.lblUsername;
                workSheet.Cells[1, 4].Value = FreshMVC.Resources.PackBuddyShared.lblName;
                workSheet.Cells[1, 5].Value = FreshMVC.Resources.PackBuddyShared.lblPhone;
                workSheet.Cells[1, 6].Value = FreshMVC.Resources.PackBuddyShared.lblRecharge;
                workSheet.Cells[1, 7].Value = FreshMVC.Resources.PackBuddyShared.lblRechargeTime;
                workSheet.Cells[1, 8].Value = FreshMVC.Resources.PackBuddyShared.lblAgent;
                workSheet.Cells[1, 9].Value = FreshMVC.Resources.PackBuddyShared.lblStatus;

                int recordIndex = 2;
                string message = string.Empty;

                foreach (DataRow dr in dsAdmin.Tables[0].Rows)
                {
                    var am = new PaymentModel();
                    am.Number = int.Parse(dr["rownumber"].ToString());
                    am.id = dr["CCASH_ID"].ToString();
                    am.Username = dr["CUSR_USERNAME"].ToString();
                    am.RefNo = dr["CCASH_APPOTHER"].ToString();
                    am.Amount = dr["CCASH_CASHIN"].ToString();
                    am.Created = DateTime.Parse(dr["CCASH_CREATEDON"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                    am.AccountName = dr["CUSR_FIRSTNAME"].ToString();
                    am.MerchantCode = dr["CUSR_REFERRALID"].ToString();
                    am.Status = dr["CCASH_STATUS"].ToString() == "0" ? Resources.PackBuddyShared.lblInProgress : dr["CCASH_STATUS"].ToString() == "1" ? Resources.PackBuddyShared.lblSuccess : Resources.PackBuddyShared.lblFailed;

                    workSheet.Row(recordIndex).Height = 20;
                    workSheet.Row(recordIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                    workSheet.Cells[recordIndex, 1].Value = am.Number;
                    workSheet.Cells[recordIndex, 2].Value = am.RefNo;
                    workSheet.Cells[recordIndex, 3].Value = am.Username;
                    workSheet.Cells[recordIndex, 4].Value = am.AccountName;
                    workSheet.Cells[recordIndex, 5].Value = am.Username;
                    workSheet.Cells[recordIndex, 6].Value = am.Amount;
                    workSheet.Cells[recordIndex, 7].Value = am.Created;
                    workSheet.Cells[recordIndex, 8].Value = am.MerchantCode;
                    workSheet.Cells[recordIndex, 9].Value = am.Status;

                    recordIndex++;
                }

                fileContents = excelPackage.GetAsByteArray();

                if (fileContents == null || fileContents.Length == 0)
                {
                    return NotFound();
                }

                return File(
                   fileContents: fileContents,
                   contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                   fileDownloadName: string.Format("RechargeListing_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmss"))
               );
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

        #region WithdrawalListing
        public IActionResult WithdrawalListing(int selectedPage = 1, string filterType = "", string filterValue = "", string fromDate = "", string toDate = "")
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
            {
                return RedirectToAction("Login", "Admin", new
                {
                    reloadPage = true
                });
            }

            filterValue = Helper.NVL(filterValue);
            string fromDateInput = fromDate;
            string toDateInput = toDate;

            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                DateTime sd = Convert.ToDateTime(DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                DateTime ed = Convert.ToDateTime(DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));

                fromDate = sd.ToString("yyyy-MM-dd 00:00:00.000");
                toDate = ed.ToString("yyyy-MM-dd 23:59:59.999");
            }

            int ok;
            string msg;
            var model = new PaginationPaymentModel();
            int pages = 0;
            var dsAdmin = AdminGeneralDB.GetAllWithdrawalList(selectedPage, filterType, filterValue, fromDate, toDate, out pages, out ok, out msg);

            Misc.ConstructPageList(selectedPage, pages, model);

            //if the selected page is -1, then set the last selected page
            if (model.Pages.Count() != 0 && selectedPage == -1)
            {
                model.Pages.Last().Selected = true;
                selectedPage = int.Parse(model.Pages.Last().Value);
            }

            foreach (DataRow dr in dsAdmin.Tables[0].Rows)
            {
                var am = new PaymentModel();
                am.Number = int.Parse(dr["rownumber"].ToString());
                am.id = dr["CCASH_ID"].ToString();
                am.Username = dr["CUSR_USERNAME"].ToString();
                am.RefNo = dr["CCASH_APPOTHER"].ToString();
                am.Amount = dr["CCASH_CASHOUT"].ToString();
                am.ServiceFee = dr["CCASH_APPRATE"].ToString();
                am.FinalAmount = (decimal.Parse(dr["CCASH_CASHOUT"].ToString()) - decimal.Parse(dr["CCASH_APPRATE"].ToString())).ToString("#0.00");
                am.Created = DateTime.Parse(dr["CCASH_CREATEDON"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                am.BalanceBeforeWdr = (decimal.Parse(dr["CCASH_WALLET"].ToString()) + decimal.Parse(dr["CCASH_CASHOUT"].ToString())).ToString("#0.00");
                am.BalanceAfterWdr = (decimal.Parse(dr["CCASH_WALLET"].ToString())).ToString("#0.00");
                am.AccountName = dr["CUSR_FIRSTNAME"].ToString();
                am.MerchantCode = dr["CUSR_REFERRALID"].ToString();
                am.Status = dr["CCASH_STATUS"].ToString() == "0" ? Resources.PackBuddyShared.lblInProgress : dr["CCASH_STATUS"].ToString() == "1" ? Resources.PackBuddyShared.lblSuccess : Resources.PackBuddyShared.lblFailed;
                model.List.Add(am);
            }

            #region filtering
            List<SelectListItem> filterOptionList = new List<SelectListItem>();
            List<String> filterList = new List<string> { "Name" };


            foreach (var value in filterList)
            {
                filterOptionList.Add(new SelectListItem() { Text = value, Value = value });
            }

            if (filterType == null || filterType == "")
            {
                model.SelectedFilteringCriteria = filterOptionList.First().Value;

            }
            else
            {
                model.SelectedFilteringCriteria = filterType;
            }
            model.FilterValue = Helper.NVL(filterValue);

            model.FilteringCriteria = filterOptionList;

            model.FromDate = fromDateInput;
            model.ToDate = toDateInput;
            #endregion

            return PartialView("WithdrawalListing", model);
        }

        [HttpPost]
        public IActionResult ApproveWithdrawal(int idz = 0)
        {
            try
            {
                if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
                {
                    return RedirectToAction("Login", "Admin", new
                    {
                        reloadPage = true
                    });
                }

                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var product = dbContext.CvdCashwalletlog.FirstOrDefault(c => c.CcashId == idz);
                    if (product != null)
                    {
                        product.CcashStatus = 1;
                        dbContext.CvdCashwalletlog.Update(product);
                        dbContext.SaveChanges();
                    }
                }

                return WithdrawalListing();
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
        public IActionResult RejectWithdrawal(int idz = 0)
        {
            try
            {
                if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
                {
                    return RedirectToAction("Login", "Admin", new
                    {
                        reloadPage = true
                    });
                }

                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var product = dbContext.CvdCashwalletlog.FirstOrDefault(c => c.CcashId == idz);
                    if (product != null)
                    {
                        product.CcashStatus = -1;
                        dbContext.CvdCashwalletlog.Update(product);
                        dbContext.SaveChanges();
                    }
                }

                return WithdrawalListing();
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

        public IActionResult DownloadWithdrawalListing(int selectedPage = 1, string filterType = "", string filterValue = "", string fromDate = "", string toDate = "")
        {
            try
            {
                if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
                {
                    return RedirectToAction("Login", "Admin", new
                    {
                        reloadPage = true
                    });
                }

                filterValue = Helper.NVL(filterValue);
                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    DateTime sd = Convert.ToDateTime(DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    DateTime ed = Convert.ToDateTime(DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));

                    fromDate = sd.ToString("yyyy-MM-dd 00:00:00.000");
                    toDate = ed.ToString("yyyy-MM-dd 23:59:59.999");
                }

                int ok;
                string msg;
                int pages = 0;
                var dsAdmin = AdminGeneralDB.GetAllWithdrawalList(selectedPage, filterType, filterValue, fromDate, toDate, out pages, out ok, out msg);

                byte[] fileContents;

                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage excelPackage = new ExcelPackage();
                var workSheet = excelPackage.Workbook.Worksheets.Add("Report");
                workSheet.DefaultRowHeight = 12;

                //Header of table  
                //  
                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cells[1, 1].Value = "#";
                workSheet.Cells[1, 2].Value = FreshMVC.Resources.PackBuddyShared.lblReferenceID;
                workSheet.Cells[1, 3].Value = FreshMVC.Resources.PackBuddyShared.lblUsername;
                workSheet.Cells[1, 4].Value = FreshMVC.Resources.PackBuddyShared.lblName;
                workSheet.Cells[1, 5].Value = FreshMVC.Resources.PackBuddyShared.lblPhone;
                workSheet.Cells[1, 6].Value = FreshMVC.Resources.PackBuddyShared.lblWithdrawalAmount;
                workSheet.Cells[1, 7].Value = FreshMVC.Resources.PackBuddyShared.lblServiceFee;
                workSheet.Cells[1, 8].Value = FreshMVC.Resources.PackBuddyShared.lblFinalAmount;
                workSheet.Cells[1, 9].Value = FreshMVC.Resources.PackBuddyShared.lblApplicationTime;
                workSheet.Cells[1, 10].Value = FreshMVC.Resources.PackBuddyShared.lblBalanceBeforeWith;
                workSheet.Cells[1, 11].Value = FreshMVC.Resources.PackBuddyShared.lblBalanceAfterWith;
                workSheet.Cells[1, 12].Value = FreshMVC.Resources.PackBuddyShared.lblStatus;

                int recordIndex = 2;

                foreach (DataRow dr in dsAdmin.Tables[0].Rows)
                {
                    var am = new PaymentModel();
                    am.Number = int.Parse(dr["rownumber"].ToString());
                    am.id = dr["CCASH_ID"].ToString();
                    am.Username = dr["CUSR_USERNAME"].ToString();
                    am.RefNo = dr["CCASH_APPOTHER"].ToString();
                    am.Amount = dr["CCASH_CASHOUT"].ToString();
                    am.ServiceFee = dr["CCASH_APPRATE"].ToString();
                    am.FinalAmount = (decimal.Parse(dr["CCASH_CASHOUT"].ToString()) - decimal.Parse(dr["CCASH_APPRATE"].ToString())).ToString("#0.00");
                    am.Created = DateTime.Parse(dr["CCASH_CREATEDON"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                    am.BalanceBeforeWdr = (decimal.Parse(dr["CCASH_WALLET"].ToString()) + decimal.Parse(dr["CCASH_CASHOUT"].ToString())).ToString("#0.00");
                    am.BalanceAfterWdr = (decimal.Parse(dr["CCASH_WALLET"].ToString())).ToString("#0.00");
                    am.AccountName = dr["CUSR_FIRSTNAME"].ToString();
                    am.MerchantCode = dr["CUSR_REFERRALID"].ToString();
                    am.Status = dr["CCASH_STATUS"].ToString() == "0" ? Resources.PackBuddyShared.lblInProgress : dr["CCASH_STATUS"].ToString() == "1" ? Resources.PackBuddyShared.lblSuccess : Resources.PackBuddyShared.lblFailed;

                    workSheet.Row(recordIndex).Height = 20;
                    workSheet.Row(recordIndex).Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                    workSheet.Cells[recordIndex, 1].Value = am.Number;
                    workSheet.Cells[recordIndex, 2].Value = am.RefNo;
                    workSheet.Cells[recordIndex, 3].Value = am.Username;
                    workSheet.Cells[recordIndex, 4].Value = am.AccountName;
                    workSheet.Cells[recordIndex, 5].Value = am.Username;
                    workSheet.Cells[recordIndex, 6].Value = am.Amount;
                    workSheet.Cells[recordIndex, 7].Value = am.ServiceFee;
                    workSheet.Cells[recordIndex, 8].Value = am.FinalAmount;
                    workSheet.Cells[recordIndex, 9].Value = am.Created;
                    workSheet.Cells[recordIndex, 10].Value = am.BalanceBeforeWdr;
                    workSheet.Cells[recordIndex, 11].Value = am.BalanceAfterWdr;
                    workSheet.Cells[recordIndex, 12].Value = am.Status;
                    recordIndex++;
                }
                fileContents = excelPackage.GetAsByteArray();

                if (fileContents == null || fileContents.Length == 0)
                {
                    return NotFound();
                }

                return File(
                   fileContents: fileContents,
                   contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                   fileDownloadName: string.Format("WithdrawalListing_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmss"))
               );
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

        #region SystemConfiguration
        public IActionResult SystemConfiguration()
        {
            if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
            {
                return RedirectToAction("Login", "Admin", new
                {
                    reloadPage = true
                });
            }

            var model = new SystemConfigurationModel();
            using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
            {
                var parameters = dbContext.CvdParameter.ToList();

                if (parameters != null)
                {
                    foreach(var parameter in parameters)
                    {
                        switch (parameter.CparaName)
                        {
                            case "WithdrawalMinAmount":
                                model.WithdrawalMinAmount = parameter.CparaDecimalvalue;
                                break;
                            case "WithdrawalMaxAmount":
                                model.WithdrawalMaxAmount = parameter.CparaDecimalvalue;
                                break;
                            case "WithdrawalServiceFee":
                                model.WithdrawalServiceFee = parameter.CparaDecimalvalue;
                                break;
                            case "FirstChargeLevel1":
                                model.FirstChargeLevel1 = parameter.CparaDecimalvalue;
                                break;
                            case "FirstChargeLevel2":
                                model.FirstChargeLevel2 = parameter.CparaDecimalvalue;
                                break;
                            case "RechargeMinAmount":
                                model.RechargeMinAmount = parameter.CparaDecimalvalue;
                                break;
                            case "RechargeMaxAmount":
                                model.RechargeMaxAmount = parameter.CparaDecimalvalue;
                                break;
                            case "WinningLevel1":
                                model.WinningLevel1 = parameter.CparaDecimalvalue;
                                break;
                            case "WinningLevel2":
                                model.WinningLevel2 = parameter.CparaDecimalvalue;
                                break;
                            case "GameServiceFee":
                                model.GameServiceFee = parameter.CparaDecimalvalue;
                                break;
                            case "GameHandlingFee":
                                model.GameHandlingFee = parameter.CparaDecimalvalue;
                                break;
                            case "SponsorBonusLevel1":
                                model.SponsorBonusLevel1 = parameter.CparaDecimalvalue;
                                break;
                            case "SponsorBonusLevel2":
                                model.SponsorBonusLevel2 = parameter.CparaDecimalvalue;
                                break;
                            case "SponsorBonusLevel3":
                                model.SponsorBonusLevel3 = parameter.CparaDecimalvalue;
                                break;
                            case "GatewayPaymentHost":
                                model.GatewayPaymentHost = parameter.CparaStringvalue;
                                break;
                            case "GatewayWithdrawalHost":
                                model.GatewayWithdrawalHost = parameter.CparaStringvalue;
                                break;
                            case "GatewayMemberID":
                                model.GatewayMemberID = parameter.CparaStringvalue;
                                break;
                            case "GatewayPaymentKey":
                                model.GatewayPaymentKey = parameter.CparaStringvalue;
                                break;
                            case "SupportPhoneNumber":
                                model.SupportPhoneNumber = parameter.CparaStringvalue;
                                break;
                            case "SupportApkUrl":
                                model.SupportApkUrl = parameter.CparaStringvalue;
                                break;
                        }
                    }
                }
            }
            return PartialView("SystemConfiguration", model);
        }


        [HttpPost]
        public IActionResult EditSystemConfigurationMethod(SystemConfigurationModel model)
        {
            try
            {
                if (HttpContext.Session.GetString("Admin") == null || HttpContext.Session.GetString("Admin") == "")
                {
                    return RedirectToAction("Login", "Admin", new
                    {
                        reloadPage = true
                    });
                }

                if (string.IsNullOrEmpty(model.GatewayPaymentHost))
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblPaymentHostIsRequired
                    });
                }

                if (string.IsNullOrEmpty(model.GatewayWithdrawalHost))
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblWithdrawHostIsRequired
                    }); ;
                }

                if (string.IsNullOrEmpty(model.GatewayMemberID))
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblMemberIDIsRequired
                    });
                }

                if (string.IsNullOrEmpty(model.GatewayPaymentKey))
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblPaymentKeyIsRequired
                    });
                }

                if (string.IsNullOrEmpty(model.SupportPhoneNumber))
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblPhoneNumberIsRequired
                    });
                }

                if (string.IsNullOrEmpty(model.SupportApkUrl))
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        status = false,
                        message = Resources.PackBuddyShared.lblApkUrlIsRequired
                    });
                }

                using (SpeedyDbContext dbContext = new SpeedyDbContext(optionBuilder.Options))
                {
                    var parameters = dbContext.CvdParameter.ToList();
                    foreach (var parameter in parameters)
                    {
                        switch (parameter.CparaName)
                        {
                            case "WithdrawalMinAmount":
                                parameter.CparaDecimalvalue = model.WithdrawalMinAmount;
                                break;
                            case "WithdrawalMaxAmount":
                                parameter.CparaDecimalvalue = model.WithdrawalMaxAmount;
                                break;
                            case "WithdrawalServiceFee":
                                parameter.CparaDecimalvalue = model.WithdrawalServiceFee;
                                break;
                            case "FirstChargeLevel1":
                                parameter.CparaDecimalvalue = model.FirstChargeLevel1;
                                break;
                            case "FirstChargeLevel2":
                                parameter.CparaDecimalvalue = model.FirstChargeLevel2;
                                break;
                            case "RechargeMinAmount":
                                parameter.CparaDecimalvalue = model.RechargeMinAmount;
                                break;
                            case "RechargeMaxAmount":
                                parameter.CparaDecimalvalue = model.RechargeMaxAmount;
                                break;
                            case "WinningLevel1":
                                parameter.CparaDecimalvalue = model.WinningLevel1;
                                break;
                            case "WinningLevel2":
                                parameter.CparaDecimalvalue = model.WinningLevel2;
                                break;
                            case "GameServiceFee":
                                parameter.CparaDecimalvalue = model.GameServiceFee;
                                break;
                            case "GameHandlingFee":
                                parameter.CparaDecimalvalue = model.GameHandlingFee;
                                break;
                            case "SponsorBonusLevel1":
                                parameter.CparaDecimalvalue = model.SponsorBonusLevel1;
                                break;
                            case "SponsorBonusLevel2":
                                parameter.CparaDecimalvalue = model.SponsorBonusLevel2;
                                break;
                            case "SponsorBonusLevel3":
                                parameter.CparaDecimalvalue = model.SponsorBonusLevel3;
                                break;
                            case "GatewayPaymentHost":
                                parameter.CparaStringvalue = model.GatewayPaymentHost;
                                break;
                            case "GatewayWithdrawalHost":
                                parameter.CparaStringvalue = model.GatewayWithdrawalHost;
                                break;
                            case "GatewayMemberID":
                                parameter.CparaStringvalue = model.GatewayMemberID;
                                break;
                            case "GatewayPaymentKey":
                                parameter.CparaStringvalue = model.GatewayPaymentKey;
                                break;
                            case "SupportPhoneNumber":
                                parameter.CparaStringvalue = model.SupportPhoneNumber;
                                break;
                            case "SupportApkUrl":
                                parameter.CparaStringvalue = model.SupportApkUrl;
                                break;
                        }
                    }
                    dbContext.SaveChanges();

                }
                return SystemConfiguration();
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
