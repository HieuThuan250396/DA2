namespace WebShop.Models.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PURCHASEORDDETAIL")]
    public partial class PURCHASEORDDETAIL
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string ORDERNOP { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string INVTID { get; set; }

        public int ID { get; set; }

        public int? QTY { get; set; }

        public decimal? PURCHASEPRICE { get; set; }

        public decimal? AMOUNT { get; set; }

        public virtual INVENTORY INVENTORY { get; set; }

        public virtual PURCHASEORDER PURCHASEORDER { get; set; }
    }
}
