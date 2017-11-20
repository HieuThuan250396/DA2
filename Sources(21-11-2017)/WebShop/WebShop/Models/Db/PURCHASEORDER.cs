namespace WebShop.Models.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PURCHASEORDER")]
    public partial class PURCHASEORDER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PURCHASEORDER()
        {
            PURCHASEORDDETAILs = new HashSet<PURCHASEORDDETAIL>();
        }

        [Key]
        [StringLength(20)]
        public string ORDERNOP { get; set; }

        [Required]
        [StringLength(20)]
        public string VENDORID { get; set; }

        public DateTime? ORDERDATE { get; set; }

        public decimal? TOTALAMT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PURCHASEORDDETAIL> PURCHASEORDDETAILs { get; set; }

        public virtual VENDOR VENDOR { get; set; }
    }
}
