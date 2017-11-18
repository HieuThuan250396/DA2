namespace WebShop.Models.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SLSORDERDETAIL")]
    public partial class SLSORDERDETAIL
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string ORDERNO { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string INVTID { get; set; }

        public int ID { get; set; }

        public int QTY { get; set; }

        public decimal? AMOUNT { get; set; }

        public virtual INVENTORY INVENTORY { get; set; }

        public virtual SALESORDER SALESORDER { get; set; }
    }
}
