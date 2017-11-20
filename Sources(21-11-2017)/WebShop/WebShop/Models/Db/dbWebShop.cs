namespace WebShop.Models.Db
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class dbWebShop : DbContext
    {
        public dbWebShop()
            : base("name=dbWebShop")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<CATEGORY> CATEGORies { get; set; }
        public virtual DbSet<CUSTOMER> CUSTOMERs { get; set; }
        public virtual DbSet<INVENTORY> INVENTORies { get; set; }
        public virtual DbSet<PAYMENT> PAYMENTs { get; set; }
        public virtual DbSet<PURCHASEORDDETAIL> PURCHASEORDDETAILs { get; set; }
        public virtual DbSet<PURCHASEORDER> PURCHASEORDERs { get; set; }
        public virtual DbSet<SALESORDER> SALESORDERs { get; set; }
        public virtual DbSet<SLSORDERDETAIL> SLSORDERDETAILs { get; set; }
        public virtual DbSet<STAFF> STAFFs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<USER> USERs { get; set; }
        public virtual DbSet<VENDOR> VENDORs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CATEGORY>()
                .HasMany(e => e.INVENTORies)
                .WithRequired(e => e.CATEGORY)
                .HasForeignKey(e => e.IDCATEGORY)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CUSTOMER>()
                .Property(e => e.STATUS)
                .IsUnicode(false);

            modelBuilder.Entity<CUSTOMER>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<INVENTORY>()
                .Property(e => e.SALESPRICE)
                .HasPrecision(18, 0);

            modelBuilder.Entity<INVENTORY>()
                .Property(e => e.STATUS)
                .IsUnicode(false);

            modelBuilder.Entity<INVENTORY>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<PAYMENT>()
                .Property(e => e.PAYMENTNO)
                .IsUnicode(false);

            modelBuilder.Entity<PAYMENT>()
                .Property(e => e.PAYMENTAMT)
                .HasPrecision(18, 0);

            modelBuilder.Entity<PAYMENT>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<PURCHASEORDDETAIL>()
                .Property(e => e.PURCHASEPRICE)
                .HasPrecision(18, 0);

            modelBuilder.Entity<PURCHASEORDDETAIL>()
                .Property(e => e.AMOUNT)
                .HasPrecision(18, 0);

            modelBuilder.Entity<PURCHASEORDER>()
                .Property(e => e.TOTALAMT)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SALESORDER>()
                .Property(e => e.TOTALAMT)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SALESORDER>()
                .Property(e => e.FLAG)
                .IsUnicode(false);

            modelBuilder.Entity<SALESORDER>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<SLSORDERDETAIL>()
                .Property(e => e.AMOUNT)
                .HasPrecision(18, 0);

            modelBuilder.Entity<STAFF>()
                .Property(e => e.STATUS)
                .IsUnicode(false);

            modelBuilder.Entity<STAFF>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<STAFF>()
                .HasMany(e => e.PAYMENTs)
                .WithOptional(e => e.STAFF)
                .HasForeignKey(e => e.SALESPERSONSID);

            modelBuilder.Entity<USER>()
                .Property(e => e.ROLES)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.PASS)
                .IsUnicode(false);

            modelBuilder.Entity<VENDOR>()
                .Property(e => e.STATUS)
                .IsUnicode(false);

            modelBuilder.Entity<VENDOR>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<VENDOR>()
                .HasMany(e => e.PURCHASEORDERs)
                .WithRequired(e => e.VENDOR)
                .WillCascadeOnDelete(false);
        }
    }
}
