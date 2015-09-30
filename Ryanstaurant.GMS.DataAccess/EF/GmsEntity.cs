using System.Data.Entity;

namespace Ryanstaurant.GMS.DataAccess.EF
{
    public partial class GmsEntity : DbContext
    {
        public GmsEntity():base("Gms")
        {
        }

        public virtual DbSet<GMS_Sysconfig> GMS_Sysconfig { get; set; }
        public virtual DbSet<GMS_Token> GMS_Token { get; set; }
        public virtual DbSet<GMS_TokenLocation> GMS_TokenLocation { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GMS_Sysconfig>()
                .Property(e => e.ShortCall)
                .IsUnicode(false);

            modelBuilder.Entity<GMS_Sysconfig>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<GMS_Sysconfig>()
                .Property(e => e.ConfigValue)
                .IsUnicode(false);

        }
    }
}
