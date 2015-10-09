namespace Ryanstaurant.UMS.DataAccess.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class UmsEntity : DbContext
    {
        public UmsEntity()
            : base("name=Ums")
        {
        }

        public virtual DbSet<UMS_Authorities> UMS_Authorities { get; set; }
        public virtual DbSet<UMS_Employees> UMS_Employees { get; set; }
        public virtual DbSet<UMS_EmpRoles> UMS_EmpRoles { get; set; }
        public virtual DbSet<UMS_Roles> UMS_Roles { get; set; }
        public virtual DbSet<UMS_Token> UMS_Tokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UMS_Authorities>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<UMS_Authorities>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<UMS_Employees>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<UMS_Employees>()
                .Property(e => e.LoginName)
                .IsUnicode(false);

            modelBuilder.Entity<UMS_Employees>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<UMS_Employees>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<UMS_Roles>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<UMS_Roles>()
                .Property(e => e.Description)
                .IsUnicode(false);
        }
    }
}
