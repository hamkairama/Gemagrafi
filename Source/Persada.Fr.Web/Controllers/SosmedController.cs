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
    public class SosmedController : Controller
    {
        ResultStatus rs = new ResultStatus();
        ISosmed repo;
        public SosmedController()
        {
            repo = new SosmedRepo();
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
            List<GEMA_TM_SOSMED> sosmedRes = new List<GEMA_TM_SOSMED>();

            sosmedRes = repo.GridBind();
            return View(sosmedRes);
        }

        public ActionResult Create()
        {
            ViewBag.GetTypeSosmedList = Dropdown.GetTypeSosmedList();
            return View();
        }

        public ActionResult ActionCreate(GEMA_TM_SOSMED sosmedView)
        {
            try
            {
                sosmedView.CREATED_BY = CurrentUser.GetCurrentUserId();
                sosmedView.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                rs = repo.Add(sosmedView);
                if (rs.IsSuccess)
                {
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
            GEMA_TM_SOSMED sosmedView = new GEMA_TM_SOSMED();
            GEMA_TM_SOSMED sosmedRes = new GEMA_TM_SOSMED();

            sosmedView.ID = id;
            ViewBag.GetTypeSosmedList = Dropdown.GetTypeSosmedList();

            sosmedRes = repo.Retrieve(id);
            return PartialView(sosmedRes);
        }
        public ActionResult ActionEdit(GEMA_TM_SOSMED sosmedView)
        {
            try
            {
                sosmedView.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId();
                sosmedView.LAST_MODIFIED_TIME = CurrentUser.GetCurrentDateTime();

                rs = repo.Edit(sosmedView);
                if (rs.IsSuccess)
                {
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
            GEMA_TM_SOSMED sosmedView = new GEMA_TM_SOSMED();
            GEMA_TM_SOSMED sosmedRes = new GEMA_TM_SOSMED();

            sosmedView.ID = id;

            sosmedRes = repo.Retrieve(id);
            return PartialView(sosmedRes);
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
            GEMA_TM_SOSMED sosmedView = new GEMA_TM_SOSMED();
            GEMA_TM_SOSMED sosmedRes = new GEMA_TM_SOSMED();

            sosmedView.ID = id;

            sosmedRes = repo.Retrieve(id);
            return PartialView(sosmedRes);
        }

    }
}