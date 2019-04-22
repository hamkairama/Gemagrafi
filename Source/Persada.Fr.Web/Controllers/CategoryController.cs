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
using Persada.Fr.Model;
using System.Collections.Generic;
using Newtonsoft.Json;
using Persada.Fr.CommonFunction;
using Persada.Fr.Facade;
using Persada.Fr.Facade.Interface;
using Persada.Fr.Facade.Repository;

namespace Persada.Fr.Web.Controllers
{
    public class CategoryController : Controller
    {
        ResultStatus rs = new ResultStatus();
        ICategory repo;
        public CategoryController()
        {
            repo = new CategoryRepo();
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
            List<GEMA_TM_CATEGORY> categoryRes = new List<GEMA_TM_CATEGORY>();

            categoryRes = repo.GridBind();
            return View(categoryRes);
        }

        public ActionResult Create()
        {
            ViewBag.GetClassButtonList = Dropdown.GetClassButtonList();
            return View();
        }

        public ActionResult ActionCreate(GEMA_TM_CATEGORY categoryView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";
                if (postedFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                    physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                    categoryView.IMAGE_PATH = Common.GetPathFolderImg() + ImageName;
                }

                categoryView.CREATED_BY = CurrentUser.GetCurrentUserId();
                categoryView.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                rs = repo.Add(categoryView);
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
            GEMA_TM_CATEGORY categoryView = new GEMA_TM_CATEGORY();
            GEMA_TM_CATEGORY categoryRes = new GEMA_TM_CATEGORY();
            ViewBag.GetClassButtonList = Dropdown.GetClassButtonList();

            categoryView.ID = id;

            categoryRes = repo.Retrieve(id);
            return View(categoryRes);
        }
        public ActionResult ActionEdit(GEMA_TM_CATEGORY categoryView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";
                if (postedFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                    physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                    categoryView.IMAGE_PATH = Common.GetPathFolderImg() + ImageName;
                }

                categoryView.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId();
                categoryView.LAST_MODIFIED_TIME = CurrentUser.GetCurrentDateTime();

                rs = repo.Edit(categoryView);
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
            GEMA_TM_CATEGORY categoryView = new GEMA_TM_CATEGORY();
            GEMA_TM_CATEGORY categoryRes = new GEMA_TM_CATEGORY();

            categoryView.ID = id;

            categoryRes = repo.Retrieve(id);
            return View(categoryRes);
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
            GEMA_TM_CATEGORY categoryView = new GEMA_TM_CATEGORY();
            GEMA_TM_CATEGORY categoryRes = new GEMA_TM_CATEGORY();

            categoryView.ID = id;

            categoryRes = repo.Retrieve(id);
            return View(categoryRes);
        }

    }
}