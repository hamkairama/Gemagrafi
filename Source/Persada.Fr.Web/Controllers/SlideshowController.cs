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
using System.Text;
using Persada.Fr.Facade;
using Persada.Fr.Model;
using Persada.Fr.Facade.Interface;
using Persada.Fr.Facade.Repository;

namespace Persada.Fr.Web.Controllers
{
    public class SlideshowController : Controller
    {
        ResultStatus rs = new ResultStatus();
        ISlideshow repo;
        public SlideshowController()
        {
            repo = new SlideshowRepo();
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
            List<GEMA_TM_SLIDESHOW> slideshowRes = new List<GEMA_TM_SLIDESHOW>();

            slideshowRes = repo.GridBind();
            return View(slideshowRes);
        }

        public ActionResult Create()
        {
            ViewBag.GetClassActiveList = Dropdown.GetClassActiveList();

            return View();
        }

        public ActionResult ActionCreate(GEMA_TM_SLIDESHOW slideshowView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";
                if (postedFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                    physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                    slideshowView.PHOTO_PATH = Common.GetPathFolderImg() + ImageName;
                }

                slideshowView.CREATED_BY = CurrentUser.GetCurrentUserId();
                slideshowView.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                rs = repo.Add(slideshowView);
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
            GEMA_TM_SLIDESHOW slideshowView = new GEMA_TM_SLIDESHOW();
            GEMA_TM_SLIDESHOW slideshowRes = new GEMA_TM_SLIDESHOW();
            ViewBag.GetClassActiveList = Dropdown.GetClassActiveList();

            slideshowView.ID = id;

            slideshowRes = repo.Retrieve(id);
            return PartialView(slideshowRes);
        }
        public ActionResult ActionEdit(GEMA_TM_SLIDESHOW slideshowView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";
                if (postedFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                    physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                    slideshowView.PHOTO_PATH = Common.GetPathFolderImg() + ImageName;
                }

                slideshowView.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId();
                slideshowView.LAST_MODIFIED_TIME = CurrentUser.GetCurrentDateTime();

                rs = repo.Edit(slideshowView);
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
            GEMA_TM_SLIDESHOW slideshowView = new GEMA_TM_SLIDESHOW();
            GEMA_TM_SLIDESHOW slideshowRes = new GEMA_TM_SLIDESHOW();

            slideshowView.ID = id;

            slideshowRes = repo.Retrieve(id);
            return PartialView(slideshowRes);
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
            GEMA_TM_SLIDESHOW slideshowView = new GEMA_TM_SLIDESHOW();
            GEMA_TM_SLIDESHOW slideshowRes = new GEMA_TM_SLIDESHOW();

            slideshowView.ID = id;

            slideshowRes = repo.Retrieve(id);

            return PartialView(slideshowRes);
        }

    }
}