namespace Persada.Fr.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GEMA_TM_SUB_CATEGORY
    {
        public int ID { get; set; }

        public int CATEGORY_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string SUB_CATEGORY_NAME { get; set; }

        [Required]
        [StringLength(100)]
        public string IMAGE_PATH { get; set; }

        public byte[] IMAGE_BIT { get; set; }

        [StringLength(500)]
        public string DESCRIPTION { get; set; }

        public DateTime CREATED_TIME { get; set; }

        [Required]
        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }

        public virtual GEMA_TM_CATEGORY GEMA_TM_CATEGORY { get; set; }
    }
}
