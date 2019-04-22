using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Persada.Fr.CommonFunction;
using Persada.Fr.Facade;
using Persada.Fr.Facade.Interface;
using Persada.Fr.Facade.Repository;
using Persada.Fr.Model;
using Persada.Fr.Model.Master;
using Persada.Fr.ModelView;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Persada.Fr.Web.Controllers
{
    public class UserController : Controller
    {
        ResultStatus rs = new ResultStatus();
        IUser repo;
        public UserController()
        {
            repo = new UserRepo();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            ViewBag.msgSuccess = TempData["msgSuccess"];
            ViewBag.msgError = TempData["msgError"];
            List<GEMA_TM_USER> userRes = new List<GEMA_TM_USER>();

            userRes = repo.GridBind();
            return View(userRes);
        }

        public ActionResult Create()
        {
            ViewBag.GetGenderList = Dropdown.GetGenderList();
            return View();
        }

        public ActionResult ActionCreate(TOURIS_TV_USER userView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";

                GEMA_TM_USER user = new GEMA_TM_USER();
                GEMA_TM_USER_PROFILE userProfile = new GEMA_TM_USER_PROFILE();
                List<GEMA_TM_USER_PROFILE_SOSMED> userProfileSosmed = new List<GEMA_TM_USER_PROFILE_SOSMED>();
                if (postedFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                    physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                    userProfile.PHOTO_PATH = Common.GetPathFolderImg() + ImageName;
                }

                user.USER_ID = userView.USER_ID;
                user.USER_MAIL = userView.USER_MAIL;
                user.USER_NAME = userView.USER_NAME;
                user.PASSWORD = userView.PASSWORD;
                user.IS_SUPER_ADMIN = userView.IS_SUPER_ADMIN;
                if (CurrentUser.GetCurrentUserId() != "")
                {
                    user.CREATED_BY = CurrentUser.GetCurrentUserId();
                }
                else
                {
                    user.CREATED_BY = userView.USER_ID;
                }
                user.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                userProfile.GENDER = userView.GENDER;
                userProfile.BORN = userView.BORN;
                userProfile.ADDRESS = userView.ADDRESS;
                userProfile.DESCRIPTION = userView.DESCRIPTION;
                userProfile.JOB = userView.JOB;
                userProfile.COMPANY = userView.COMPANY;
                userProfile.HOBBY = userView.HOBBY;
                userProfile.CREATED_BY = user.CREATED_BY;
                userProfile.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                rs = repo.Add(user, userProfile, userProfileSosmed);
                if (rs.IsSuccess)
                {
                    if (physicalPath != "")
                    {
                        postedFile.SaveAs(physicalPath);
                    }

                    rs.SetSuccessStatus("Data has been created successfully");
                    TempData["msgSuccess"] = rs.MessageText;
                }
                else
                {
                    rs.SetErrorStatus("Data failed to created");
                    TempData["msgError"] = rs.MessageText;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                rs.SetErrorStatus("Data failed to created");
                TempData["msgError"] = rs.MessageText;
            }

            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            GEMA_TM_USER user = new GEMA_TM_USER();

            ViewBag.GetGenderList = Dropdown.GetGenderList();

            user = repo.Retrieve(id);
            TOURIS_TV_USER userView = MapToView(user);

            return PartialView(userView);
        }
        public ActionResult ActionEdit(TOURIS_TV_USER userView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";
                GEMA_TM_USER user = new GEMA_TM_USER();
                GEMA_TM_USER_PROFILE userProfile = new GEMA_TM_USER_PROFILE();
                List<GEMA_TM_USER_PROFILE_SOSMED> userProfileSosmed = new List<GEMA_TM_USER_PROFILE_SOSMED>();

                if (postedFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                    physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                    userProfile.PHOTO_PATH = Common.GetPathFolderImg() + ImageName;
                }
                user.USER_ID = userView.USER_ID;
                user.USER_MAIL = userView.USER_MAIL;
                user.USER_NAME = userView.USER_NAME;
                user.PASSWORD = userView.PASSWORD;
                user.IS_SUPER_ADMIN = userView.IS_SUPER_ADMIN;
                user.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId();
                user.LAST_MODIFIED_TIME = CurrentUser.GetCurrentDateTime();

                userProfile.GENDER = userView.GENDER;
                userProfile.BORN = userView.BORN;
                userProfile.ADDRESS = userView.ADDRESS;
                userProfile.DESCRIPTION = userView.DESCRIPTION;
                userProfile.JOB = userView.JOB;
                userProfile.COMPANY = userView.COMPANY;
                userProfile.HOBBY = userView.HOBBY;

                rs = repo.Edit(user, userProfile, userProfileSosmed);
                if (rs.IsSuccess)
                {
                    if (physicalPath != "")
                    {
                        postedFile.SaveAs(physicalPath);
                    }
                    rs.SetSuccessStatus("Data has been edited successfully");
                    TempData["msgSuccess"] = rs.MessageText;
                }
                else
                {
                    rs.SetErrorStatus("Data failed to edited");
                    TempData["msgError"] = rs.MessageText;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                rs.SetErrorStatus("Data failed to edited");
                TempData["msgError"] = rs.MessageText;
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(int id)
        {
            GEMA_TM_USER user = new GEMA_TM_USER();

            user = repo.Retrieve(id);
            TOURIS_TV_USER userView = MapToView(user);
            return PartialView(userView);
        }
        public ActionResult ActionDelete(int id)
        {
            try
            {
                rs = repo.Delete(id, CurrentUser.GetCurrentUserId(), CurrentUser.GetCurrentDateTime());
                if (rs.IsSuccess)
                {
                    rs.SetSuccessStatus("Data has been deleted successfully");
                    TempData["msgSuccess"] = rs.MessageText;
                }
                else
                {
                    rs.SetErrorStatus("Data failed to deleted");
                    TempData["msgError"] = rs.MessageText;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                rs.SetErrorStatus("Data failed to deleted");
                TempData["msgError"] = rs.MessageText;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            GEMA_TM_USER user = new GEMA_TM_USER();

            user = repo.Retrieve(id);
            TOURIS_TV_USER userView = MapToView(user);
            return PartialView(userView);
        }

        public ActionResult ActionLogin(CUSTOM_LOGIN login)
        {
            GEMA_TM_USER userView = new GEMA_TM_USER();
            GEMA_TM_USER userRes = new GEMA_TM_USER();

            if (ModelState.IsValid)
            {
                userRes = repo.Login(login.Email, login.Password);

                if (userRes != null)
                {
                    Session["USER_ID_ID"] = userRes.ID;
                    Session["USER_ID"] = userRes.USER_ID;
                    Session["USER_EMAIL"] = userRes.USER_MAIL;
                    Session["IS_SUPER_ADMIN"] = userRes.IS_SUPER_ADMIN;
                    Session["USER_NAME"] = userRes.USER_NAME;                    
                }
                else
                {
                    TempData["msgError"] = "Data incorrect";
                }
            }
            else
            {
                TempData["ErrorPageModal"] = "Invalid data";
            }           

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            ViewBag.msgSuccess = TempData["msgSuccess"];
            ViewBag.msgError = TempData["msgError"];
            return PartialView();
        }

        public ActionResult ActionLogoff()
        {
            Session["USER_ID_ID"] = null;
            Session["USER_ID"] = null;
            Session["USER_EMAIL"] = null;
            Session["IS_SUPER_ADMIN"] = null;
            Session["USER_NAME"] = null;
            Session["GetPriceListBooking"] = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePassword()
        {
            ViewBag.msgSuccess = TempData["msgSuccess"];
            ViewBag.msgError = TempData["msgError"];
            return PartialView();
        }

        public ActionResult ActionChangePassword(CUSTOM_CHANGE_PASSWORD changePassword)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    changePassword.IdUser = CurrentUser.GetCurrentUserIdId();
                    rs = repo.ChangePassword(CurrentUser.GetCurrentUserIdId(), changePassword.OldPassword, changePassword.NewPassword);
                    if (rs.IsSuccess)
                    {
                        TempData["msgSuccess"] = rs.MessageText;
                    }
                    else
                    {
                        TempData["msgError"] = rs.MessageText;
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    rs.SetErrorStatus("Data failed to changed");
                    TempData["msgError"] = rs.MessageText;
                }
            }
            else
            {
                TempData["ErrorPageModal"] = "Invalid data";
            }
                

            return RedirectToAction("Index", "Home");
        }

        private TOURIS_TV_USER MapToView(GEMA_TM_USER user)
        {
            TOURIS_TV_USER userView = new TOURIS_TV_USER();
            if (user != null)
            {
                userView.ID = user.ID;
                userView.USER_ID = user.USER_ID;
                userView.USER_MAIL = user.USER_MAIL;
                userView.USER_NAME = user.USER_NAME;
                userView.IS_SUPER_ADMIN = user.IS_SUPER_ADMIN;
                userView.LAST_LOGIN = user.LAST_LOGIN;
                userView.PASSWORD = user.PASSWORD;
                userView.PHOTO_PATH = user.GEMA_TM_USER_PROFILE.FirstOrDefault().PHOTO_PATH;
                userView.GENDER = user.GEMA_TM_USER_PROFILE.FirstOrDefault().GENDER;
                userView.BORN = user.GEMA_TM_USER_PROFILE.FirstOrDefault().BORN;
                userView.ADDRESS = user.GEMA_TM_USER_PROFILE.FirstOrDefault().ADDRESS;
                userView.DESCRIPTION = user.GEMA_TM_USER_PROFILE.FirstOrDefault().DESCRIPTION;
                userView.JOB = user.GEMA_TM_USER_PROFILE.FirstOrDefault().JOB;
                userView.COMPANY = user.GEMA_TM_USER_PROFILE.FirstOrDefault().COMPANY;
                userView.HOBBY = user.GEMA_TM_USER_PROFILE.FirstOrDefault().HOBBY;
                userView.CREATED_BY = user.CREATED_BY;
                userView.CREATED_TIME = user.CREATED_TIME;
                userView.LAST_MODIFIED_BY = user.LAST_MODIFIED_BY;
                userView.LAST_MODIFIED_TIME = user.LAST_MODIFIED_TIME;

                if (user.GEMA_TM_USER_PROFILE != null)
                {
                    TOURIS_TV_USER_PROFILE userProfileView = new TOURIS_TV_USER_PROFILE();
                    userProfileView.ID = user.ID;
                    userProfileView.USER_ID_ID = user.GEMA_TM_USER_PROFILE.FirstOrDefault().USER_ID_ID;
                    userProfileView.PHOTO_PATH = user.GEMA_TM_USER_PROFILE.FirstOrDefault().PHOTO_PATH;
                    userProfileView.GENDER = user.GEMA_TM_USER_PROFILE.FirstOrDefault().GENDER;
                    userProfileView.BORN = user.GEMA_TM_USER_PROFILE.FirstOrDefault().BORN;
                    userProfileView.ADDRESS = user.GEMA_TM_USER_PROFILE.FirstOrDefault().ADDRESS;
                    userProfileView.DESCRIPTION = user.GEMA_TM_USER_PROFILE.FirstOrDefault().DESCRIPTION;
                    userProfileView.JOB = user.GEMA_TM_USER_PROFILE.FirstOrDefault().JOB;
                    userProfileView.COMPANY = user.GEMA_TM_USER_PROFILE.FirstOrDefault().COMPANY;
                    userProfileView.HOBBY = user.GEMA_TM_USER_PROFILE.FirstOrDefault().HOBBY;
                    userProfileView.CREATED_BY = user.GEMA_TM_USER_PROFILE.FirstOrDefault().CREATED_BY;
                    userProfileView.CREATED_TIME = user.GEMA_TM_USER_PROFILE.FirstOrDefault().CREATED_TIME;
                    userProfileView.LAST_MODIFIED_BY = user.GEMA_TM_USER_PROFILE.FirstOrDefault().LAST_MODIFIED_BY;
                    userProfileView.LAST_MODIFIED_TIME = user.GEMA_TM_USER_PROFILE.FirstOrDefault().LAST_MODIFIED_TIME;

                    if (user.GEMA_TM_USER_PROFILE.FirstOrDefault().GEMA_TM_USER_PROFILE_SOSMED.Count > 0)
                    {
                        foreach (var itemSosmed in user.GEMA_TM_USER_PROFILE.FirstOrDefault().GEMA_TM_USER_PROFILE_SOSMED)
                        {
                            TOURIS_TV_USER_PROFILE_SOSMED userProfileSosmedView = new TOURIS_TV_USER_PROFILE_SOSMED();
                            userProfileSosmedView.ID = user.ID;
                            userProfileSosmedView.USER_PROFILE_ID = itemSosmed.USER_PROFILE_ID;
                            userProfileSosmedView.SOSMED_NAME = itemSosmed.SOSMED_NAME;
                            userProfileSosmedView.SOSMED_PATH = itemSosmed.SOSMED_PATH;
                            userProfileSosmedView.CREATED_BY = itemSosmed.CREATED_BY;
                            userProfileSosmedView.CREATED_TIME = itemSosmed.CREATED_TIME;
                            userProfileSosmedView.LAST_MODIFIED_BY = itemSosmed.LAST_MODIFIED_BY;
                            userProfileSosmedView.LAST_MODIFIED_TIME = itemSosmed.LAST_MODIFIED_TIME;

                            userProfileView.TOURIS_TV_USER_PROFILE_SOSMED.Add(userProfileSosmedView);
                        }
                    }

                    userView.TOURIS_TV_USER_PROFILE.Add(userProfileView);
                }

            }

            return userView;
        }


    }
}