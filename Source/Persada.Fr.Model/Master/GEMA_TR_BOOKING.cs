namespace Persada.Fr.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GEMA_TR_BOOKING
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GEMA_TR_BOOKING()
        {
            GEMA_TR_BOOKING_DETAIL = new HashSet<GEMA_TR_BOOKING_DETAIL>();
        }

        public int ID { get; set; }

        public int TR_NUMBER { get; set; }

        public int USER_ID_ID { get; set; }

        public int STATUS { get; set; }

        [Required]
        [StringLength(100)]
        public string PAYMENT_TYPE { get; set; }

        public decimal? DISCOUNT { get; set; }

        public decimal TOTAL_PAYMENT { get; set; }

        public decimal GRAND_TOTAL_PAYMENT { get; set; }

        public DateTime? RECEIVE_PAYMENT_TIME { get; set; }

        [StringLength(50)]
        public string RECEIVE_PAYMENT_BY { get; set; }

        public DateTime? COMPLETE_PAYMENT_TIME { get; set; }

        [StringLength(50)]
        public string COMPLETE_PAYMENT_BY { get; set; }

        [StringLength(100)]
        public string PAYMENT_IMAGE_PATH { get; set; }

        public DateTime CREATED_TIME { get; set; }

        [Required]
        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }

        public virtual GEMA_TM_USER GEMA_TM_USER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GEMA_TR_BOOKING_DETAIL> GEMA_TR_BOOKING_DETAIL { get; set; }
    }
}
