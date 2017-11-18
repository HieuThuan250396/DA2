namespace WebShop.Models.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("STAFF")]
    public partial class STAFF
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public STAFF()
        {
            PAYMENTs = new HashSet<PAYMENT>();
        }

        [StringLength(20)]
        public string STAFFID { get; set; }

        [Required]
        [StringLength(50)]
        public string NAME { get; set; }

        [Required]
        [StringLength(50)]
        public string ADDRESS { get; set; }

        [Required]
        [StringLength(20)]
        public string PHONE { get; set; }

        [Required]
        [StringLength(20)]
        public string FAX { get; set; }

        [Required]
        [StringLength(50)]
        public string EMAIL { get; set; }

        [StringLength(2)]
        public string STATUS { get; set; }

        [StringLength(200)]
        public string DESCRIPTION { get; set; }

        [StringLength(20)]
        public string USERID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PAYMENT> PAYMENTs { get; set; }

        public virtual USER USER { get; set; }
    }
}
