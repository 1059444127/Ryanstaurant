namespace Ryanstaurant.UMS.DataAccess.EF
{
    using System.Data.Entity;

    public partial class UmsEntities : DbContext
    {
        public UmsEntities()
            : base("name=UmsEntities")
        {
        }

        public virtual DbSet<authority> authority { get; set; }
        public virtual DbSet<emp_role> emp_role { get; set; }
        public virtual DbSet<employee> employee { get; set; }
        public virtual DbSet<role> role { get; set; }
        public virtual DbSet<sysconfig> sysconfig { get; set; }
        public virtual DbSet<UserToken> UserToken { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<authority>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<authority>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.LoginName)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<role>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<role>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<sysconfig>()
                .Property(e => e.ShortCall)
                .IsUnicode(false);

            modelBuilder.Entity<sysconfig>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<sysconfig>()
                .Property(e => e.ConfigValue)
                .IsUnicode(false);

            modelBuilder.Entity<UserToken>()
                .Property(e => e.TokenKey)
                .IsUnicode(false);
        }
    }
}
