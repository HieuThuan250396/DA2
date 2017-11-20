namespace WebShop.Models.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("INVENTORY")]
    public partial class INVENTORY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public INVENTORY()
        {
            PURCHASEORDDETAILs = new HashSet<PURCHASEORDDETAIL>();
            SLSORDERDETAILs = new HashSet<SLSORDERDETAIL>();
        }

        [Key]
        [StringLength(20)]
        public string INVTID { get; set; }

        [Required]
        [StringLength(50)]
        public string INVTNAME { get; set; }

        public int IDCATEGORY { get; set; }

        public decimal SALESPRICE { get; set; }

        [StringLength(2)]
        public string STATUS { get; set; }

        [StringLength(200)]
        public string DESCRIPTION { get; set; }

        public string IMAGE { get; set; }
        public int QTY { get; set; }
        public virtual CATEGORY CATEGORY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PURCHASEORDDETAIL> PURCHASEORDDETAILs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SLSORDERDETAIL> SLSORDERDETAILs { get; set; }
    }
}
