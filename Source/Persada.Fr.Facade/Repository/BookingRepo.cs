using Newtonsoft.Json.Linq;
using Persada.Fr.CommonFunction;
using Persada.Fr.Facade.Interface;
using Persada.Fr.Model;
using Persada.Fr.Model.Master;
using Persada.Fr.ModelView;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persada.Fr.Facade.Repository
{
    public class BookingRepo : ApiResData, IBooking
    {
        private EnumStatus eStat = new EnumStatus();
        private ResultStatus rs = new ResultStatus();
        private DbCtx _ctx;

        public BookingRepo()
        {
            _ctx = new DbCtx();
        }

        public List<GEMA_TR_BOOKING> GridBind()
        {
            try
            {
                return _ctx.GEMA_TR_BOOKING.Where(x => x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<GEMA_TR_BOOKING> GridBind(int bookingStatus)
        {
            try
            {
                return _ctx.GEMA_TR_BOOKING.Where(x => x.STATUS == bookingStatus).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<GEMA_TR_BOOKING> GridBind(int bookingStatus, int userIdId)
        {
            try
            {
                return _ctx.GEMA_TR_BOOKING.Where(x => x.STATUS == bookingStatus && x.USER_ID_ID == userIdId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<GEMA_TR_BOOKING> GridBindByUserIdId(int userIdId)
        {
            try
            {
                return _ctx.GEMA_TR_BOOKING.Where(x => x.USER_ID_ID == userIdId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GEMA_TR_BOOKING Retrieve(int id)
        {
            try
            {
                return _ctx.GEMA_TR_BOOKING.Where(x => x.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultStatus Add(GEMA_TR_BOOKING booking)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    int booking_id = 0;
                    rs = AddBooking(booking, ref booking_id);
                    if (rs.IsSuccess)
                    {
                        rs = AddBookingDetail(booking.GEMA_TR_BOOKING_DETAIL.FirstOrDefault(), booking_id);
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    rs.SetErrorStatus(ex.Message);
                    transaction.Rollback();
                }
            }

            return rs;
        }

        private ResultStatus AddBooking (GEMA_TR_BOOKING booking, ref int booking_id)
        {
            _ctx.GEMA_TR_BOOKING.Add(booking);
            _ctx.SaveChanges();

            booking_id = _ctx.GEMA_TR_BOOKING.Where(x => x.TR_NUMBER == booking.TR_NUMBER).FirstOrDefault().ID;
            rs.SetSuccessStatus();

            return rs;
        }

        public ResultStatus AddBookingDetail(GEMA_TR_BOOKING_DETAIL bookingDetail, int booking_id)
        {
            bookingDetail.BOOKING_ID = booking_id;
            _ctx.GEMA_TR_BOOKING_DETAIL.Add(bookingDetail);
            _ctx.SaveChanges();
            
            rs.SetSuccessStatus();

            return rs;
        }

        public ResultStatus Edit(GEMA_TR_BOOKING booking)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    GEMA_TR_BOOKING bookingNew = _ctx.GEMA_TR_BOOKING.Find(booking.ID);
                    if (booking.STATUS == (int)EnumList.BookingStatus.Submitted)
                    {
                        bookingNew.STATUS = (int)EnumList.BookingStatus.Uploaded;
                        bookingNew.RECEIVE_PAYMENT_BY = booking.LAST_MODIFIED_BY;
                        bookingNew.RECEIVE_PAYMENT_TIME = booking.LAST_MODIFIED_TIME;
                        bookingNew.PAYMENT_IMAGE_PATH = booking.PAYMENT_IMAGE_PATH;
                    }
                    else if (booking.STATUS == (int)EnumList.BookingStatus.Uploaded)
                    {
                        bookingNew.STATUS = (int)EnumList.BookingStatus.Completed;
                        bookingNew.COMPLETE_PAYMENT_BY = booking.LAST_MODIFIED_BY;
                        bookingNew.COMPLETE_PAYMENT_TIME = booking.LAST_MODIFIED_TIME;
                    }

                    bookingNew.LAST_MODIFIED_TIME = booking.LAST_MODIFIED_TIME;
                    bookingNew.LAST_MODIFIED_BY = booking.LAST_MODIFIED_BY;
                    _ctx.Entry(bookingNew).State = System.Data.Entity.EntityState.Modified;
                    _ctx.SaveChanges();
                    rs.SetSuccessStatus();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    rs.SetErrorStatus(ex.Message);
                    transaction.Rollback();
                }
            }

            return rs;
        }

        public ResultStatus Delete(int id, string modifiedBy, DateTime modifiedTime)
        {
            try
            {
                GEMA_TR_BOOKING booking = _ctx.GEMA_TR_BOOKING.Find(id);
                booking.LAST_MODIFIED_TIME = modifiedTime;
                booking.LAST_MODIFIED_BY = modifiedBy;
                booking.ROW_STATUS = eStat.fg.NotActive;

                _ctx.Entry(booking).State = EntityState.Modified;
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }
    }
}
