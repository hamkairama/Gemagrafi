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
    public class BookingController : Controller
    {
        ResultStatus rs = new ResultStatus();
        IBooking bookingRepo;
        IPriceList priceListRepo;

        public BookingController()
        {
            CurrentUser.IsLogin();
            bookingRepo = new BookingRepo();
            priceListRepo = new PriceListRepo();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Cart(int priceListId)
        {
            ViewBag.GetPaymentType = Dropdown.GetPaymentType();
            List<GEMA_TM_PRICEL_LIST> pricelistList = new List<GEMA_TM_PRICEL_LIST>();
            List<GEMA_TM_PRICEL_LIST> totalPricelistBooking = new List<GEMA_TM_PRICEL_LIST>();

            GEMA_TM_PRICEL_LIST priceList = priceListRepo.Retrieve(priceListId);
            if (priceList != null)
            {
                pricelistList.Add(priceList);

                if (Session["GetPriceListBooking"] == null)
                {
                    Session["GetPriceListBooking"] = pricelistList;
                }
                else
                {
                    totalPricelistBooking = (List<GEMA_TM_PRICEL_LIST>)Session["GetPriceListBooking"];
                    if (totalPricelistBooking.Count() <= 1)
                    {
                        foreach (GEMA_TM_PRICEL_LIST item in totalPricelistBooking)
                        {
                            pricelistList.Add(item);
                            Session["GetPriceListBooking"] = pricelistList;
                        }
                    }
                }
            }

            return View();
        }

        public ActionResult DeleteCart(string type)
        {
            rs.SetSuccessStatus();
            List<GEMA_TM_PRICEL_LIST> pricelistList = new List<GEMA_TM_PRICEL_LIST>();

            if (Session["GetPriceListBooking"] != null)
            {
                List<GEMA_TM_PRICEL_LIST> totalPricelistBooking = (List<GEMA_TM_PRICEL_LIST>)Session["GetPriceListBooking"];
                if (totalPricelistBooking.Count() > 1)
                {
                    foreach (GEMA_TM_PRICEL_LIST item in totalPricelistBooking)
                    {
                        if (item.TYPE != type)
                        {
                            pricelistList.Add(item);
                            Session["GetPriceListBooking"] = pricelistList;
                        }
                    }
                }
                else
                {
                    Session["GetPriceListBooking"] = null;
                    return RedirectToAction("Cart", "Booking", new { priceListId = 0 });
                }

            }
            return Json(rs);
        }

        public ActionResult GetAllBooking()
        {
            ViewBag.msgSuccess = TempData["msgSuccess"];
            ViewBag.msgError = TempData["msgError"];
            ViewBag.flag = "ALL";
            List<GEMA_TR_BOOKING> bookingRes = new List<GEMA_TR_BOOKING>();

            bookingRes = bookingRepo.GridBind();
            return View("ListBooking", bookingRes);
        }

        public ActionResult GetMyBooking(int userIdId)
        {
            ViewBag.msgSuccess = TempData["msgSuccess"];
            ViewBag.msgError = TempData["msgError"];
            ViewBag.flag = "MY";
            List<GEMA_TR_BOOKING> bookingRes = new List<GEMA_TR_BOOKING>();

            bookingRes = bookingRepo.GridBindByUserIdId(userIdId);
            return View("ListBooking", bookingRes);
        }

        public ActionResult Create()
        {
            ViewBag.GetClassActiveList = Dropdown.GetClassActiveList();

            return View();
        }

        public ActionResult ActionCreate(CUSTOM_BOOKING bookingView)
        {
            try
            {
                GEMA_TR_BOOKING booking = new GEMA_TR_BOOKING();

                booking.USER_ID_ID = CurrentUser.GetCurrentUserIdId();
                booking.CREATED_BY = CurrentUser.GetCurrentUserId();
                booking.CREATED_TIME = CurrentUser.GetCurrentDateTime();
                booking.TOTAL_PAYMENT = bookingView.total;
                booking.GRAND_TOTAL_PAYMENT = bookingView.totalFinal;
                booking.PAYMENT_TYPE = bookingView.paymentType;

                foreach (var item in bookingView.listHeaderDetail)
                {
                    GEMA_TR_BOOKING_DETAIL detail = new GEMA_TR_BOOKING_DETAIL();
                    detail.PRICE_LIST_ID = item.priceListId;
                    detail.START_TIME = item.dateTimeStart;
                    detail.END_TIME = item.dateTimeStart.AddHours(6);
                    detail.CREATED_BY = CurrentUser.GetCurrentUserId();
                    detail.CREATED_TIME = CurrentUser.GetCurrentDateTime();
                    booking.GEMA_TR_BOOKING_DETAIL.Add(detail);
                }

                rs = bookingRepo.Add(booking);
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
            GEMA_TR_BOOKING bookingView = new GEMA_TR_BOOKING();
            GEMA_TR_BOOKING bookingRes = new GEMA_TR_BOOKING();
            ViewBag.GetClassActiveList = Dropdown.GetClassActiveList();

            bookingView.ID = id;

            bookingRes = bookingRepo.Retrieve(id);
            return PartialView(bookingRes);
        }
        public ActionResult ActionEdit(GEMA_TR_BOOKING bookingView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";
                if (bookingView.STATUS == (int)EnumList.BookingStatus.Submitted && postedFile == null)
                {
                    rs.SetErrorStatus("Please upload the file");
                    TempData["msgError"] = rs.MessageText;
                }
                else
                {
                    if (bookingView.STATUS == (int)EnumList.BookingStatus.Submitted)
                    {
                        string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                        physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                        bookingView.PAYMENT_IMAGE_PATH = Common.GetPathFolderImg() + ImageName;
                    }
                   
                    bookingView.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId();
                    bookingView.LAST_MODIFIED_TIME = CurrentUser.GetCurrentDateTime();

                    rs = bookingRepo.Edit(bookingView);
                    if (rs.IsSuccess)
                    {
                        if (bookingView.STATUS == (int)EnumList.BookingStatus.Submitted)
                        {
                            postedFile.SaveAs(physicalPath);
                        }
                        rs.SetSuccessStatus("Data has been updated successfully");
                        TempData["msgSuccess"] = rs.MessageText;
                    }
                    else
                    {
                        rs.SetErrorStatus("Data failed to updated. Please call our administrator");
                        TempData["msgError"] = rs.MessageText;
                    }
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
            GEMA_TR_BOOKING bookingView = new GEMA_TR_BOOKING();
            GEMA_TR_BOOKING bookingRes = new GEMA_TR_BOOKING();

            bookingView.ID = id;

            bookingRes = bookingRepo.Retrieve(id);
            return PartialView(bookingRes);
        }
        public ActionResult ActionDelete(int id)
        {
            try
            {
                rs = bookingRepo.Delete(id, CurrentUser.GetCurrentUserId(), CurrentUser.GetCurrentDateTime());
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
            GEMA_TR_BOOKING bookingView = new GEMA_TR_BOOKING();
            GEMA_TR_BOOKING bookingRes = new GEMA_TR_BOOKING();

            bookingView.ID = id;

            bookingRes = bookingRepo.Retrieve(id);

            return PartialView(bookingRes);
        }

    }
}