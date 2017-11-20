namespace WebShop.Models.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PAYMENT")]
    public partial class PAYMENT
    {
        public int PAYMENTID { get; set; }

        [StringLength(20)]
        public string ORDERNO { get; set; }

        [StringLength(20)]
        public string SALESPERSONSID { get; set; }

        [Required]
        [StringLength(20)]
        public string PAYMENTNO { get; set; }

        public DateTime? PAYMENTDATE { get; set; }

        public decimal? PAYMENTAMT { get; set; }

        [StringLength(200)]
        public string DESCRIPTION { get; set; }

        public virtual SALESORDER SALESORDER { get; set; }

        public virtual STAFF STAFF { get; set; }
    }
}
