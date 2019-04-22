using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Persada.Fr.ModelView;
using System.Collections.Generic;
using Newtonsoft.Json;
using Persada.Fr.CommonFunction;
using Persada.Fr.Facade;
using Persada.Fr.Model;
using Persada.Fr.Facade.Interface;
using Persada.Fr.Facade.Repository;

namespace Persada.Fr.Web.Controllers
{
    public class SubCategoryController : Controller
    {
        ResultStatus rs = new ResultStatus();
        ISubCategory repo;
        public SubCategoryController()
        {
            repo = new SubCategoryRepo();
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
            List<GEMA_TM_SUB_CATEGORY> subCategoryRes = new List<GEMA_TM_SUB_CATEGORY>();

            subCategoryRes = repo.GridBind();
            return View(subCategoryRes);
        }

        public ActionResult Create()
        {
            ViewBag.GetCategoryList = repo.GetCategoryList();
            ViewBag.GetNullList = Dropdown.GetNullList();
            return View();
        }

        public ActionResult ActionCreate(GEMA_TM_SUB_CATEGORY subCategoryView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";
                if (postedFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                    physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                    subCategoryView.IMAGE_PATH = Common.GetPathFolderImg() + ImageName;
                }

                subCategoryView.CREATED_BY = CurrentUser.GetCurrentUserId();
                subCategoryView.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                rs = repo.Add(subCategoryView);
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
            GEMA_TM_SUB_CATEGORY subCategoryView = new GEMA_TM_SUB_CATEGORY();
            GEMA_TM_SUB_CATEGORY subCategoryRes = new GEMA_TM_SUB_CATEGORY();

            subCategoryView.ID = id;
            subCategoryRes = repo.Retrieve(id);
            ViewBag.GetCategoryList = repo.GetCategoryList();

            return View(subCategoryRes);
        }
        public ActionResult ActionEdit(GEMA_TM_SUB_CATEGORY subCategoryView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";
                if (postedFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                    physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                    subCategoryView.IMAGE_PATH = Common.GetPathFolderImg() + ImageName;
                }

                subCategoryView.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId();
                subCategoryView.LAST_MODIFIED_TIME = CurrentUser.GetCurrentDateTime();

                rs = repo.Edit(subCategoryView);
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

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            GEMA_TM_SUB_CATEGORY subCategoryView = new GEMA_TM_SUB_CATEGORY();
            GEMA_TM_SUB_CATEGORY subCategoryRes = new GEMA_TM_SUB_CATEGORY();

            subCategoryView.ID = id;

            subCategoryRes = repo.Retrieve(id);
            return View(subCategoryRes);
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
            GEMA_TM_SUB_CATEGORY subCategoryView = new GEMA_TM_SUB_CATEGORY();
            GEMA_TM_SUB_CATEGORY subCategoryRes = new GEMA_TM_SUB_CATEGORY();

            subCategoryView.ID = id;

            subCategoryRes = repo.Retrieve(id);
            //string mapUrl = subCategoryRes.ADDRESS + ". " + subCategoryRes.VILLAGE_NAME + ", " + subCategoryRes.DISTRICT_NAME + ". " + subCategoryRes.CITY_NAME + ", " + subCategoryRes.PROVINCE_NAME + ". " + subCategoryRes.COUNTRY_NAME;
            //ViewBag.MapUrl = mapUrl.Replace(" ", "+");
            return PartialView("Detail", subCategoryRes);
        }

      

    }
}