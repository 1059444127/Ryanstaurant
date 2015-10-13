namespace Ryanstaurant.OMS.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OmsEntity : DbContext
    {
        public OmsEntity()
            : base("name=Oms")
        {
        }

        public virtual DbSet<OMS_Changes> OMS_Changes { get; set; }
        public virtual DbSet<OMS_Charges> OMS_Charges { get; set; }
        public virtual DbSet<OMS_ComboDetails> OMS_ComboDetails { get; set; }
        public virtual DbSet<OMS_Combos> OMS_Combos { get; set; }
        public virtual DbSet<OMS_Discounts> OMS_Discounts { get; set; }
        public virtual DbSet<OMS_ItemDerates> OMS_ItemDerates { get; set; }
        public virtual DbSet<OMS_Items> OMS_Items { get; set; }
        public virtual DbSet<OMS_MenuDetail> OMS_MenuDetail { get; set; }
        public virtual DbSet<OMS_Menus> OMS_Menus { get; set; }
        public virtual DbSet<OMS_OrderChanges> OMS_OrderChanges { get; set; }
        public virtual DbSet<OMS_OrderCharges> OMS_OrderCharges { get; set; }
        public virtual DbSet<OMS_OrderDetail> OMS_OrderDetail { get; set; }
        public virtual DbSet<OMS_OrderDiscounts> OMS_OrderDiscounts { get; set; }
        public virtual DbSet<OMS_Orders> OMS_Orders { get; set; }
        public virtual DbSet<OMS_SpecialRequestDetail> OMS_SpecialRequestDetail { get; set; }
        public virtual DbSet<OMS_SpecialRequests> OMS_SpecialRequests { get; set; }
        public virtual DbSet<OMS_TableRelations> OMS_TableRelations { get; set; }
        public virtual DbSet<OMS_Tables> OMS_Tables { get; set; }
        public virtual DbSet<OMS_Token> OMS_Token { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OMS_Changes>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OMS_Changes>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<OMS_Charges>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OMS_Charges>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<OMS_Combos>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<OMS_Combos>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<OMS_Combos>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OMS_Discounts>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<OMS_Discounts>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<OMS_Discounts>()
                .Property(e => e.DiscountLine)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OMS_Items>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<OMS_Items>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OMS_Items>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OMS_Items>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<OMS_Menus>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<OMS_Menus>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<OMS_Menus>()
                .Property(e => e.SubDescription)
                .IsUnicode(false);

            modelBuilder.Entity<OMS_OrderDetail>()
                .Property(e => e.OraginPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OMS_OrderDetail>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OMS_OrderDetail>()
                .Property(e => e.CancelReason)
                .IsUnicode(false);

            modelBuilder.Entity<OMS_Orders>()
                .Property(e => e.SubTotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OMS_Orders>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OMS_Orders>()
                .Property(e => e.PendingReason)
                .IsUnicode(false);

            modelBuilder.Entity<OMS_Orders>()
                .Property(e => e.CancelReason)
                .IsUnicode(false);

            modelBuilder.Entity<OMS_SpecialRequests>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<OMS_SpecialRequests>()
                .Property(e => e.RequestGroup)
                .IsUnicode(false);

            modelBuilder.Entity<OMS_Tables>()
                .Property(e => e.DisplayNo)
                .IsUnicode(false);
        }
    }
}
