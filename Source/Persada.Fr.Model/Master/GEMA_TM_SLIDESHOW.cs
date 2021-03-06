namespace Persada.Fr.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GEMA_TM_SLIDESHOW
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string TITTLE { get; set; }

        public string CONTENT_DESCRIPTION { get; set; }

        [Required]
        [StringLength(100)]
        public string CLASS { get; set; }

        [StringLength(100)]
        public string PHOTO_PATH { get; set; }

        [StringLength(100)]
        public string URL { get; set; }

        public DateTime CREATED_TIME { get; set; }

        [Required]
        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }
    }
}
