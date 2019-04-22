namespace Persada.Fr.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GEMA_TM_PRICEL_LIST
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GEMA_TM_PRICEL_LIST()
        {
            GEMA_TR_BOOKING_DETAIL = new HashSet<GEMA_TR_BOOKING_DETAIL>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string TYPE { get; set; }

        public double PRICE { get; set; }

        [StringLength(500)]
        public string DESCRIPTION { get; set; }

        [StringLength(100)]
        public string IMAGE_PATH { get; set; }

        public byte[] IMAGE_BIT { get; set; }

        public DateTime CREATED_TIME { get; set; }

        [Required]
        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GEMA_TR_BOOKING_DETAIL> GEMA_TR_BOOKING_DETAIL { get; set; }
    }
}
