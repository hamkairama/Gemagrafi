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
    public class PriceListController : Controller
    {
        ResultStatus rs = new ResultStatus();
        IPriceList repo;
        public PriceListController()
        {
            repo = new PriceListRepo();
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
            List<GEMA_TM_PRICEL_LIST> priceListRes = new List<GEMA_TM_PRICEL_LIST>();

            priceListRes = repo.GridBind();
            return View(priceListRes);
        }

        public ActionResult Single()
        {
            ViewBag.msgSuccess = TempData["msgSuccess"];
            ViewBag.msgError = TempData["msgError"];
            List<GEMA_TM_PRICEL_LIST> priceListRes = new List<GEMA_TM_PRICEL_LIST>();

            priceListRes = repo.GridBind();
            return View(priceListRes);
        }
        public ActionResult Calender()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.GetClassButtonList = Dropdown.GetClassButtonList();
            return View();
        }

        public ActionResult ActionCreate(GEMA_TM_PRICEL_LIST priceListView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";
                if (postedFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                    physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                    priceListView.IMAGE_PATH = Common.GetPathFolderImg() + ImageName;
                }

                priceListView.CREATED_BY = CurrentUser.GetCurrentUserId();
                priceListView.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                rs = repo.Add(priceListView);
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
            GEMA_TM_PRICEL_LIST priceListView = new GEMA_TM_PRICEL_LIST();
            GEMA_TM_PRICEL_LIST priceListRes = new GEMA_TM_PRICEL_LIST();
            ViewBag.GetClassButtonList = Dropdown.GetClassButtonList();

            priceListView.ID = id;

            priceListRes = repo.Retrieve(id);
            return View(priceListRes);
        }
        public ActionResult ActionEdit(GEMA_TM_PRICEL_LIST priceListView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";
                if (postedFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                    physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                    priceListView.IMAGE_PATH = Common.GetPathFolderImg() + ImageName;
                }

                priceListView.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId();
                priceListView.LAST_MODIFIED_TIME = CurrentUser.GetCurrentDateTime();

                rs = repo.Edit(priceListView);
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
            GEMA_TM_PRICEL_LIST priceListView = new GEMA_TM_PRICEL_LIST();
            GEMA_TM_PRICEL_LIST priceListRes = new GEMA_TM_PRICEL_LIST();

            priceListView.ID = id;

            priceListRes = repo.Retrieve(id);
            return View(priceListRes);
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
            GEMA_TM_PRICEL_LIST priceListView = new GEMA_TM_PRICEL_LIST();
            GEMA_TM_PRICEL_LIST priceListRes = new GEMA_TM_PRICEL_LIST();

            priceListView.ID = id;

            priceListRes = repo.Retrieve(id);
            return View(priceListRes);
        }

    }
}