namespace WebShop.Models.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CUSTOMER")]
    public partial class CUSTOMER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CUSTOMER()
        {
            SALESORDERs = new HashSet<SALESORDER>();
        }

        [Key]
        [StringLength(20)]
        public string CUSTID { get; set; }

        [Required]
        [StringLength(50)]
        public string CUSTNAME { get; set; }

        [Required]
        [StringLength(50)]
        public string ADDRESS { get; set; }

        [Required]
        [StringLength(20)]
        public string PHONE { get; set; }

        [StringLength(20)]
        public string FAX { get; set; }

        [Required]
        [StringLength(50)]
        public string EMAIL { get; set; }

        [StringLength(2)]
        public string STATUS { get; set; }

        [StringLength(200)]
        public string DESCRIPTION { get; set; }

        [Required]
        [StringLength(20)]
        public string PASS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SALESORDER> SALESORDERs { get; set; }
    }
}
