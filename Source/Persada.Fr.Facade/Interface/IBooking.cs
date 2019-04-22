using Newtonsoft.Json.Linq;
using Persada.Fr.CommonFunction;
using Persada.Fr.Model;
using Persada.Fr.Model.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persada.Fr.Facade.Interface
{
    public interface IBooking
    {
        List<GEMA_TR_BOOKING> GridBind();
        GEMA_TR_BOOKING Retrieve(int id);
        ResultStatus Add(GEMA_TR_BOOKING slideshow);
        ResultStatus AddBookingDetail(GEMA_TR_BOOKING_DETAIL bookingDetail, int booking_id);
        ResultStatus Edit(GEMA_TR_BOOKING slideshow);
        ResultStatus Delete(int id, string modifiedBy, DateTime modifiedTime);
        List<GEMA_TR_BOOKING> GridBind(int bookingStatus);
        List<GEMA_TR_BOOKING> GridBind(int bookingStatus, int userIdId);
        List<GEMA_TR_BOOKING> GridBindByUserIdId(int userIdId);

    }
}
