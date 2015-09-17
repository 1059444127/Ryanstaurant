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
        public virtual DbSet<Dishes> Dishes { get; set; }
        public virtual DbSet<emp_role> emp_role { get; set; }
        public virtual DbSet<employee> employee { get; set; }
        public virtual DbSet<menu> menu { get; set; }
        public virtual DbSet<navigator> navigator { get; set; }
        public virtual DbSet<role> role { get; set; }
        public virtual DbSet<seatimageconfig> seatimageconfig { get; set; }
        public virtual DbSet<SeatList> SeatList { get; set; }
        public virtual DbSet<sysconfig> sysconfig { get; set; }
        public virtual DbSet<table_seats> table_seats { get; set; }
        public virtual DbSet<tableimageconfig> tableimageconfig { get; set; }
        public virtual DbSet<tablelist> tablelist { get; set; }
        public virtual DbSet<UserToken> UserToken { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<authority>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<authority>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Dishes>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Dishes>()
                .Property(e => e.ShortCall)
                .IsUnicode(false);

            modelBuilder.Entity<Dishes>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Dishes>()
                .Property(e => e.Cost)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Dishes>()
                .Property(e => e.MainType)
                .IsUnicode(false);

            modelBuilder.Entity<Dishes>()
                .Property(e => e.SubType)
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

            modelBuilder.Entity<menu>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<menu>()
                .Property(e => e.MType)
                .IsUnicode(false);

            modelBuilder.Entity<menu>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<menu>()
                .Property(e => e.SubDescription)
                .IsUnicode(false);

            modelBuilder.Entity<navigator>()
                .Property(e => e.Label)
                .IsUnicode(false);

            modelBuilder.Entity<navigator>()
                .Property(e => e.ClassName)
                .IsUnicode(false);

            modelBuilder.Entity<navigator>()
                .Property(e => e.PicURL)
                .IsUnicode(false);

            modelBuilder.Entity<role>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<role>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<seatimageconfig>()
                .Property(e => e.PicURL)
                .IsUnicode(false);

            modelBuilder.Entity<SeatList>()
                .Property(e => e.DisplayNo)
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

            modelBuilder.Entity<tableimageconfig>()
                .Property(e => e.PicURL)
                .IsUnicode(false);

            modelBuilder.Entity<tablelist>()
                .Property(e => e.DisplayNo)
                .IsUnicode(false);

            modelBuilder.Entity<UserToken>()
                .Property(e => e.TokenKey)
                .IsUnicode(false);
        }
    }
}
