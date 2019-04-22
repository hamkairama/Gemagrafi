namespace Persada.Fr.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GEMA_TR_BOOKING_DETAIL
    {
        public int ID { get; set; }

        public int BOOKING_ID { get; set; }

        public int PRICE_LIST_ID { get; set; }

        public DateTime START_TIME { get; set; }

        public DateTime END_TIME { get; set; }

        public DateTime CREATED_TIME { get; set; }

        [Required]
        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }

        public virtual GEMA_TM_PRICEL_LIST GEMA_TM_PRICEL_LIST { get; set; }

        public virtual GEMA_TR_BOOKING GEMA_TR_BOOKING { get; set; }
    }
}
