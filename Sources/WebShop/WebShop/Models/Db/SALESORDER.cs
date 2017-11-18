namespace WebShop.Models.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SALESORDER")]
    public partial class SALESORDER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SALESORDER()
        {
            PAYMENTs = new HashSet<PAYMENT>();
            SLSORDERDETAILs = new HashSet<SLSORDERDETAIL>();
        }

        [Key]
        [StringLength(20)]
        public string ORDERNO { get; set; }

        [StringLength(20)]
        public string CUSTID { get; set; }

        public DateTime? ORDERDATE { get; set; }

        public decimal? TOTALAMT { get; set; }

        [StringLength(2)]
        public string FLAG { get; set; }

        [StringLength(200)]
        public string DESCRIPTION { get; set; }

        public virtual CUSTOMER CUSTOMER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PAYMENT> PAYMENTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SLSORDERDETAIL> SLSORDERDETAILs { get; set; }
    }
}
