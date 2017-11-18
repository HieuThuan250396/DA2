namespace WebShop.Models.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VENDOR")]
    public partial class VENDOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VENDOR()
        {
            PURCHASEORDERs = new HashSet<PURCHASEORDER>();
        }

        [StringLength(20)]
        public string VENDORID { get; set; }

        [Required]
        [StringLength(50)]
        public string VENDORNAME { get; set; }

        [Required]
        [StringLength(50)]
        public string ADDRESS { get; set; }

        [Required]
        [StringLength(50)]
        public string EMAIL { get; set; }

        [Required]
        [StringLength(20)]
        public string PHONE { get; set; }

        [Required]
        [StringLength(20)]
        public string FAX { get; set; }

        [StringLength(2)]
        public string STATUS { get; set; }

        [StringLength(200)]
        public string DESCRIPTION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PURCHASEORDER> PURCHASEORDERs { get; set; }
    }
}