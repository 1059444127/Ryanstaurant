namespace Ryanstaurant.OMS.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OmsEntity : DbContext
    {
        public OmsEntity()
            : base("name=OmsEntity")
        {
        }

        public virtual DbSet<Derates> Derates { get; set; }
        public virtual DbSet<ItemDerates> ItemDerates { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuItem> MenuItem { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<TableRelations> TableRelations { get; set; }
        public virtual DbSet<Tables> Tables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Derates>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Derates>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Derates>()
                .Property(e => e.DerateLine)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.SubDescription)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.OraginPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Tables>()
                .Property(e => e.DisplayNo)
                .IsUnicode(false);
        }
    }
}
