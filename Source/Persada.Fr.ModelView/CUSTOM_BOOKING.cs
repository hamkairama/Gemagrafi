using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persada.Fr.ModelView
{
    public class CUSTOM_BOOKING
    {
        public decimal total { get; set; }
        public decimal totalFinal { get; set; }
        public string paymentType { get; set; }
        public decimal discount { get; set; }

        public CUSTOM_BOOKING_DETAIL[] listHeaderDetail { get; set; }
    }

    public class CUSTOM_BOOKING_DETAIL
    {
        public int priceListId { get; set; }
        public DateTime dateTimeStart { get; set; }
    }
}
