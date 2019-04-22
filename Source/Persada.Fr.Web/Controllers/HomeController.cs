using Newtonsoft.Json;
using Persada.Fr.CommonFunction;
using Persada.Fr.Facade.Interface;
using Persada.Fr.Facade.Repository;
using Persada.Fr.Model;
using Persada.Fr.ModelView;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Persada.Fr.Web.Controllers
{
    public class HomeController : Controller
    {
        ISlideshow slideShowRepo;
        ICategory categoryRepo;
        ISosmed sosmedRepo;
        ISubCategory subCategoryRepo;
        public HomeController()
        {
            slideShowRepo = new SlideshowRepo();
            categoryRepo = new CategoryRepo();
            sosmedRepo = new SosmedRepo();
            subCategoryRepo = new SubCategoryRepo();
        }
        public ActionResult Index()
        {
            ViewBag.isHome = true;
            ViewBag.bg1 = true;
            ViewBag.GetSlideshow = slideShowRepo.GridBind();
            List<GEMA_TM_CATEGORY> categories = categoryRepo.GridBind();
            Session["GetSosmedList"] = sosmedRepo.GridBind();
            Session["SubCategoryRandomList"] = subCategoryRepo.GridBindTop5();
            ViewBag.msgSuccess = TempData["msgSuccess"];
            ViewBag.msgError = TempData["msgError"];
            ViewBag.ErrorPageModal = TempData["ErrorPageModal"];

            return View(categories);
        }
        public ActionResult NewYork()
        {
            return View();
        }

        public ActionResult Bicycles()
        {
            return View();
        }

        public ActionResult Single()
        {
            return View();
        }

        public ActionResult Parts()
        {
            return View();
        }

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult FormInput()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.msgSuccess = TempData["msgSuccess"];
            ViewBag.msgError = TempData["msgError"];
            return PartialView();
        }

        public ActionResult Accessories()
        {
            return View();
        }

        public ActionResult ErrorPage()
        {
            return View();
        }


        public DataTable GetdtLatLong()
        {
            string strAddress = "Jl. Pos Pengumben Raya No. 77. kebon jeruk, Jakarta Barat. DKI Jakarta, Indonesia";
            string url = "http://maps.google.com/maps/api/geocode/xml?address=" + strAddress + "&sensor=false";
            DataTable dtGMap = new DataTable();
            DataTable dtCoordinates = new DataTable();
            WebRequest request = WebRequest.Create(url);

            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    DataSet dsResult = new DataSet();
                    dsResult.ReadXml(reader);

                    dtCoordinates.Columns.AddRange(new DataColumn[4] { new DataColumn("id", typeof(int)),
                        new DataColumn("Address", typeof(string)),
                        new DataColumn("Latitude",typeof(string)),
                        new DataColumn("Longitude",typeof(string)) });
                    foreach (DataRow row in dsResult.Tables["result"].Rows)
                    {
                        string geometry_id = dsResult.Tables["geometry"].Select("result_id = " + row["result_id"].ToString())[0]["geometry_id"].ToString();
                        DataRow location = dsResult.Tables["location"].Select("geometry_id = " + geometry_id)[0];
                        dtCoordinates.Rows.Add(row["result_id"], row["formatted_address"], location["lat"], location["lng"]);
                    }
                }
            }

            dtGMap = dtCoordinates;
            return dtGMap;
        }

        public ActionResult About()
        {
            return PartialView();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.msgSuccess = TempData["msgSuccess"];
            ViewBag.msgError = TempData["msgError"];

            return View();
        }

        public ActionResult ErrorPageModal()
        {
            ViewBag.ErrorPageModal = TempData["ErrorPageModal"];

            return PartialView();
        }

        public ActionResult SendEmail(CUSTOM_CONTACT_US contactUs, HttpPostedFileBase postedFile)
        {
            ResultStatus rs = new ResultStatus();
            MailMessage mail = new MailMessage();
            CUSTOM_MAIL customMail = new CUSTOM_MAIL();

            string[] to = { "admin@professionalis.me" };
            string[] from = { "admin@professionalis.me" };
            string[] cc = { contactUs.EMAIL_SENDER };
            string subject = "[professionalis.me] Customer Service - " + contactUs.NAME_SENDER;
            string body = contactUs.DESCRIPTION;

            string attachmentName = "";
            if (postedFile != null)
            {
                attachmentName = postedFile.FileName;
            }

            if (ModelState.IsValid)
            {
                customMail.TO = to;
                customMail.FROM = from;
                customMail.CC = cc;
                customMail.SUBJECT = subject;
                customMail.BODY = body;
                customMail.ISBODYHTML = true;
                try
                {
                    Email email = new Email();
                    mail = email.MappingEmail(customMail);
                    rs = email.SendEmail(mail, attachmentName);
                    TempData["msgSuccess"] = rs.MessageText;
                }
                catch (DataException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    rs.SetErrorStatus("Data failed to sent");
                    TempData["msgError"] = rs.MessageText;
                }
            }
            else
            {
                TempData["ErrorPageModal"] = "Invalid data";
            }

            return RedirectToAction("Index");
        }
    }
}